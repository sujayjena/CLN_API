using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface ICustomerRepository
    {
        #region Customer

        Task<int> SaveCustomer(Customer_Request parameters);

        Task<IEnumerable<Customer_Response>> GetCustomerList(BaseSearchEntity parameters);

        Task<Customer_Response?> GetCustomerById(int Id);

        Task<int> UpdateCustomerAddress(Customer_Request parameters);

        #endregion

        #region Customer Contact Detail

        Task<int> SaveCustomerContactDetail(ContactDetail_Request parameters);

        Task<IEnumerable<ContactDetail_Response>> GetCustomerContactDetailList(CustomerContactDetail_Search parameters);

        Task<ContactDetail_Response?> GetCustomerContactDetailById(int Id);

        #endregion

        #region Customer Address

        Task<int> SaveCustomerAddress(Address_Request parameters);

        Task<IEnumerable<Address_Response>> GetCustomerAddressList(CustomerAddress_Search parameters);

        Task<Address_Response?> GetCustomerAddressById(int Id);

        #endregion
    }
}
