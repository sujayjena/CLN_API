using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface IEmployeeLevelRepository
    {
        Task<int> SaveEmployeeLevel(EmployeeLevel_Request parameters);

        Task<IEnumerable<EmployeeLevel_Response>> GetEmployeeLevelList(BaseSearchEntity parameters);

        Task<EmployeeLevel_Response?> GetEmployeeLevelById(long Id);
    }
}
