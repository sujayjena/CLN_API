using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface ICategoryIOTRepository
    {
        #region Tracking Device
        Task<int> SaveTrackingDevice(TrackingDevice_Request parameters);

        Task<IEnumerable<TrackingDevice_Response>> GetTrackingDeviceList(BaseSearchEntity parameters);

        Task<TrackingDevice_Response?> GetTrackingDeviceById(long Id);
        #endregion

        #region Make
        Task<int> SaveMake(Make_Request parameters);

        Task<IEnumerable<Make_Response>> GetMakeList(BaseSearchEntity parameters);

        Task<Make_Response?> GetMakeById(long Id);
        #endregion

        #region SIM Provideer
        Task<int> SaveSIMProvider(SIMProvider_Request parameters);

        Task<IEnumerable<SIMProvider_Response>> GetSIMProviderList(BaseSearchEntity parameters);

        Task<SIMProvider_Response?> GetSIMProviderById(long Id);
        #endregion

        #region Platform
        Task<int> SavePlatform(Platform_Request parameters);

        Task<IEnumerable<Platform_Response>> GetPlatformList(BaseSearchEntity parameters);

        Task<Platform_Response?> GetPlatformById(long Id);
        #endregion
    }
}
