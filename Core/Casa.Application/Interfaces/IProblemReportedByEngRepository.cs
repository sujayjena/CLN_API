using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface IProblemReportedByEngRepository
    {
        Task<int> SaveProblemReportedByEng(ProblemReportedByEng_Request parameters);

        Task<IEnumerable<ProblemReportedByEng_Response>> GetProblemReportedByEngList(BaseSearchEntity parameters);

        Task<ProblemReportedByEng_Response?> GetProblemReportedByEngById(long Id);
    }
}
