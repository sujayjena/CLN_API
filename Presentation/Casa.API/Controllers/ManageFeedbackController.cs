﻿using CLN.Application.Enums;
using CLN.Application.Interfaces;
using CLN.Application.Models;
using CLN.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageFeedbackController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly IManageFeedbackRepository _manageFeedbackRepository;

        public ManageFeedbackController(IManageFeedbackRepository manageFeedbackRepository)
        {
            _manageFeedbackRepository = manageFeedbackRepository;
            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveManageFeedback(ManageFeedback_Request parameters)
        {
            int result = await _manageFeedbackRepository.SaveManageFeedback(parameters);

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
        public async Task<ResponseModel> GetManageFeedbackList(BaseSearchEntity parameters)
        {
            IEnumerable<ManageFeedback_Response> lstRoles = await _manageFeedbackRepository.GetManageFeedbackList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetManageFeedbackById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _manageFeedbackRepository.GetManageFeedbackById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }
    }
}
