using CLN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class ComplaintTypeModel
    {
    }
    public class ComplaintType_Request : BaseEntity
    {
        public string? ComplaintType { get; set; }
        public bool? IsActive { get; set; }
    }

    public class ComplaintType_Response : BaseResponseEntity
    {
        public string? ComplaintType { get; set; }
        public bool? IsActive { get; set; }
    }
}
