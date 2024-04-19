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

        public async Task<IEnumerable<Vendor_Response>> GetVendorList(BaseSearchEntity parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<Vendor_Response>("GetVendorList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<Vendor_Response?> GetVendorById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", Id);

            return (await ListByStoredProcedure<Vendor_Response>("GetVendorById", queryParameters)).FirstOrDefault();
        }

        #endregion

        #region Vendor Contact Detail

        public async Task<int> SaveVendorContactDetail(ContactDetail_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@RefId", parameters.RefId);
            queryParameters.Add("@RefType", "Vendor");
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

        public async Task<IEnumerable<ContactDetail_Response>> GetVendorContactDetailList(VendorContactDetail_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@RefId", parameters.VendorId);
            queryParameters.Add("@RefType", "Vendor");
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

        public async Task<ContactDetail_Response?> GetVendorContactDetailById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", Id);
            queryParameters.Add("@RefType", "Vendor");

            return (await ListByStoredProcedure<ContactDetail_Response>("GetContactDetailById", queryParameters)).FirstOrDefault();
        }

        #endregion

        #region Vendor Address

        public async Task<int> SaveVendorAddress(Address_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@RefId", parameters.RefId);
            queryParameters.Add("@RefType", "Vendor");
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

        public async Task<IEnumerable<Address_Response>> GetVendorAddressList(VendorAddress_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@RefId", parameters.VendorId);
            queryParameters.Add("@RefType", "Vendor");
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<Address_Response>("GetAddressList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<Address_Response?> GetVendorAddressById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", Id);
            queryParameters.Add("@RefType", "Vendor");

            return (await ListByStoredProcedure<Address_Response>("GetAddressById", queryParameters)).FirstOrDefault();
        }

        #endregion
    }
}
