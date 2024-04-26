using CLN.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface ILeaveRepository
    {
        Task<int> SaveLeave(Leave_Request parameters);

        Task<IEnumerable<Leave_Response>> GetLeaveList(LeaveSearch parameters);

        Task<Leave_Response?> GetLeaveById(int Id);

        Task<IEnumerable<LeaveReason_Response>> GetLeaveReasonListById(int LeaveId);
    }
}
