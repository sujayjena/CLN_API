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
    internal class ManageSalesModel
    {
    }

    // Consignee details    
    public class ManageSales_Consignee_Request : BaseEntity
    {
        public ManageSales_Consignee_Request()
        {
            AccessoryDetail = new List<ManageSales_Accessory_Request>();
        }

        public int? ConsigneeTypeId { get; set; }

        public string ConsigneeName { get; set; }

        public string ConsigneeMobileNumber { get; set; }

        public int? ConsigneeAddressId { get; set; }

        public bool? IsBuyerSameAsConsignee { get; set; }

        public int AddressId { get; set; }

        [JsonIgnore]
        public int RefId { get; set; }

        public string Address1 { get; set; }

        public int? RegionId { get; set; }

        public int? StateId { get; set; }

        public int? DistrictId { get; set; }

        public int? CityId { get; set; }

        public string PinCode { get; set; }

        public bool? IsActive { get; set; }

        public ManageSales_Buyer_Request BuyerDetail { get; set; }
        public List<ManageSales_Accessory_Request> AccessoryDetail { get; set; }
    }

    public class ManageSales_Buyer_Request
    {
        public int? BuyerTypeId { get; set; }

        public string BuyerName { get; set; }

        public string MobileNumber { get; set; }

        public int AddressId { get; set; }

        [JsonIgnore]
        public int RefId { get; set; }

        public string Address1 { get; set; }

        public int? RegionId { get; set; }

        public int? StateId { get; set; }

        public int? DistrictId { get; set; }

        public int? CityId { get; set; }

        public string PinCode { get; set; }
    }

    public class ManageSales_Accessory_Request : BaseEntity
    {
        [JsonIgnore]
        public int? CustomerId { get; set; }

        public int? AccessoryId { get; set; }

        public string AccessoryName { get; set; }

        public int? Quantity { get; set; }

        public bool? IsActive { get; set; }
    }

    public class ManageSalesList_Response : BaseResponseEntity
    {
        public string BuyerName { get; set; }

        public string BuyerMobileNumber { get; set; }

        public int? AddressId { get; set; }

        public string BuyerAddress1 { get; set; }

        public int? BuyerRegionId { get; set; }

        public string BuyerRegionName { get; set; }

        public int? BuyerStateId { get; set; }

        public string BuyerStateName { get; set; }

        public int? BuyerDistrictId { get; set; }

        public string BuyerDistrictName { get; set; }

        public int? BuyerCityId { get; set; }

        public string BuyerCityName { get; set; }

        public string BuyerPinCode { get; set; }

        public string ConsigneeName { get; set; }

        public string ConsigneeMobileNumber { get; set; }

        public bool? IsActive { get; set; }
    }

    public class ManageSalesDetail_Response : BaseResponseEntity
    {
        public int? BuyerTypeId { get; set; }

        public string BuyerType { get; set; }

        public string BuyerName { get; set; }

        public string BuyerMobileNumber { get; set; }

        public string BuyerAddress1 { get; set; }

        public int? BuyerRegionId { get; set; }

        public string BuyerRegionName { get; set; }

        public int? BuyerStateId { get; set; }

        public string BuyerStateName { get; set; }

        public int? BuyerDistrictId { get; set; }

        public string BuyerDistrictName { get; set; }

        public int? BuyerCityId { get; set; }

        public string BuyerCityName { get; set; }

        public string BuyerPinCode { get; set; }

        public bool? IsBuyerSameAsConsignee { get; set; }

        public int? ConsigneeTypeId { get; set; }

        public string ConsigneeName { get; set; }

        public string ConsigneeMobileNumber { get; set; }

        public int? ConsigneeAddressId { get; set; }

        public string ConsigneeAddress1 { get; set; }

        public int? ConsigneeRegionId { get; set; }

        public string ConsigneeRegionName { get; set; }

        public int? ConsigneeStateId { get; set; }

        public string ConsigneeStateName { get; set; }

        public int? ConsigneeDistrictId { get; set; }

        public string ConsigneeDistrictName { get; set; }

        public int? ConsigneeCityId { get; set; }

        public string ConsigneeCityName { get; set; }

        public string ConsigneePinCode { get; set; }

        public bool? IsActive { get; set; }
    }

    public class ManageSales_Accessory_Response : BaseEntity
    {
        public int? CustomerId { get; set; }

        public int? AccessoryId { get; set; }

        public string AccessoryName { get; set; }

        public int? Quantity { get; set; }

        public bool? IsActive { get; set; }
    }
}
