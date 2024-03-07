using CLN.Application.Models;

namespace CLN.Application.Interfaces
{
    public interface IProfileRepository
    {
        Task<IEnumerable<RoleResponse>> GetRolesList(SearchRoleRequest request);
       
        Task<UsersLoginSessionData?> ValidateUserLoginByEmail(LoginByMobileNoRequestModel parameters);
        Task SaveUserLoginHistory(UserLoginHistorySaveParameters parameters);

        Task<UsersLoginSessionData?> GetProfileDetailsByToken(string token);
    }
}
