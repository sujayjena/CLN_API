using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface IManageStockRepository
    {
        #region Generate Part Request
        Task<int> SaveGeneratePartRequest(GeneratePartRequest_Request parameters);
        Task<IEnumerable<GeneratePartRequest_Response>> GetGeneratePartRequestList(GeneratePartRequestSearch_Request parameters);
        Task<GeneratePartRequest_Response?> GetGeneratePartRequestById(int Id);
        #endregion

        #region Generate Challan
        Task<int> SaveGenerateChallan(GenerateChallan_Request parameters);
        Task<IEnumerable<GenerateChallan_Response>> GetGenerateChallanList(GenerateChallanSearch_Request parameters);
        Task<GenerateChallan_Response?> GetGenerateChallanById(int Id);
        Task<int> SaveGenerateChallanPartDetails(GenerateChallanPartDetails_Request parameters);
        Task<IEnumerable<GenerateChallanPartDetails_Response>> GetGenerateChallanPartDetailsList(GenerateChallanPartDetailsSearch_Request parameters);
        #endregion
    }
}
