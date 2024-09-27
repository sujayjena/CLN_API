using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface ITechSupportRepository
    {
        Task<int> SaveTechSupport(TechSupport_Request parameters);

        Task<IEnumerable<TechSupport_Response>> GetTechSupportList(BaseSearchEntity parameters);

        Task<TechSupport_Response?> GetTechSupportById(long Id);
    }
}
