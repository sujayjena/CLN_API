using CLN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class RatePerKMModel
    {
    }
    public class RatePerKM_Request : BaseEntity
    {
        public int Distance { get; set; }
        public decimal Rate { get; set; }
        public int VehicleTypeId { get; set; }
        public int EmployeeLevelId { get; set; }
        public bool? IsActive { get; set; }
    }

    public class RatePerKM_Response : BaseResponseEntity
    {
        public int Distance { get; set; }
        public decimal Rate { get; set; }
        public int VehicleTypeId { get; set; }
        public string VehicleType { get; set; }
        public int EmployeeLevelId { get; set; }
        public string EmployeeLevel { get; set; }
        public bool? IsActive { get; set; }
    }
}
