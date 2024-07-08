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
    public class WarrantyController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly IWarrantyRepository _warrantyRepository;

        public WarrantyController(IWarrantyRepository warrantyRepository)
        {
            _warrantyRepository = warrantyRepository;
            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        #region Warranty Status
        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveWarrantyStatus(WarrantyStatus_Request parameters)
        {
            int result = await _warrantyRepository.SaveWarrantyStatus(parameters);

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
        public async Task<ResponseModel> GetWarrantyStatusList(BaseSearchEntity parameters)
        {
            IEnumerable<WarrantyStatus_Response> lstRoles = await _warrantyRepository.GetWarrantyStatusList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetWarrantyStatusById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _warrantyRepository.GetWarrantyStatusById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }
        #endregion

        #region Warranty Type
        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveWarrantyType(WarrantyType_Request parameters)
        {
            int result = await _warrantyRepository.SaveWarrantyType(parameters);

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
        public async Task<ResponseModel> GetWarrantyTypeList(BaseSearchEntity parameters)
        {
            IEnumerable<WarrantyType_Response> lstRoles = await _warrantyRepository.GetWarrantyTypeList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetWarrantyTypeById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _warrantyRepository.GetWarrantyTypeById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }
        #endregion
    }
}
