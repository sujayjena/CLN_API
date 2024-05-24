using CLN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class EmployeeLevelModel
    {
    }
    public class EmployeeLevel_Request : BaseEntity
    {
        public string? EmployeeLevel { get; set; }
        public bool? IsActive { get; set; }
    }

    public class EmployeeLevel_Response : BaseResponseEntity
    {
        public string? EmployeeLevel { get; set; }
        public bool? IsActive { get; set; }
    }
}
