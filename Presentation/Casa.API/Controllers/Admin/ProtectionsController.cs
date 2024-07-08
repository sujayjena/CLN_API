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
    public class ProtectionsController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly IProtectionsRepository _protectionsRepository;

        public ProtectionsController(IProtectionsRepository protectionsRepository)
        {
            _protectionsRepository = protectionsRepository;
            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveProtections(Protections_Request parameters)
        {
            int result = await _protectionsRepository.SaveProtections(parameters);

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
        public async Task<ResponseModel> GetProtectionsList(BaseSearchEntity parameters)
        {
            IEnumerable<Protections_Response> lstRoles = await _protectionsRepository.GetProtectionsList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetProtectionsById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _protectionsRepository.GetProtectionsById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }
    }
}
