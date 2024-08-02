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

        Task<IEnumerable<VendorList_Response>> GetVendorList(BaseSearchEntity parameters);

        Task<VendorList_Response?> GetVendorById(int Id);

        Task<IEnumerable<Vendor_ImportDataValidation>> ImportVendor(List<Vendor_ImportData> parameters);
        Task<IEnumerable<Contact_ImportDataValidation>> ImportVendorContact(List<Contact_ImportData> parameters);
        Task<IEnumerable<Address_ImportDataValidation>> ImportVendorAddress(List<Address_ImportData> parameters);

        #endregion

        #region Vendor Detail

        Task<int> SaveVendorDetail(VendorDetail_Request parameters);

        Task<IEnumerable<VendorDetailList_Response>> GetVendorDetailList(VendorDetail_Search parameters);

        Task<VendorDetailList_Response?> GetVendorDetailById(int Id);

        #endregion
    }
}
