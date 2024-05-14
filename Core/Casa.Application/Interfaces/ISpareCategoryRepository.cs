using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface ISpareCategoryRepository
    {
        Task<int> SaveSpareCategory(SpareCategory_Request parameters);

        Task<IEnumerable<SpareCategory_Response>> GetSpareCategoryList(BaseSearchEntity parameters);

        Task<SpareCategory_Response?> GetSpareCategoryById(int Id);
    }
}
