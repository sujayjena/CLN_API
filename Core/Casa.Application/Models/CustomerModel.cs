using CLN.Domain.Entities;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
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
        public int? CustomerTypeId { get; set; }

        public string? CustomerName { get; set; }

        public string? CustomerCode { get; set; }

        public string? LandLineNumber { get; set; }

        public string? MobileNumber { get; set; }

        public string? EmailId { get; set; }

        public string? Website { get; set; }

        public string? Remark { get; set; }

        public string? RefParty { get; set; }

        [JsonIgnore]
        public string? GSTImageFileName { get; set; }

        public string? GSTImage_Base64 { get; set; }

        public string? GSTImageOriginalFileName { get; set; }

        [JsonIgnore]
        public string? PanCardImageFileName { get; set; }

        public string? PanCardImage_Base64 { get; set; }

        public string? PanCardOriginalFileName { get; set; }

        public bool? IsActive { get; set; }

        public ContactDetail_Request ContactDetail { get; set; }

        public Address_Request AddressDetail { get; set; }
    }

    public class CustomerList_Response : BaseResponseEntity
    {
        public int? Id { get; set; }

        public int? CustomerTypeId { get; set; }

        public string? CustomerType { get; set; }

        public string? CustomerName { get; set; }

        public string? CustomerCode { get; set; }

        public string? LandLineNumber { get; set; }

        public string? MobileNumber { get; set; }

        public string? EmailId { get; set; }

        public string? Website { get; set; }

        public string? Remark { get; set; }

        public string? RefParty { get; set; }

        public string? GSTImage { get; set; }

        public string? GSTImageOriginalFileName { get; set; }

        public string? GSTImageURL { get; set; }

        public string? PanCardImage { get; set; }

        public string? PanCardOriginalFileName { get; set; }

        public string? PanCardImageURL { get; set; }

        public string? Address1 { get; set; }

        public string? Address2 { get; set; }

        public int? RegionId { get; set; }

        public string? RegionName { get; set; }

        public int? StateId { get; set; }

        public string? StateName { get; set; }

        public int? DistrictId { get; set; }

        public string? DistrictName { get; set; }

        public int? CityId { get; set; }

        public string? CityName { get; set; }

        public bool? IsActive { get; set; }
    }

    public class CustomerDetail_Response : BaseResponseEntity
    {
        public CustomerDetail_Response()
        {
            ContactDetail = new ContactDetail_Response();
            AddressDetail = new Address_Response();
        }

        public int? CustomerTypeId { get; set; }

        public string? CustomerName { get; set; }

        public string? CustomerCode { get; set; }

        public string? LandLineNumber { get; set; }

        public string? MobileNumber { get; set; }

        public string? EmailId { get; set; }

        public string? Website { get; set; }

        public string? Remark { get; set; }

        public string? RefParty { get; set; }

        public string? GSTImage { get; set; }

        public string? GSTImageOriginalFileName { get; set; }

        public string? GSTImageURL { get; set; }

        public string? PanCardImage { get; set; }

        public string? PanCardOriginalFileName { get; set; }

        public string? PanCardImageURL { get; set; }

        public bool? IsActive { get; set; }

        public ContactDetail_Response ContactDetail { get; set; }

        public Address_Response AddressDetail { get; set; }
    }
}
