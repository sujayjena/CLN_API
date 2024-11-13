using CLN.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface IManageReportRepository
    {
        Task<IEnumerable<Ticket_TRC_Report_Response>> GetTicket_TRC_Report(ManageReport_Search parameters);
    }
}
