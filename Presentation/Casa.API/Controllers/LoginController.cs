using CLN.API.CustomAttributes;
using CLN.Application.Constants;
using CLN.Application.Helpers;
using CLN.Application.Interfaces;
using CLN.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace CLN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ResponseModel _response;
        private IProfileRepository _profileRepository;
        private IJwtUtilsRepository _jwt;

        public LoginController(IProfileRepository profileRepository, IJwtUtilsRepository jwt)
        {
            _profileRepository = profileRepository;
            _jwt = jwt;

            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ResponseModel> MobileAppLogin(MobileAppLoginRequestModel parameters)
        {
            LoginByMobileNoRequestModel loginParameters = new LoginByMobileNoRequestModel();
            loginParameters.MobileNo = parameters.MobileNo;
            loginParameters.Password = parameters.Password;
            loginParameters.MobileUniqueId = parameters.MobileUniqueId;
            loginParameters.Remember = parameters.Remember;
            loginParameters.IsWebOrMobileUser = parameters.IsWebOrMobileUser;

            //_response.Data = await Login(loginParameters);

            var vLoginObj = new ResponseModel();
            vLoginObj.Data = await Login(loginParameters);

            return vLoginObj;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ResponseModel> Login(LoginByMobileNoRequestModel parameters)
        {
            (string, DateTime) tokenResponse;
            SessionDataEmployee employeeSessionData;
            UsersLoginSessionData? loginResponse;
            UserLoginHistorySaveParameters loginHistoryParameters;

            parameters.Password = EncryptDecryptHelper.EncryptString(parameters.Password);

            loginResponse = await _profileRepository.ValidateUserLoginByEmail(parameters);

            if (loginResponse != null)
            {
                if (loginResponse.IsActive == true && (loginResponse.IsWebUser == true && parameters.IsWebOrMobileUser == "W" || loginResponse.IsMobileUser == true && parameters.IsWebOrMobileUser == "M"))
                {
                    tokenResponse = _jwt.GenerateJwtToken(loginResponse);

                    if (loginResponse.UserId != null)
                    {
                        //var vRoleList = await _profileRepository.GetRoleMaster_Employee_PermissionById(Convert.ToInt64(loginResponse.EmployeeId));
                        //var vUserNotificationList = await _notificationService.GetNotificationListById(Convert.ToInt64(loginResponse.EmployeeId));

                        employeeSessionData = new SessionDataEmployee
                        {
                            UserId = loginResponse.UserId,
                            UserCode = loginResponse.UserCode,
                            Name = loginResponse.FirstName + " " + loginResponse.LastName,
                            RoleId = loginResponse.RoleId,
                            EmailId = loginResponse.EmailId,
                            MobileNo = loginResponse.MobileNo,
                            RoleName = loginResponse.RoleName,
                            Token = tokenResponse.Item1,
                            //UserRoleList = vRoleList.ToList(),
                           //UserNotificationList = vUserNotificationList.ToList()
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

                    await _profileRepository.SaveUserLoginHistory(loginHistoryParameters);

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

            await _profileRepository.SaveUserLoginHistory(logoutParameters);

            _response.Message = MessageConstants.LogoutSuccessful;

            return _response;
        }
    }
}
