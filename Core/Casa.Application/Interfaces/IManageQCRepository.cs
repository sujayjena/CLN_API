using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface IManageQCRepository
    {
        #region Manage QC

        Task<IEnumerable<ManageQCList_Response>> GetManageQCList(BaseSearchEntity parameters);

        Task<ManageQCList_Response?> GetManageQCById(int Id);

        #endregion

        #region Customer BOM

        Task<int> SaveCustomerBOM(CustomerBOM_Request parameters);

        Task<IEnumerable<CustomerBOM_Response>> GetCustomerBOMList(CustomerBOM_Search parameters);

        Task<CustomerBOM_Response?> GetCustomerBOMById(int Id);

        #endregion

        #region Customer Accessory

        Task<int> SaveManageQCAccessory(CustomerAccessory_Request parameters);

        Task<IEnumerable<CustomerAccessory_Response>> GetManageQCAccessoryList(CustomerAccessory_Search parameters);

        Task<CustomerAccessory_Response?> GetManageQCAccessoryById(int Id);

        Task<int> DeleteManageQCAccessory(int Id);

        #endregion

        #region Customer Battery

        Task<int> SaveCustomerBattery(CustomerBattery_Request parameters);

        Task<IEnumerable<CustomerBattery_Response>> GetCustomerBatteryList(CustomerBattery_Search parameters);

        Task<CustomerBattery_Response?> GetCustomerBatteryById(int Id);

        #endregion
    }
}
