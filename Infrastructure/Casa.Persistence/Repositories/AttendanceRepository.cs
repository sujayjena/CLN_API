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
    public class AttendanceRepository : GenericRepository, IAttendanceRepository
    {

        private IConfiguration _configuration;

        public AttendanceRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> SaveAttendance(Attendance_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@PunchType", parameters.PunchType);
            queryParameters.Add("@Latitude", parameters.Latitude);
            queryParameters.Add("@Longitude", parameters.Longitude);
            queryParameters.Add("@BatteryStatus", parameters.BatteryStatus);
            queryParameters.Add("@Address", parameters.Address);
            queryParameters.Add("@Remark", parameters.Remark);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveAttendance", queryParameters);
        }

        public async Task<IEnumerable<Attendance_Response>> GetAttendanceList(AttendanceSearch parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@CompanyId", parameters.CompanyId);
            queryParameters.Add("@BranchId", parameters.BranchId);
            queryParameters.Add("@FromPunchInDate", parameters.FromPunchInDate);
            queryParameters.Add("@ToPunchInDate", parameters.ToPunchInDate);
            queryParameters.Add("@EmployeeName", parameters.EmployeeName);
            queryParameters.Add("@EmployeeId", parameters.EmployeeId);
            queryParameters.Add("@FilterType", parameters.FilterType);

            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@SortBy", parameters.SortBy);
            queryParameters.Add("@OrderBy", parameters.OrderBy);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<Attendance_Response>("GetAttendanceList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<IEnumerable<Attendance_Response>> GetAttendanceById(int UserId)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@UserId", UserId);
            return (await ListByStoredProcedure<Attendance_Response>("GetAttendanceById", queryParameters));
        }
    }
}
