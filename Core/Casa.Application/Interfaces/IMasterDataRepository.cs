using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface IMasterDataRepository
    {
        Task<IEnumerable<SelectListResponse>> GetReportingToEmployeeForSelectList(ReportingToEmpListParameters parameters);
        Task<IEnumerable<EmployeesListByReportingTo_Response>> GetEmployeesListByReportingTo(int EmployeeId);
        Task<IEnumerable<SelectListResponse>> GetTicketListForSelectList(TicketListForSelect_Search parameters);
        Task<IEnumerable<SelectListResponse>> GetPartRequestEnggListForSelectList(PartRequestEnggListForSelect_Search parameters);
        Task<IEnumerable<SelectListResponse>> GetPartReturnEnggListForSelectList();
    }
}
