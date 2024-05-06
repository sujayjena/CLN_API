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
            queryParameters.Add("@ExpenseNumber", parameters.ExpenseNumber);
            queryParameters.Add("@TicketId", parameters.TicketId);
            queryParameters.Add("@StatusId", parameters.StatusId);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveExpense", queryParameters);
        }

        public async Task<IEnumerable<Expense_Response>> GetExpenseList(Expense_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@EmployeeId", parameters.EmployeeId);
            queryParameters.Add("@FilterType", parameters.FilterType);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
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

        #endregion

        #region Expense Details

        public async Task<int> SaveExpenseDetails(ExpenseDetails_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@ExpenseId", parameters.ExpenseId);
            queryParameters.Add("@ExpenseDate", parameters.ExpenseDate);
            queryParameters.Add("@ExpenseTypeId", parameters.ExpenseTypeId);
            queryParameters.Add("@ExpenseDescription", parameters.ExpenseDescription);
            queryParameters.Add("@ExpenseAmount", parameters.ExpenseAmount);
            queryParameters.Add("@ExpenseImageFileName", parameters.ExpenseImageFileName);
            queryParameters.Add("@ExpenseImageOriginalFileName", parameters.ExpenseImageOriginalFileName);
            queryParameters.Add("@ExpenseDetailStatusId", parameters.ExpenseDetailStatusId);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveExpenseDetails", queryParameters);
        }

        public async Task<IEnumerable<ExpenseDetails_Response>> GetExpenseDetailsList(ExpenseDetails_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@ExpenseId", parameters.ExpenseId);
            queryParameters.Add("@ExpenseDetailStatusId", parameters.ExpenseDetailStatusId);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
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

        #endregion
    }
}
