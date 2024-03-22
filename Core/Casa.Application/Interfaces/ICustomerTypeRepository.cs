using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface ICustomerTypeRepository
    {
        Task<int> SaveCustomerType(CustomerType_Request parameters);

        Task<IEnumerable<CustomerType_Response>> GetCustomerTypeList(BaseSearchEntity parameters);

        Task<CustomerType_Response?> GetCustomerTypeById(long Id);
    }
}
