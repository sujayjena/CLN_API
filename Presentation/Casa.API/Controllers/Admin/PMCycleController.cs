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
    public class PMCycleController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly IPMCycleRepository _pmCycleRepository;

        public PMCycleController(IPMCycleRepository pmCycleRepository)
        {
            _pmCycleRepository = pmCycleRepository;
            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SavePMCycle(PMCycle_Request parameters)
        {
            int result = await _pmCycleRepository.SavePMCycle(parameters);

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
        public async Task<ResponseModel> GetPMCycleList(BaseSearchEntity parameters)
        {
            IEnumerable<PMCycle_Response> lstRoles = await _pmCycleRepository.GetPMCycleList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetPMCycleById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _pmCycleRepository.GetPMCycleById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }
    }
}
