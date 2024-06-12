using CLN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class SLAModels
    {
    }
    public class SLAPriority_Request : BaseEntity
    {
        public string? SLAPriority { get; set; }
        public bool? IsActive { get; set; }
    }

    public class SLAPriority_Response : BaseResponseEntity
    {
        public string? SLAPriority { get; set; }
        public bool? IsActive { get; set; }
    }
}
