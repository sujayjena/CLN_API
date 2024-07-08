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
    public class ReceiveModeController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly IReceiveModeRepository _receiveModeRepository;

        public ReceiveModeController(IReceiveModeRepository receiveModeRepository)
        {
            _receiveModeRepository = receiveModeRepository;
            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveReceiveMode(ReceiveMode_Request parameters)
        {
            int result = await _receiveModeRepository.SaveReceiveMode(parameters);

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
            }

            _response.Id = result;
            return _response;
        }


        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetReceiveModeList(BaseSearchEntity parameters)
        {
            IEnumerable<ReceiveMode_Response> lstRoles = await _receiveModeRepository.GetReceiveModeList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetReceiveModeById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _receiveModeRepository.GetReceiveModeById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }
    }
}
