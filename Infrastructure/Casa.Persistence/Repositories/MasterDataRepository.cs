﻿using CLN.Application.Helpers;
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

            return await ListByStoredProcedure<SelectListResponse>("GetReportingToEmployeeForSelectList", queryParameters);
        }
    }
}
