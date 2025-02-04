using CLN.Application.Helpers;
using CLN.Application.Interfaces;
using CLN.Application.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Persistence.Repositories
{
    public class ManageStockRepository : GenericRepository, IManageStockRepository
    {
        private IConfiguration _configuration;

        public ManageStockRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        #region Generate Part Request
        public async Task<int> SaveGeneratePartRequest(GeneratePartRequest_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@SpareCategoryId", parameters.SpareCategoryId);
            queryParameters.Add("@ProductMakeId", parameters.ProductMakeId);
            queryParameters.Add("@BMSMakeId", parameters.BMSMakeId);
            queryParameters.Add("@SpareDetailsId", parameters.SpareDetailsId);
            queryParameters.Add("@UOMId", parameters.UOMId);
            queryParameters.Add("@TypeOfBMSId", parameters.TypeOfBMSId);
            queryParameters.Add("@AvailableQty", parameters.AvailableQty);
            queryParameters.Add("@RequiredQty", parameters.RequiredQty);
            queryParameters.Add("@RequestedQty", parameters.RequestedQty);
            queryParameters.Add("@Remarks", parameters.Remarks);
            //queryParameters.Add("@CompanyId", parameters.CompanyId);
            //queryParameters.Add("@BranchId", parameters.BranchId);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveGeneratePartRequest", queryParameters);
        }

        public async Task<IEnumerable<GeneratePartRequest_Response>> GetGeneratePartRequestList(GeneratePartRequestSearch_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            //queryParameters.Add("@CompanyId", parameters.CompanyId);
            //queryParameters.Add("@BranchId", parameters.BranchId);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<GeneratePartRequest_Response>("GetGeneratePartRequestList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<GeneratePartRequest_Response?> GetGeneratePartRequestById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);

            return (await ListByStoredProcedure<GeneratePartRequest_Response>("GetGeneratePartRequestById", queryParameters)).FirstOrDefault();
        }

        #endregion

        #region Generate Challan
        public async Task<int> SaveGenerateChallan(GenerateChallan_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            //queryParameters.Add("@CompanyId", parameters.CompanyId);
            //queryParameters.Add("@BranchId", parameters.BranchId);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveGenerateChallan", queryParameters);
        }

        public async Task<IEnumerable<GenerateChallan_Response>> GetGenerateChallanList(GenerateChallanSearch_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            //queryParameters.Add("@CompanyId", parameters.CompanyId);
            //queryParameters.Add("@BranchId", parameters.BranchId);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<GenerateChallan_Response>("GetGenerateChallanList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<GenerateChallan_Response?> GetGenerateChallanById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);

            return (await ListByStoredProcedure<GenerateChallan_Response>("GetGenerateChallanById", queryParameters)).FirstOrDefault();
        }

        public async Task<int> SaveGenerateChallanPartDetails(GenerateChallanPartDetails_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@GenerateChallanId", parameters.GenerateChallanId);
            queryParameters.Add("@SpareDetailsId", parameters.SpareDetailsId);
            queryParameters.Add("@UOMId", parameters.UOMId);
            queryParameters.Add("@TypeOfBMSId", parameters.TypeOfBMSId);
            queryParameters.Add("@AvailableQty", parameters.AvailableQty);
            queryParameters.Add("@RequiredQty", parameters.RequiredQty);
            queryParameters.Add("@RequestedQty", parameters.RequestedQty);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveGenerateChallanPartDetails", queryParameters);
        }

        public async Task<IEnumerable<GenerateChallanPartDetails_Response>> GetGenerateChallanPartDetailsList(GenerateChallanPartDetailsSearch_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@GenerateChallanId", parameters.GenerateChallanId);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<GenerateChallanPartDetails_Response>("GetGenerateChallanPartDetailsList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }
        #endregion

        #region Stock In
        public async Task<IEnumerable<SelectListResponse>> GetRequestIdListForSelectList(RequestIdListParameters parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@StatusId", parameters.StatusId);

            return await ListByStoredProcedure<SelectListResponse>("GetRequestIdListForSelectList", queryParameters);
        }

        public async Task<int> SaveStockIn(StockIn_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@GenerateChallanId", parameters.GenerateChallanId);
            queryParameters.Add("@SpareDetailsId", parameters.SpareDetailsId);
            queryParameters.Add("@UOMId", parameters.UOMId);
            queryParameters.Add("@AvailableQty", parameters.AvailableQty);
            queryParameters.Add("@RequiredQty", parameters.RequiredQty);
            queryParameters.Add("@RequestedQty", parameters.RequestedQty);
            queryParameters.Add("@ReceivedQty", parameters.ReceivedQty);
            queryParameters.Add("@StatusId", parameters.StatusId);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveStockIn", queryParameters);
        }
        public async Task<IEnumerable<StockIn_Response>> GetStockInList(StockInListSearch_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@GenerateChallanId", parameters.GenerateChallanId);
            queryParameters.Add("@StatusId", parameters.StatusId);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@SortBy", parameters.SortBy);
            queryParameters.Add("@OrderBy", parameters.OrderBy);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<StockIn_Response>("GetStockInList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }
        public async Task<StockIn_Response?> GetStockInById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);

            return (await ListByStoredProcedure<StockIn_Response>("GetStockInById", queryParameters)).FirstOrDefault();
        }
        #endregion

        #region Stock Allocation To Engineer / TRC

        public async Task<IEnumerable<StockAllocationList_Response>> GetStockAllocationList(BaseSearchEntity parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@SortBy", parameters.SortBy);
            queryParameters.Add("@OrderBy", parameters.OrderBy);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<StockAllocationList_Response>("GetStockAllocationList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<int> SaveStockAllocated(StockAllocated_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@RequestId", parameters.RequestId);
            queryParameters.Add("@EngineerId", parameters.EngineerId);
            queryParameters.Add("@AllocatedType", parameters.AllocatedType);
            queryParameters.Add("@StatusId", parameters.StatusId);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveStockAllocated", queryParameters);
        }

        public async Task<IEnumerable<StockAllocatedList_Response>> GetStockAllocatedList(StockAllocated_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@AllocatedType", parameters.AllocatedType);
            queryParameters.Add("@EngineerId", parameters.EngineerId);
            queryParameters.Add("@StatusId", parameters.StatusId);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<StockAllocatedList_Response>("GetStockAllocatedList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<StockAllocatedList_Response?> GetStockAllocatedById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);

            return (await ListByStoredProcedure<StockAllocatedList_Response>("GetStockAllocatedById", queryParameters)).FirstOrDefault();
        }

        public async Task<int> SaveStockAllocatedPartDetails(StockAllocatedPartDetails_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@EngineerId", parameters.EngineerId);
            queryParameters.Add("@StockAllocatedId", parameters.StockAllocatedId);
            queryParameters.Add("@SpareCategoryId", parameters.SpareCategoryId);
            queryParameters.Add("@ProductMakeId", parameters.ProductMakeId);
            queryParameters.Add("@BMSMakeId", parameters.BMSMakeId);
            queryParameters.Add("@SpareDetailsId", parameters.SpareId);
            queryParameters.Add("@AvailableQty", parameters.AvailableQty);
            queryParameters.Add("@RequiredQty", parameters.RequiredQty);
            queryParameters.Add("@AllocatedQty", parameters.AllocatedQty);
            queryParameters.Add("@ReceivedQty", parameters.ReceivedQty);
            queryParameters.Add("@RGP", parameters.RGP);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveStockAllocatedPartDetails", queryParameters);
        }

        public async Task<IEnumerable<StockAllocatedPartDetails_Response>> GetStockAllocatedPartDetailsList(StockAllocatedPartDetails_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@StockAllocatedId", parameters.StockAllocatedId);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<StockAllocatedPartDetails_Response>("GetStockAllocatedPartDetailsList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<StockAllocatedPartDetails_Response?> GetStockAllocatedPartDetailsById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);

            return (await ListByStoredProcedure<StockAllocatedPartDetails_Response>("GetStockAllocatedPartDetailsById", queryParameters)).FirstOrDefault();
        }

        #endregion

        #region Engineer Stock Master

        public async Task<IEnumerable<EnggStockMaster_Response>> GetEnggStockMasterList(EnggStockMasterListSearch_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@EngineerId", parameters.EngineerId);
            queryParameters.Add("@SpareDetailsId", parameters.SpareDetailsId);
            queryParameters.Add("@StockType", parameters.StockType);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@SortBy", parameters.SortBy);
            queryParameters.Add("@OrderBy", parameters.OrderBy);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<EnggStockMaster_Response>("GetEnggStockMasterList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<EnggStockMaster_Response?> GetEnggStockMasterById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);

            return (await ListByStoredProcedure<EnggStockMaster_Response>("GetEnggStockMasterById", queryParameters)).FirstOrDefault();
        }

        public async Task<int> UpdateEnggStockMaster(EnggStockMaster_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@MinQty", parameters.MinQty);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("UpdateEnggStockMaster", queryParameters);
        }

        #endregion

        #region Engineer Part Return

        public async Task<IEnumerable<EnggPartsReturn_For_Mobile_RequestIdList>> GetRequestIdListForPartReturnRequest(EnggPartsReturn_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@RequestType", parameters.RequestType);
            queryParameters.Add("@EngineerId", parameters.EngineerId);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<EnggPartsReturn_For_Mobile_RequestIdList>("GetRequestIdListForPartReturnRequest", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<IEnumerable<EnggSpareDetailsListByRequestNumber_ResponseMobile>> GetEngineerPartRequestDetailByRequestNumber(EnggPartsReturn_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@RequestType", parameters.RequestType);
            queryParameters.Add("@RequestNumber", parameters.SearchText.SanitizeValue());

            var result = await ListByStoredProcedure<EnggSpareDetailsListByRequestNumber_ResponseMobile>("GetEngineerPartRequestDetailByRequestNumber", queryParameters);

            return result;
        }

        public async Task<int> SaveEngineerPartReturn(EnggPartsReturn_RequestWeb parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@EngineerId", parameters.EngineerId);
            queryParameters.Add("@RequestNumber", parameters.RequestNumber);
            queryParameters.Add("@SpareDetailsId", parameters.SpareDetailsId);
            queryParameters.Add("@ReturnQuantity", parameters.ReturnQuantity);
            queryParameters.Add("@StatusId", parameters.StatusId);
            queryParameters.Add("@RequestType", parameters.RequestType);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveEngineerPartsReturn", queryParameters);
        }

        public async Task<int> SaveEngineerPartReturn_Mobile(EnggPartsReturn_RequestMobile parameters)
        {
            int i = 0;
            foreach (var item in parameters.SpareDetailsList)
            {
                DynamicParameters queryParameters = new DynamicParameters();

                queryParameters.Add("@Id", parameters.Id);
                queryParameters.Add("@EngineerId", parameters.EngineerId);
                queryParameters.Add("@RequestNumber", parameters.RequestNumber);
                queryParameters.Add("@SpareDetailsId", item.SpareDetailsId);
                queryParameters.Add("@ReturnQuantity", item.ReturnQuantity);
                queryParameters.Add("@StatusId", item.StatusId);
                queryParameters.Add("@RequestType", parameters.RequestType);
                queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

                await SaveByStoredProcedure<int>("SaveEngineerPartsReturn", queryParameters);

                i++;
            }
            return i;
        }

        public async Task<IEnumerable<EnggPartsReturn_ResponseWeb>> GetEngineerPartReturnList(EnggPartsReturn_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@RequestType", parameters.RequestType);
            queryParameters.Add("@StatusId", parameters.StatusId);
            queryParameters.Add("@EngineerId", parameters.EngineerId);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@SortBy", parameters.SortBy);
            queryParameters.Add("@OrderBy", parameters.OrderBy);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<EnggPartsReturn_ResponseWeb>("GetEngineerPartReturnList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<EnggPartsReturn_ResponseWeb?> GetEngineerPartReturnById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);

            return (await ListByStoredProcedure<EnggPartsReturn_ResponseWeb>("GetEngineerPartReturnById", queryParameters)).FirstOrDefault();
        }

        public async Task<int> ApproveOrRejectEngineerPartReturn(EnggPartsReturn_ApprovedRequest parameters)
        {
            int i = 0;
            foreach (var item in parameters.PartList)
            {
                DynamicParameters queryParameters = new DynamicParameters();

                queryParameters.Add("@Id", item.Id);
                queryParameters.Add("@RequestNumber", parameters.RequestNumber);
                queryParameters.Add("@EngineerId", parameters.EngineerId);
                queryParameters.Add("@SpareDetailsId", item.SpareDetailsId);
                queryParameters.Add("@StatusId", item.StatusId);
                queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

                await SaveByStoredProcedure<int>("SaveApproveOrRejectEngineerPartReturn", queryParameters);

                i = i + 1;
            }

            return i;
        }

        public async Task<IEnumerable<EnggPendingSpareDetailsList_ResponseWeb>> GetEngineerPartReturnPendingList_Engineer_N_TRC(EnggPendingPartsReturn_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@RequestType", parameters.RequestType);
            queryParameters.Add("@RequestNumber", parameters.RequestNumber);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<EnggPendingSpareDetailsList_ResponseWeb>("GetEngineerPendingPartReturnRequestDetail_Engg_N_TRC", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        #endregion

        #region Stock Master

        public async Task<int> SaveStockMaster(StockMaster_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@EngineerId", parameters.EngineerId);
            queryParameters.Add("@SpareDetailsId", parameters.SpareDetailsId);
            queryParameters.Add("@Quantity", parameters.Quantity);
            queryParameters.Add("@StockType", parameters.StockType);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveStockMaster", queryParameters);
        }

        public async Task<StockMaster_Response?> GetStockMasterBySpareDetailsId(int SpareDetailsId)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@SpareDetailsId", SpareDetailsId);

            return (await ListByStoredProcedure<StockMaster_Response>("GetStockMasterBySpareDetailsId", queryParameters)).FirstOrDefault();
        }

        public async Task<IEnumerable<StockMaster_Response>> GetStockMasterList(BaseSearchEntity parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@SortBy", parameters.SortBy);
            queryParameters.Add("@OrderBy", parameters.OrderBy);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<StockMaster_Response>("GetStockMasterList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }


        public Task<IEnumerable<OrderReceivedEngineer_Response>> GetOrderReceivedEngineerList(EnggPartsReturn_Search parameters)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Order Received Engineer

        public async Task<IEnumerable<OrderReceivedEngineer_Response>> GetOrderReceivedEngineerList(OrderReceivedEngineer_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@EngineerId", parameters.EngineerId);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@SortBy", parameters.SortBy);
            queryParameters.Add("@OrderBy", parameters.OrderBy);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<OrderReceivedEngineer_Response>("GetEngineerOrderReceivedList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<IEnumerable<EngineerOrderListByEngineerId_Response>> GetEngineerOrderListByEngineerId(EngineerOrderListByEngineerId_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@EngineerId", parameters.EngineerId);
            queryParameters.Add("@RequestType", parameters.RequestType);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<EngineerOrderListByEngineerId_Response>("GetEngineerOrderListByEngineerId", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        #endregion
    }
}
