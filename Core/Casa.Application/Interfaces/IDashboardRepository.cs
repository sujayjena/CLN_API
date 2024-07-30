using CLN.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface IDashboardRepository
    {
        Task<IEnumerable<DashboardTicketCount_Response>> GetDashboardTicketCount(Dashboard_Search_Request parameters);
    }
}
