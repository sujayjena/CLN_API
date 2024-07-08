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
    public class ProblemReportedByEngController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly IProblemReportedByEngRepository _problemReportedByEngRepository;

        public ProblemReportedByEngController(IProblemReportedByEngRepository problemReportedByEngRepository)
        {
            _problemReportedByEngRepository = problemReportedByEngRepository;
            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveProblemReportedByEng(ProblemReportedByEng_Request parameters)
        {
            int result = await _problemReportedByEngRepository.SaveProblemReportedByEng(parameters);

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
        public async Task<ResponseModel> GetProblemReportedByEngList(BaseSearchEntity parameters)
        {
            IEnumerable<ProblemReportedByEng_Response> lstRoles = await _problemReportedByEngRepository.GetProblemReportedByEngList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetProblemReportedByEngById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _problemReportedByEngRepository.GetProblemReportedByEngById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }
    }
}
