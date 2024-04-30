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
        Task<int> SaveVendor(Vendor_Request parameters);

        Task<IEnumerable<Vendor_Response>> GetVendorList(BaseSearchEntity parameters);

        Task<Vendor_Response?> GetVendorById(int Id);
    }
}
