using CLN.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface IManageTicketRepository
    {
        Task<int> SaveManageTicket(ManageTicket_Request parameters);

        Task<IEnumerable<ManageTicketList_Response>> GetManageTicketList(ManageTicket_Search parameters);

        Task<ManageTicketDetail_Response?> GetManageTicketById(int Id);


        Task<int> SaveManageTicketPartDetail(ManageTicketPartDetails_Request parameters);

        Task<IEnumerable<ManageTicketPartDetails_Response>> GetManageTicketPartDetailById(int Id);


        Task<IEnumerable<ManageTicketCustomerMobileNumber_Response>> GetCustomerMobileNumberList(string SearchText);

        Task<ManageTicketCustomerDetail_Response?> GetCustomerDetailByMobileNumber(string mobile);
    }
}
