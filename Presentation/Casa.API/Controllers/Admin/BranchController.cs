using CLN.Application.Enums;
using CLN.Application.Helpers;
using CLN.Application.Interfaces;
using CLN.Application.Models;
using CLN.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace CLN.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly IBranchRepository _branchRepository;
        private readonly ICompanyRepository _companyRepository;
        private IFileManager _fileManager;

        public BranchController(IBranchRepository branchRepository, ICompanyRepository companyRepository, IFileManager fileManager)
        {
            _branchRepository = branchRepository;
            _companyRepository = companyRepository;
            _fileManager = fileManager;

            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        #region Branch 

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveBranch(Branch_Request parameters)
        {
            #region Branch Restriction 

            if (parameters.Id == 0)
            {
                int iBranchCanAdd = 0;
                var tblCompanies = await _companyRepository.GetCompanyById(parameters.CompanyId ?? 0);
                if (tblCompanies != null)
                {
                    iBranchCanAdd = tblCompanies.NoofBranchAdd ?? 0;
                }

                var bParameter = new BranchSearch_Request();
                var tblBranchesList = await _branchRepository.GetBranchList(bParameter);
                var vBranchList = tblBranchesList.ToList();
                if (vBranchList.Count > 0)
                {
                    if (iBranchCanAdd == vBranchList.Count)
                    {
                        _response.IsSuccess = false;
                        _response.Message = "You are not allowed to create more then " + iBranchCanAdd + " branch, Please contact your administrator to access this feature!";
                        return _response;
                    }
                    else if (vBranchList.Count > iBranchCanAdd)
                    {
                        _response.IsSuccess = false;
                        _response.Message = "You are not allowed to create more then " + iBranchCanAdd + " branch, Please contact your administrator to access this feature!";
                        return _response;
                    }
                }
            }

            #endregion   

            int result = await _branchRepository.SaveBranch(parameters);

            if (result == (int)SaveOperationEnums.NoRecordExists)
            {
                _response.Message = "No record exists";
            }
            else if (result == (int)SaveOperationEnums.ReocrdExists)
            {
                _response.Message = "Record already exists";
            }
            else if (result == (int)SaveOperationEnums.NoResult)
            {
                _response.Message = "Something went wrong, please try again";
            }
            else
            {
                _response.Message = "Record details saved sucessfully";

                #region // Add/Update Branch Region

                // Delete Region of Branch
                var vRegionDELETEObj = new BranchRegion_Request()
                {
                    Action = "DELETE",
                    BranchId = result,
                    RegionId = 0
                };
                int resultRegionMappingDELETE = await _branchRepository.SaveBranchRegion(vRegionDELETEObj);


                // Add new mapping of Branch
                foreach (var vRegionitem in parameters.RegionList)
                {
                    var vRegionMapObj = new BranchRegion_Request()
                    {
                        Action = "INSERT",
                        BranchId = result,
                        RegionId = vRegionitem.RegionId
                    };

                    int resultRegionMapping = await _branchRepository.SaveBranchRegion(vRegionMapObj);
                }

                #endregion

                #region // Add/Update Branch State

                // Delete state of Branch
                var vStateDELETEObj = new BranchState_Request()
                {
                    Action = "DELETE",
                    BranchId = result,
                    StateId = 0
                };
                int resultStateMappingDELETE = await _branchRepository.SaveBranchState(vStateDELETEObj);


                // Add new mapping of Branch
                foreach (var vStateitem in parameters.StateList)
                {
                    var vStateMapObj = new BranchState_Request()
                    {
                        Action = "INSERT",
                        BranchId = result,
                        StateId = vStateitem.StateId
                    };

                    int resultBranchMapping = await _branchRepository.SaveBranchState(vStateMapObj);
                }

                #endregion
            }

            _response.Id = result;
            return _response;
        }


        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetBranchList(BranchSearch_Request parameters)
        {
            IEnumerable<Branch_Response> lstBranchs = await _branchRepository.GetBranchList(parameters);
            _response.Data = lstBranchs.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetBranchById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _branchRepository.GetBranchById(Id);
                if (vResultObj != null)
                {
                    var regionlistObj = await _branchRepository.GetBranchRegionByBranchId(vResultObj.Id, 0);
                    var statelistObj = await _branchRepository.GetBranchStateByBranchId(vResultObj.Id, 0);

                    vResultObj.RegionList = regionlistObj.ToList();
                    vResultObj.StateList = statelistObj.ToList();
                }
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion
    }
}
