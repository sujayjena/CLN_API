using CLN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class PartStatusModel
    {
    }
    public class PartStatus_Request : BaseEntity
    {
        public string? PartStatus { get; set; }
        public bool? IsActive { get; set; }
    }

    public class PartStatus_Response : BaseResponseEntity
    {
        public string? PartStatus { get; set; }
        public bool? IsActive { get; set; }
    }
}
