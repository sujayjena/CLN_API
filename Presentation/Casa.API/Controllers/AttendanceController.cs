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
    public class AttendanceController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly IAttendanceRepository _attendanceRepository;
        private IFileManager _fileManager;

        public AttendanceController(IAttendanceRepository attendanceRepository, IFileManager fileManager)
        {
            _attendanceRepository = attendanceRepository;
            _fileManager = fileManager;

            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveAttendance(Attendance_Request parameters)
        {
            int result = await _attendanceRepository.SaveAttendance(parameters);

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
        public async Task<ResponseModel> GetAttendanceList(AttendanceSearch parameters)
        {
            var objList = await _attendanceRepository.GetAttendanceList(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetAttendanceById(int userId)
        {
            if (userId <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _attendanceRepository.GetAttendanceById(userId);
                _response.Data = vResultObj;
            }
            return _response;
        }
    }
}
