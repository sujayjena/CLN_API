using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface IRectificationActionRepository
    {
        Task<int> SaveRectificationAction(RectificationAction_Request parameters);

        Task<IEnumerable<RectificationAction_Response>> GetRectificationActionList(BaseSearchEntity parameters);

        Task<RectificationAction_Response?> GetRectificationActionById(long Id);
    }
}
