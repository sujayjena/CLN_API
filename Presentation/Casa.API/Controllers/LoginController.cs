using CLN.API.CustomAttributes;
using CLN.Application.Constants;
using CLN.Application.Enums;
using CLN.Application.Helpers;
using CLN.Application.Interfaces;
using CLN.Application.Models;
using CLN.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CLN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ResponseModel _response;
        private ILoginRepository _loginRepository;
        private IJwtUtilsRepository _jwt;
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly INotificationRepository _notificationRepository;

        public LoginController(ILoginRepository loginRepository, IJwtUtilsRepository jwt, IRolePermissionRepository rolePermissionRepository, IUserRepository userRepository, IBranchRepository branchRepository, ICompanyRepository companyRepository, INotificationRepository notificationRepository)
        {
            _loginRepository = loginRepository;
            _jwt = jwt;
            _userRepository = userRepository;
            _rolePermissionRepository = rolePermissionRepository;
            _branchRepository = branchRepository;
            _companyRepository = companyRepository;
            _notificationRepository = notificationRepository;

            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ResponseModel> OTPGenerate(OTPRequestModel parameters)
        {
            int result = await _loginRepository.ValidateUserMobile(parameters);

            if (result == (int)SaveOperationEnums.NoResult)
            {
                _response.Message = "No record exists";
            }
            else
            {
                int iOTP = Utilities.GenerateRandomNumForOTP();
                if (iOTP > 0)
                {
                    parameters.OTP = Convert.ToString(iOTP);
                }

                // Opt save
                int resultOTP = await _loginRepository.SaveOTP(parameters);

                if (resultOTP > 0)
                {
                    _response.Message = "OTP sent successfully.";
                }
            }

            return _response;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ResponseModel> OTPVerification(OTPVerifyModel parameters)
        {
            if (parameters.OTP == "1234")
            {
                _response.Message = "OTP verified sucessfully.";
            }
            else
            {
                _response.Message = "Invalid OTP!";
                _response.IsSuccess = false;
            }

            //int result = await _loginRepository.VerifyOTP(parameters);

            //if (result == (int)SaveOperationEnums.NoResult)
            //{
            //    _response.Message = "Invalid OTP!";
            //}
            //else if (result == (int)SaveOperationEnums.ReocrdExists)
            //{
            //    _response.Message = "OTP timeout!";
            //}
            //else
            //{
            //    _response.Message = "OTP verified sucessfully.";
            //}

            return _response;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ResponseModel> MobileAppLogin(MobileAppLoginRequestModel parameters)
        {
            LoginByMobileNumberRequestModel loginParameters = new LoginByMobileNumberRequestModel();
            loginParameters.MobileNumber = parameters.MobileNumber;
            loginParameters.Password = parameters.Password;
            loginParameters.MobileUniqueId = parameters.MobileUniqueId;
            loginParameters.Remember = parameters.Remember;
            loginParameters.IsWebOrMobileUser = parameters.IsWebOrMobileUser;

            //_response.Data = await Login(loginParameters);

            var vLoginObj = await Login(loginParameters);

            return vLoginObj;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ResponseModel> Login(LoginByMobileNumberRequestModel parameters)
        {
            (string, DateTime) tokenResponse;
            SessionDataEmployee employeeSessionData;
            UsersLoginSessionData? loginResponse;
            UserLoginHistorySaveParameters loginHistoryParameters;

            parameters.Password = EncryptDecryptHelper.EncryptString(parameters.Password);

            loginResponse = await _loginRepository.ValidateUserLoginByEmail(parameters);

            if (loginResponse != null)
            {
                if (loginResponse.IsActive == true && (loginResponse.IsWebUser == true && parameters.IsWebOrMobileUser == "W" || loginResponse.IsMobileUser == true && parameters.IsWebOrMobileUser == "M"))
                {
                    tokenResponse = _jwt.GenerateJwtToken(loginResponse);

                    if (loginResponse.UserId != null)
                    {
                        string strBrnachIdList = string.Empty;

                        var vRoleList = await _rolePermissionRepository.GetRoleMasterEmployeePermissionById(Convert.ToInt64(loginResponse.UserId));

                        // Notification List
                        var vNotification_SearchObj = new Notification_Search()
                        {
                            NotifyDate = null,
                            UserId = Convert.ToInt32(loginResponse.UserId)
                        };
                        var vUserNotificationList = await _notificationRepository.GetNotificationList(vNotification_SearchObj);

                        var vUserDetail = await _userRepository.GetUserById(Convert.ToInt32(loginResponse.UserId));
                        var vUserBranchMappingDetail = await _branchRepository.GetBranchMappingByEmployeeId(EmployeeId: Convert.ToInt32(loginResponse.UserId), BranchId: 0);
                        if (vUserBranchMappingDetail.ToList().Count > 0)
                        {
                            strBrnachIdList = string.Join(",", vUserBranchMappingDetail.ToList().OrderBy(x => x.BranchId).Select(x => x.BranchId));
                        }

                        int iBranchCanAdd = 0;
                        int iUserCanAdd = 0;
                        var tblCompanies = await _companyRepository.GetCompanyById(vUserDetail.CompanyId ?? 0);
                        if (tblCompanies != null)
                        {
                            iBranchCanAdd = tblCompanies.NoofBranchAdd ?? 0;
                            iUserCanAdd = tblCompanies.NoofUserAdd ?? 0;
                        }

                        employeeSessionData = new SessionDataEmployee
                        {
                            UserId = loginResponse.UserId,
                            UserCode = loginResponse.UserCode,
                            UserName = loginResponse.UserName,
                            MobileNumber = loginResponse.MobileNumber,
                            EmailId = loginResponse.EmailId,
                            UserType = loginResponse.UserType,
                            RoleId = loginResponse.RoleId,
                            RoleName = loginResponse.RoleName,
                            IsMobileUser = loginResponse.IsMobileUser,
                            IsWebUser = loginResponse.IsWebUser,
                            IsActive = loginResponse.IsActive,
                            Token = tokenResponse.Item1,

                            CompanyId = vUserDetail != null ? Convert.ToInt32(vUserDetail.CompanyId) : 0,
                            CompanyName = vUserDetail != null ? vUserDetail.CompanyName : String.Empty,
                            DepartmentId = vUserDetail != null ? Convert.ToInt32(vUserDetail.DepartmentId) : 0,
                            DepartmentName = vUserDetail != null ? vUserDetail.DepartmentName : String.Empty,
                            BranchId = strBrnachIdList,
                            BranchCanAdd = iBranchCanAdd,
                            UserCanAdd = iUserCanAdd,

                            ProfileImage = vUserDetail != null ? vUserDetail.ProfileImage : String.Empty,
                            ProfileOriginalFileName = vUserDetail != null ? vUserDetail.ProfileOriginalFileName : String.Empty,
                            ProfileImageURL = vUserDetail != null ? vUserDetail.ProfileImageURL : String.Empty,

                            UserRoleList = vRoleList.ToList(),
                            UserNotificationList = vUserNotificationList.ToList()
                        };

                        _response.Data = employeeSessionData;
                    }

                    //Login History
                    loginHistoryParameters = new UserLoginHistorySaveParameters
                    {
                        UserId = loginResponse.UserId,
                        UserToken = tokenResponse.Item1,
                        IsLoggedIn = true,
                        IPAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                        DeviceName = HttpContext.Request.Headers["User-Agent"],
                        TokenExpireOn = tokenResponse.Item2,
                        RememberMe = parameters.Remember
                    };

                    await _loginRepository.SaveUserLoginHistory(loginHistoryParameters);

                    _response.Message = MessageConstants.LoginSuccessful;
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.Message = ErrorConstants.InactiveProfileError;
                }
            }
            else
            {
                _response.IsSuccess = false;
                _response.Message = "Invalid credential, please try again with correct credential";
            }

            return _response;
        }

        [HttpPost]
        [Route("[action]")]
        [CustomAuthorize]
        public async Task<ResponseModel> Logout()
        {
            string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last().SanitizeValue()!;
            //UsersLoginSessionData? sessionData = (UsersLoginSessionData?)HttpContext.Items["SessionData"]!;

            UserLoginHistorySaveParameters logoutParameters = new UserLoginHistorySaveParameters
            {
                UserId = SessionManager.LoggedInUserId,
                UserToken = token,
                IsLoggedIn = false, //To Logout set IsLoggedIn = false
                IPAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                DeviceName = HttpContext.Request.Headers["User-Agent"],
                TokenExpireOn = DateTime.Now,
                RememberMe = false
            };

            await _loginRepository.SaveUserLoginHistory(logoutParameters);

            _response.Message = MessageConstants.LogoutSuccessful;

            return _response;
        }
    }
}
