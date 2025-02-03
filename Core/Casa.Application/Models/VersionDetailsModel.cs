using CLN.Domain.Entities;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class VersionDetails_Search : BaseSearchEntity
    {
        [DefaultValue("")]
        public string? PackageName { get; set; }

        [DefaultValue("")]
        public string? UpdateType { get; set; }
    }

    public class VersionDetails_Request : BaseEntity
    {
        public long? AppVersionNo { get; set; }

        [DefaultValue("")]
        public string? AppVersionName { get; set; }

        [DefaultValue("")]
        public string? UpdateMsg { get; set; }

        [DefaultValue("")]
        public string? PackageName { get; set; }

        [DefaultValue("")]
        public string? UpdateType { get; set; }
        public bool? IsActive { get; set; }
    }

    public class VersionDetails_Response : BaseResponseEntity
    {
        public long? AppVersionNo { get; set; }
        public string? AppVersionName { get; set; }
        public string? UpdateMsg { get; set; }
        public string? PackageName { get; set; }
        public string? UpdateType { get; set; }
        public bool IsActive { get; set; }
    }
}
