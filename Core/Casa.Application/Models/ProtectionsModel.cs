using CLN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class ProtectionsModel
    {
    }
    public class Protections_Request : BaseEntity
    {
        public string? Protections { get; set; }
        public bool? IsActive { get; set; }
    }

    public class Protections_Response : BaseResponseEntity
    {
        public string? Protections { get; set; }
        public bool? IsActive { get; set; }
    }
}
