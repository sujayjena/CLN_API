using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface IComplaintTypeRepository
    {
        Task<int> SaveComplaintType(ComplaintType_Request parameters);

        Task<IEnumerable<ComplaintType_Response>> GetComplaintTypeList(BaseSearchEntity parameters);

        Task<ComplaintType_Response?> GetComplaintTypeById(long Id);
    }
}
