using CLN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class EmployeeGradeModel
    {
    }
    public class EmployeeGrade_Request : BaseEntity
    {
        public string? Grade { get; set; }

        public bool? IsActive { get; set; }
    }

    public class EmployeeGrade_Response : BaseResponseEntity
    {
        public string? Grade { get; set; }

        public bool? IsActive { get; set; }
    }
}
