using CLN.Application.Helpers;
using CLN.Application.Interfaces;
using CLN.Application.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Persistence.Repositories
{
    public class LeaveRepository : GenericRepository, ILeaveRepository
    {

        private IConfiguration _configuration;

        public LeaveRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> SaveLeave(Leave_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@StartDate", parameters.StartDate);
            queryParameters.Add("@EndDate", parameters.EndDate);
            queryParameters.Add("@EmployeeId", parameters.EmployeeId);
            queryParameters.Add("@LeaveTypeId", parameters.LeaveTypeId);
            queryParameters.Add("@Remark", parameters.Remark);
            queryParameters.Add("@Reason", parameters.Reason);
            queryParameters.Add("@StatusId", parameters.StatusId);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveLeave", queryParameters);
        }

        public async Task<IEnumerable<Leave_Response>> GetLeaveList(LeaveSearch parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@CompanyId", parameters.CompanyId);
            queryParameters.Add("@BranchId", parameters.BranchId);
            queryParameters.Add("@EmployeeName", parameters.EmployeeName);
            queryParameters.Add("@LeaveType", parameters.LeaveType);
            queryParameters.Add("@LeaveReason", parameters.LeaveReason);
            queryParameters.Add("@LeaveStatusId", parameters.LeaveStatusId);
            queryParameters.Add("@EmployeeId", parameters.EmployeeId);
            queryParameters.Add("@FilterType", parameters.FilterType);

            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<Leave_Response>("GetLeaveList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<Leave_Response?> GetLeaveById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<Leave_Response>("GetLeaveById", queryParameters)).FirstOrDefault();
        }

        public async Task<IEnumerable<LeaveReason_Response>> GetLeaveReasonListById(int LeaveId)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@LeaveId", LeaveId);

            var result = await ListByStoredProcedure<LeaveReason_Response>("GetLeaveReasonListById", queryParameters);
            return result;
        }
    }
}
