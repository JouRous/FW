using System;
using System.Linq;
using System.Threading.Tasks;
using Abstractions.Entities;
using Abstractions.ViewModels;
using AutoMapper;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
  public class ListItemController : BaseController
  {
    private readonly IRepository<List> _listRepository;
    private readonly IRepository<Ticket> _ticketRepository;
    private readonly IMapper _mapper;

    public ListItemController(IRepository<List> listRepository, IRepository<Ticket> ticketRepository, IMapper mapper)
    {
      _listRepository = listRepository;
      _ticketRepository = ticketRepository;
      _mapper = mapper;
    }

    ///<summary>Get list</summary>
    [HttpGet]
    public async Task<ActionResult> GetList()
    {
      var lists = await _listRepository.Query().ToListAsync();

      return Ok(lists);
    }

    ///<summary>Get list by id</summary>
    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(Guid id)
    {
      var list = await _listRepository.Query(l => l.Id == id)
        .FirstOrDefaultAsync();

      return Ok(list);
    }

    ///<summary>Get ticket by list id</summary>
    [HttpGet("{id}/tickets")]
    public async Task<ActionResult> GetTickets(Guid id)
    {
      var tickets = await _listRepository.Query(l => l.Id == id)
        .Include(l => l.Tickets)
        .Select(l => l.Tickets)
        .FirstOrDefaultAsync();

      return Ok(tickets);
    }

    ///<summary>Create list</summary>
    [HttpPost]
    public async Task<ActionResult> Create(CreateListParams createListParams)
    {
      var list = _mapper.Map<List>(createListParams);

      _listRepository.Add(list);
      await _listRepository.SaveChangesAsync();

      return Ok(list);
    }

    ///<summary>Move list (Update pos)</summary>
    [HttpPost("{id}/pos")]
    public async Task<ActionResult> MoveList(Guid id, MoveListParams moveListParams)
    {
      var list = await _listRepository.FirstOrDefaultAsync(id);
      list.Pos = moveListParams.Pos;

      await _listRepository.SaveChangesAsync();

      return Ok(list);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
      var list = await _listRepository.FirstOrDefaultAsync(id);
      _listRepository.Remove(list);
      await _listRepository.SaveChangesAsync();

      return Ok(list);
    }
  }
}