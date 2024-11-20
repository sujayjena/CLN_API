using CLN.Domain.Entities;
using CLN.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class BOM_Request : BaseEntity
    {
        public string? PartCode { get; set; }

        public int? ProductCategoryId { get; set; }

        public int? SegmentId { get; set; }

        public int? SubSegmentId { get; set; }

        public int? ProductModelId { get; set; }

        public string? DrawingNumber { get; set; }

        public string? Warranty { get; set; }

        [JsonIgnore]
        public string? PartImage { get; set; }

        public string? PartImageOriginalFileName { get; set; }

        public string? PartImage_Base64 { get; set; }

        public string? Remarks { get; set; }

        public bool? IsActive { get; set; }
    }

    public class BOM_Response : BaseResponseEntity
    {
        public string? PartCode { get; set; }

        public int? ProductCategoryId { get; set; }

        public string? ProductCategory { get; set; }

        public int? SegmentId { get; set; }

        public string? Segment { get; set; }

        public int? SubSegmentId { get; set; }

        public string? SubSegment { get; set; }

        public int? ProductModelId { get; set; }

        public string? ProductModel { get; set; }

        public string? DrawingNumber { get; set; }

        public string? Warranty { get; set; }

        public string? PartImage { get; set; }

        public string? PartImageOriginalFileName { get; set; }

        public string? PartImageURL { get; set; }

        public string? Remarks { get; set; }

        public bool? IsActive { get; set; }
    }

    #region Import and Download

    public class BOM_ImportData
    {
        public string? PartCode { get; set; }

        public string? ProductCategory { get; set; }

        public string? Segment { get; set; }

        public string? SubSegment { get; set; }

        public string? ProductModel { get; set; }

        public string? DrawingNumber { get; set; }

        public string? Warranty { get; set; }

        public string? Remarks { get; set; }

        public string? IsActive { get; set; }
    }

    public class BOM_ImportDataValidation
    {
        public string? PartCode { get; set; }

        public string? ProductCategory { get; set; }

        public string? Segment { get; set; }

        public string? SubSegment { get; set; }

        public string? ProductModel { get; set; }

        public string? DrawingNumber { get; set; }

        public string? Warranty { get; set; }

        public string? Remarks { get; set; }

        public string? IsActive { get; set; }

        public string? ValidationMessage { get; set; }
    }

    #endregion
}
