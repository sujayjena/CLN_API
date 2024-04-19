using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface IVendorRepository
    {
        #region Vendor

        Task<int> SaveVendor(Vendor_Request parameters);

        Task<IEnumerable<Vendor_Response>> GetVendorList(BaseSearchEntity parameters);

        Task<Vendor_Response?> GetVendorById(int Id);

        #endregion


        #region Vendor Contact Detail

        Task<int> SaveVendorContactDetail(ContactDetail_Request parameters);

        Task<IEnumerable<ContactDetail_Response>> GetVendorContactDetailList(VendorContactDetail_Search parameters);

        Task<ContactDetail_Response?> GetVendorContactDetailById(int Id);

        #endregion

        #region Vendor Address

        Task<int> SaveVendorAddress(Address_Request parameters);

        Task<IEnumerable<Address_Response>> GetVendorAddressList(VendorAddress_Search parameters);

        Task<Address_Response?> GetVendorAddressById(int Id);

        #endregion
    }
}
