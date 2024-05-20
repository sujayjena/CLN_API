using CLN.Domain.Entities;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    internal class ManageQCModel
    {
    }

    // Consignee details    
    public class ManageQC_Consignee_Request : BaseEntity
    {
        public ManageQC_Consignee_Request()
        {
            AccessoryList = new List<CustomerAccessory_Request>();
        }

        public int? ConsigneeTypeId { get; set; }

        public string ConsigneeName { get; set; }

        public string? ConsigneeCode { get; set; }

        public string ConsigneeMobileNumber { get; set; }

        [JsonIgnore]
        public int RefId { get; set; }

        public int? ConsigneeAddressId { get; set; }

        public string ConsigneeAddress1 { get; set; }

        public int? ConsigneeRegionId { get; set; }

        public int? ConsigneeStateId { get; set; }

        public int? ConsigneeDistrictId { get; set; }

        public int? ConsigneeCityId { get; set; }

        public string ConsigneePinCode { get; set; }

        public bool? IsActive { get; set; }

        public List<CustomerAccessory_Request> AccessoryList { get; set; }
    }

    public class ManageQC_Consignee_Response : BaseEntity
    {
        public ManageQC_Consignee_Response()
        {
            AccessoryList = new List<CustomerAccessory_Response>();
        }

        public int? ConsigneeTypeId { get; set; }

        public string ConsigneeName { get; set; }

        public string? ConsigneeCode { get; set; }

        public string ConsigneeMobileNumber { get; set; }

        [JsonIgnore]
        public int RefId { get; set; }

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

        public List<CustomerAccessory_Response> AccessoryList { get; set; }
    }

    public class ManageQCList_Response : BaseResponseEntity
    {
        public int? ConsigneeTypeId { get; set; }

        public string ConsigneeName { get; set; }

        public string ConsigneeCode { get; set; }

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
}
