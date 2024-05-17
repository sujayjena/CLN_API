using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface IManageFeedbackRepository
    {
        Task<int> SaveManageFeedback(ManageFeedback_Request parameters);

        Task<IEnumerable<ManageFeedback_Response>> GetManageFeedbackList(BaseSearchEntity parameters);

        Task<ManageFeedback_Response?> GetManageFeedbackById(long Id);
    }
}
