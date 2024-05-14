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
    public class WarrantyRepository : GenericRepository, IWarrantyRepository
    {

        private IConfiguration _configuration;

        public WarrantyRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        #region Warranty Status
        public async Task<int> SaveWarrantyStatus(WarrantyStatus_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@WarrantyStatus", parameters.WarrantyStatus);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveWarrantyStatus", queryParameters);
        }

        public async Task<IEnumerable<WarrantyStatus_Response>> GetWarrantyStatusList(BaseSearchEntity parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<WarrantyStatus_Response>("GetWarrantyStatusList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<WarrantyStatus_Response?> GetWarrantyStatusById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<WarrantyStatus_Response>("GetWarrantyStatusById", queryParameters)).FirstOrDefault();
        }

        #endregion

        #region Warranty Type
        public async Task<int> SaveWarrantyType(WarrantyType_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@WarrantyType", parameters.WarrantyType);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveWarrantyType", queryParameters);
        }

        public async Task<IEnumerable<WarrantyType_Response>> GetWarrantyTypeList(BaseSearchEntity parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<WarrantyType_Response>("GetWarrantyTypeList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<WarrantyType_Response?> GetWarrantyTypeById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<WarrantyType_Response>("GetWarrantyTypeById", queryParameters)).FirstOrDefault();
        }

        #endregion
    }
}
