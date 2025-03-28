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
    public class AdminDaysController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly IAdminDaysRepository _AdminDaysRepository;

        public AdminDaysController(IAdminDaysRepository AdminDaysRepository)
        {
            _AdminDaysRepository = AdminDaysRepository;
            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveAdminDays(AdminDays_Request parameters)
        {
            int result = await _AdminDaysRepository.SaveAdminDays(parameters);

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
        public async Task<ResponseModel> GetAdminDaysList(AdminDays_Search parameters)
        {
            IEnumerable<AdminDays_Response> lstRoles = await _AdminDaysRepository.GetAdminDaysList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetAdminDaysById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _AdminDaysRepository.GetAdminDaysById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }
    }
}
