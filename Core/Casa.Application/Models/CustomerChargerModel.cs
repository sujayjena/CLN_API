using CLN.Domain.Entities;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class CustomerCharger_Search : BaseSearchEntity
    {
        public long CustomerId { get; set; }
    }

    public class CustomerCharger_Request : BaseEntity
    {
        public int? CustomerId { get; set; }
        public int? PartCodeId { get; set; }
        public string ChargerSerial { get; set; }
        public string ChargerModel { get; set; }
        public string WarrantyPeriod { get; set; }
        public string ChargerName { get; set; }
    }

    public class CustomerCharger_Response : BaseResponseEntity
    {
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public int? PartCodeId { get; set; }
        public string? PartCode { get; set; }
        public string ChargerSerial { get; set; }
        public string ChargerModel { get; set; }
        public string WarrantyPeriod { get; set; }
        public string ChargerName { get; set; }
    }
}
