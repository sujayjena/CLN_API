﻿using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface IExpenseTypeRepository
    {
        #region Expense Type
        Task<int> SaveExpenseType(ExpenseType_Request parameters);

        Task<IEnumerable<ExpenseType_Response>> GetExpenseTypeList(BaseSearchEntity parameters);

        Task<ExpenseType_Response?> GetExpenseTypeById(long Id);
        #endregion

        #region Expense Matrix
        Task<int> SaveExpenseMatrix(ExpenseMatrix_Request parameters);

        Task<IEnumerable<ExpenseMatrix_Response>> GetExpenseMatrixList(ExpenseMatrixSearch_Request parameters);

        Task<ExpenseMatrix_Response?> GetExpenseMatrixById(long Id);

        Task<IEnumerable<ExpenseMatrixDataValidationErrors>> ImportExpenseMatrix(List<ImportedExpenseMatrix> parameters);
        #endregion
    }
}
