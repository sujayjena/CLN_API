using CLN.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface IExpenseRepository 
    {
        #region Expense

        Task<int> SaveExpense(Expense_Request parameters);

        Task<IEnumerable<Expense_Response>> GetExpenseList(Expense_Search parameters);

        Task<Expense_Response?> GetExpenseById(int Id);

        Task<int> UpdateIsExport(UpdateIsExport_Request parameters);

        Task<int> UpdateDownloadedExpense(UpdateDownloadedExpense_Request parameters);

        Task<IEnumerable<ExpenseForPDF_Response>> GetExpenseForPDF(UpdateDownloadedExpense_Request parameters);

        #endregion

        #region Expense Details

        Task<int> SaveExpenseDetails(ExpenseDetails_Request parameters);

        Task<IEnumerable<ExpenseDetails_Response>> GetExpenseDetailsList(ExpenseDetails_Search parameters);

        Task<ExpenseDetails_Response?> GetExpenseDetailsById(int Id);

        Task<int> ExpenseDetailsApproveNReject(Expense_ApproveNReject parameters);

        Task<IEnumerable<ExpenseDetailsRemarks_Response>> GetExpenseDetailsRemarksListById(int ExpenseDetailsId);

        #endregion

        #region Daily Travel Expense

        Task<int> SaveDailyTravelExpense(DailyTravelExpense_Request parameters);

        Task<IEnumerable<DailyTravelExpense_Response>> GetDailyTravelExpenseList(DailyTravelExpense_Search parameters);

        Task<DailyTravelExpense_Response?> GetDailyTravelExpenseById(int Id);

        Task<int> DailyTravelExpenseApproveNReject(DailyTravelExpense_ApproveNReject parameters);

        Task<IEnumerable<ExpenseDetailsRemarks_Response>> GetDailyTravelExpenseRemarksListById(int DailyTravelExpenseId);

        #endregion
    }
}
