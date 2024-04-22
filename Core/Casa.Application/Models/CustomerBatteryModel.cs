using CLN.Domain.Entities;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class CustomerBattery_Search : BaseSearchEntity
    {
        public long CustomerId { get; set; }
    }

    public class CustomerBattery_Request : BaseEntity
    {
        public int? CustomerId { get; set; }
        public string SerialNumber { get; set; }
        public string Specification { get; set; }
        public DateTime? WarrantyStartDate { get; set; }
        public DateTime? WarrantyEndDate { get; set; }
        public DateTime? ManufacturingDate { get; set; }
        public DateTime? SalesDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public int? WarrantyStatusId { get; set; }
        public bool? IsActive { get; set; }
    }

    public class CustomerBattery_Response : BaseEntity
    {
        public int? CustomerId { get; set; }
        public string SerialNumber { get; set; }
        public string Specification { get; set; }
        public DateTime? WarrantyStartDate { get; set; }
        public DateTime? WarrantyEndDate { get; set; }
        public DateTime? ManufacturingDate { get; set; }
        public DateTime? SalesDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public int? WarrantyStatusId { get; set; }
        public string WarrantyStatus { get; set; }
        public bool? IsActive { get; set; }
    }
}
