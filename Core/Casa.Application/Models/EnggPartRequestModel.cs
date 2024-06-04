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
    public class EnggPartRequest_Search : BaseSearchEntity
    {
        public int? EngineerId { get; set; }
        public int? StatusId { get; set; }
    }

    public class EnggPartRequestDetails_Search : BaseSearchEntity
    {
        public int? RequestId { get; set; }
    }

    public class EnggPartRequest_Request : BaseEntity
    {
        public EnggPartRequest_Request()
        {
            PartDetail = new List<EnggPartRequestDetails_Request>();
        }

        public string? RequestNumber { get; set; }

        public DateTime? RequestDate { get; set; }

        public int? EngineerId { get; set; }

        public string? Remarks { get; set; }

        public int? StatusId { get; set; }

        public bool? IsActive { get; set; }

        public List<EnggPartRequestDetails_Request> PartDetail { get; set; }
    }

    public class EnggPartRequestDetails_Request : BaseEntity
    {
        public int? RequestId { get; set; }

        public int? SpareId { get; set; }

        public int? UOMId { get; set; }

        public int? TypeOfBMSId { get; set; }

        public int? AvailableQty { get; set; }

        public int? RequiredQty { get; set; }

        [DefaultValue("")]
        public string? Remarks { get; set; }

        [DefaultValue(false)]
        public bool? RGP { get; set; }
    }



    public class EnggPartRequest_Response : BaseResponseEntity
    {
        public EnggPartRequest_Response()
        {
            PartDetail = new List<EnggPartRequestDetails_Response>();
        }

        public string? RequestNumber { get; set; }

        public DateTime? RequestDate { get; set; }

        public int? EngineerId { get; set; }

        public string? EngineerName { get; set; }

        public string? Remarks { get; set; }

        public int? StatusId { get; set; }

        public string? StatusName { get; set; }

        public bool? IsActive { get; set; }

        public List<EnggPartRequestDetails_Response> PartDetail { get; set; }
    }

    public class EnggPartRequestDetails_Response
    {
        public int? Id { get; set; }
        public int? RequestId { get; set; }
        public string? RequestNumber { get; set; }
        public int? SpareId { get; set; }
        public string? SpareDesc { get; set; }
        public string? UniqueCode { get; set; }
        public int? UOMId { get; set; }
        public string? UOMName { get; set; }
        public int? TypeOfBMSId { get; set; }
        public string? TypeOfBMS { get; set; }
        public int? AvailableQty { get; set; }
        public int? RequiredQty { get; set; }
        public string? Remarks { get; set; }
        [DefaultValue(false)]
        public bool? RGP { get; set; }
    }
}
