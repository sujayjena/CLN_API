using CLN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class CategoryIOTModel
    {
    }

    #region Tracking Device
    public class TrackingDevice_Request : BaseEntity
    {
        public string? TrackingDeviceName { get; set; }
        public bool? IsActive { get; set; }
    }

    public class TrackingDevice_Response : BaseResponseEntity
    {
        public string? TrackingDeviceName { get; set; }
        public bool? IsActive { get; set; }
    }
    #endregion

    #region Make
    public class Make_Request : BaseEntity
    {
        public string? MakeName { get; set; }
        public bool? IsActive { get; set; }
    }

    public class Make_Response : BaseResponseEntity
    {
        public string? MakeName { get; set; }
        public bool? IsActive { get; set; }
    }
    #endregion

    #region SIM Provider
    public class SIMProvider_Request : BaseEntity
    {
        public string? SIMProviderName { get; set; }
        public bool? IsActive { get; set; }
    }

    public class SIMProvider_Response : BaseResponseEntity
    {
        public string? SIMProviderName { get; set; }
        public bool? IsActive { get; set; }
    }
    #endregion

    #region Platform
    public class Platform_Request : BaseEntity
    {
        public string? PlatformName { get; set; }
        public bool? IsActive { get; set; }
    }

    public class Platform_Response : BaseResponseEntity
    {
        public string? PlatformName { get; set; }
        public bool? IsActive { get; set; }
    }
    #endregion
}
