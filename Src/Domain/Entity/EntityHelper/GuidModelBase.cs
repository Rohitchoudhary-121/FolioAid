using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.EntityHelper
{
    public abstract class GuidModelBase
    {
        [Key]
        [StringLength(36)]
        public string Id { get; set; }

        protected GuidModelBase()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
