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
            queryParameters.Add("@SpareDetailsId", parameters.SpareDetailsId);
            queryParameters.Add("@TypeOfBMSId", parameters.TypeOfBMSId);
            queryParameters.Add("@AvailableQty", parameters.AvailableQty);
            queryParameters.Add("@OrderQty", parameters.OrderQty);
            queryParameters.Add("@RequiredQty", parameters.RequiredQty);
            queryParameters.Add("@Remarks", parameters.Remarks);
            queryParameters.Add("@CompanyId", parameters.CompanyId);
            queryParameters.Add("@BranchId", parameters.BranchId);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveGeneratePartRequest", queryParameters);
        }

        public async Task<IEnumerable<GeneratePartRequest_Response>> GetGeneratePartRequestList(GeneratePartRequestSearch_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@CompanyId", parameters.CompanyId);
            queryParameters.Add("@BranchId", parameters.BranchId);
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
            queryParameters.Add("@CompanyId", parameters.CompanyId);
            queryParameters.Add("@BranchId", parameters.BranchId);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveGenerateChallan", queryParameters);
        }

        public async Task<IEnumerable<GenerateChallan_Response>> GetGenerateChallanList(GenerateChallanSearch_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@CompanyId", parameters.CompanyId);
            queryParameters.Add("@BranchId", parameters.BranchId);
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
            queryParameters.Add("@OrderQty", parameters.OrderQty);
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

            queryParameters.Add("@GenerateChallanId", parameters.GenerateChallanId);
            queryParameters.Add("@SpareDetailsId", parameters.SpareDetailsId);
            queryParameters.Add("@AvailableQty", parameters.AvailableQty);
            queryParameters.Add("@OrderQty", parameters.OrderQty);
            queryParameters.Add("@ReceivedQty", parameters.ReceivedQty);
            queryParameters.Add("@StatusId", parameters.StatusId);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveStockIn", queryParameters);
        }
        public async Task<IEnumerable<StockIn_Response>> GetStockInList(StockInListSearch_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@GenerateChallanId", parameters.GenerateChallanId);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
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

        #region Stock Allocation

        public async Task<IEnumerable<StockAllocationList_Response>> GetStockAllocationList(BaseSearchEntity parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<StockAllocationList_Response>("GetStockAllocationList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        #region Stock Allocate To Engineer / TRC

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
            queryParameters.Add("@StockAllocatedId", parameters.StockAllocatedId);
            queryParameters.Add("@SpareId", parameters.SpareId);
            queryParameters.Add("@AvailableQty", parameters.AvailableQty);
            queryParameters.Add("@RequiredQty", parameters.RequiredQty);
            queryParameters.Add("@AllocatedQty", parameters.AllocatedQty);
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

        #endregion

        #region Stock Master
        public async Task<StockMaster_Response?> GetStockMasterBySpareDetailsId(int SpareDetailsId)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@SpareDetailsId", SpareDetailsId);

            return (await ListByStoredProcedure<StockMaster_Response>("GetStockMasterBySpareDetailsId", queryParameters)).FirstOrDefault();
        }
        #endregion

        #region Engineer Stock Master
        public async Task<IEnumerable<EnggStockMaster_Response>> GetEnggStockMasterList(EnggStockMasterListSearch_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@EngineerId", parameters.EngineerId);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await ListByStoredProcedure<EnggStockMaster_Response>("GetEnggStockMasterList", queryParameters);
        }

        #endregion
    }
}
