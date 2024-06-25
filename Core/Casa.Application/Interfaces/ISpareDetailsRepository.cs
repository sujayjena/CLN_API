using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface ISpareDetailsRepository
    {
        Task<int> SaveSpareDetails(SpareDetails_Request parameters);

        Task<IEnumerable<SpareDetails_Response>> GetSpareDetailsList(SpareDetails_Search parameters);

        Task<SpareDetails_Response?> GetSpareDetailsById(int Id);

        Task<IEnumerable<SpareDetails_ImportDataValidation>> ImportProblemReportedByCustsDetails(List<SpareDetails_ImportData> parameters);
    }
}
