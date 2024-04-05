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
    public class ProblemReportedController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly IProblemReportedRepository _problemReportedRepository;

        public ProblemReportedController(IProblemReportedRepository problemReportedRepository)
        {
            _problemReportedRepository = problemReportedRepository;
            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveProblemReported(ProblemReported_Request parameters)
        {
            int result = await _problemReportedRepository.SaveProblemReported(parameters);

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
        public async Task<ResponseModel> GetProblemReportedList(BaseSearchEntity parameters)
        {
            IEnumerable<ProblemReported_Response> lstRoles = await _problemReportedRepository.GetProblemReportedList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetProblemReportedById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _problemReportedRepository.GetProblemReportedById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }
    }
}
