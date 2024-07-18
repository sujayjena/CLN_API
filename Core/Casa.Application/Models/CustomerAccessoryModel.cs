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

    public class CustomerAccessory_Search : BaseSearchEntity
    {
        public long CustomerId { get; set; }
    }

    public class CustomerAccessory_Request : BaseEntity
    {
        public int? CustomerId { get; set; }
        public int? PartCodeId { get; set; }
        public string? AccessoryBOMNumber { get; set; }
        public string? DrawingNumber { get; set; }
        public string? AccessoryName { get; set; }
        public int? Quantity { get; set; }
        public bool? IsActive { get; set; }
    }

    public class CustomerAccessory_Response : BaseResponseEntity
    {
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int? PartCodeId { get; set; }
        public string PartCode { get; set; }
        public string AccessoryBOMNumber { get; set; }
        public string DrawingNumber { get; set; }
        public string AccessoryName { get; set; }
        public int? Quantity { get; set; }
        public bool? IsActive { get; set; }
    }

    #region Import and Download

    public class CustomerAccessory_ImportRequest
    {
        public IFormFile FileUpload { get; set; }
    }

    public class CustomerAccessory_ImportData
    {
        public string? CustomerId { get; set; }
        public string? PartCode { get; set; }
        public string? AccessoryBOMNumber { get; set; }
        public string? DrawingNumber { get; set; }
        public string? AccessoryName { get; set; }
        public string? Quantity { get; set; }
        public string? IsActive { get; set; }
    }

    public class CustomerAccessory_ImportDataValidation
    {
        public string? CustomerId { get; set; }
        public string? PartCode { get; set; }
        public string? AccessoryBOMNumber { get; set; }
        public string? DrawingNumber { get; set; }
        public string? AccessoryName { get; set; }
        public string? Quantity { get; set; }
        public string? IsActive { get; set; }
        public string ValidationMessage { get; set; }
    }

    #endregion
}
