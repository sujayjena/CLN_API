using CLN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class CustomerTypeModel
    {
    }
    public class CustomerType_Request : BaseEntity
    {
        public string? CustomerType { get; set; }
        public bool? IsActive { get; set; }
    }

    public class CustomerType_Response : BaseResponseEntity
    {
        public string? CustomerType { get; set; }
        public bool? IsActive { get; set; }
    }
}
