using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class DashboardModel
    {
    }
    public class Dashboard_Search_Request
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? EmployeeId { get; set; }

        [DefaultValue("All")]
        public string? FilterType { get; set; }
    }
    public class DashboardTicketCount_Response
    {
        public int? AssignCount { get; set; }
        public int? ClosedCount { get; set; }
        public int? PendingCount { get; set; }
        public int? HoldCount { get; set; }

    }
}
