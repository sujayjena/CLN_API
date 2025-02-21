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

        Task<IEnumerable<CustomerBOM_ImportDataValidation>> ImportBOM(List<CustomerBOM_ImportData> parameters);

        #endregion

        #region Customer Accessory

        Task<int> SaveManageQCAccessory(CustomerAccessory_Request parameters);

        Task<IEnumerable<CustomerAccessory_Response>> GetManageQCAccessoryList(CustomerAccessory_Search parameters);

        Task<CustomerAccessory_Response?> GetManageQCAccessoryById(int Id);

        Task<int> DeleteManageQCAccessory(int Id);

        Task<IEnumerable<CustomerAccessory_ImportDataValidation>> ImportAccessory(List<CustomerAccessory_ImportData> parameters);

        #endregion

        #region Customer Battery

        Task<int> SaveCustomerBattery(CustomerBattery_Request parameters);

        Task<IEnumerable<CustomerBattery_Response>> GetCustomerBatteryList(CustomerBattery_Search parameters);

        Task<CustomerBattery_Response?> GetCustomerBatteryById(int Id);

        Task<IEnumerable<CustomerBattery_ImportDataValidation>> ImportBattery(List<CustomerBattery_ImportData> parameters);

        Task<int> AssignBatteryToCustomer(AssignBattery_Request parameters);

        Task<int> AssignBatteryHoldOrInactive(AssignBatteryHoldOrInactive_Request parameters);

        Task<IEnumerable<ValidateProductSerialNumber_Response>> ValidateProductSerialNumber(ValidateProductSerialNumber_Request parameters);

        #endregion

        #region Customer Charger

        Task<int> SaveCustomerCharger(CustomerCharger_Request parameters);

        Task<IEnumerable<CustomerCharger_Response>> GetCustomerChargerList(CustomerCharger_Search parameters);

        Task<CustomerCharger_Response?> GetCustomerChargerById(int Id);

        #endregion
    }
}
