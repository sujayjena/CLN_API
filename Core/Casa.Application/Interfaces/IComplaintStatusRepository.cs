using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface IComplaintStatusRepository
    {
        Task<int> SaveComplaintStatus(ComplaintStatus_Request parameters);

        Task<IEnumerable<ComplaintStatus_Response>> GetComplaintStatusList(BaseSearchEntity parameters);

        Task<ComplaintStatus_Response?> GetComplaintStatusById(long Id);
    }
}
