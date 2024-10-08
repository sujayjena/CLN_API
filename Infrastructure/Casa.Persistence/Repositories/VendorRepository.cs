﻿using CLN.Application.Helpers;
using CLN.Application.Interfaces;
using CLN.Application.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Persistence.Repositories
{
    public class VendorRepository : GenericRepository, IVendorRepository
    {
        private IConfiguration _configuration;

        public VendorRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        #region Vendor

        public async Task<int> SaveVendor(Vendor_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@VendorTypeId", parameters.VendorTypeId);
            queryParameters.Add("@VendorName", parameters.VendorName);
            queryParameters.Add("@LandLineNumber", parameters.LandLineNumber);
            queryParameters.Add("@MobileNumber", parameters.MobileNumber);
            queryParameters.Add("@EmailId", parameters.EmailId);
            queryParameters.Add("@SpecialRemark", parameters.SpecialRemark);
            queryParameters.Add("@PanCardNo", parameters.PanCardNo);
            queryParameters.Add("@PanCardImage", parameters.PanCardImageFileName);
            queryParameters.Add("@PanCardOriginalFileName", parameters.PanCardOriginalFileName);
            queryParameters.Add("@GSTNo", parameters.GSTNo);
            queryParameters.Add("@GSTImage", parameters.GSTImageFileName);
            queryParameters.Add("@GSTImageOriginalFileName", parameters.GSTImageOriginalFileName);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveVendor", queryParameters);
        }

        public async Task<IEnumerable<VendorList_Response>> GetVendorList(BaseSearchEntity parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<VendorList_Response>("GetVendorList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<VendorList_Response?> GetVendorById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", Id);

            return (await ListByStoredProcedure<VendorList_Response>("GetVendorById", queryParameters)).FirstOrDefault();
        }

        public async Task<IEnumerable<Vendor_ImportDataValidation>> ImportVendor(List<Vendor_ImportData> parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            string xmlData = ConvertListToXml(parameters);
            queryParameters.Add("@XmlData", xmlData);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await ListByStoredProcedure<Vendor_ImportDataValidation>("ImportVendor", queryParameters);
        }

        public async Task<IEnumerable<Contact_ImportDataValidation>> ImportVendorContact(List<Contact_ImportData> parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            string xmlData = ConvertListToXml(parameters);
            queryParameters.Add("@XmlData", xmlData);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await ListByStoredProcedure<Contact_ImportDataValidation>("ImportContact", queryParameters);
        }

        public async Task<IEnumerable<Address_ImportDataValidation>> ImportVendorAddress(List<Address_ImportData> parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            string xmlData = ConvertListToXml(parameters);
            queryParameters.Add("@XmlData", xmlData);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await ListByStoredProcedure<Address_ImportDataValidation>("ImportAddress", queryParameters);
        }

        #endregion

        #region Vendor Detail

        public async Task<int> SaveVendorDetail(VendorDetail_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@VendorId", parameters.VendorId);
            queryParameters.Add("@ChargerSerial", parameters.ChargerSerial);
            queryParameters.Add("@ChargerModel", parameters.ChargerModel);
            queryParameters.Add("@WarrantyPeriod", parameters.WarrantyPeriod);
            queryParameters.Add("@ChargerName", parameters.ChargerName);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveVendorDetail", queryParameters);
        }

        public async Task<IEnumerable<VendorDetailList_Response>> GetVendorDetailList(VendorDetail_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@VendorId", parameters.VendorId);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<VendorDetailList_Response>("GetVendorDetailList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<VendorDetailList_Response?> GetVendorDetailById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", Id);

            return (await ListByStoredProcedure<VendorDetailList_Response>("GetVendorDetailById", queryParameters)).FirstOrDefault();
        }

        #endregion

        #region Inverter Detail

        public async Task<int> SaveInverterDetail(InverterDetail_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@VendorId", parameters.VendorId);
            queryParameters.Add("@InverterSerial", parameters.InverterSerial);
            queryParameters.Add("@InverterModel", parameters.InverterModel);
            queryParameters.Add("@WarrantyPeriod", parameters.WarrantyPeriod);
            queryParameters.Add("@InverterName", parameters.InverterName);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveInverterDetail", queryParameters);
        }

        public async Task<IEnumerable<InverterDetailList_Response>> GetInverterDetailList(VendorDetail_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@VendorId", parameters.VendorId);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<InverterDetailList_Response>("GetInverterDetailList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<InverterDetailList_Response?> GetInverterDetailById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", Id);

            return (await ListByStoredProcedure<InverterDetailList_Response>("GetInverterDetailById", queryParameters)).FirstOrDefault();
        }

        #endregion
    }
}
