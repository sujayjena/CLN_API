using CLN.Application.Helpers;
using CLN.Application.Interfaces;
using CLN.Application.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Persistence.Repositories
{
    public class ManageQCRepository : GenericRepository, IManageQCRepository
    {
        private IConfiguration _configuration;

        public ManageQCRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        #region Manage QC

        public async Task<IEnumerable<ManageQCList_Response>> GetManageQCList(BaseSearchEntity parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<ManageQCList_Response>("GetManageQCList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<ManageQCList_Response?> GetManageQCById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);

            return (await ListByStoredProcedure<ManageQCList_Response>("GetManageQCById", queryParameters)).FirstOrDefault();
        }

        #endregion


        #region Customer BOM

        public async Task<int> SaveCustomerBOM(CustomerBOM_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@CustomerId", parameters.CustomerId);
            queryParameters.Add("@PartCode", parameters.PartCode);
            queryParameters.Add("@CustomerCode", parameters.CustomerCode);
            queryParameters.Add("@ProductCategoryId", parameters.ProductCategoryId);
            queryParameters.Add("@SegmentId", parameters.SegmentId);
            queryParameters.Add("@SubSegmentId", parameters.SubSegmentId);
            queryParameters.Add("@ProductModelId", parameters.ProductModelId);
            queryParameters.Add("@DrawingNumber", parameters.DrawingNumber);
            queryParameters.Add("@Warranty", parameters.Warranty);
            queryParameters.Add("@PartImage", parameters.PartImage);
            queryParameters.Add("@PartImageOriginalFileName", parameters.PartImageOriginalFileName);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveCustomerBOM", queryParameters);
        }

        public async Task<IEnumerable<CustomerBOM_Response>> GetCustomerBOMList(CustomerBOM_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@CustomerId", parameters.CustomerId);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<CustomerBOM_Response>("GetCustomerBOMList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<CustomerBOM_Response?> GetCustomerBOMById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);

            return (await ListByStoredProcedure<CustomerBOM_Response>("GetCustomerBOMById", queryParameters)).FirstOrDefault();
        }

        #endregion

        #region Customer Accessory

        public async Task<int> SaveManageQCAccessory(CustomerAccessory_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@CustomerId", parameters.CustomerId);
            queryParameters.Add("@AccessoryName", parameters.AccessoryName);
            queryParameters.Add("@Quantity", parameters.Quantity);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveCustomerAccessory", queryParameters);
        }

        public async Task<IEnumerable<CustomerAccessory_Response>> GetManageQCAccessoryList(CustomerAccessory_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@CustomerId", parameters.CustomerId);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<CustomerAccessory_Response>("GetAccessoryList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<CustomerAccessory_Response?> GetManageQCAccessoryById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);

            return (await ListByStoredProcedure<CustomerAccessory_Response>("GetAccessoryById", queryParameters)).FirstOrDefault();
        }

        public async Task<int> DeleteManageQCAccessory(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", Id);

            return await SaveByStoredProcedure<int>("DeleteCustomerAccessory", queryParameters);
        }

        #endregion

        #region Customer Battery

        public async Task<int> SaveCustomerBattery(CustomerBattery_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@CustomerId", parameters.CustomerId);
            queryParameters.Add("@PartCodeId", parameters.PartCodeId);
            queryParameters.Add("@SerialNumber", parameters.SerialNumber);
            queryParameters.Add("@ProductSerialNumber", parameters.ProductSerialNumber);
            queryParameters.Add("@InvoiceNumber", parameters.InvoiceNumber);
            queryParameters.Add("@ManufacturingDate", parameters.ManufacturingDate);
            queryParameters.Add("@WarrantyStartDate", parameters.WarrantyStartDate);
            queryParameters.Add("@WarrantyEndDate", parameters.WarrantyEndDate);
            queryParameters.Add("@WarrantyStatusId", parameters.WarrantyStatusId);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveCustomerBattery", queryParameters);
        }

        public async Task<IEnumerable<CustomerBattery_Response>> GetCustomerBatteryList(CustomerBattery_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@CustomerId", parameters.CustomerId);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<CustomerBattery_Response>("GetCustomerBatteryList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<CustomerBattery_Response?> GetCustomerBatteryById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);

            return (await ListByStoredProcedure<CustomerBattery_Response>("GetCustomerBatteryById", queryParameters)).FirstOrDefault();
        }

        public async Task<IEnumerable<CustomerBattery_ImportDataValidation>> ImportBattery(List<CustomerBattery_ImportData> parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            string xmlData = ConvertListToXml(parameters);
            queryParameters.Add("@XmlData", xmlData);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);
            return await ListByStoredProcedure<CustomerBattery_ImportDataValidation>("ImportCustomerBattery", queryParameters);
        }

        #endregion

        #region Customer Charger

        public async Task<int> SaveCustomerCharger(CustomerCharger_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@CustomerId", parameters.CustomerId);
            queryParameters.Add("@PartCodeId", parameters.PartCodeId);
            queryParameters.Add("@ChargerSerial", parameters.ChargerSerial);
            queryParameters.Add("@ChargerModel", parameters.ChargerModel);
            queryParameters.Add("@WarrantyPeriod", parameters.WarrantyPeriod);
            queryParameters.Add("@ChargerName", parameters.ChargerName);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveCustomerCharger", queryParameters);
        }

        public async Task<IEnumerable<CustomerCharger_Response>> GetCustomerChargerList(CustomerCharger_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@CustomerId", parameters.CustomerId);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<CustomerCharger_Response>("GetCustomerChargerList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<CustomerCharger_Response?> GetCustomerChargerById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);

            return (await ListByStoredProcedure<CustomerCharger_Response>("GetCustomerChargerById", queryParameters)).FirstOrDefault();
        }
      
        #endregion
    }
}
