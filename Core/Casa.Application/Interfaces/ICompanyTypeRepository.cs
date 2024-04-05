using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface ICompanyTypeRepository
    {
        Task<int> SaveCompanyType(CompanyType_Request parameters);

        Task<IEnumerable<CompanyType_Response>> GetCompanyTypeList(BaseSearchEntity parameters);

        Task<CompanyType_Response?> GetCompanyTypeById(long Id);
    }
}
