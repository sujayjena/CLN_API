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
        public async Task<ResponseModel> GetDashboard_TicetStatusSummary(Dashboard_TicetStatusSummary_Search_Request parameters)
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

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetDashboard_SurveyNPSSummary(DashboardNPS_Search_Request parameters)
        {
            var objList = await _dashboardRepository.GetDashboard_SurveyNPSSummary(parameters);

            decimal vAverageCount = 0;

            if (objList.ToList().Count > 0)
            {
                vAverageCount = objList.ToList().Sum(x => Convert.ToDecimal(x.NPS_Without_Perct)) / objList.ToList().Count();
            }

            _response.Data = objList.ToList();
            _response.Id = Convert.ToInt32(vAverageCount);

            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetDashboard_TicketTRCSummary(Dashboard_Search_Request parameters)
        {
            var objList = await _dashboardRepository.GetDashboard_TicketTRCSummary(parameters);
            _response.Data = objList.ToList();
            return _response;
        }

    }
}
