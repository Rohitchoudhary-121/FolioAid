using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommandResults.Models
{
    public class GetAllTechnologiesResultModel:GuidModelBase
    {
        public string Name { get; set; }
    }

    public class TechnologyListResult
    {
        public IEnumerable<GetAllTechnologiesResultModel> Technologies { get; set; }
    }
}
