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
    public class BloodGroupController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly IBloodGroupRepository _bloodGroupRepository;

        public BloodGroupController(IBloodGroupRepository bloodGroupRepository)
        {
            _bloodGroupRepository = bloodGroupRepository;
            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveBloodGroup(BloodGroup_Request parameters)
        {
            int result = await _bloodGroupRepository.SaveBloodGroup(parameters);

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
        public async Task<ResponseModel> GetBloodGroupList(BaseSearchEntity parameters)
        {
            IEnumerable<BloodGroup_Response> lstRoles = await _bloodGroupRepository.GetBloodGroupList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetBloodGroupById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _bloodGroupRepository.GetBloodGroupById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }
    }
}
