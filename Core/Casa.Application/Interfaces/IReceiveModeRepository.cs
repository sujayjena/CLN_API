using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface IReceiveModeRepository
    {
        Task<int> SaveReceiveMode(ReceiveMode_Request parameters);

        Task<IEnumerable<ReceiveMode_Response>> GetReceiveModeList(BaseSearchEntity parameters);

        Task<ReceiveMode_Response?> GetReceiveModeById(int Id);
    }
}
