using CLN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class WarrantyStatusModel
    {
    }

    public class WarrantyStatus_Request : BaseEntity
    {
        public string? WarrantyStatus { get; set; }
        public bool? IsActive { get; set; }
    }

    public class WarrantyStatus_Response : BaseResponseEntity
    {
        public string? WarrantyStatus { get; set; }
        public bool? IsActive { get; set; }
    }
}
