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

        Task<int> SaveEnggPartRequest(EnggPartRequest_Request parameters);
        Task<IEnumerable<EnggPartRequest_Response>> GetEnggPartRequestList(EnggPartRequest_Search parameters);
        Task<EnggPartRequest_Response?> GetEnggPartRequestById(int Id);

        Task<int> SaveEnggPartRequestDetail(EnggPartRequestDetails_Request parameters);
        Task<IEnumerable<EnggPartRequestDetails_Response>> GetEnggPartRequestDetailList(EnggPartRequestDetails_Search parameters);
        Task<EnggPartRequestDetails_Response?> GetEnggPartRequestDetailById(int Id);

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
