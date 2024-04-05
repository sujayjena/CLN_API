using CLN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class VehicleTypeModel
    {
    }
    public class VehicleType_Request : BaseEntity
    {
        public string? VehicleType { get; set; }
        public bool? IsActive { get; set; }
    }

    public class VehicleType_Response : BaseResponseEntity
    {
        public string? VehicleType { get; set; }
        public bool? IsActive { get; set; }
    }
}
