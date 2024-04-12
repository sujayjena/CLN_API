using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface IWarrantyStatusRepository
    {
        Task<int> SaveWarrantyStatus(WarrantyStatus_Request parameters);

        Task<IEnumerable<WarrantyStatus_Response>> GetWarrantyStatusList(BaseSearchEntity parameters);

        Task<WarrantyStatus_Response?> GetWarrantyStatusById(int Id);
    }
}
