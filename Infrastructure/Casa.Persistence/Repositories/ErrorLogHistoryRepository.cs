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
    public class ErrorLogHistoryRepository : GenericRepository, IErrorLogHistoryRepository
    {

        private IConfiguration _configuration;

        public ErrorLogHistoryRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> SaveErrorLogHistory(ErrorLogHistory_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@ModuleName", parameters.ModuleName);
            queryParameters.Add("@JsonData", parameters.JsonData);
            queryParameters.Add("@ErrorMessage", parameters.ErrorMessage);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveErrorLogHistory", queryParameters);
        }
    }
}
