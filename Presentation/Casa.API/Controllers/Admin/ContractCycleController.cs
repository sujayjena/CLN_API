using CLN.Application.Enums;
using CLN.Application.Helpers;
using CLN.Application.Interfaces;
using CLN.Application.Models;
using CLN.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLN.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractCycleController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly IContractCycleRepository _contractCycleRepository;
        private IFileManager _fileManager;

        public ContractCycleController(IContractCycleRepository contractCycleRepository,IFileManager fileManager)
        {
            _contractCycleRepository = contractCycleRepository;
            _fileManager = fileManager;
            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveContractCycle(ContractCycle_Request parameters)
        {
            // File Upload
            if (parameters! != null && !string.IsNullOrWhiteSpace(parameters.ContractCycleFile_Base64))
            {
                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(parameters.ContractCycleFile_Base64, "\\Uploads\\ContractCycle\\", parameters.ContractCycleFileName);

                if (!string.IsNullOrWhiteSpace(vUploadFile))
                {
                    parameters.ContractCycleFileName = vUploadFile;
                }
            }

            int result = await _contractCycleRepository.SaveContractCycle(parameters);

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
                _response.Message = "Record details saved successfully";
            }

            _response.Id = result;
            return _response;
        }


        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetContractCycleList(BaseSearchEntity parameters)
        {
            IEnumerable<ContractCycle_Response> lstRoles = await _contractCycleRepository.GetContractCycleList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetContractCycleById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _contractCycleRepository.GetContractCycleById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }
    }
}
