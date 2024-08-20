using CLN.Application.Enums;
using CLN.Application.Helpers;
using CLN.Application.Interfaces;
using CLN.Application.Models;
using CLN.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : CustomBaseController
    {
        private ResponseModel _response;
        private IFileManager _fileManager;

        private readonly IDashboardRepository _dashboardRepository;

        public DashboardController(IFileManager fileManager, IDashboardRepository dashboardRepository)
        {
            _fileManager = fileManager;

            _response = new ResponseModel();
            _response.IsSuccess = true;
            _dashboardRepository = dashboardRepository;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetDashboard_TicketResolvedSummary(Dashboard_Search_Request parameters)
        {
            var objList = await _dashboardRepository.GetDashboard_TicketResolvedSummary(parameters);
            _response.Data = objList.ToList();
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetDashboard_TicetStatusSummary(Dashboard_Search_Request parameters)
        {
            var objList = await _dashboardRepository.GetDashboard_TicetStatusSummary(parameters);
            _response.Data = objList.ToList();
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> Dashboard_TicketVisitSummary(Dashboard_Search_Request parameters)
        {
            var objList = await _dashboardRepository.GetDashboard_TicketVisitSummary(parameters);
            _response.Data = objList.ToList();
            return _response;
        }
    }
}
