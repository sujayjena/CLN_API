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
        Task<IEnumerable<CustomerWiseReport_Response>> GetCustomerWiseReport(ManageReport_Search parameters);
        Task<IEnumerable<CustomerSatisfactionReport_Response>> GetCustomerSatisfactionReport(ManageReport_Search parameters);
        Task<IEnumerable<FTFReport_Response>> GetFTFReport(ManageReport_Search parameters);
        Task<IEnumerable<LogisticSummaryReport_Response>> GetLogisticSummaryReport(ManageReport_Search parameters);
        Task<IEnumerable<ExpenseReport_Response>> GetExpenseReport(ManageReport_Search parameters);
        Task<IEnumerable<TicketActivityReport_Response>> GetTicketActivityReport(ManageReport_Search parameters);
        Task<IEnumerable<InMaterialConsumptionReport_Response>> GetInMaterialConsumptionReport(ManageReport_Search parameters);
    }
}
