using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface IRatePerKMRepository
    {
        Task<int> SaveRatePerKM(RatePerKM_Request parameters);

        Task<IEnumerable<RatePerKM_Response>> GetRatePerKMList(BaseSearchEntity parameters);

        Task<RatePerKM_Response?> GetRatePerKMById(long Id);
    }
}
