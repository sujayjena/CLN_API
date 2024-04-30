using CLN.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface IManageEnquiryRepository
    {
        Task<int> SaveManageEnquiry(ManageEnquiry_Request parameters);

        Task<IEnumerable<ManageEnquiry_Response>> GetManageEnquiryList(ManageEnquiry_Search parameters);

        Task<ManageEnquiry_Response?> GetManageEnquiryById(int Id);
    }
}
