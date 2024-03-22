using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface IContractCycleRepository
    {
        Task<int> SaveContractCycle(ContractCycle_Request parameters);

        Task<IEnumerable<ContractCycle_Response>> GetContractCycleList(BaseSearchEntity parameters);

        Task<ContractCycle_Response?> GetContractCycleById(long Id);
    }
}
