using System.Collections.Generic;
using System.Linq;
using Abstractions.Entities;
using Abstractions.ViewModels;
using AutoMapper;

namespace Application.Mapper
{
  public class ListMapperProfile : Profile
  {
    public ListMapperProfile()
    {
      CreateMap<List, CreateListParams>().ReverseMap();

      CreateMap<List, ListGetResult>();
      CreateMap<List, ListGetResultWithTicket>();
    }
  }
}