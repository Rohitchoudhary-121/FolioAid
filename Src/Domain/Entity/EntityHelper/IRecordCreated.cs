using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.EntityHelper
{
    public interface IRecordCreated
    {
        /// <summary>
        /// Record created
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Record modified
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
    }
}
