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
        public string? EnquiryNumber { get; set; }
        public DateTime? EnquiryDate { get; set; }
        public string? EnquiryTime { get; set; }

        public int? CD_LoggingSourceId { get; set; }

        public string? CD_CallerName { get; set; }

        public string? CD_CallerMobile { get; set; }

        public string? CD_CallerEmailId { get; set; }


        public int? CD_CallerAddressId { get; set; }

        public string? CD_CallerAddress1 { get; set; }

        public int? CD_CallerRegionId { get; set; }

        public int? CD_CallerStateId { get; set; }

        public int? CD_CallerDistrictId { get; set; }

        public int? CD_CallerCityId { get; set; }

        public string? CD_CallerPinCode { get; set; }

        public string? CD_CallerRemarks { get; set; }

        public bool? CD_IsSiteAddressSameAsCaller { get; set; }

        public string? CD_BatterySerialNumber { get; set; }

        public int? CD_CustomerTypeId { get; set; }

        public string? CD_CustomerName { get; set; }

        public string? CD_CustomerMobile { get; set; }


        public int? CD_CustomerAddressId { get; set; }

        public string? CD_CustomerAddress1 { get; set; }

        public int? CD_CustomerRegionId { get; set; }

        public int? CD_CustomerStateId { get; set; }

        public int? CD_CustomerDistrictId { get; set; }

        public int? CD_CustomerCityId { get; set; }

        public string? CD_CustomerPinCode { get; set; }


        public string? CD_SiteCustomerName { get; set; }

        public string? CD_SiteContactName { get; set; }

        public string? CD_SitContactMobile { get; set; }


        public int? CD_SiteAddressId { get; set; }

        public string? CD_SiteCustomerAddress1 { get; set; }

        public int? CD_SiteCustomerRegionId { get; set; }

        public int? CD_SiteCustomerStateId { get; set; }

        public int? CD_SiteCustomerDistrictId { get; set; }

        public int? CD_SiteCustomerCityId { get; set; }

        public string? CD_SiteCustomerPinCode { get; set; }


        public string? BD_BatteryPartCode { get; set; }

        public int? BD_BatterySegmentId { get; set; }

        public int? BD_BatterySubSegmentId { get; set; }

        public int? BD_BatterySpecificationId { get; set; }

        public int? BD_BatteryCellChemistryId { get; set; }

        public DateTime? BD_DateofManufacturing { get; set; }

        public int? BD_ProbReportedByCustId { get; set; }

        public DateTime? BD_WarrantyStartDate { get; set; }

        public DateTime? BD_WarrantyEndDate { get; set; }

        public int? BD_WarrantyStatusId { get; set; }

        public int? BD_TechnicalSupportEnggId { get; set; }

        [DefaultValue(false)]
        public bool? IsConvertToTicket { get; set; }

        public bool? IsActive { get; set; }
    }

    public class ManageEnquiry_Response : BaseResponseEntity
    {
        public string? EnquiryNumber { get; set; }
        public DateTime? EnquiryDate { get; set; }
        public TimeSpan? EnquiryTime { get; set; }

        public int? CD_LoggingSourceId { get; set; }
        public string? CD_SourceChannel { get; set; }
        public string? CD_CallerName { get; set; }
        public string? CD_CallerMobile { get; set; }
        public string? CD_CallerEmailId { get; set; }

        public int? CD_CallerAddressId { get; set; }
        public string? CD_CallerAddress1 { get; set; }
        public int? CD_CallerRegionId { get; set; }
        public string? CD_CallerRegionName { get; set; }
        public int? CD_CallerStateId { get; set; }
        public string? CD_CallerStateName { get; set; }
        public int? CD_CallerDistrictId { get; set; }
        public string? CD_CallerDistrictName { get; set; }
        public int? CD_CallerCityId { get; set; }
        public string? CD_CallerCityName { get; set; }
        public string? CD_CallerPinCode { get; set; }

        public string? CD_CallerRemarks { get; set; }
        public bool? CD_IsSiteAddressSameAsCaller { get; set; }
        public string? CD_BatterySerialNumber { get; set; }
        public int? CD_CustomerTypeId { get; set; }
        public string? CD_CustomerType { get; set; }
        public string? CD_CustomerName { get; set; }
        public string? CD_CustomerMobile { get; set; }

        public int? CD_CustomerAddressId { get; set; }
        public string? CD_CustomerAddress1 { get; set; }
        public int? CD_CustomerRegionId { get; set; }
        public string? CD_CustomerRegionName { get; set; }
        public int? CD_CustomerStateId { get; set; }
        public string? CD_CustomerStateName { get; set; }
        public int? CD_CustomerDistrictId { get; set; }
        public string? CD_CustomerDistrictName { get; set; }
        public int? CD_CustomerCityId { get; set; }
        public string? CD_CustomerCityName { get; set; }
        public string? CD_CustomerPinCode { get; set; }

        public string? CD_SiteCustomerName { get; set; }
        public string? CD_SiteContactName { get; set; }
        public string? CD_SitContactMobile { get; set; }

        public int? CD_SiteAddressId { get; set; }
        public string? CD_SiteCustomerAddress1 { get; set; }
        public int? CD_SiteCustomerRegionId { get; set; }
        public string? CD_SiteCustomerRegionName { get; set; }
        public int? CD_SiteCustomerStateId { get; set; }
        public string? CD_SiteCustomerStateName { get; set; }
        public int? CD_SiteCustomerDistrictId { get; set; }
        public string? CD_SiteCustomerDistrictName { get; set; }
        public int? CD_SiteCustomerCityId { get; set; }
        public string? CD_SiteCustomerCityName { get; set; }
        public string? CD_SiteCustomerPinCode { get; set; }
        public string? BD_BatteryPartCode { get; set; }

        public int? BD_BatterySegmentId { get; set; }
        public string? BD_Segment { get; set; }
        public int? BD_BatterySubSegmentId { get; set; }
        public string? BD_SubSegment { get; set; }
        public int? BD_BatterySpecificationId { get; set; }
        public string? BD_ProductSpeces { get; set; }
        public int? BD_BatteryCellChemistryId { get; set; }
        public string? BD_CellChemistry { get; set; }
        public DateTime? BD_DateofManufacturing { get; set; }
        public int? BD_ProbReportedByCustId { get; set; }
        public string? BD_ProblemReported { get; set; }
        public DateTime? BD_WarrantyStartDate { get; set; }
        public DateTime? BD_WarrantyEndDate { get; set; }
        public int? BD_WarrantyStatusId { get; set; }
        public string? BD_WarrantyStatus { get; set; }
        public int? BD_TechnicalSupportEnggId { get; set; }
        public string? BD_TechnicalSupportEngg { get; set; }

        [DefaultValue(false)]
        public bool? IsConvertToTicket { get; set; }

        public bool? IsActive { get; set; }
    }
}
