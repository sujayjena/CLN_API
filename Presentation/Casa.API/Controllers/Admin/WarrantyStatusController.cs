﻿using CLN.Application.Enums;
using CLN.Application.Interfaces;
using CLN.Application.Models;
using CLN.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLN.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarrantyStatusController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly IWarrantyStatusRepository _warrantyStatusRepository;

        public WarrantyStatusController(IWarrantyStatusRepository warrantyStatusRepository)
        {
            _warrantyStatusRepository = warrantyStatusRepository;
            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveWarrantyStatus(WarrantyStatus_Request parameters)
        {
            int result = await _warrantyStatusRepository.SaveWarrantyStatus(parameters);

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
        public async Task<ResponseModel> GetWarrantyStatusList(BaseSearchEntity parameters)
        {
            IEnumerable<WarrantyStatus_Response> lstRoles = await _warrantyStatusRepository.GetWarrantyStatusList(parameters);
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
                var vResultObj = await _warrantyStatusRepository.GetWarrantyStatusById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }
    }
}
