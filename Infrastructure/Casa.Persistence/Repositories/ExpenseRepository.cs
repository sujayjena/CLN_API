using CLN.Application.Helpers;
using CLN.Application.Interfaces;
using CLN.Application.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Persistence.Repositories
{
    public class ExpenseRepository : GenericRepository, IExpenseRepository
    {
        private IConfiguration _configuration;

        public ExpenseRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        #region Expense

        public async Task<int> SaveExpense(Expense_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@IsMySelf", parameters.IsMySelf);
            queryParameters.Add("@ExpenseNumber", parameters.ExpenseNumber);
            queryParameters.Add("@EmployeeId", parameters.EmployeeId);
            queryParameters.Add("@TicketId", parameters.TicketId);
            queryParameters.Add("@StatusId", parameters.StatusId);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveExpense", queryParameters);
        }

        public async Task<IEnumerable<Expense_Response>> GetExpenseList(Expense_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@FromDate", parameters.FromDate);
            queryParameters.Add("@ToDate", parameters.ToDate);
            queryParameters.Add("@EmployeeId", parameters.EmployeeId);
            queryParameters.Add("@StatusId", parameters.StatusId);
            queryParameters.Add("@ExpenseId", parameters.ExpenseId);
            queryParameters.Add("@IsDownloded", parameters.IsDownloded);
            queryParameters.Add("@FilterType", parameters.FilterType);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@SortBy", parameters.SortBy);
            queryParameters.Add("@OrderBy", parameters.OrderBy);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<Expense_Response>("GetExpenseList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<Expense_Response?> GetExpenseById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", Id);

            return (await ListByStoredProcedure<Expense_Response>("GetExpenseById", queryParameters)).FirstOrDefault();
        }

        public async Task<int> UpdateIsExport(UpdateIsExport_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@Module", parameters.Module);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("UpdateIsExport", queryParameters);
        }

        public async Task<int> UpdateDownloadedExpense(UpdateDownloadedExpense_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@ExpenseId", parameters.ExpenseId);
            queryParameters.Add("@ExpenseType", parameters.ExpenseType);

            return await SaveByStoredProcedure<int>("UpdateDownloadedExpense", queryParameters);
        }

        public async Task<IEnumerable<ExpenseForPDF_Response>> GetExpenseForPDF(ExpenseForPDF_Search_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@ExpenseId", parameters.ExpenseId);

            var result = await ListByStoredProcedure<ExpenseForPDF_Response>("GetExpenseForPDF", queryParameters);

            return result;
        }

        #endregion

        #region Expense Details

        public async Task<int> SaveExpenseDetails(ExpenseDetails_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@ExpenseId", parameters.ExpenseId);
            queryParameters.Add("@IsSingleDayExpense", parameters.IsSingleDayExpense);
            queryParameters.Add("@FromDate", parameters.FromDate);
            queryParameters.Add("@ToDate", parameters.ToDate);
            queryParameters.Add("@ExpenseTypeId", parameters.ExpenseTypeId);
            queryParameters.Add("@VehicleTypeId", parameters.VehicleTypeId);
            queryParameters.Add("@ExpenseDescription", parameters.ExpenseDescription);
            queryParameters.Add("@ApprovedAmount", parameters.ApprovedAmount);
            queryParameters.Add("@ExpenseAmount", parameters.ExpenseAmount);
            queryParameters.Add("@ExpenseImageFileName", parameters.ExpenseImageFileName);
            queryParameters.Add("@ExpenseImageOriginalFileName", parameters.ExpenseImageOriginalFileName);
            queryParameters.Add("@ExpenseDetailStatusId", parameters.ExpenseDetailStatusId);
            queryParameters.Add("@CityGradeId", parameters.CityGradeId);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveExpenseDetails", queryParameters);
        }

        public async Task<IEnumerable<ExpenseDetails_Response>> GetExpenseDetailsList(ExpenseDetails_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@ExpenseId", parameters.ExpenseId);
            queryParameters.Add("@ExpenseDetailStatusId", parameters.ExpenseDetailStatusId);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@SortBy", parameters.SortBy);
            queryParameters.Add("@OrderBy", parameters.OrderBy);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<ExpenseDetails_Response>("GetExpenseDetailsList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<ExpenseDetails_Response?> GetExpenseDetailsById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", Id);

            return (await ListByStoredProcedure<ExpenseDetails_Response>("GetExpenseDetailsById", queryParameters)).FirstOrDefault();
        }

        public async Task<int> ExpenseDetailsApproveNReject(Expense_ApproveNReject parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@ExpenseId", parameters.ExpenseId);
            queryParameters.Add("@ExpenseDetailStatusId", parameters.ExpenseDetailStatusId);
            queryParameters.Add("@Remarks", parameters.Remarks);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("ExpenseDetailsApproveNReject", queryParameters);
        }

        public async Task<IEnumerable<ExpenseDetailsRemarks_Response>> GetExpenseDetailsRemarksListById(int LeaveId)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@ExpenseDetailsId", LeaveId);

            var result = await ListByStoredProcedure<ExpenseDetailsRemarks_Response>("GetExpenseDetailsRemarksListById", queryParameters);
            return result;
        }

        #endregion

        #region Daily Travel Expense

        public async Task<int> SaveDailyTravelExpense(DailyTravelExpense_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@IsMySelf", parameters.IsMySelf);
            queryParameters.Add("@ExpenseNumber", parameters.ExpenseNumber);
            queryParameters.Add("@IsTicetExpense", parameters.IsTicetExpense);
            queryParameters.Add("@EmployeeId", parameters.EmployeeId);
            queryParameters.Add("@TicketId", parameters.TicketId);
            queryParameters.Add("@ExpenseDate", parameters.ExpenseDate);
            queryParameters.Add("@ExpenseTypeId", parameters.ExpenseTypeId);
            queryParameters.Add("@ExpenseDesc", parameters.ExpenseDesc);
            queryParameters.Add("@VehicleTypeId", parameters.VehicleTypeId);
            queryParameters.Add("@RatePerKm", parameters.RatePerKm);
            queryParameters.Add("@TotalKm", parameters.TotalKm);
            queryParameters.Add("@TotalAmount", parameters.TotalAmount);
            queryParameters.Add("@Remarks", parameters.Remarks);
            queryParameters.Add("@StatusId", parameters.StatusId);
            queryParameters.Add("@ExpenseImageFileName", parameters.ExpenseImageFileName);
            queryParameters.Add("@ExpenseImageOriginalFileName", parameters.ExpenseImageOriginalFileName);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveDailyTravelExpense", queryParameters);
        }

        public async Task<IEnumerable<DailyTravelExpense_Response>> GetDailyTravelExpenseList(DailyTravelExpense_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@FromDate", parameters.FromDate);
            queryParameters.Add("@ToDate", parameters.ToDate);
            queryParameters.Add("@EmployeeId", parameters.EmployeeId);
            queryParameters.Add("@StatusId", parameters.StatusId);
            queryParameters.Add("@ExpenseId", parameters.ExpenseId);
            queryParameters.Add("@IsDownloded", parameters.IsDownloded);
            queryParameters.Add("@FilterType", parameters.FilterType);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@SortBy", parameters.SortBy);
            queryParameters.Add("@OrderBy", parameters.OrderBy);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<DailyTravelExpense_Response>("GetDailyTravelExpenseList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<DailyTravelExpense_Response?> GetDailyTravelExpenseById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", Id);

            return (await ListByStoredProcedure<DailyTravelExpense_Response>("GetDailyTravelExpenseById", queryParameters)).FirstOrDefault();
        }

        public async Task<int> DailyTravelExpenseApproveNReject(DailyTravelExpense_ApproveNReject parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@StatusId", parameters.StatusId);
            queryParameters.Add("@Remarks", parameters.Remarks);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("DailyTravelExpenseApproveNReject", queryParameters);
        }

        public async Task<IEnumerable<ExpenseDetailsRemarks_Response>> GetDailyTravelExpenseRemarksListById(int DailyTravelExpenseId)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@DailyTravelExpenseId", DailyTravelExpenseId);

            var result = await ListByStoredProcedure<ExpenseDetailsRemarks_Response>("GetDailyTravelExpenseRemarksListById", queryParameters);
            return result;
        }

        public async Task<IEnumerable<DailyTravelExpenseForPDF_Response>> GetDailyTravelExpenseForPDF(ExpenseForPDF_Search_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@ExpenseId", parameters.ExpenseId);

            var result = await ListByStoredProcedure<DailyTravelExpenseForPDF_Response>("GetDailyTravelExpenseForPDF", queryParameters);

            return result;
        }

        #endregion
    }
}
