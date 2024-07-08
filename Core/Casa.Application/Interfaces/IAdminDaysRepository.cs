using CLN.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface IAdminDaysRepository
    {
        Task<int> SaveAdminDays(AdminDays_Request parameters);

        Task<IEnumerable<AdminDays_Response>> GetAdminDaysList(AdminDays_Search parameters);

        Task<AdminDays_Response?> GetAdminDaysById(int Id);
    }
}
