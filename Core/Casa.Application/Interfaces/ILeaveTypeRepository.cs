using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface ILeaveTypeRepository
    {
        Task<int> SaveLeaveType(LeaveType_Request parameters);

        Task<IEnumerable<LeaveType_Response>> GetLeaveTypeList(BaseSearchEntity parameters);

        Task<LeaveType_Response?> GetLeaveTypeById(long Id);
    }
}
