using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface IWarrantyRepository
    {
        #region Warranty Status
        Task<int> SaveWarrantyStatus(WarrantyStatus_Request parameters);

        Task<IEnumerable<WarrantyStatus_Response>> GetWarrantyStatusList(BaseSearchEntity parameters);

        Task<WarrantyStatus_Response?> GetWarrantyStatusById(int Id);
        #endregion

        #region Warranty Type
        Task<int> SaveWarrantyType(WarrantyType_Request parameters);

        Task<IEnumerable<WarrantyType_Response>> GetWarrantyTypeList(BaseSearchEntity parameters);

        Task<WarrantyType_Response?> GetWarrantyTypeById(int Id);
        #endregion
    }
}
