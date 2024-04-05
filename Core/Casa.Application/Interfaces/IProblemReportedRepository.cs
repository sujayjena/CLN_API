using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface IProblemReportedRepository
    {
        Task<int> SaveProblemReported(ProblemReported_Request parameters);

        Task<IEnumerable<ProblemReported_Response>> GetProblemReportedList(BaseSearchEntity parameters);

        Task<ProblemReported_Response?> GetProblemReportedById(long Id);
    }
}
