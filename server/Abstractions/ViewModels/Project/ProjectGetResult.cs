using System;
using System.Collections.Generic;

namespace Abstractions.ViewModels
{
  public class ProjectGetResult : BaseGetResult
  {
    public string Name { get; set; }
    public string ShortDescription { get; set; }
    public int MembersCount { get; set; }
    public List<Guid> BoardIds { get; set; }
  }
}