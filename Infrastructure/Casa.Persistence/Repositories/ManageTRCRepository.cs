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
    public class ManageTRCRepository : GenericRepository, IManageTRCRepository
    {
        private IConfiguration _configuration;

        public ManageTRCRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> SaveManageTRC(ManageTRC_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@TicketId", parameters.TicketId);
            queryParameters.Add("@TRCNumber", parameters.TRCNumber);
            queryParameters.Add("@TRCDate", parameters.TRCDate);
            queryParameters.Add("@TRCTime", parameters.TRCTime);

            queryParameters.Add("@DA_DefectObserved", parameters.DA_DefectObserved);
            queryParameters.Add("@DA_ActionTaken", parameters.DA_ActionTaken);
            queryParameters.Add("@DA_Remarks", parameters.DA_Remarks);

            queryParameters.Add("@PI_BatteryReceivedDate", parameters.PI_BatteryReceivedDate);
            queryParameters.Add("@PI_BatteryReceivedTime", parameters.PI_BatteryReceivedTime);
            queryParameters.Add("@PI_PDIDoneDate", parameters.PI_PDIDoneDate);
            queryParameters.Add("@PI_PDIDoneTime", parameters.PI_PDIDoneTime);
            queryParameters.Add("@PI_PDIDoneById", parameters.PI_PDIDoneById);
            queryParameters.Add("@PI_Note", parameters.PI_Note);

            queryParameters.Add("@TRCStatusId", parameters.TRCStatusId);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveTRC", queryParameters);
        }

        public async Task<int> SaveManageTRCPartDetail(ManageTRCPartDetails_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@TRCId", parameters.TRCId);
            queryParameters.Add("@SparePartNo", parameters.SparePartNo);
            queryParameters.Add("@PartDescription", parameters.PartDescription);
            queryParameters.Add("@Quantity", parameters.Quantity);
            queryParameters.Add("@Remarks", parameters.Remarks);
            queryParameters.Add("@PartStatusId", parameters.PartStatusId);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveTRCPartDetails", queryParameters);
        }

        public async Task<IEnumerable<ManageTRCList_Response>> GetManageTRCList(ManageTRC_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<ManageTRCList_Response>("GetTRCList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<ManageTRCDetail_Response?> GetManageTRCById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", Id);

            return (await ListByStoredProcedure<ManageTRCDetail_Response>("GetTRCById", queryParameters)).FirstOrDefault();
        }

        public async Task<IEnumerable<ManageTRCPartDetails_Response>> GetManageTRCPartDetailById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@TRCId", Id);

            var result = await ListByStoredProcedure<ManageTRCPartDetails_Response>("GetTRCPartDetailById", queryParameters);

            return result;
        }
    }
}
