using CLN.Domain.Entities;
using CLN.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class CustomerBOM_Search : BaseSearchEntity
    {
        public long CustomerId { get; set; }
    }

    public class CustomerBOM_Request : BaseEntity
    {
        public int? CustomerId { get; set; }

        public string? PartCode { get; set; }

        public string? CustomerCode { get; set; }

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

    public class CustomerBOM_Response : BaseResponseEntity
    {
        public int? CustomerId { get; set; }

        public string? CustomerName { get; set; }

        public int? PartCodeId { get; set; }
        public string? PartCode { get; set; }

        public string? CustomerCode { get; set; }

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

    public class CustomerBOM_ImportRequest
    {
        public IFormFile FileUpload { get; set; }
    }

    public class CustomerBOM_ImportData
    {
        public string? CustomerName { get; set; }

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

    public class CustomerBOM_ImportDataValidation
    {
        public string? CustomerName { get; set; }

        public string? PartCode { get; set; }

        public string? ProductCategory { get; set; }

        public string? Segment { get; set; }

        public string? SubSegment { get; set; }

        public string? ProductModel { get; set; }

        public string? DrawingNumber { get; set; }

        public string? Warranty { get; set; }

        public string? Remarks { get; set; }

        public string? IsActive { get; set; }

        public string ValidationMessage { get; set; }
    }

    #endregion
}
