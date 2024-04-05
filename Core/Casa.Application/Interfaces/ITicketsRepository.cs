using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface ITicketsRepository
    {
        #region Ticket Category
        Task<int> SaveTicketCategory(TicketCategory_Request parameters);

        Task<IEnumerable<TicketCategory_Response>> GetTicketCategoryList(BaseSearchEntity parameters);

        Task<TicketCategory_Response?> GetTicketCategoryById(long Id);
        #endregion

        #region Ticket Status
        Task<int> SaveTicketStatus(TicketStatus_Request parameters);

        Task<IEnumerable<TicketStatus_Response>> GetTicketStatusList(BaseSearchEntity parameters);

        Task<TicketStatus_Response?> GetTicketStatusById(long Id);
        #endregion

        #region Ticket Type
        Task<int> SaveTicketType(TicketType_Request parameters);

        Task<IEnumerable<TicketType_Response>> GetTicketTypeList(BaseSearchEntity parameters);

        Task<TicketType_Response?> GetTicketTypeById(long Id);
        #endregion
    }
}
