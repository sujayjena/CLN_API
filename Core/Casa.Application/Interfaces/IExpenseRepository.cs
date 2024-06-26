﻿using CLN.Application.Models;
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

        #endregion

        #region Expense Details

        Task<int> SaveExpenseDetails(ExpenseDetails_Request parameters);

        Task<IEnumerable<ExpenseDetails_Response>> GetExpenseDetailsList(ExpenseDetails_Search parameters);

        Task<ExpenseDetails_Response?> GetExpenseDetailsById(int Id);

        Task<int> ExpenseDetailsApproveNReject(Expense_ApproveNReject parameters);

        Task<IEnumerable<ExpenseDetailsRemarks_Response>> GetExpenseDetailsRemarksListById(int ExpenseDetailsId);

        #endregion
    }
}
