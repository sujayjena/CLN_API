using CLN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class LeaveTypeModel
    {
    }
    public class LeaveType_Request : BaseEntity
    {
        public string? LeaveType { get; set; }
        public bool? IsActive { get; set; }
    }

    public class LeaveType_Response : BaseResponseEntity
    {
        public string? LeaveType { get; set; }
        public bool? IsActive { get; set; }
    }
}
