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
    public class SLAController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly ISLARepository _slaRepository;

        public SLAController(ISLARepository slaRepository)
        {
            _slaRepository = slaRepository;
            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        #region Priority

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveSLAPriority(SLAPriority_Request parameters)
        {
            int result = await _slaRepository.SaveSLAPriority(parameters);

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
        public async Task<ResponseModel> GetSLAPriorityList(BaseSearchEntity parameters)
        {
            IEnumerable<SLAPriority_Response> lstRoles = await _slaRepository.GetSLAPriorityList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetSLAPriorityById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _slaRepository.GetSLAPriorityById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }
        #endregion


        #region  Sla Matrix

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveSlaMatrix(SlaMatrix_Request parameters)
        {
            int result = await _slaRepository.SaveSlaMatrix(parameters);

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
        public async Task<ResponseModel> GetSlaMatrixList(BaseSearchEntity parameters)
        {
            IEnumerable<SlaMatrix_Response> lstRoles = await _slaRepository.GetSlaMatrixList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetSlaMatrixById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _slaRepository.GetSlaMatrixById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion
    }
}
