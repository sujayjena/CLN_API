using CLN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class SpareCategoryModel
    {
    }

    public class SpareCategory_Request : BaseEntity
    {
        [DefaultValue("")]
        public string? SpareCategory { get; set; }
        public bool? IsActive { get; set; }
    }

    public class SpareCategory_Response : BaseResponseEntity
    {
        public string? SpareCategory { get; set; }
        public bool? IsActive { get; set; }
    }
}
