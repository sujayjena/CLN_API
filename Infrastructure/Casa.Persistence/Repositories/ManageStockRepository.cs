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
    }
}
