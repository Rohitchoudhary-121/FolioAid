using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommandResults.Models
{
   public class GetAllIndustriesResultModel : GuidModelBase
    {
        public string Name { get; set; }
    }

    public class IndustryListResult
    {
        public IEnumerable<GetAllIndustriesResultModel> Industries { get; set; }
    }
}
