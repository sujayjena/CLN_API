using CLN.Application.Helpers;
using CLN.Application.Interfaces;
using CLN.Application.Models;
using CLN.Domain.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace CLN.Persistence.Repositories
{
    public class ProfileRepository : GenericRepository, IProfileRepository
    {

        private IConfiguration _configuration;

        public ProfileRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }
        public async Task<IEnumerable<RoleResponse>> GetRolesList(SearchRoleRequest parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@PageNo", parameters.pagination.PageNo);
            queryParameters.Add("@PageSize", parameters.pagination.PageSize);
            queryParameters.Add("@Total", parameters.pagination.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@SortBy", parameters.pagination.SortBy.SanitizeValue());
            queryParameters.Add("@OrderBy", parameters.pagination.OrderBy.SanitizeValue());
            queryParameters.Add("@RoleName", parameters.RoleName.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);

            var result = await ListByStoredProcedure<RoleResponse>("GetRoles", queryParameters);
            parameters.pagination.Total = queryParameters.Get<int>("Total");

            return result;
        }
    

        public async Task<UsersLoginSessionData?> ValidateUserLoginByEmail(LoginByMobileNoRequestModel parameters)
        {
            IEnumerable<UsersLoginSessionData> lstResponse;
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Username", parameters.MobileNo.SanitizeValue());
            queryParameters.Add("@Password", parameters.Password.SanitizeValue());
            queryParameters.Add("@MobileUniqueId", parameters.MobileUniqueId.SanitizeValue());

            lstResponse = await ListByStoredProcedure<UsersLoginSessionData>("ValidateUserLoginByUsername", queryParameters);
            return lstResponse.FirstOrDefault();
        }

        public async Task SaveUserLoginHistory(UserLoginHistorySaveParameters parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@UserId", parameters.UserId);
            queryParameters.Add("@UserToken", parameters.UserToken.SanitizeValue());
            queryParameters.Add("@TokenExpireOn", parameters.TokenExpireOn);
            queryParameters.Add("@DeviceName", parameters.DeviceName.SanitizeValue());
            queryParameters.Add("@IPAddress", parameters.IPAddress.SanitizeValue());
            queryParameters.Add("@RememberMe", parameters.RememberMe);
            queryParameters.Add("@IsLoggedIn", parameters.IsLoggedIn);

            await ExecuteNonQuery("SaveUserLoginHistory", queryParameters);
        }

        public async Task<UsersLoginSessionData?> GetProfileDetailsByToken(string token)
        {
            IEnumerable<UsersLoginSessionData> lstResponse;
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Token", token);
            lstResponse = await ListByStoredProcedure<UsersLoginSessionData>("GetProfileDetailsByToken", queryParameters);

            return lstResponse.FirstOrDefault();
        }
    }
}
