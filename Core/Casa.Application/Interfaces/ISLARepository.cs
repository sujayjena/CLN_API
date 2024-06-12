using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface ISLARepository
    {
        #region Priority

        Task<int> SaveSLAPriority(SLAPriority_Request parameters);

        Task<IEnumerable<SLAPriority_Response>> GetSLAPriorityList(BaseSearchEntity parameters);

        Task<SLAPriority_Response?> GetSLAPriorityById(long Id);

        #endregion

        #region Sla Matrix

        Task<int> SaveSlaMatrix(SlaMatrix_Request parameters);

        Task<IEnumerable<SlaMatrix_Response>> GetSlaMatrixList(BaseSearchEntity parameters);

        Task<SlaMatrix_Response?> GetSlaMatrixById(long Id);

        #endregion
    }
}
