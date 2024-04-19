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

        public bool? IsBuyerSameAsConsignee { get; set; }

        public bool? IsActive { get; set; }

        public ManageSales_Buyer_Request BuyerDetail { get; set; }
        public List<ManageSales_Accessory_Request> AccessoryDetail { get; set; }
    }

    public class ManageSales_Buyer_Request : BaseEntity
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





    public class ManageSales_Response : BaseResponseEntity
    {
        public int? Id { get; set; }
        public int? CompanyTypeId { get; set; }
        public string CompanyType { get; set; }
        public string CompanyName { get; set; }
        public string LandLineNumber { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
        public string Website { get; set; }
        public string Remark { get; set; }
        public string RefParty { get; set; }
        public string GSTImage { get; set; }
        public string GSTImageOriginalFileName { get; set; }
        public string GSTImageURL { get; set; }
        public string PanCardImage { get; set; }
        public string PanCardOriginalFileName { get; set; }
        public string PanCardImageURL { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int? RegionId { get; set; }
        public string RegionName { get; set; }
        public int? StateId { get; set; }
        public string StateName { get; set; }
        public int? DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int? CityId { get; set; }
        public string CityName { get; set; }
        public bool? IsActive { get; set; }
    }
}
