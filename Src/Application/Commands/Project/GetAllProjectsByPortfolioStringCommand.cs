using Application.CommandResults.Models;
using Domain.Common;
using Domain.Entity.EntityHelper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Commands.Project
{
    public  class GetAllProjectsByPortfolioStringCommand : IRequest<GenericBaseResult<PaginatedProjectListResult>> { 
  
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
   
     public string? PortfolioString { get; set; }
    public string? Search { get; set; }
    public string[]? Technologies { get; set; }
    public string[]? Industries { get; set; }
    
    }
}
