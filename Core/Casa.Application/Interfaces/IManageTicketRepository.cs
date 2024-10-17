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

        Task<int> CreateDuplicateTicket(int TicketId, int IsEngineerType = 0);

        Task<IEnumerable<ManageTicketList_Response>> GetManageTicketList(ManageTicket_Search parameters);

        Task<ManageTicketDetail_Response?> GetManageTicketById(int Id);


        Task<IEnumerable<ManageTicketRemarks_Response>> GetTicketRemarkListById(ManageTicketRemarks_Search parameters);

        Task<IEnumerable<ManageTicketStatusLog_Response>> GetManageTicketStatusLogById(int Id);

        Task<int> SaveTicketVisitHistory(ManageTicketEngineerVisitHistory_Request parameters);

        Task<IEnumerable<ManageTicketEngineerVisitHistory_Response>> GetTicketVisitHistoryList(ManageTicketEngineerVisitHistory_Search parameters);


        Task<int> SaveManageTicketPartDetail(ManageTicketPartDetails_Request parameters);

        Task<IEnumerable<ManageTicketPartDetails_Response>> GetManageTicketPartDetailById(int Id);

        Task<int> DeleteManageTicketPartDetail(int Id);


        Task<IEnumerable<ManageTicketCustomerMobileNumber_Response>> GetCustomerMobileNumberList(string SearchText);

        Task<ManageTicketCustomerDetail_Response?> GetCustomerDetailByMobileNumber(string mobile);

        Task<int> SaveManageTicketLogHistory(int TicketId);
        Task<IEnumerable<ManageTicketLogHistory_Response>> GetManageTicketLogHistoryList(ManageTicketLogHistory_Search parameters);

        Task<IEnumerable<ValidateTicketProductSerialNumber_Response>> ValidateTicketProductSerialNumberById(string ProductSerialNumber, bool IsOldProduct, int TicketId);

        Task<int> SaveFeedbackQuestionAnswer(FeedbackQuestionAnswer_Request parameters);

        Task<IEnumerable<FeedbackQuestionAnswer_Response>> GetFeedbackQuestionAnswerList(FeedbackQuestionAnswerSearch_Request parameters);
    }
}
