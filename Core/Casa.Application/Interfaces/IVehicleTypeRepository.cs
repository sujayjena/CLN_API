using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface IVehicleTypeRepository
    {
        Task<int> SaveVehicleType(VehicleType_Request parameters);

        Task<IEnumerable<VehicleType_Response>> GetVehicleTypeList(BaseSearchEntity parameters);

        Task<VehicleType_Response?> GetVehicleTypeById(long Id);
    }
}
