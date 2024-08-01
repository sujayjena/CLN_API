﻿using CLN.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface IDashboardRepository
    {
        Task<IEnumerable<Dashboard_TicketResolvedSummary_Result>> GetDashboard_TicketResolvedSummary(Dashboard_Search_Request parameters);

        Task<IEnumerable<Dashboard_TicetStatusSummary_Result>> GetDashboard_TicetStatusSummary(Dashboard_Search_Request parameters);

    }
}
