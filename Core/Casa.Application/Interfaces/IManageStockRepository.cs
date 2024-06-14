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

        #region Stock In
        Task<IEnumerable<SelectListResponse>> GetRequestIdListForSelectList(RequestIdListParameters parameters);
        Task<int> SaveStockIn(StockIn_Request parameters);
        Task<IEnumerable<StockIn_Response>> GetStockInList(StockInListSearch_Request parameters);
        Task<StockIn_Response?> GetStockInById(int Id);
        #endregion

        #region Stock Allocation  To Engineer / TRC

        Task<IEnumerable<StockAllocationList_Response>> GetStockAllocationList(BaseSearchEntity parameters);

        Task<int> SaveStockAllocated(StockAllocated_Request parameters);

        Task<IEnumerable<StockAllocatedList_Response>> GetStockAllocatedList(StockAllocated_Search parameters);

        Task<StockAllocatedList_Response?> GetStockAllocatedById(int Id);


        Task<int> SaveStockAllocatedPartDetails(StockAllocatedPartDetails_Request parameters);

        Task<IEnumerable<StockAllocatedPartDetails_Response>> GetStockAllocatedPartDetailsList(StockAllocatedPartDetails_Search parameters);

        Task<StockAllocatedPartDetails_Response?> GetStockAllocatedPartDetailsById(int Id);

        #endregion

        #region Engineer Stock Master

        Task<IEnumerable<EnggStockMaster_Response>> GetEnggStockMasterList(EnggStockMasterListSearch_Request parameters);

        #endregion

        #region Engineer Part Return

        Task<IEnumerable<EnggPartsReturn_For_Mobile_RequestIdList>> GetRequestIdListForPartReturnRequest(EnggPartsReturn_Search parameters);

        Task<IEnumerable<EnggSpareDetailsListByRequestNumber_ResponseMobile>> GetEngineerPartRequestDetailByRequestNumber(EnggPartsReturn_Search parameters);

        Task<int> SaveEngineerPartReturn(EnggPartsReturn_RequestWeb parameters);

        Task<IEnumerable<EnggPartsReturn_ResponseWeb>> GetEngineerPartReturnList(EnggPartsReturn_Search parameters);

        Task<EnggPartsReturn_ResponseWeb?> GetEngineerPartReturnById(int Id);

        Task<int> ApproveOrRejectEngineerPartReturn(EnggPartsReturn_ApprovedRequest parameters);

        Task<int> SaveEngineerPartReturn_Mobile(EnggPartsReturn_RequestMobile parameters);

        #endregion

        #region Stock Master

        Task<StockMaster_Response?> GetStockMasterBySpareDetailsId(int SpareDetailsId);

        #endregion
    }
}
