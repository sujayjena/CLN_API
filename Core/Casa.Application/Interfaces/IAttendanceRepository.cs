using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface IAttendanceRepository
    {
        Task<int> SaveAttendance(Attendance_Request parameters);

        Task<IEnumerable<Attendance_Response>> GetAttendanceList(AttendanceSearch parameters);

        Task<IEnumerable<Attendance_Response>> GetAttendanceById(int UserId);
    }
}
