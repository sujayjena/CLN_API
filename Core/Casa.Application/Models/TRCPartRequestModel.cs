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
    public class TRCPartRequest_Search : BaseSearchEntity
    {
        //public int? CompanyId { get; set; }

        //[DefaultValue("")]
        //public string? BranchId { get; set; }
        public int? EngineerId { get; set; }
        public int? StatusId { get; set; }
        public int? SpareDetailsId { get; set; }
    }

    public class TRCPartRequestDetails_Search : BaseSearchEntity
    {
        public int? RequestId { get; set; }
    }

    public class TRCPartRequest_Request : BaseEntity
    {
        public TRCPartRequest_Request()
        {
            PartDetail = new List<TRCPartRequestDetails_Request>();
        }

        public string? RequestNumber { get; set; }

        public DateTime? RequestDate { get; set; }

        public int? EngineerId { get; set; }

        public string? Remarks { get; set; }

        public int? StatusId { get; set; }

        public bool? IsActive { get; set; }

        public List<TRCPartRequestDetails_Request> PartDetail { get; set; }
    }

    public class TRCPartRequestDetails_Request : BaseEntity
    {
        public int? RequestId { get; set; }
        public int? SpareCategoryId { get; set; }
        public int? ProductMakeId { get; set; }
        public int? BMSMakeId { get; set; }
        public int? SpareDetailsId { get; set; }

        public int? UOMId { get; set; }

        public int? TypeOfBMSId { get; set; }

        public decimal? AvailableQty { get; set; }

        public decimal? RequiredQty { get; set; }

        [DefaultValue("")]
        public string? Remarks { get; set; }

        [DefaultValue(false)]
        public bool? RGP { get; set; }
    }



    public class TRCPartRequest_Response : BaseResponseEntity
    {
        public TRCPartRequest_Response()
        {
            PartDetail = new List<TRCPartRequestDetails_Response>();
        }

        public string? RequestNumber { get; set; }

        public DateTime? RequestDate { get; set; }

        public int? EngineerId { get; set; }

        public string? EngineerName { get; set; }

        public decimal? TotalRequestedQty { get; set; }

        public string? Remarks { get; set; }

        public int? StatusId { get; set; }

        public string? StatusName { get; set; }

        public bool? IsActive { get; set; }

        public List<TRCPartRequestDetails_Response> PartDetail { get; set; }
    }

    public class TRCPartRequestDetails_Response 
    {
        public int Id { get; set; }
        //public int? EngineerId { get; set; }
        //public string EngineerName { get; set; }
        public int? RequestId { get; set; }
        public string? RequestNumber { get; set; }
        public int? SpareCategoryId { get; set; }
        public string? SpareCategory { get; set; }
        public int? ProductMakeId { get; set; }
        public string? ProductMake { get; set; }
        public int? BMSMakeId { get; set; }
        public string? BMSMake { get; set; }
        public int? SpareDetailsId { get; set; }
        public string? SpareDesc { get; set; }
        public string? UniqueCode { get; set; }
        public int? UOMId { get; set; }
        public string? UOMName { get; set; }
        public int? TypeOfBMSId { get; set; }
        public string? TypeOfBMS { get; set; }
        public decimal? AvailableQty { get; set; }
        public decimal? RequiredQty { get; set; }
        public string? Remarks { get; set; }
        public bool? RGP { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    #region TRC Import/Export

    public class TRCPartRequest_ImportData
    {
        public string? EngineerName { get; set; }
        public string? PartCode { get; set; }
        public string? UOM { get; set; }
        public string? TypeOfBMS { get; set; }
        public string? Quantity { get; set; }
        public string? RGP { get; set; }
        public string? Remarks { get; set; }
    }

    public class TRCPartRequest_ImportDataValidation
    {
        public string? EngineerName { get; set; }
        public string? PartCode { get; set; }
        public string? UOM { get; set; }
        public string? TypeOfBMS { get; set; }
        public string? Quantity { get; set; }
        public string? RGP { get; set; }
        public string? Remarks { get; set; }
        public string? ValidationMessage { get; set; }
    }

    #endregion
}
