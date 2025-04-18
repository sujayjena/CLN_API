using CLN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class TRCProblemModel
    {
    }

    #region Wire Harness

    public class WireHarness_Request : BaseEntity
    {
        public string? WireHarnessName { get; set; }

        public bool? IsActive { get; set; }
    }

    public class WireHarness_Response : BaseResponseEntity
    {
        public string? WireHarnessName { get; set; }

        public bool? IsActive { get; set; }
    }

    #endregion

    #region Connector

    public class Connector_Request : BaseEntity
    {
        public string? ConnectorName { get; set; }

        public bool? IsActive { get; set; }
    }

    public class Connector_Response : BaseResponseEntity
    {
        public string? ConnectorName { get; set; }

        public bool? IsActive { get; set; }
    }

    #endregion

    #region Other Damage

    public class OtherDamage_Request : BaseEntity
    {
        public string? OtherDamageName { get; set; }

        public bool? IsActive { get; set; }
    }

    public class OtherDamage_Response : BaseResponseEntity
    {
        public string? OtherDamageName { get; set; }

        public bool? IsActive { get; set; }
    }

    #endregion
}
