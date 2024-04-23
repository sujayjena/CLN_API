using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface IBranchRepository
    {
        Task<int> SaveBranch(Branch_Request parameters);

        Task<IEnumerable<Branch_Response>> GetBranchList(BaseSearchEntity parameters);

        Task<Branch_Response?> GetBranchById(int Id);



        Task<int> SaveBranchMapping(BranchMapping_Request parameters);

        Task<IEnumerable<BranchMapping_Response>> GetBranchMappingByEmployeeId(int EmployeeId, int BranchId);
    }
}
