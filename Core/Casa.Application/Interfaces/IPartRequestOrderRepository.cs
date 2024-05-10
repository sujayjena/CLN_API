using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface IPartRequestOrderRepository
    {
        #region Engg Part Request

        Task<int> SaveEnggPartRequestOrder(EnggPartRequestOrder_Request parameters);
        Task<IEnumerable<EnggPartRequestOrder_Response>> GetEnggPartRequestOrderList(EnggPartRequestOrderSearch_Request parameters);
        Task<EnggPartRequestOrder_Response?> GetEnggPartRequestOrderById(int Id);

        Task<int> SaveEnggPartRequestOrderDetails(EnggPartRequestOrderDetails_Request parameters);
        Task<IEnumerable<EnggPartRequestOrderDetails_Response>> GetEnggPartRequestOrderDetailsList(EnggPartRequestOrderDetailsSearch_Request parameters);
        Task<EnggPartRequestOrderDetails_Response?> GetEnggPartRequestOrderDetailsById(int Id);

        #endregion


        #region TRC Part Request

        Task<int> SaveTRCPartRequest(TRCPartRequest_Request parameters);
        Task<IEnumerable<TRCPartRequest_Response>> GetTRCPartRequestList(TRCPartRequest_Search parameters);
        Task<TRCPartRequest_Response?> GetTRCPartRequestById(int Id);

        Task<int> SaveTRCPartRequestDetail(TRCPartRequestDetails_Request parameters);
        Task<IEnumerable<TRCPartRequestDetails_Response>> GetTRCPartRequestDetailList(TRCPartRequestDetails_Search parameters);
        Task<TRCPartRequestDetails_Response?> GetTRCPartRequestDetailById(int Id);

        #endregion
    }
}
