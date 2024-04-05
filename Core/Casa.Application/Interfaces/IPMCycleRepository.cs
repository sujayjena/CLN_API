using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface IPMCycleRepository
    {
        Task<int> SavePMCycle(PMCycle_Request parameters);

        Task<IEnumerable<PMCycle_Response>> GetPMCycleList(BaseSearchEntity parameters);

        Task<PMCycle_Response?> GetPMCycleById(long Id);
    }
}
