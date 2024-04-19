﻿using CLN.Application.Helpers;
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
    public class CustomerRepository : GenericRepository, ICustomerRepository
    {
        private IConfiguration _configuration;

        public CustomerRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        #region Customer

        public async Task<int> SaveCustomer(Customer_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@CompanyTypeId", parameters.CompanyTypeId);
            queryParameters.Add("@CompanyName", parameters.CompanyName);
            queryParameters.Add("@LandLineNumber", parameters.LandLineNumber);
            queryParameters.Add("@MobileNumber", parameters.MobileNumber);
            queryParameters.Add("@EmailId", parameters.EmailId);
            queryParameters.Add("@Website", parameters.Website);
            queryParameters.Add("@Remark", parameters.Remark);
            queryParameters.Add("@RefParty", parameters.RefParty);
            queryParameters.Add("@GSTImage", parameters.GSTImageFileName);
            queryParameters.Add("@GSTImageOriginalFileName", parameters.GSTImageOriginalFileName);
            queryParameters.Add("@PanCardImage", parameters.PanCardImageFileName);
            queryParameters.Add("@PanCardOriginalFileName", parameters.PanCardOriginalFileName);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveCustomer", queryParameters);
        }

        public async Task<IEnumerable<Customer_Response>> GetCustomerList(BaseSearchEntity parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<Customer_Response>("GetCustomerList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<Customer_Response?> GetCustomerById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", Id);

            return (await ListByStoredProcedure<Customer_Response>("GetCustomerById", queryParameters)).FirstOrDefault();
        }

        #endregion

        #region Customer Contact Detail

        public async Task<int> SaveCustomerContactDetail(ContactDetail_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@RefId", parameters.RefId);
            queryParameters.Add("@RefType", "Customer");
            queryParameters.Add("@ContactName", parameters.ContactName);
            queryParameters.Add("@MobileNumber", parameters.MobileNumber);
            queryParameters.Add("@EmailId", parameters.EmailId);
            queryParameters.Add("@AadharCardImage", parameters.AadharCardImageFileName);
            queryParameters.Add("@AadharCardOriginalFileName", parameters.AadharCardOriginalFileName);
            queryParameters.Add("@PanCardImage", parameters.PanCardImageFileName);
            queryParameters.Add("@PanCardOriginalFileName", parameters.PanCardOriginalFileName);
            queryParameters.Add("@IsDefault", parameters.IsDefault);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveContactDetail", queryParameters);
        }

        public async Task<IEnumerable<ContactDetail_Response>> GetCustomerContactDetailList(CustomerContactDetail_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@RefId", parameters.CustomerId);
            queryParameters.Add("@RefType", "Customer");
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<ContactDetail_Response>("GetContactDetailList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<ContactDetail_Response?> GetCustomerContactDetailById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", Id);
            queryParameters.Add("@RefType", "Customer");

            return (await ListByStoredProcedure<ContactDetail_Response>("GetContactDetailById", queryParameters)).FirstOrDefault();
        }

        #endregion

        #region Customer Address

        public async Task<int> SaveCustomerAddress(Address_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@RefId", parameters.RefId);
            queryParameters.Add("@RefType", "Customer");
            queryParameters.Add("@Address1", parameters.Address1);
            queryParameters.Add("@Address2", parameters.Address2);
            queryParameters.Add("@RegionId", parameters.RegionId);
            queryParameters.Add("@StateId", parameters.StateId);
            queryParameters.Add("@DistrictId", parameters.DistrictId);
            queryParameters.Add("@CityId", parameters.CityId);
            queryParameters.Add("@PinCode", parameters.PinCode);
            queryParameters.Add("@IsDefault", parameters.IsDefault);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveAddress", queryParameters);
        }

        public async Task<IEnumerable<Address_Response>> GetCustomerAddressList(CustomerAddress_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@RefId", parameters.CustomerId);
            queryParameters.Add("@RefType", "Customer");
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@RefId", parameters.CustomerId);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<Address_Response>("GetAddressList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<Address_Response?> GetCustomerAddressById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", Id);
            queryParameters.Add("@RefType", "Customer");

            return (await ListByStoredProcedure<Address_Response>("GetAddressById", queryParameters)).FirstOrDefault();
        }

        #endregion
    }
}