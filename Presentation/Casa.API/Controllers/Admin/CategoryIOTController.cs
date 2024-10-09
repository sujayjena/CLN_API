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
    public class CategoryIOTController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly ICategoryIOTRepository _categoryIOTRepository;

        public CategoryIOTController(ICategoryIOTRepository categoryIOTRepository)
        {
            _categoryIOTRepository = categoryIOTRepository;
            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        #region Tracking Device

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveTrackingDevice(TrackingDevice_Request parameters)
        {
            int result = await _categoryIOTRepository.SaveTrackingDevice(parameters);

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
        public async Task<ResponseModel> GetTrackingDeviceList(BaseSearchEntity parameters)
        {
            IEnumerable<TrackingDevice_Response> lstRoles = await _categoryIOTRepository.GetTrackingDeviceList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetTrackingDeviceById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _categoryIOTRepository.GetTrackingDeviceById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }
        #endregion

        #region Make

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveMake(Make_Request parameters)
        {
            int result = await _categoryIOTRepository.SaveMake(parameters);

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
        public async Task<ResponseModel> GetMakeList(BaseSearchEntity parameters)
        {
            IEnumerable<Make_Response> lstRoles = await _categoryIOTRepository.GetMakeList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetMakeById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _categoryIOTRepository.GetMakeById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }
        #endregion

        #region SIM Provider

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveSIMProvider(SIMProvider_Request parameters)
        {
            int result = await _categoryIOTRepository.SaveSIMProvider(parameters);

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
        public async Task<ResponseModel> GetSIMProviderList(BaseSearchEntity parameters)
        {
            IEnumerable<SIMProvider_Response> lstRoles = await _categoryIOTRepository.GetSIMProviderList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetSIMProviderById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _categoryIOTRepository.GetSIMProviderById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }
        #endregion

        #region Platform

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SavePlatform(Platform_Request parameters)
        {
            int result = await _categoryIOTRepository.SavePlatform(parameters);

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
        public async Task<ResponseModel> GetPlatformList(BaseSearchEntity parameters)
        {
            IEnumerable<Platform_Response> lstRoles = await _categoryIOTRepository.GetPlatformList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetPlatformById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _categoryIOTRepository.GetPlatformById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }
        #endregion
    }
}
