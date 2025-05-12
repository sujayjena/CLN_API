using CLN.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface IErrorLogHistoryRepository
    {
        Task<int> SaveErrorLogHistory(ErrorLogHistory_Request parameters);
    }
}
