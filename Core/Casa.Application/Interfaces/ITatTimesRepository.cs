using CLN.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface ITatTimesRepository
    {
        Task<int> SaveTatTimes(TatTimes_Request parameters);

        Task<IEnumerable<TatTimes_Response>> GetTatTimesList(TatTimes_Search parameters);

        Task<TatTimes_Response?> GetTatTimesById(int Id);
    }
}
