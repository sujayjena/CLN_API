using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface IPartStatusRepository
    {
        Task<int> SavePartStatus(PartStatus_Request parameters);

        Task<IEnumerable<PartStatus_Response>> GetPartStatusList(BaseSearchEntity parameters);

        Task<PartStatus_Response?> GetPartStatusById(long Id);
    }
}
