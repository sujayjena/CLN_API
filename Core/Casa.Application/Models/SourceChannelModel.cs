using CLN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class SourceChannelModel
    {
    }

    #region Source Channel
    public class SourceChannel_Request : BaseEntity
    {
        public string? SourceChannel { get; set; }
        public bool? IsActive { get; set; }
    }

    public class SourceChannel_Response : BaseResponseEntity
    {
        public string? SourceChannel { get; set; }
        public bool? IsActive { get; set; }
    }
    #endregion

    #region Caller Type
    public class CallerType_Request : BaseEntity
    {
        [DefaultValue("")]
        public string? CallerType { get; set; }
        public bool? IsActive { get; set; }
    }

    public class CallerType_Response : BaseResponseEntity
    {
        public string? CallerType { get; set; }
        public bool? IsActive { get; set; }
    }
    #endregion
}
