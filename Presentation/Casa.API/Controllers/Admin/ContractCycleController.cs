using CLN.Application.Enums;
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

        public ContractCycleController(IContractCycleRepository contractCycleRepository)
        {
            _contractCycleRepository = contractCycleRepository;
            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveContractCycle(ContractCycle_Request parameters)
        {
            int result = await _contractCycleRepository.SaveContractCycle(parameters);

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
