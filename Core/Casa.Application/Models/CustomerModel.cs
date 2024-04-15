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
    public class CustomerModel
    {
    }

    public class Customer_Request : BaseEntity
    {
        public int? CompanyTypeId { get; set; }

        public string CompanyName { get; set; }

        public string LandLineNumber { get; set; }

        public string MobileNumber { get; set; }

        public string EmailId { get; set; }

        public string Website { get; set; }

        public string Remark { get; set; }

        public string RefParty { get; set; }

        [JsonIgnore]
        public string GSTImageFileName { get; set; }

        public string? GSTImage_Base64 { get; set; }

        public string GSTImageOriginalFileName { get; set; }

        [JsonIgnore]
        public string PanCardImageFileName { get; set; }

        public string PanCardImage_Base64 { get; set; }

        public string PanCardOriginalFileName { get; set; }

        public int? AddressId { get; set; }

        public bool? IsActive { get; set; }

        public CustomerContactDetail_Request ContactDetail { get; set; }

        public Address_Request AddressDetail { get; set; }
    }

    public class Customer_Response : BaseResponseEntity
    {
        public int? CompanyTypeId { get; set; }

        public string CompanyName { get; set; }

        public string LandLineNumber { get; set; }

        public string MobileNumber { get; set; }

        public string EmailId { get; set; }

        public string Website { get; set; }

        public string Remark { get; set; }

        public string RefParty { get; set; }

        public string GSTImageFileName { get; set; }

        public string GSTImageURL { get; set; }

        public string GSTImageOriginalFileName { get; set; }

        public string PanCardImageFileName { get; set; }

        public string PanCardImageURL { get; set; }

        public string PanCardOriginalFileName { get; set; }

        public int? AddressId { get; set; }

        public bool? IsActive { get; set; }
    }


    public class CustomerContactDetail_Search : BaseSearchEntity
    {
        public long CustomerId { get; set; }
    }

    public class CustomerContactDetail_Request : BaseEntity
    {
        public int? CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string MobileNumber { get; set; }

        public string EmailId { get; set; }

        [JsonIgnore]
        public string AadharCardImageFileName { get; set; }

        public string AadharCardOriginalFileName { get; set; }

        public string? AadharCardImage_Base64 { get; set; }

        [JsonIgnore]
        public string PanCardImageFileName { get; set; }

        public string PanCardOriginalFileName { get; set; }

        public string? PanCardImage_Base64 { get; set; }

        public bool? IsDefault { get; set; }

        public bool? IsActive { get; set; }
    }

    public class CustomerContactDetail_Response : BaseResponseEntity
    {
        public int? CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string MobileNumber { get; set; }

        public string EmailId { get; set; }

        public string AadharCardImageFileName { get; set; }

        public string AadharCardOriginalFileName { get; set; }

        public string? AadharCardImageURL { get; set; }

        public string PanCardImageFileName { get; set; }

        public string PanCardOriginalFileName { get; set; }

        public string? PanCardImageURL { get; set; }

        public bool? IsDefault { get; set; }

        public bool? IsActive { get; set; }
    }

    public class CustomerAddress_Search : BaseSearchEntity
    {
        public long CustomerId { get; set; }
    }
}
