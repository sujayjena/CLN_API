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
    public class ProtectionsRepository : GenericRepository, IProtectionsRepository
    {

        private IConfiguration _configuration;

        public ProtectionsRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> SaveProtections(Protections_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@Protections", parameters.Protections);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveProtections", queryParameters);
        }

        public async Task<IEnumerable<Protections_Response>> GetProtectionsList(BaseSearchEntity parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<Protections_Response>("GetProtectionsList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<Protections_Response?> GetProtectionsById(long Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<Protections_Response>("GetProtectionsById", queryParameters)).FirstOrDefault();
        }
    }
}
