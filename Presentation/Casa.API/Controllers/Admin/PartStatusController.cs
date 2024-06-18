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
    public class PartStatusController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly IPartStatusRepository _partStatusRepository;

        public PartStatusController(IPartStatusRepository partStatusRepository)
        {
            _partStatusRepository = partStatusRepository;
            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SavePartStatus(PartStatus_Request parameters)
        {
            int result = await _partStatusRepository.SavePartStatus(parameters);

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
        public async Task<ResponseModel> GetPartStatusList(BaseSearchEntity parameters)
        {
            IEnumerable<PartStatus_Response> lstRoles = await _partStatusRepository.GetPartStatusList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetPartStatusById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _partStatusRepository.GetPartStatusById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }
    }
}