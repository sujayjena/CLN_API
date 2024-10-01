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
    public class BranchRepository : GenericRepository, IBranchRepository
    {
        private IConfiguration _configuration;

        public BranchRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> SaveBranch(Branch_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@BranchName", parameters.BranchName);
            queryParameters.Add("@CompanyId", parameters.CompanyId);
            queryParameters.Add("@EmailId", parameters.EmailId);
            queryParameters.Add("@MobileNo", parameters.MobileNo);
            queryParameters.Add("@DepartmentHead", parameters.DepartmentHead);
            queryParameters.Add("@AddressLine1", parameters.AddressLine1);
            queryParameters.Add("@AddressLine2", parameters.AddressLine2);
            queryParameters.Add("@RegionId", parameters.RegionId);
            queryParameters.Add("@StateId", parameters.StateId);
            queryParameters.Add("@DistrictId", parameters.DistrictId);
            queryParameters.Add("@CityId", parameters.CityId);
            queryParameters.Add("@Pincode", parameters.Pincode);
            if (SessionManager.LoggedInUserId == 1)
            {
                queryParameters.Add("@NoofUserAdd", parameters.NoofUserAdd);
            }
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveBranch", queryParameters);
        }

        public async Task<IEnumerable<Branch_Response>> GetBranchList(BranchSearch_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@CompanyId", parameters.CompanyId);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<Branch_Response>("GetBranchList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<Branch_Response?> GetBranchById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<Branch_Response>("GetBranchById", queryParameters)).FirstOrDefault();
        }

        public async Task<int> SaveBranchRegion(BranchRegion_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Action", parameters.Action);
            queryParameters.Add("@BranchId", parameters.BranchId);
            queryParameters.Add("@RegionId", parameters.RegionId);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveBranchRegion", queryParameters);
        }

        public async Task<IEnumerable<BranchRegion_Response>> GetBranchRegionByBranchId(long BranchId, long RegionId)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@BranchId", BranchId);
            queryParameters.Add("@RegionId", RegionId);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<BranchRegion_Response>("GetBranchRegionByBranchId", queryParameters);

            return result;
        }

        public async Task<int> SaveBranchState(BranchState_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Action", parameters.Action);
            queryParameters.Add("@BranchId", parameters.BranchId);
            queryParameters.Add("@StateId", parameters.StateId);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveBranchState", queryParameters);
        }

        public async Task<IEnumerable<BranchState_Response>> GetBranchStateByBranchId(long BranchId, long StateId)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@BranchId", BranchId);
            queryParameters.Add("@StateId", StateId);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<BranchState_Response>("GetBranchStateByBranchId", queryParameters);

            return result;
        }

        #region Branch Mapping

        public async Task<int> SaveBranchMapping(BranchMapping_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Action", parameters.Action);
            queryParameters.Add("@EmployeeId", parameters.UserId);
            queryParameters.Add("@BranchId", parameters.BranchId);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveBranchMapping", queryParameters);
        }

        public async Task<IEnumerable<BranchMapping_Response>> GetBranchMappingByEmployeeId(int EmployeeId, int BranchId)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@EmployeeId", EmployeeId);
            queryParameters.Add("@BranchId", BranchId);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<BranchMapping_Response>("GetBranchMappingByEmployeeId", queryParameters);

            return result;
        }

        #endregion
    }
}
