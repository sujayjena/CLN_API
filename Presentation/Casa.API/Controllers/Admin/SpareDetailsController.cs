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
    public class SpareDetailsController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly ISpareDetailsRepository _spareDetailsRepository;

        public SpareDetailsController(ISpareDetailsRepository spareDetailsRepository)
        {
            _spareDetailsRepository = spareDetailsRepository;

            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        #region SpareDetails 

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveSpareDetails(SpareDetails_Request parameters)
        {
            int result = await _spareDetailsRepository.SaveSpareDetails(parameters);

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
        public async Task<ResponseModel> GetSpareDetailsList(SpareDetails_Search parameters)
        {
            IEnumerable<SpareDetails_Response> lstSpareDetailss = await _spareDetailsRepository.GetSpareDetailsList(parameters);
            _response.Data = lstSpareDetailss.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetSpareDetailsById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _spareDetailsRepository.GetSpareDetailsById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion
    }
}
