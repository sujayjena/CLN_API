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
    public class LeaveController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly ILeaveRepository _leaveRepository;
        private IFileManager _fileManager;

        public LeaveController(ILeaveRepository leaveRepository, IFileManager fileManager)
        {
            _leaveRepository = leaveRepository;
            _fileManager = fileManager;

            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveLeave(Leave_Request parameters)
        {
            int result = await _leaveRepository.SaveLeave(parameters);

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
        public async Task<ResponseModel> GetLeaveList(LeaveSearch parameters)
        {
            var objList = await _leaveRepository.GetLeaveList(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetLeaveById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vLeaveDetails_Response = new LeaveDetails_Response();

                var vResultObj = await _leaveRepository.GetLeaveById(Id);
                if (vResultObj != null)
                {
                    vLeaveDetails_Response.Id = vResultObj.Id;
                    vLeaveDetails_Response.StartDate = vResultObj.StartDate;
                    vLeaveDetails_Response.EndDate = vResultObj.EndDate;
                    vLeaveDetails_Response.EmployeeId = vResultObj.EmployeeId;
                    vLeaveDetails_Response.EmployeeName = vResultObj.EmployeeName;
                    vLeaveDetails_Response.LeaveTypeId = vResultObj.LeaveTypeId;
                    vLeaveDetails_Response.LeaveType = vResultObj.LeaveType;
                    vLeaveDetails_Response.Remark = vResultObj.Remark;
                    vLeaveDetails_Response.Reason = vResultObj.Reason;
                    vLeaveDetails_Response.StatusId = vResultObj.StatusId;
                    vLeaveDetails_Response.StatusName = vResultObj.StatusName;
                    vLeaveDetails_Response.IsActive = vResultObj.IsActive;
                    vLeaveDetails_Response.CreatorName = vResultObj.CreatorName;
                    vLeaveDetails_Response.CreatedBy = vResultObj.CreatedBy;
                    vLeaveDetails_Response.CreatedDate = vResultObj.CreatedDate;
                    vLeaveDetails_Response.ModifierName = vResultObj.ModifierName;
                    vLeaveDetails_Response.ModifiedBy = vResultObj.ModifiedBy;
                    vLeaveDetails_Response.ModifiedDate = vResultObj.ModifiedDate;

                    var objLeaveReasonList = await _leaveRepository.GetLeaveReasonListById(vResultObj.Id);
                    foreach (var item in objLeaveReasonList)
                    {
                        vLeaveDetails_Response.reasonList.Add(item);
                    }
                }

                _response.Data = vLeaveDetails_Response;
            }
            return _response;
        }
    }
}
