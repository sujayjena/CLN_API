using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface IProtectionsRepository
    {
        Task<int> SaveProtections(Protections_Request parameters);

        Task<IEnumerable<Protections_Response>> GetProtectionsList(BaseSearchEntity parameters);

        Task<Protections_Response?> GetProtectionsById(long Id);
    }
}
