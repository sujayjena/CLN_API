using CLN.Domain.Entities;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class EnggPartRequestOrderModel
    {
        
    }

    public class EnggPartRequestOrder_Request : BaseEntity
    {
        public EnggPartRequestOrder_Request()
        {
            EnggPartRequestOrderDetailList = new List<EnggPartRequestOrderDetails_Request>();
        }

        public DateTime OrderDate { get; set; }

        public int EngineerId { get; set; }

        [DefaultValue("")]
        public string? Remarks { get; set; }

        public int CompanyId { get; set; }

        public int BranchId { get; set; }

        public List<EnggPartRequestOrderDetails_Request> EnggPartRequestOrderDetailList { get; set; }
    }

    public class EnggPartRequestOrderSearch_Request : BaseSearchEntity
    {
        public int CompanyId { get; set; }

        [DefaultValue("")]
        public string? BranchId { get; set; }
        public int EngineerId { get; set; }
        public int StatusId { get; set; }
    }

    public class EnggPartRequestOrderDetailsSearch_Request : BaseSearchEntity
    {
        public int OrderId { get; set; }
    }

    public class EnggPartRequestOrder_Response : BaseResponseEntity
    {
        public int Id { get; set; }
        public string? OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public int? EngineerId { get; set; }
        public string? EngineerName { get; set; }
        public string? Remarks { get; set; }
        public int? CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public int? BranchId { get; set; }
        public string? BranchName { get; set; }
    }

    public class EnggPartRequestOrderDetailsById_Response : BaseResponseEntity
    {
        public EnggPartRequestOrderDetailsById_Response()
        {
            EnggPartRequestOrderDetailList = new List<EnggPartRequestOrderDetails_Response>();
        }
        public int Id { get; set; }
        public string? OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public int? EngineerId { get; set; }
        public string? EngineerName { get; set; }
        public string? Remarks { get; set; }
        public int? CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public int? BranchId { get; set; }
        public string? BranchName { get; set; }
        public List<EnggPartRequestOrderDetails_Response> EnggPartRequestOrderDetailList { get; set; }
    }

    public class EnggPartRequestOrderDetails_Request : BaseEntity
    {
        public int? OrderId { get; set; }
        public int? SpareDetailsId { get; set; }
        public int? TypeOfBMSId { get; set; }
        public int? AvailableQty { get; set; }
        public int? OrderQty { get; set; }
        public int? StatusId { get; set; }
    }

    public class EnggPartRequestOrderDetails_Response : BaseResponseEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int? SpareDetailsId { get; set; }
        public string? UniqueCode { get; set; }
        public string? SpareCategory { get; set; }
        public string? SpareDesc { get; set; }
        public string? UOMName { get; set; }
        public int? TypeOfBMSId { get; set; }
        public string? TypeOfBMS { get; set; }
        public int? AvailableQty { get; set; }
        public int? OrderQty { get; set; }
        public int? StatusId { get; set; }
        public string? StatusName { get; set; }
    }
}
