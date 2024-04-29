﻿using CLN.Domain.Entities;
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
    public class ManageTicket_Search : BaseSearchEntity
    {

    }

    public class ManageTicket_Request : BaseEntity
    {
        public ManageTicket_Request()
        {
            PartDetail = new List<ManageTicketPartDetails_Request>();
        }

        public string TicketNumber { get; set; }

        public DateTime? TicketdDate { get; set; }

        public string? TicketdTime { get; set; }


        public int? CD_LoggingSourceId { get; set; }

        public string CD_CallerName { get; set; }

        public string CD_Caller { get; set; }


        public int? CD_CallerAddressId { get; set; }

        public string CD_CallerAddress1 { get; set; }

        public int? CD_CallerRegionId { get; set; }

        public int? CD_CallerStateId { get; set; }

        public int? CD_CallerDistrictId { get; set; }

        public int? CD_CallerCityId { get; set; }

        public string CD_CallerPinCode { get; set; }


        public bool? CD_IsSiteAddressSameAsCaller { get; set; }

        public string CD_BatterySerialNumber { get; set; }

        public int? CD_CustomerTypeId { get; set; }

        public string CD_CustomerName { get; set; }


        public int? CD_CustomerAddressId { get; set; }

        public string CD_CustomerAddress1 { get; set; }

        public int? CD_CustomerRegionId { get; set; }

        public int? CD_CustomerStateId { get; set; }

        public int? CD_CustomerDistrictId { get; set; }

        public int? CD_CustomerCityId { get; set; }

        public string CD_CustomerPinCode { get; set; }


        public string CD_SiteCustomerName { get; set; }

        public string CD_SiteContactName { get; set; }

        public string CD_SitContactMobile { get; set; }


        public int? CD_SiteAddressId { get; set; }

        public string CD_SiteCustomerAddress1 { get; set; }

        public int? CD_SiteCustomerRegionId { get; set; }

        public int? CD_SiteCustomerStateId { get; set; }

        public int? CD_SiteCustomerDistrictId { get; set; }

        public int? CD_SiteCustomerCityId { get; set; }

        public string CD_SiteCustomerPinCode { get; set; }


        public string BD_BatteryPartCode { get; set; }

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


        public int? TS_Visual { get; set; }

        public string TS_BatterTerminalVoltage { get; set; }

        public string TS_LifeCycle { get; set; }

        public string TS_StringVoltageVariation { get; set; }

        public string TS_BatteryTemperature { get; set; }

        public string TS_CurrentDischargingValue { get; set; }

        public int? TS_ProtectionsId { get; set; }

        public string TS_CurrentChargingValue { get; set; }

        public int? TS_AllocateToServiceEnggId { get; set; }


        public int? CP_Visual { get; set; }

        public string CP_VisualImageFileName { get; set; }

        public string CP_VisualImageOriginalFileName { get; set; }

        public string? CP_VisualImage_Base64 { get; set; }


        public string CP_TerminalVoltage { get; set; }

        public int? CP_CommunicationWithBattery { get; set; }

        public int? CP_TerminalWire { get; set; }

        public string CP_TerminalWireImageFileName { get; set; }

        public string CP_TerminalWireImageOriginalFileName { get; set; }

        public string? CP_TerminalWireImage_Base64 { get; set; }

        public string CP_LifeCycle { get; set; }

        public string CP_StringVoltageVariation { get; set; }

        public int? CP_BatteryParametersSetting { get; set; }

        public string CP_BatteryParametersSettingImageFileName { get; set; }

        public string CP_BatteryParametersSettingImageOriginalFileName { get; set; }

        public string? CP_BatteryParametersSettingImage_Base64 { get; set; }

        public string CP_BMSSoftware { get; set; }

        public int? CP_Spare { get; set; }


        public int? CC_BatteryRepairedOnSite { get; set; }

        public int? CC_BatteryRepairedToPlant { get; set; }


        public bool? OV_IsCustomerAvailable { get; set; }

        public string OV_EngineerName { get; set; }

        public string OV_EngineerNumber { get; set; }

        public string OV_CustomerName { get; set; }

        public string OV_CustomerNameSecondary { get; set; }

        public string OV_CustomerMobileNumber { get; set; }

        public string OV_RequestOTP { get; set; }

        public string OV_Signature { get; set; }

        [DefaultValue(false)]
        public bool? OV_IsMoveToTRC { get; set; }

        public int? TicketdStatusId { get; set; }

        public bool? IsActive { get; set; }

        public List<ManageTicketPartDetails_Request> PartDetail { get; set; }

        //[DefaultValue(false)]
        //public bool IsTechnicalSupportAddUpdate { get; set; }

        //[DefaultValue(false)]
        //public bool IsAllocatedServiceEnggAddUpdate { get; set; }

        //public ManageTicketCaller_Request CallerDetail { get; set; }
        //public ManageTicketCustomer_Request BatteryCustomerDetail { get; set; }

        //public ManageTicketSiteCustomer_Request SiteCustomerDetail { get; set; }
        //public ManageTicketBatteryDetail_Request BatteryDetail { get; set; }
        //public ManageTicketDetailsTechnicalSupport_Request TechnicalSupportDetail { get; set; }
        //public ManageTicketDetailsCheckingParameters_Request CheckingParametersDetail { get; set; }
        //public ManageTicketDetailsConclusion_Request ConclusionDetail { get; set; }
    }

    public class ManageTicketPartDetails_Request : BaseEntity
    {
        [JsonIgnore]
        public int? TicketId { get; set; }

        public string SparePartNo { get; set; }

        public string PartDescription { get; set; }

        public int? Quantity { get; set; }

        public int? PartStatusId { get; set; }
    }

    public class ManageTicketList_Response : BaseResponseEntity
    {
        public string TicketNumber { get; set; }
        public DateTime? TicketDate { get; set; }
        public TimeSpan? TicketTime { get; set; }


        public int? CD_LoggingSourceId { get; set; }
        public string CD_LoggingSourceChannel { get; set; }
        public string CD_CallerName { get; set; }
        public string CD_Caller { get; set; }
        public string CD_BatterySerialNumber { get; set; }
        public int? CD_CustomerTypeId { get; set; }
        public string CustomerType { get; set; }
        public string CD_CustomerName { get; set; }
        public string CD_SiteCustomerName { get; set; }
        public string CD_SiteContactName { get; set; }
        public string CD_SitContactMobile { get; set; }

        public string BD_BatteryPartCode { get; set; }
        public int? BD_BatterySegmentId { get; set; }
        public string BD_Segment { get; set; }
        public int? BD_BatterySubSegmentId { get; set; }
        public string BD_SubSegment { get; set; }
        public int? BD_BatterySpecificationId { get; set; }
        public string BD_BatterySpecification { get; set; }
        public int? BD_BatteryCellChemistryId { get; set; }
        public string BD_CellChemistry { get; set; }
        public DateTime? BD_DateofManufacturing { get; set; }
        public int? BD_ProbReportedByCustId { get; set; }
        public string BD_ProbReportedByCust { get; set; }
        public DateTime? BD_WarrantyStartDate { get; set; }
        public DateTime? BD_WarrantyEndDate { get; set; }
        public int? BD_WarrantyStatusId { get; set; }
        public string BD_WarrantyStatus { get; set; }
        public int? BD_TechnicalSupportEnggId { get; set; }
        public string BD_TechnicalSupportEngg { get; set; }

        public int? TS_Visual { get; set; }
        public string TS_BatterTerminalVoltage { get; set; }
        public string TS_LifeCycle { get; set; }
        public string TS_StringVoltageVariation { get; set; }
        public string TS_BatteryTemperature { get; set; }
        public string TS_CurrentDischargingValue { get; set; }
        public int? TS_ProtectionsId { get; set; }
        public string TS_Protections { get; set; }
        public string TS_CurrentChargingValue { get; set; }
        public int? TS_AllocateToServiceEnggId { get; set; }
        public string TS_AllocateToServiceEngg { get; set; }

        public int? CP_Visual { get; set; }
        public string CP_VisualImageFileName { get; set; }
        public string CP_VisualImageOriginalFileName { get; set; }
        public string CP_TerminalVoltage { get; set; }
        public int? CP_CommunicationWithBattery { get; set; }
        public int? CP_TerminalWire { get; set; }
        public string CP_TerminalWireImageFileName { get; set; }
        public string CP_TerminalWireImageOriginalFileName { get; set; }
        public string CP_LifeCycle { get; set; }
        public string CP_StringVoltageVariation { get; set; }
        public int? CP_BatteryParametersSetting { get; set; }
        public string CP_BatteryParametersSettingImageFileName { get; set; }
        public string CP_BatteryParametersSettingImageOriginalFileName { get; set; }
        public string CP_BMSSoftware { get; set; }
        public int? CP_Spare { get; set; }

        public int? CC_BatteryRepairedOnSite { get; set; }
        public int? CC_BatteryRepairedToPlant { get; set; }

        public bool? OV_IsCustomerAvailable { get; set; }
        public string OV_EngineerName { get; set; }
        public string OV_EngineerNumber { get; set; }
        public string OV_CustomerName { get; set; }
        public string OV_CustomerNameSecondary { get; set; }
        public string OV_CustomerMobileNumber { get; set; }
        public string OV_RequestOTP { get; set; }
        public string OV_Signature { get; set; }
        public bool? OV_IsMoveToTRC { get; set; }

        public int? TicketStatusId { get; set; }
        public string TicketStatus { get; set; }
        public bool? IsActive { get; set; }
    }

    public class ManageTicketDetail_Response : BaseResponseEntity
    {
        public ManageTicketDetail_Response()
        {
            PartDetails = new List<ManageTicketPartDetails_Response>();
        }

        public string TicketNumber { get; set; }
        public DateTime? TicketDate { get; set; }
        public TimeSpan? TicketTime { get; set; }


        public int? CD_LoggingSourceId { get; set; }
        public string CD_LoggingSourceChannel { get; set; }
        public string CD_CallerName { get; set; }
        public string CD_Caller { get; set; }

        public int? CD_CallerAddressId { get; set; }
        public string CD_CallerAddress1 { get; set; }
        public int? CD_CallerRegionId { get; set; }
        public string CD_CallerRegionName { get; set; }
        public int? CD_CallerStateId { get; set; }
        public string CD_CallerStateName { get; set; }
        public int? CD_CallerDistrictId { get; set; }
        public string CD_CallerDistrictName { get; set; }
        public int? CD_CallerCityId { get; set; }
        public string CD_CallerCityName { get; set; }
        public string CD_CallerPinCode { get; set; }

        public bool? CD_IsSiteAddressSameAsCaller { get; set; }
        public string CD_BatterySerialNumber { get; set; }
        public int? CD_CustomerTypeId { get; set; }
        public string CustomerType { get; set; }
        public string CD_CustomerName { get; set; }

        public int? CD_CustomerAddressId { get; set; }
        public string CD_CustomerAddress1 { get; set; }
        public int? CD_CustomerRegionId { get; set; }
        public string CD_CustomerRegionName { get; set; }
        public int? CD_CustomerStateId { get; set; }
        public string CD_CustomerStateName { get; set; }
        public int? CD_CustomerDistrictId { get; set; }
        public string CD_CustomerDistrictName { get; set; }
        public int? CD_CustomerCityId { get; set; }
        public string CD_CustomerCityName { get; set; }
        public string CD_CustomerPinCode { get; set; }

        public string CD_SiteCustomerName { get; set; }
        public string CD_SiteContactName { get; set; }
        public string CD_SitContactMobile { get; set; }

        public int? CD_SiteAddressId { get; set; }
        public string CD_SiteCustomerAddress1 { get; set; }
        public int? CD_SiteCustomerRegionId { get; set; }
        public string CD_SiteCustomerRegionName { get; set; }
        public int? CD_SiteCustomerStateId { get; set; }
        public string CD_SiteCustomerStateName { get; set; }
        public int? CD_SiteCustomerDistrictId { get; set; }
        public string CD_SiteCustomerDistrictName { get; set; }
        public int? CD_SiteCustomerCityId { get; set; }
        public string CD_SiteCustomerCityName { get; set; }
        public string CD_SiteCustomerPinCode { get; set; }


        public string BD_BatteryPartCode { get; set; }
        public int? BD_BatterySegmentId { get; set; }
        public string BD_Segment { get; set; }
        public int? BD_BatterySubSegmentId { get; set; }
        public string BD_SubSegment { get; set; }
        public int? BD_BatterySpecificationId { get; set; }
        public string BD_BatterySpecification { get; set; }
        public int? BD_BatteryCellChemistryId { get; set; }
        public string BD_CellChemistry { get; set; }
        public DateTime? BD_DateofManufacturing { get; set; }
        public int? BD_ProbReportedByCustId { get; set; }
        public string BD_ProbReportedByCust { get; set; }
        public DateTime? BD_WarrantyStartDate { get; set; }
        public DateTime? BD_WarrantyEndDate { get; set; }
        public int? BD_WarrantyStatusId { get; set; }
        public string BD_WarrantyStatus { get; set; }
        public int? BD_TechnicalSupportEnggId { get; set; }
        public string BD_TechnicalSupportEngg { get; set; }


        public int? TS_Visual { get; set; }
        public string TS_BatterTerminalVoltage { get; set; }
        public string TS_LifeCycle { get; set; }
        public string TS_StringVoltageVariation { get; set; }
        public string TS_BatteryTemperature { get; set; }
        public string TS_CurrentDischargingValue { get; set; }
        public int? TS_ProtectionsId { get; set; }
        public string TS_Protections { get; set; }
        public string TS_CurrentChargingValue { get; set; }
        public int? TS_AllocateToServiceEnggId { get; set; }
        public string TS_AllocateToServiceEngg { get; set; }


        public int? CP_Visual { get; set; }
        public string CP_VisualImageFileName { get; set; }
        public string CP_VisualImageOriginalFileName { get; set; }
        public string CP_VisualImageURL { get; set; }

        public string CP_TerminalVoltage { get; set; }
        public int? CP_CommunicationWithBattery { get; set; }
        public int? CP_TerminalWire { get; set; }
        public string CP_TerminalWireImageFileName { get; set; }
        public string CP_TerminalWireImageOriginalFileName { get; set; }
        public string CP_TerminalWireImageURL { get; set; }

        public string CP_LifeCycle { get; set; }
        public string CP_StringVoltageVariation { get; set; }
        public int? CP_BatteryParametersSetting { get; set; }
        public string CP_BatteryParametersSettingImageFileName { get; set; }
        public string CP_BatteryParametersSettingImageOriginalFileName { get; set; }
        public string CP_BatteryParametersSettingImageURL { get; set; }

        public string CP_BMSSoftware { get; set; }
        public int? CP_Spare { get; set; }


        public int? CC_BatteryRepairedOnSite { get; set; }
        public int? CC_BatteryRepairedToPlant { get; set; }


        public bool? OV_IsCustomerAvailable { get; set; }
        public string OV_EngineerName { get; set; }
        public string OV_EngineerNumber { get; set; }
        public string OV_CustomerName { get; set; }
        public string OV_CustomerNameSecondary { get; set; }
        public string OV_CustomerMobileNumber { get; set; }
        public string OV_RequestOTP { get; set; }
        public string OV_Signature { get; set; }

        [DefaultValue(false)]
        public bool? OV_IsMoveToTRC { get; set; }


        public int? TicketStatusId { get; set; }
        public string TicketStatus { get; set; }
        public bool? IsActive { get; set; }

        public List<ManageTicketPartDetails_Response> PartDetails { get; set; }
    }

    public class ManageTicketPartDetails_Response : BaseEntity
    {
        [JsonIgnore]
        public int? TicketId { get; set; }

        public string SparePartNo { get; set; }

        public string PartDescription { get; set; }

        public int? Quantity { get; set; }

        public int? PartStatusId { get; set; }
    }
}