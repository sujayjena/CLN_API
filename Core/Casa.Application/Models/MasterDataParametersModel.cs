using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class SelectListResponse
    {
        public long Value { get; set; }
        public string? Text { get; set; }
    }
    public class ReportingToEmpListParameters
    {
        public long RoleId { get; set; }
        public long? RegionId { get; set; }
    }
    public partial class EmployeesListByReportingTo_Response
    {
        public int? Id { get; set; }
        public string? EmployeeName { get; set; }
        public int? CompanyId { get; set; }
        public string? BranchId { get; set; }
        public int? UserId { get; set; }
    }

    public class RequestIdListParameters
    {
        public int? StatusId { get; set; }
    }
}
