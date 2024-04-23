using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface IEnggPartRequestOrderRepository
    {
        Task<int> SaveEnggPartRequestOrder(EnggPartRequestOrder_Request parameters);
        Task<IEnumerable<EnggPartRequestOrder_Response>> GetEnggPartRequestOrderList(EnggPartRequestOrderSearch_Request parameters);
        Task<EnggPartRequestOrder_Response?> GetEnggPartRequestOrderById(int Id);
        Task<int> SaveEnggPartRequestOrderDetails(EnggPartRequestOrderDetails_Request parameters);
        Task<IEnumerable<EnggPartRequestOrderDetails_Response>> GetEnggPartRequestOrderDetailsList(EnggPartRequestOrderDetailsSearch_Request parameters);
        Task<EnggPartRequestOrderDetails_Response?> GetEnggPartRequestOrderDetailsById(int Id);
    }
}
