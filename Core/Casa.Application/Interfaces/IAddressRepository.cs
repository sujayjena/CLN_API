using CLN.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface IAddressRepository
    {
        Task<int> SaveAddress(Address_Request parameters);

        Task<IEnumerable<Address_Response>> GetAddressList(Address_Search parameters);

        Task<Address_Response?> GetAddressById(int Id);
    }
}
