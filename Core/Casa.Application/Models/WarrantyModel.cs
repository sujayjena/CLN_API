using CLN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class WarrantyModel
    {
    }

    #region Warranty Status
    public class WarrantyStatus_Request : BaseEntity
    {
        public string? WarrantyStatus { get; set; }
        public bool? IsActive { get; set; }
    }

    public class WarrantyStatus_Response : BaseResponseEntity
    {
        public string? WarrantyStatus { get; set; }
        public bool? IsActive { get; set; }
    }

    #endregion

    #region Warranty Type
    public class WarrantyType_Request : BaseEntity
    {
        public string? WarrantyType { get; set; }
        public bool? IsActive { get; set; }
    }

    public class WarrantyType_Response : BaseResponseEntity
    {
        public string? WarrantyType { get; set; }
        public bool? IsActive { get; set; }
    }

    #endregion
}
