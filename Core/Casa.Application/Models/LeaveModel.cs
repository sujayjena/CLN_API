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
    public class LeaveModel
    {
    }
    public class Leave_Request : BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }

        [DefaultValue("")]
        public string? Remark { get; set; }

        [DefaultValue("")]
        public string? Reason { get; set; }
        public int StatusId { get; set; }
        public bool? IsActive { get; set; }
    }

    public class Leave_Response : BaseResponseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public int LeaveTypeId { get; set; }
        public string? LeaveType { get; set; }
        public string? Remark { get; set; }
        public string? Reason { get; set; }
        public int StatusId { get; set; }
        public string? StatusName { get; set; }
        public bool? IsActive { get; set; }
    }

    public class LeaveSearch : BaseSearchEntity
    {
        [DefaultValue(0)]
        public int CompanyId { get; set; }

        [DefaultValue("")]
        public string? BranchId { get; set; }

        [DefaultValue("")]
        public string? EmployeeName { get; set; }

        [DefaultValue("")]
        public string? LeaveType { get; set; }

        [DefaultValue("")]
        public string? LeaveReason { get; set; }

        [DefaultValue(0)]
        public int LeaveStatusId { get; set; }
        public int EmployeeId { get; set; }

        [DefaultValue("All")]
        public string? FilterType { get; set; }
    }
}
