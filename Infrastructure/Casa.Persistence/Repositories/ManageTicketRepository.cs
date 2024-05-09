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
    public class ManageTicketRepository : GenericRepository, IManageTicketRepository
    {
        private IConfiguration _configuration;

        public ManageTicketRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> SaveManageTicket(ManageTicket_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            //queryParameters.Add("@Id", parameters.Id);
            //queryParameters.Add("@TicketId", parameters.TicketId);
            //queryParameters.Add("@TRCNumber", parameters.TRCNumber);
            //queryParameters.Add("@TRCDate", parameters.TRCDate);
            //queryParameters.Add("@TRCTime", parameters.TRCTime);

            //queryParameters.Add("@DA_DefectObserved", parameters.DA_DefectObserved);
            //queryParameters.Add("@DA_ActionTaken", parameters.DA_ActionTaken);
            //queryParameters.Add("@DA_Remarks", parameters.DA_Remarks);

            //queryParameters.Add("@PI_BatteryReceivedDate", parameters.PI_BatteryReceivedDate);
            //queryParameters.Add("@PI_BatteryReceivedTime", parameters.PI_BatteryReceivedTime);
            //queryParameters.Add("@PI_PDIDoneDate", parameters.PI_PDIDoneDate);
            //queryParameters.Add("@PI_PDIDoneTime", parameters.PI_PDIDoneTime);
            //queryParameters.Add("@PI_PDIDoneById", parameters.PI_PDIDoneById);
            //queryParameters.Add("@PI_Note", parameters.PI_Note);

            //queryParameters.Add("@TRCStatusId", parameters.TRCStatusId);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveTicket", queryParameters);
        }

        public async Task<int> SaveManageTicketPartDetail(ManageTicketPartDetails_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@TicketId", parameters.TicketId);
            queryParameters.Add("@SparePartNo", parameters.SparePartNo);
            queryParameters.Add("@PartDescription", parameters.PartDescription);
            queryParameters.Add("@Quantity", parameters.Quantity);
            queryParameters.Add("@PartStatusId", parameters.PartStatusId);

            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveTicketPartDetails", queryParameters);
        }

        public async Task<IEnumerable<ManageTicketList_Response>> GetManageTicketList(ManageTicket_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@EmployeeId", parameters.EmployeeId);
            queryParameters.Add("@TicketStatusId", parameters.TicketStatusId);
            queryParameters.Add("@FilterType", parameters.FilterType);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<ManageTicketList_Response>("GetTicketList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<ManageTicketDetail_Response?> GetManageTicketById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", Id);

            return (await ListByStoredProcedure<ManageTicketDetail_Response>("GetTicketById", queryParameters)).FirstOrDefault();
        }

        public async Task<IEnumerable<ManageTicketPartDetails_Response>> GetManageTicketPartDetailById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@TicketId", Id);

            var result = await ListByStoredProcedure<ManageTicketPartDetails_Response>("GetTicketPartDetailById", queryParameters);

            return result;
        }
    }
}
