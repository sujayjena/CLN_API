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
    public class SLARepository : GenericRepository, ISLARepository
    {

        private IConfiguration _configuration;

        public SLARepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        #region Priority
        public async Task<int> SaveSLAPriority(SLAPriority_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@SLAPriority", parameters.SLAPriority);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveSLAPriority", queryParameters);
        }

        public async Task<IEnumerable<SLAPriority_Response>> GetSLAPriorityList(BaseSearchEntity parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<SLAPriority_Response>("GetSLAPriorityList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<SLAPriority_Response?> GetSLAPriorityById(long Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<SLAPriority_Response>("GetSLAPriorityById", queryParameters)).FirstOrDefault();
        }
        #endregion

        #region Sla Matrix

        public async Task<int> SaveSlaMatrix(SlaMatrix_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@TicketStatusFromId", parameters.TicketStatusFromId);
            queryParameters.Add("@TicketStatusToId", parameters.TicketStatusToId);
            queryParameters.Add("@SeqNo", parameters.SeqNo);
            queryParameters.Add("@TicketPriorityId", parameters.TicketPriorityId);
            queryParameters.Add("@Days", parameters.Days);
            queryParameters.Add("@Hrs", parameters.Hrs);
            queryParameters.Add("@Min", parameters.Min);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveSlaMatrix", queryParameters);
        }

        public async Task<IEnumerable<SlaMatrix_Response>> GetSlaMatrixList(BaseSearchEntity parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<SlaMatrix_Response>("GetSlaMatrixList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<SlaMatrix_Response?> GetSlaMatrixById(long Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<SlaMatrix_Response>("GetSlaMatrixById", queryParameters)).FirstOrDefault();
        }

        #endregion
    }
}
