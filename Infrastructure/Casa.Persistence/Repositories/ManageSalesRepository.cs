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
    public class ManageSalesRepository : GenericRepository, IManageSalesRepository
    {
        private IConfiguration _configuration;

        public ManageSalesRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        #region Manage Sales

        public async Task<IEnumerable<ManageSalesList_Response>> GetManageSalesList(BaseSearchEntity parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<ManageSalesList_Response>("GetManageSalesList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<ManageSalesDetailById_Response?> GetManageSalesById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);

            return (await ListByStoredProcedure<ManageSalesDetailById_Response>("GetManageSalesById", queryParameters)).FirstOrDefault();
        }

        #endregion

        #region Customer Accessory

        public async Task<int> SaveManageSalesAccessory(CustomerAccessory_Request parameters)
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

        public async Task<IEnumerable<CustomerAccessory_Request>> GetManageSalesAccessoryList(CustomerAccessory_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@CustomerId", parameters.CustomerId);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<CustomerAccessory_Request>("GetAccessoryList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<CustomerAccessory_Request?> GetManageSalesAccessoryById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);

            return (await ListByStoredProcedure<CustomerAccessory_Request>("GetAccessoryById", queryParameters)).FirstOrDefault();
        }

        #endregion

        #region Customer Battery

        public async Task<int> SaveCustomerBattery(CustomerBattery_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@CustomerId", parameters.CustomerId);
            queryParameters.Add("@SerialNumber", parameters.SerialNumber);
            queryParameters.Add("@Specification", parameters.Specification);
            queryParameters.Add("@WarrantyStartDate", parameters.WarrantyStartDate);
            queryParameters.Add("@WarrantyEndDate", parameters.WarrantyEndDate);
            queryParameters.Add("@ManufacturingDate", parameters.ManufacturingDate);
            queryParameters.Add("@SalesDate", parameters.SalesDate);
            queryParameters.Add("@ReceivedDate", parameters.ReceivedDate);
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
            DynamicParameters queryParameters =     new DynamicParameters();
            queryParameters.Add("@Id", Id);

            return (await ListByStoredProcedure<CustomerBattery_Response>("GetCustomerBatteryById", queryParameters)).FirstOrDefault();
        }

        #endregion
    }
}
