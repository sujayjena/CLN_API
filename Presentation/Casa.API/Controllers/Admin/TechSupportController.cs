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
    public class TechSupportController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly ITechSupportRepository _techSupportRepository;

        public TechSupportController(ITechSupportRepository techSupportRepository)
        {
            _techSupportRepository = techSupportRepository;
            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveTechSupport(TechSupport_Request parameters)
        {
            int result = await _techSupportRepository.SaveTechSupport(parameters);

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
        public async Task<ResponseModel> GetTechSupportList(BaseSearchEntity parameters)
        {
            IEnumerable<TechSupport_Response> lstRoles = await _techSupportRepository.GetTechSupportList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetTechSupportById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _techSupportRepository.GetTechSupportById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }
    }
}
