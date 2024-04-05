using CLN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class ComplaintStatusModel
    {
    }
    public class ComplaintStatus_Request : BaseEntity
    {
        public string? ComplaintStatus { get; set; }
        public bool? IsActive { get; set; }
    }

    public class ComplaintStatus_Response : BaseResponseEntity
    {
        public string? ComplaintStatus { get; set; }
        public bool? IsActive { get; set; }
    }
}
