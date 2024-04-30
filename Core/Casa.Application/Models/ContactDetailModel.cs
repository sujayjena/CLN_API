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
    public class ContactDetailModel
    {
    }

    public class ContactDetail_Search : BaseSearchEntity
    {
        public long RefId { get; set; }

        public string? RefType { get; set; }
    }

    public class ContactDetail_Request : BaseEntity
    {
        [JsonIgnore]
        public int RefId { get; set; }

        [JsonIgnore]
        public string? RefType { get; set; }

        public string ContactName { get; set; }

        public string MobileNumber { get; set; }

        public string EmailId { get; set; }

        [JsonIgnore]
        public string? AadharCardImageFileName { get; set; }

        public string? AadharCardOriginalFileName { get; set; }

        public string? AadharCardImage_Base64 { get; set; }

        [JsonIgnore]
        public string? PanCardImageFileName { get; set; }

        public string? PanCardOriginalFileName { get; set; }

        public string? PanCardImage_Base64 { get; set; }

        public bool? IsDefault { get; set; }

        public bool? IsActive { get; set; }
    }

    public class ContactDetail_Response : BaseResponseEntity
    {
        public int? RefId { get; set; }

        public string RefType { get; set; }

        public string ContactName { get; set; }

        public string MobileNumber { get; set; }

        public string EmailId { get; set; }

        public string? AadharCardImageFileName { get; set; }

        public string? AadharCardOriginalFileName { get; set; }

        public string? AadharCardImageURL { get; set; }

        public string? PanCardImageFileName { get; set; }

        public string? PanCardOriginalFileName { get; set; }

        public string? PanCardImageURL { get; set; }

        public bool? IsDefault { get; set; }

        public bool? IsActive { get; set; }
    }
}
