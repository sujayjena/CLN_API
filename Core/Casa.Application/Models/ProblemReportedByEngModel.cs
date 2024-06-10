using CLN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class ProblemReportedByEngModel
    {
    }
    public class ProblemReportedByEng_Request : BaseEntity
    {
        public int? SegmentId { get; set; }
        public int? ProductCategoryId { get; set; }
        public string? ProblemReportedByEng { get; set; }
        public bool? IsActive { get; set; }
    }

    public class ProblemReportedByEng_Response : BaseResponseEntity
    {
        public int? SegmentId { get; set; }
        public string? Segment { get; set; }
        public int? ProductCategoryId { get; set; }
        public string? ProductCategory { get; set; }
        public string? ProblemReportedByEng { get; set; }
        public bool? IsActive { get; set; }
    }
}
