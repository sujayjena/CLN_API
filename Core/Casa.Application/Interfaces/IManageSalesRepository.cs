using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface IManageSalesRepository
    {
        Task<int> SaveCustomerAccessory(ManageSales_Accessory_Request parameters);

        Task<IEnumerable<ManageSales_Response>> GetCustomerAccessoryList(BaseSearchEntity parameters);

        Task<ManageSales_Response?> GetCustomerAccessoryById(int Id);
    }
}
