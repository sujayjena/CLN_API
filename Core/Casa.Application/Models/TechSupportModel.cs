using CLN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class TechSupportModel
    {
    }
    public class TechSupport_Request : BaseEntity
    {
        public string? TechSupportName { get; set; }
        public string? TechSupportMobileNo { get; set; }
        public bool? IsActive { get; set; }
    }

    public class TechSupport_Response : BaseResponseEntity
    {
        public string? TechSupportName { get; set; }
        public string? TechSupportMobileNo { get; set; }
        public bool? IsActive { get; set; }
    }
}
