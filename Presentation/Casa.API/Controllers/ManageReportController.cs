using CLN.Application.Interfaces;
using CLN.Application.Models;
using CLN.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageReportController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly IManageReportRepository _manageReportRepository;

        public ManageReportController(IManageReportRepository manageReportRepository)
        {
            _manageReportRepository = manageReportRepository;
            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetTicket_TRC_Report(ManageReport_Search parameters)
        {
            IEnumerable<Ticket_TRC_Report_Response> lstRoles = await _manageReportRepository.GetTicket_TRC_Report(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetCustomerWiseReport(ManageReport_Search parameters)
        {
            IEnumerable<CustomerWiseReport_Response> lstRoles = await _manageReportRepository.GetCustomerWiseReport(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetCustomerSatisfactionReport(ManageReport_Search parameters)
        {
            IEnumerable<CustomerSatisfactionReport_Response> lstRoles = await _manageReportRepository.GetCustomerSatisfactionReport(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }
    }
}
