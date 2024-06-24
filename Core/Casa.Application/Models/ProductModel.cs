using CLN.Domain.Entities;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class Product_Search : BaseSearchEntity
    {
    }

    public class Product_Request : BaseEntity
    {
        public string? ProductName { get; set; }
        public int? ProductCategoryId { get; set; }
        public string? ProductCategory { get; set; }
        public int? SegmentId { get; set; }
        public string? Segment { get; set; }
        public int? SubSegmentId { get; set; }
        public string? SubSegment { get; set; }
        public int? ProductModelId { get; set; }
        public string? ProductModel { get; set; }
        public bool? IsActive { get; set; }
    }

    public class Product_Response : BaseResponseEntity
    {
        public string? ProductName { get; set; }
        public int? ProductCategoryId { get; set; }
        public string? ProductCategory { get; set; }
        public int? SegmentId { get; set; }
        public string? Segment { get; set; }
        public int? SubSegmentId { get; set; }
        public string? SubSegment { get; set; }
        public int? ProductModelId { get; set; }
        public string? ProductModel { get; set; }
        public bool? IsActive { get; set; }
    }
}
