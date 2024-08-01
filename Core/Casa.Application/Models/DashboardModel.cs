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
        [DefaultValue(0)]
        public int CompanyId { get; set; }

        [DefaultValue("")]
        public string BranchId { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        [DefaultValue(0)]
        public int EmployeeId { get; set; }

        [DefaultValue("All")]
        public string FilterType { get; set; }
    }

    public class Dashboard_TicketResolvedSummary_Result
    {
        public int? EngineerId { get; set; }
        public string EngineerName { get; set; }
        public int? Ticket { get; set; }
    }

    public class Dashboard_TicetStatusSummary_Result
    {
        public int Id { get; set; }
        public string TicketStatus { get; set; }
        public int? Ticket { get; set; }
    }

}
