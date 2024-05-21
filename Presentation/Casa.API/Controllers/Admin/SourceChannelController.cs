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
    public class SourceChannelController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly ISourceChannelRepository _sourceChannelRepository;

        public SourceChannelController(ISourceChannelRepository sourceChannelRepository)
        {
            _sourceChannelRepository = sourceChannelRepository;
            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        #region Source channel
        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveSourceChannel(SourceChannel_Request parameters)
        {
            int result = await _sourceChannelRepository.SaveSourceChannel(parameters);

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
        public async Task<ResponseModel> GetSourceChannelList(BaseSearchEntity parameters)
        {
            IEnumerable<SourceChannel_Response> lstRoles = await _sourceChannelRepository.GetSourceChannelList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetSourceChannelById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _sourceChannelRepository.GetSourceChannelById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion

        #region caller type
        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveCallerType(CallerType_Request parameters)
        {
            int result = await _sourceChannelRepository.SaveCallerType(parameters);

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
        public async Task<ResponseModel> GetCallerTypeList(BaseSearchEntity parameters)
        {
            IEnumerable<CallerType_Response> lstRoles = await _sourceChannelRepository.GetCallerTypeList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetCallerTypeById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _sourceChannelRepository.GetCallerTypeById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion
    }
}
