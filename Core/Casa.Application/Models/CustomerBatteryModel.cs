using CLN.Domain.Entities;
using CLN.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class CustomerBattery_Search : BaseSearchEntity
    {
        public long? CustomerId { get; set; }
        public long? ProductCategoryId { get; set; }
    }

    public class CustomerBattery_Request : BaseEntity
    {
        public int? CustomerId { get; set; }

        public int? PartCodeId { get; set; }

        public string? SerialNumber { get; set; }

        public string? ProductSerialNumber { get; set; }

        public string? InvoiceNumber { get; set; }

        public DateTime? ManufacturingDate { get; set; }

        public DateTime? WarrantyStartDate { get; set; }

        public DateTime? WarrantyEndDate { get; set; }
        public int? WarrantyStatusId { get; set; }

        public int? WarrantyTypeId { get; set; }

        public bool? IsActive { get; set; }
    }

    public class CustomerBattery_Response : BaseResponseEntity
    {
        public int? CustomerId { get; set; }

        public string? CustomerName { get; set; }

        public int? PartCodeId { get; set; }

        public string? PartCode { get; set; }

        public string? CustomerCode { get; set; }

        public string? SerialNumber { get; set; }

        public string? ProductSerialNumber { get; set; }

        public string? InvoiceNumber { get; set; }

        public int? ProductCategoryId { get; set; }

        public string? ProductCategory { get; set; }

        public int? SegmentId { get; set; }

        public string? Segment { get; set; }

        public int? SubSegmentId { get; set; }

        public string? SubSegment { get; set; }

        public int? ProductModelId { get; set; }

        public string? ProductModel { get; set; }

        public string? DrawingNumber { get; set; }

        public DateTime? ManufacturingDate { get; set; }

        public string? Warranty { get; set; }

        public DateTime? WarrantyStartDate { get; set; }

        public DateTime? WarrantyEndDate { get; set; }

        public int? WarrantyStatusId { get; set; }

        public string? WarrantyStatus { get; set; }

        public int? WarrantyTypeId { get; set; }

        public string? WarrantyType { get; set; }

        public bool? IsActive { get; set; }
    }

    #region Import and Download

    public class CustomerBattery_ImportRequest
    {
        public IFormFile FileUpload { get; set; }
    }

    public class CustomerBattery_ImportData
    {
        public string? CustomerName { get; set; }
        public string? PartCode { get; set; }
        public string? ProductSerialNumber { get; set; }
        public DateTime? ManufacturingDate { get; set; }
        public DateTime? WarrantyStartDate { get; set; }
        public DateTime? WarrantyEndDate { get; set; }
        public string? WarrantyStatus { get; set; }
        public string? WarrantyType { get; set; }
    }

    public class CustomerBattery_ImportDataValidation
    {
        public string? CustomerName { get; set; }
        public string? PartCode { get; set; }
        public string? ProductSerialNumber { get; set; }
        public string? ManufacturingDate { get; set; }
        public string? WarrantyStartDate { get; set; }
        public string? WarrantyEndDate { get; set; }
        public string? WarrantyStatus { get; set; }
        public string? WarrantyType { get; set; }
        public string ValidationMessage { get; set; }
    }
    #endregion
}
