using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface IBOMRepository
    {
        Task<int> SaveBOM(BOM_Request parameters);

        Task<IEnumerable<BOM_Response>> GetBOMList(BaseSearchEntity parameters);

        Task<BOM_Response?> GetBOMById(int Id);

        Task<IEnumerable<BOM_ImportDataValidation>> ImportBOM(List<BOM_ImportData> parameters);
    }
}
