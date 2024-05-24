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
    public class EmployeeLevelController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly IEmployeeLevelRepository _employeeLevelRepository;

        public EmployeeLevelController(IEmployeeLevelRepository employeeLevelRepository)
        {
            _employeeLevelRepository = employeeLevelRepository;
            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveEmployeeLevel(EmployeeLevel_Request parameters)
        {
            int result = await _employeeLevelRepository.SaveEmployeeLevel(parameters);

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
        public async Task<ResponseModel> GetEmployeeLevelList(BaseSearchEntity parameters)
        {
            IEnumerable<EmployeeLevel_Response> lstRoles = await _employeeLevelRepository.GetEmployeeLevelList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetEmployeeLevelById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _employeeLevelRepository.GetEmployeeLevelById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }
    }
}
