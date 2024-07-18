using CLN.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class ProblemReportedModel
    {
    }
    public class ProblemReported_Request : BaseEntity
    {
        public int? ProductCategoryId { get; set; }
        public int? SegmentId { get; set; }
        public int? SubSegmentId { get; set; }
        public string? ProblemReported { get; set; }
        public bool? IsActive { get; set; }
    }

    public class ProblemReported_Response : BaseResponseEntity
    {
        public int? ProductCategoryId { get; set; }
        public string? ProductCategory { get; set; }
        public int? SegmentId { get; set; }
        public string? Segment { get; set; }
        public int? SubSegmentId { get; set; }
        public string? SubSegment { get; set; }
        public string? ProblemReported { get; set; }
        public bool? IsActive { get; set; }
    }

    public class ImportRequest
    {
        public IFormFile FileUpload { get; set; }
    }
    public class ProblemReportedByCustDataValidationErrors
    {
        public string ProductCategory { get; set; }
        public string Segment { get; set; }
        public string SubSegment { get; set; }
        public string ProblemReported { get; set; }
        public string IsActive { get; set; }
        public string ValidationMessage { get; set; }
    }
    public class ImportedProblemReportedByCustDetails
    {
        public string ProductCategory { get; set; }
        public string Segment { get; set; }
        public string SubSegment { get; set; }
        public string ProblemReported { get; set; }
        public string IsActive { get; set; }
    }
}
