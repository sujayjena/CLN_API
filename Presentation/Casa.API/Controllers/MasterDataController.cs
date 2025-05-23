﻿using CLN.Application.Interfaces;
using CLN.Application.Models;
using CLN.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterDataController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly IMasterDataRepository _masterDataRepository;

        public MasterDataController(IMasterDataRepository masterDataRepository)
        {
            _masterDataRepository = masterDataRepository;
            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetReportingToEmpListForSelectList(ReportingToEmpListParameters parameters)
        {
            IEnumerable<SelectListResponse> lstResponse = await _masterDataRepository.GetReportingToEmployeeForSelectList(parameters);
            _response.Data = lstResponse.ToList();
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetEmployeesListByReportingTo(int EmployeeId)
        {
            IEnumerable<EmployeesListByReportingTo_Response> lstResponse = await _masterDataRepository.GetEmployeesListByReportingTo(EmployeeId);
            _response.Data = lstResponse.ToList();
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetTicketListForSelectList(TicketListForSelect_Search parameters)
        {
            IEnumerable<SelectListResponse> lstResponse = await _masterDataRepository.GetTicketListForSelectList(parameters);
            _response.Data = lstResponse.ToList();
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetPartRequestEnggListForSelectList(PartRequestEnggListForSelect_Search parameters)
        {
            IEnumerable<SelectListResponse> lstResponse = await _masterDataRepository.GetPartRequestEnggListForSelectList(parameters);
            _response.Data = lstResponse.ToList();
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetPartReturnEnggListForSelectList()
        {
            IEnumerable<SelectListResponse> lstResponse = await _masterDataRepository.GetPartReturnEnggListForSelectList();
            _response.Data = lstResponse.ToList();
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetStoclAllocationEnggListForSelectList()
        {
            IEnumerable<SelectListResponse> lstResponse = await _masterDataRepository.GetStoclAllocationEnggListForSelectList();
            _response.Data = lstResponse.ToList();
            return _response;
        }
    }
}
