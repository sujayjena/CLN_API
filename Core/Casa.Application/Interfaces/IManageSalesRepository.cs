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
        Task<IEnumerable<ManageSalesList_Response>> GetManageSalesList(BaseSearchEntity parameters);

        Task<ManageSalesDetailById_Response?> GetManageSalesById(int Id);

        Task<int> SaveManageSalesAccessory(CustomerAccessory_Request parameters);

        Task<IEnumerable<CustomerAccessory_Request>> GetManageSalesAccessoryList(CustomerAccessory_Search parameters);

        Task<CustomerAccessory_Request?> GetManageSalesAccessoryById(int Id);

        Task<int> SaveCustomerBattery(CustomerBattery_Request parameters);

        Task<IEnumerable<CustomerBattery_Response>> GetCustomerBatteryList(CustomerBattery_Search parameters);

        Task<CustomerBattery_Response?> GetCustomerBatteryById(int Id);
    }
}
