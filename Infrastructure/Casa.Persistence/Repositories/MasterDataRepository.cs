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
    public class MasterDataRepository : GenericRepository, IMasterDataRepository
    {

        private IConfiguration _configuration;

        public MasterDataRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }
        public async Task<IEnumerable<SelectListResponse>> GetReportingToEmployeeForSelectList(ReportingToEmpListParameters parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@RoleId", parameters.RoleId);
            queryParameters.Add("@RegionId", parameters.RegionId.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);

            return await ListByStoredProcedure<SelectListResponse>("GetReportingToEmployeeForSelectList", queryParameters);
        }

        public async Task<IEnumerable<EmployeesListByReportingTo_Response>> GetEmployeesListByReportingTo(int EmployeeId)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@EmployeeId", EmployeeId);

            return await ListByStoredProcedure<EmployeesListByReportingTo_Response>("GetEmployeesListByReportingTo", queryParameters);
        }

        public async Task<IEnumerable<SelectListResponse>> GetTicketListForSelectList(TicketListForSelect_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@BranchId", parameters.BranchId);
            queryParameters.Add("@EmployeeId", parameters.EmployeeId);
            queryParameters.Add("@FilterType", parameters.FilterType);

            return await ListByStoredProcedure<SelectListResponse>("GetTicketListForSelectList", queryParameters);
        }

        public async Task<IEnumerable<SelectListResponse>> GetPartRequestEnggListForSelectList(PartRequestEnggListForSelect_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@RequestType", parameters.RequestType);

            return await ListByStoredProcedure<SelectListResponse>("GetPartRequestEnggListForSelectList", queryParameters);
        }
    }
}
