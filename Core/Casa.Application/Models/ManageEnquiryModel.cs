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
    public class ManageEnquiry_Search : BaseSearchEntity
    {
    }

    public class ManageEnquiry_Request : BaseEntity
    {
        public string EnquiryNumber { get; set; }

        public string CallerName { get; set; }

        public string CallerMobile { get; set; }

        public string CallerEmailId { get; set; }

        //public string BatterySerialNumber { get; set; }

        public int? AddressId { get; set; }

        public string Address1 { get; set; }

        public int? RegionId { get; set; }

        public int? StateId { get; set; }

        public int? DistrictId { get; set; }

        public int? CityId { get; set; }

        public string PinCode { get; set; }

        public string Remarks { get; set; }

        public int? StatusId { get; set; }

        [DefaultValue(false)]
        public bool? IsConvertToTicket { get; set; }

        public bool? IsActive { get; set; }
    }

    public class ManageEnquiry_Response : BaseResponseEntity
    {
        public string EnquiryNumber { get; set; }

        public string CallerName { get; set; }

        public string CallerMobile { get; set; }

        public string CallerEmailId { get; set; }

        //public string BatterySerialNumber { get; set; }

        public int? AddressId { get; set; }

        public string Address1 { get; set; }

        public int? RegionId { get; set; }

        public string RegionName { get; set; }

        public int? StateId { get; set; }

        public string StateName { get; set; }

        public int? DistrictId { get; set; }

        public string DistrictName { get; set; }

        public int? CityId { get; set; }

        public string CityName { get; set; }

        public string PinCode { get; set; }

        public string Remarks { get; set; }

        public int? StatusId { get; set; }

        public bool? IsConvertToTicket { get; set; }

        public bool? IsActive { get; set; }
    }
}
