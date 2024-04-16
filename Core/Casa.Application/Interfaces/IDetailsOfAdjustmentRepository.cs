using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface IDetailsOfAdjustmentRepository
    {
        Task<int> SaveDetailsOfAdjustment(DetailsOfAdjustment_Request parameters);

        Task<IEnumerable<DetailsOfAdjustment_Response>> GetDetailsOfAdjustmentList(BaseSearchEntity parameters);

        Task<DetailsOfAdjustment_Response?> GetDetailsOfAdjustmentById(long Id);
    }
}
