using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface ISourceChannelRepository
    {
        Task<int> SaveSourceChannel(SourceChannel_Request parameters);

        Task<IEnumerable<SourceChannel_Response>> GetSourceChannelList(BaseSearchEntity parameters);

        Task<SourceChannel_Response?> GetSourceChannelById(int Id);
    }
}
