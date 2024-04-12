using CLN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class SpareDetailsModel
    {
    }

    public class SpareDetails_Request : BaseEntity
    {
        public string? UniqueCode { get; set; }
        public string? SpareCategory { get; set; }
        public string? SpareDesc { get; set; }
        public bool? IsActive { get; set; }
    }

    public class SpareDetails_Response : BaseResponseEntity
    {
        public string? UniqueCode { get; set; }
        public string? SpareCategory { get; set; }
        public string? SpareDesc { get; set; }
        public bool? IsActive { get; set; }
    }
}
