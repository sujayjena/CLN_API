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
        Task<int> SaveManageSalesAccessory(ManageSales_Accessory_Request parameters);

        Task<IEnumerable<ManageSalesList_Response>> GetManageSalesList(BaseSearchEntity parameters);

        Task<ManageSalesDetail_Response?> GetManageSalesById(int Id);

        Task<IEnumerable<ManageSales_Accessory_Response>> GetManageSalesAccessoryList(BaseSearchEntity parameters);

        Task<ManageSales_Accessory_Response?> GetManageSalesAccessoryById(int Id);
    }
}
