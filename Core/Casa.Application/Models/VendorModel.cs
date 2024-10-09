using CLN.Domain.Entities;
using CLN.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class VendorModel
    {
    }

    public class Vendor_Request : BaseEntity
    {
        public int? VendorTypeId { get; set; }
        public string? VendorName { get; set; }

        public string? LandLineNumber { get; set; }

        public string? MobileNumber { get; set; }

        public string? EmailId { get; set; }

        public string? SpecialRemark { get; set; }

        public string? PanCardNo { get; set; }

        [JsonIgnore]
        public string? PanCardImageFileName { get; set; }

        public string? PanCardImage_Base64 { get; set; }

        public string? PanCardOriginalFileName { get; set; }

        public string? GSTNo { get; set; }

        [JsonIgnore]
        public string? GSTImageFileName { get; set; }

        public string? GSTImage_Base64 { get; set; }

        public string? GSTImageOriginalFileName { get; set; }

        public bool? IsActive { get; set; }

        public ContactDetail_Request ContactDetail { get; set; }

        public Address_Request AddressDetail { get; set; }
    }

    public class VendorList_Response : BaseResponseEntity
    {
        public int? VendorTypeId { get; set; }
        public string? VendorType { get; set; }
        public string? VendorName { get; set; }
        public string? LandLineNumber { get; set; }
        public string? MobileNumber { get; set; }
        public string? EmailId { get; set; }
        public string? SpecialRemark { get; set; }
        public string? ContactPerson { get; set; }
        public string? PanCardNo { get; set; }
        public string? PanCardImage { get; set; }
        public string? PanCardOriginalFileName { get; set; }
        public string? PanCardImageURL { get; set; }
        public string? GSTNo { get; set; }
        public string? GSTImage { get; set; }
        public string? GSTImageOriginalFileName { get; set; }
        public string? GSTImageURL { get; set; }
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

    public class Vendor_Response : BaseResponseEntity
    {
        public Vendor_Response()
        {
            ContactDetail = new ContactDetail_Response();
            AddressDetail = new Address_Response();
        }

        public int? VendorTypeId { get; set; }
        public string? VendorType { get; set; }
        public string? VendorName { get; set; }
        public string? LandLineNumber { get; set; }
        public string? MobileNumber { get; set; }
        public string? EmailId { get; set; }
        public string? SpecialRemark { get; set; }
        public string? PanCardNo { get; set; }
        public string? PanCardImage { get; set; }
        public string? PanCardOriginalFileName { get; set; }
        public string? PanCardImageURL { get; set; }
        public string? GSTNo { get; set; }
        public string? GSTImage { get; set; }
        public string? GSTImageOriginalFileName { get; set; }
        public string? GSTImageURL { get; set; }
        public bool? IsActive { get; set; }

        public ContactDetail_Response ContactDetail { get; set; }

        public Address_Response AddressDetail { get; set; }
    }



    public class VendorDetail_Search : BaseSearchEntity
    {
        public int? VendorId { get; set; }
    }

    public class VendorDetail_Request : BaseEntity
    {
        public int? VendorId { get; set; }
        public string ChargerSerial { get; set; }
        public string ChargerModel { get; set; }
        public string WarrantyPeriod { get; set; }
        public string ChargerName { get; set; }
    }

    public class VendorDetailList_Response : BaseResponseEntity
    {
        public int? VendorId { get; set; }
        public string VendorName { get; set; }
        public string ChargerSerial { get; set; }
        public string ChargerModel { get; set; }
        public string WarrantyPeriod { get; set; }
        public string ChargerName { get; set; }
    }

    #region Inverter Details
    public class InverterDetail_Request : BaseEntity
    {
        public int? VendorId { get; set; }
        public string InverterSerial { get; set; }
        public string InverterModel { get; set; }
        public string WarrantyPeriod { get; set; }
        public string InverterName { get; set; }
    }

    public class InverterDetailList_Response : BaseResponseEntity
    {
        public int? VendorId { get; set; }
        public string VendorName { get; set; }
        public string InverterSerial { get; set; }
        public string InverterModel { get; set; }
        public string WarrantyPeriod { get; set; }
        public string InverterName { get; set; }
    }
    #endregion

    #region Import and Download

    public class Vendor_ImportRequest
    {
        public IFormFile FileUpload { get; set; }
    }

    public class Vendor_ImportData
    {
        public string? VendorType { get; set; }
        public string? VendorName { get; set; }
        public string? LandLineNumber { get; set; }
        public string? MobileNumber { get; set; }
        public string? Email { get; set; }
        public string? PanCardNo { get; set; }
        public string? GSTNo { get; set; }
        public string? SpecialRemark { get; set; }
        public string? IsActive { get; set; }
    }

    public class Vendor_ImportDataValidation
    {
        public string? VendorType { get; set; }
        public string? VendorName { get; set; }
        public string? LandLineNumber { get; set; }
        public string? MobileNumber { get; set; }
        public string? Email { get; set; }
        public string? PanCardNo { get; set; }
        public string? GSTNo { get; set; }
        public string? SpecialRemark { get; set; }
        public string? IsActive { get; set; }
        public string ValidationMessage { get; set; }
    }

    #endregion

}
