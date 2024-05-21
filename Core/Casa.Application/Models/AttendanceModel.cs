using CLN.Domain.Entities;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class AttendanceModel
    {
    }

    public class Attendance_Request
    {
        [DefaultValue("")]
        public string? PunchType { get; set; }

        [DefaultValue("")]
        public string? Latitude { get; set; }

        [DefaultValue("")]
        public string? Longitude { get; set; }

        [DefaultValue("")]
        public string? BatteryStatus { get; set; }

        [DefaultValue("")]
        public string? Address { get; set; }

        [DefaultValue("")]
        public string? Remark { get; set; }
    }

    public class Attendance_Response : BaseResponseEntity
    {
        public int? UserId { get; set; }
        public int? EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public DateTime? PunchInOut { get; set; }
        public string? PunchType { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? BatteryStatus { get; set; }
        public string? Address { get; set; }
        public string? Remark { get; set; }
    }

    public class AttendanceSearch : BaseSearchEntity
    {
        [DefaultValue(0)]
        public int? CompanyId { get; set; }

        [DefaultValue("")]
        public string? BranchId { get; set; }

        public DateTime? FromPunchInDate { get; set; }
        public DateTime? ToPunchInDate { get; set; }

        [DefaultValue("")]
        public string? EmployeeName { get; set; }

        [DefaultValue(0)]
        public int? EmployeeId { get; set; }

        [DefaultValue("All")]
        public string? FilterType { get; set; }
    }
}
