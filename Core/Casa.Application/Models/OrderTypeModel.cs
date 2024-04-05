using CLN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class OrderTypeModel
    {
    }
    public class OrderType_Request : BaseEntity
    {
        public string? OrderType { get; set; }
        public bool? IsActive { get; set; }
    }

    public class OrderType_Response : BaseResponseEntity
    {
        public string? OrderType { get; set; }
        public bool? IsActive { get; set; }
    }
}
