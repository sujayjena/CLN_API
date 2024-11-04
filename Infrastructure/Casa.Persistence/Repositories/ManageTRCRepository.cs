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

            queryParameters.Add("@ATE_AssignedToEngineerId", parameters.ATE_AssignedToEngineerId);

            queryParameters.Add("@DA_ProblemObservedByEngId", parameters.DA_ProblemObservedByEngId);
            queryParameters.Add("@DA_ProblemObservedDesc", parameters.DA_ProblemObservedDesc);
            queryParameters.Add("@DA_RectificationActionId", parameters.DA_RectificationActionId);
            queryParameters.Add("@DA_ResolutionSummary", parameters.DA_ResolutionSummary);

            queryParameters.Add("@ATEFP_AssignedToEngineerId", parameters.ATEFP_AssignedToEngineerId);

            queryParameters.Add("@PI_BatteryReceivedDate", parameters.PI_BatteryReceivedDate);
            queryParameters.Add("@PI_BatteryReceivedTime", parameters.PI_BatteryReceivedTime);
            queryParameters.Add("@PI_PDIDoneDate", parameters.PI_PDIDoneDate);
            queryParameters.Add("@PI_PDIDoneTime", parameters.PI_PDIDoneTime);
            queryParameters.Add("@PI_PDIDoneById", parameters.PI_PDIDoneById);
            queryParameters.Add("@PI_SOCPercentageOriginalFileName", parameters.PI_SOCPercentageOriginalFileName);
            queryParameters.Add("@PI_SOCPercentageFileName", parameters.PI_SOCPercentageFileName);
            queryParameters.Add("@PI_VoltageDifference", parameters.PI_VoltageDifference);
            queryParameters.Add("@PI_FinalVoltageOriginalFileName", parameters.PI_FinalVoltageOriginalFileName);
            queryParameters.Add("@PI_FinalVoltageFileName", parameters.PI_FinalVoltageFileName);
          
            queryParameters.Add("@PIDD_DispatchedDeliveryChallan", parameters.PIDD_DispatchedDeliveryChallan);
            queryParameters.Add("@PIDD_DispatchedDate", parameters.PIDD_DispatchedDate);
            queryParameters.Add("@PIDD_DispatchedCity", parameters.PIDD_DispatchedCity);
          
            queryParameters.Add("@DDB_DispatchedDoneBy", parameters.DDB_DispatchedDoneBy);
            queryParameters.Add("@DDB_DocketDetails", parameters.DDB_DocketDetails);
            queryParameters.Add("@DDB_CourierName", parameters.DDB_CourierName);
           
            queryParameters.Add("@CRD_CustomerReceivingDate", parameters.CRD_CustomerReceivingDate);

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

            queryParameters.Add("@TRCStatusId", parameters.TRCStatusId);
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
