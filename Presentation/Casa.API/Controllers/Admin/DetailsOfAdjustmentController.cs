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
    public class DetailsOfAdjustmentController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly IDetailsOfAdjustmentRepository _detailsOfAdjustmentRepository;

        public DetailsOfAdjustmentController(IDetailsOfAdjustmentRepository detailsOfAdjustmentRepository)
        {
            _detailsOfAdjustmentRepository = detailsOfAdjustmentRepository;
            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveDetailsOfAdjustment(DetailsOfAdjustment_Request parameters)
        {
            int result = await _detailsOfAdjustmentRepository.SaveDetailsOfAdjustment(parameters);

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
        public async Task<ResponseModel> GetDetailsOfAdjustmentList(BaseSearchEntity parameters)
        {
            IEnumerable<DetailsOfAdjustment_Response> lstRoles = await _detailsOfAdjustmentRepository.GetDetailsOfAdjustmentList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetDetailsOfAdjustmentById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _detailsOfAdjustmentRepository.GetDetailsOfAdjustmentById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }
    }
}
