using CLN.Application.Models;
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
        Task<int> SaveExpenseType(ExpenseType_Request parameters);

        Task<IEnumerable<ExpenseType_Response>> GetExpenseTypeList(BaseSearchEntity parameters);

        Task<ExpenseType_Response?> GetExpenseTypeById(long Id);
    }
}
