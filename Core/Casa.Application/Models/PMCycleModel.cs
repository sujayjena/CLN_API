using CLN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class PMCycleModel
    {
    }
    public class PMCycle_Request : BaseEntity
    {
        public string? PMCycleName { get; set; }
        public int? Days { get; set; }

        public bool? IsActive { get; set; }
    }

    public class PMCycle_Response : BaseResponseEntity
    {
        public string? PMCycleName { get; set; }
        public int? Days { get; set; }

        public bool? IsActive { get; set; }
    }
}
