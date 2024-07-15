using CLN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class RectificationActionModel
    {
    }
    public class RectificationAction_Request : BaseEntity
    {
        public int? ProductCategoryId { get; set; }
        public int? SegmentId { get; set; }
        public string? RectificationAction { get; set; }
        public bool? IsActive { get; set; }
    }

    public class RectificationAction_Response : BaseResponseEntity
    {
        public int? ProductCategoryId { get; set; }
        public string? ProductCategory { get; set; }
        public int? SegmentId { get; set; }
        public string? Segment { get; set; }
        public string? RectificationAction { get; set; }
        public bool? IsActive { get; set; }
    }

    public class RectificationActionDataValidationErrors
    {
        public string ProductCategory { get; set; }
        public string Segment { get; set; }
        public string RectificationAction { get; set; }
        public string IsActive { get; set; }
        public string ValidationMessage { get; set; }
    }
    public class ImportedRectificationAction
    {
        public string ProductCategory { get; set; }
        public string Segment { get; set; }
        public string RectificationAction { get; set; }
        public string IsActive { get; set; }
    }
}
