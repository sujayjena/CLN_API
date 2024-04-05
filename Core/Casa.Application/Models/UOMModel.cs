using CLN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class UOMModel
    {
    }
    public class UOM_Request : BaseEntity
    {
        public string? UOMName { get; set; }
        public string? UOMDesc { get; set; }

        public bool? IsActive { get; set; }
    }

    public class UOM_Response : BaseResponseEntity
    {
        public string? UOMName { get; set; }
        public string? UOMDesc { get; set; }

        public bool? IsActive { get; set; }
    }
}
