using CLN.Application.Enums;
using CLN.Application.Helpers;
using CLN.Application.Interfaces;
using CLN.Application.Models;
using CLN.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CLN.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IBranchRepository _branchRepository;
        private IFileManager _fileManager;

        public UserController(IUserRepository userRepository, ICompanyRepository companyRepository, IBranchRepository branchRepository, IFileManager fileManager)
        {
            _userRepository = userRepository;
            _companyRepository = companyRepository;
            _branchRepository = branchRepository;
            _fileManager = fileManager;

            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        #region User 

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveUser(User_Request parameters)
        {
            #region User Restriction 

            int vCompanyNoofUserAdd = 0;
            int vBranchNoofUserAdd = 0;

            int totalCompanyUser = 0;
            int totalBranchUser = 0;

            if (parameters.Id == 0)
            {
                var baseSearch = new BaseSearchEntity();
                var vUser = await _userRepository.GetUserList(baseSearch);

                if (parameters.CompanyId > 0)
                {
                    var vCompany = await _companyRepository.GetCompanyById(parameters.CompanyId);
                    if (vCompany != null)
                    {
                        vCompanyNoofUserAdd = vCompany.NoofUserAdd ?? 0;
                    }

                    //get total company user
                    totalCompanyUser = vUser.Where(x => x.IsActive==true && x.CompanyId == parameters.CompanyId && (x.BranchId == 0 || x.BranchId == null)).Count();
                }
                if (parameters.BranchId > 0)
                {
                    var vBranch = await _branchRepository.GetBranchById(parameters.BranchId);
                    if (vBranch != null)
                    {
                        vBranchNoofUserAdd = vBranch.NoofUserAdd ?? 0;
                    }

                    //get total branch user
                    totalBranchUser = vUser.Where(x => x.IsActive == true && x.CompanyId == parameters.CompanyId && x.BranchId == parameters.BranchId).Count();
                }

                if (parameters.CompanyId > 0 && parameters.BranchId == 0)
                {
                    if (totalCompanyUser >= vCompanyNoofUserAdd)
                    {
                        _response.Message = "You are not allowed to create more then " + vCompanyNoofUserAdd + " company user, Please contact your administrator to access this feature!";
                        return _response;
                    }
                }

                if (parameters.CompanyId > 0 && parameters.BranchId > 0)
                {
                    if (totalBranchUser >= vBranchNoofUserAdd)
                    {
                        _response.Message = "You are not allowed to create more then " + vBranchNoofUserAdd + " branch user, Please contact your administrator to access this feature!";
                        return _response;
                    }
                }
            }

            #endregion

            // Aadhar Card Upload
            if (parameters !!= null && !string.IsNullOrWhiteSpace(parameters.AadharImage_Base64))
            {
                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(parameters.AadharImage_Base64, "\\Uploads\\Employee\\", parameters.AadharImageFileNaame);

                if (!string.IsNullOrWhiteSpace(vUploadFile))
                {
                    parameters.AadharImageFileNaame = vUploadFile;
                }
            }

            // Pan Card Upload
            if (parameters != null && !string.IsNullOrWhiteSpace(parameters.PanCardImage_Base64))
            {
                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(parameters.PanCardImage_Base64, "\\Uploads\\Employee\\", parameters.PanCardImageFileNaame);

                if (!string.IsNullOrWhiteSpace(vUploadFile))
                {
                    parameters.PanCardImageFileNaame = vUploadFile;
                }
            }

            int result = await _userRepository.SaveUser(parameters);

            if (result == (int)SaveOperationEnums.NoRecordExists)
            {
                _response.Message = "No record exists";
            }
            else if (result == (int)SaveOperationEnums.ReocrdExists)
            {
                _response.Message = "Record is already exists";
            }
            else if (result == (int)SaveOperationEnums.NoResult)
            {
                _response.Message = "Something went wrong, please try again";
            }
            else
            {
                _response.Message = "Record details saved sucessfully";
            }
            return _response;
        }


        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetUserList(BaseSearchEntity parameters)
        {
            IEnumerable<User_Response> lstUsers = await _userRepository.GetUserList(parameters);
            _response.Data = lstUsers.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetUserById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _userRepository.GetUserById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion
    }
}
