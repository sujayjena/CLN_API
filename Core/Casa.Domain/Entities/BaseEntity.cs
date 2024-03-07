using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Domain.Entities
{
        public abstract class BaseEntity
        {
            public int Id { get; set; }
            public DateTimeOffset? CreatedDate { get; set; }
            public string CreatedDateBy { get; set; }
            public DateTimeOffset? ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
        }
}
