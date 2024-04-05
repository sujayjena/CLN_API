using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface IOrderTypeRepository
    {
        Task<int> SaveOrderType(OrderType_Request parameters);

        Task<IEnumerable<OrderType_Response>> GetOrderTypeList(BaseSearchEntity parameters);

        Task<OrderType_Response?> GetOrderTypeById(long Id);
    }
}
