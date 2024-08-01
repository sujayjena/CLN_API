using CLN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class DetailsOfAdjustmentModel
    {
    }
    public class DetailsOfAdjustment_Request : BaseEntity
    {
        public string? DetailsOfAdjustment { get; set; }
        public int? ProductCategoryId { get; set; }
        public int? SegmentId { get; set; }
        public bool? IsActive { get; set; }
    }

    public class DetailsOfAdjustment_Response : BaseResponseEntity
    {
        public string? DetailsOfAdjustment { get; set; }
        public int? ProductCategoryId { get; set; }
        public string? ProductCategory { get; set; }
        public int? SegmentId { get; set; }
        public string? Segment { get; set; }
        public bool? IsActive { get; set; }
    }
}
