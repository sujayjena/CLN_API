using CLN.Domain.Entities;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class ManageReport_Search : BaseSearchEntity
    {
        [DefaultValue(null)]
        public DateTime? FromDate { get; set; }

        [DefaultValue(null)]
        public DateTime? ToDate { get; set; }

        [DefaultValue("")]
        public string? TicketType { get; set; }

        [DefaultValue("")]
        public string? BranchId { get; set; }
        public int? CustomerId { get; set; }
        public int? ProductCategoryId { get; set; }
        public int? SegmentId { get; set; }
        public int? SubSegmentId { get; set; }
        public int? ProductModelId { get; set; }
    }

    public class Ticket_TRC_Report_Response : BaseEntity
    {
        public string? TicketType { get; set; }
        public string? TRCLocation { get; set; }
        public DateTime? TicketDate { get; set; }
        public string? TicketCreatedBy { get; set; }
        public string? TicketNumber { get; set; }
        public DateTime? TRCDate { get; set; }
        public string? TRCNumber { get; set; }
        public string? CustomerName { get; set; }
        public string? CallerName { get; set; }
        public string? CallerMobileNo { get; set; }
        public string? CallerRegionName { get; set; }
        public string? CallerStateName { get; set; }
        public string? CallerDistrictName { get; set; }
        public string? CallerCityName { get; set; }
        public string? ProductCategory { get; set; }
        public string? Segment { get; set; }
        public string? SubSegment { get; set; }
        public string? ProductModel { get; set; }
        public string? TypeOfBMS { get; set; }
        public bool? IsOldProduct { get; set; }
        public string? ProductSerialNumber { get; set; }
        public DateTime? DateofManufacturing { get; set; }
        public string? WarrantyStatus { get; set; }
        public string? ProbReportedByCust { get; set; }
        public string? ProblemObservedByEng { get; set; }
        public string? ProblemObservedByServiceEng { get; set; }
        public string? ProblemObservedByTRCEng { get; set; }
        public string? RectificationAction { get; set; }
        public string? TicketStatus { get; set; }
        public DateTime? ResolvedDate { get; set; }
        public DateTime? CSATDate { get; set; }
        public string? CSATStatus { get; set; }
        public decimal? CSATAverage { get; set; }
        public DateTime? ClosureDate { get; set; }
        public string? TicketAging { get; set; }
    }

    public class CustomerWiseReport_Response : BaseEntity
    {
        public string? CustomerName { get; set; }
        public string? ProductCategory { get; set; }
        public string? Segment { get; set; }
        public string? SubSegment { get; set; }
        public long? NoofIssue { get; set; }
        public long? OpenIssue { get; set; }
        public long? CloseIssue { get; set; }
    }

    public class CustomerSatisfactionReport_Response : BaseEntity
    {
        public string? TicketNumber { get; set; }
        public string? TRCNumber { get; set; }
        public string? ClosedBy { get; set; }
        public DateTime? ClosedDate { get; set; }
        public DateTime? CSATDate { get; set; }
        public string? OverallExperience { get; set; }
        public string? Satisfaction { get; set; }
        public string? CustomerService { get; set; }
        public string? Timeliness { get; set; }
        public string? Resolution { get; set; }
    }

    public class FTFReport_Response : BaseEntity
    {
        public DateTime? TicketDate { get; set; }
        public long? TotalRequest { get; set; }
        public long? ResolvedTickets { get; set; }
        public long? FTFRatePerct { get; set; }
        public long? CSATScore { get; set; }
    }

    public class LogisticSummaryReport_Response : BaseEntity
    {
        public string? TicketNumber { get; set; }
        public DateTime? TicketDate { get; set; }
        public string? TRCNumber { get; set; }
        public DateTime? TRCDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public string? ReceiveMode { get; set; }
        public string? DocumentNo { get; set; }
        public string? RegionName { get; set; }
        public string? StateName { get; set; }
        public string? DistrictName { get; set; }
        public string? CityName { get; set; }
        public string? CustomerName { get; set; }
        public string? ProductCategory { get; set; }
        public string? Segment { get; set; }
        public string? SubSegment { get; set; }
        public string? ProductModel { get; set; }
        public string? ProductSerialNumber { get; set; }
        public DateTime? DispatchedDate { get; set; }
        public string? DispatchStatus { get; set; }
        public string? DispatchMode { get; set; }
        public string? DispatchAddress { get; set; }
        public string? DispatchChallanNo { get; set; }
        public string? DispatchedDocketNo { get; set; }
        public string? CourierName { get; set; }
        public DateTime? CustomerReceivingDate { get; set; }
    }

    public class ExpenseReport_Response : BaseEntity
    {
        public string? TicketNumber { get; set; }
        public DateTime? TicketDate { get; set; }
        public string? TRCNumber { get; set; }
        public DateTime? TRCDate { get; set; }
        public string? TicketType { get; set; }
        public string? TRCLocation { get; set; }
        public string? ProductCategory { get; set; }
        public string? Segment { get; set; }
        public string? SubSegment { get; set; }
        public string? ProductModel { get; set; }
        public string? ProductSerialNumber { get; set; }
        public decimal? TotalPartPrice { get; set; }
        public decimal? TotalExpense { get; set; }
        public decimal? TotalCost { get; set; }
    }

    public class TicketActivityReport_Response : BaseResponseEntity
    {
        public string? TicketNumber { get; set; }
        public DateTime? TicketDate { get; set; }
        public TimeSpan? TicketTime { get; set; }
        public string? TRCNumber { get; set; }
        public DateTime? TRCDate { get; set; }
        public int? TicketPriorityId { get; set; }
        public string? TicketPriority { get; set; }
        public string? TicketSLADays { get; set; }
        public string? TicketSLAHours { get; set; }
        public string? TicketSLAMin { get; set; }
        public string? SLAStatus { get; set; }
        public string? TicketAging { get; set; }

        public int? CD_LoggingSourceId { get; set; }
        public string? CD_LoggingSourceChannel { get; set; }
        public int? CD_CallerTypeId { get; set; }
        public string? CD_CallerType { get; set; }
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
        public int? CD_ComplaintTypeId { get; set; }
        public string? CD_ComplaintType { get; set; }
        public bool? CD_IsOldProduct { get; set; }
        public int? CD_ProductSerialNumberId { get; set; }
        public string? CD_ProductSerialNumber { get; set; }
        public int? CD_CustomerTypeId { get; set; }
        public string? CustomerType { get; set; }
        public int? CD_CustomerNameId { get; set; }
        public string? CD_CustomerName { get; set; }
        public string? CD_CustomerMobile { get; set; }
        public string? CD_CustomerEmail { get; set; }
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
        public string  ? CD_SiteCustomerName { get; set; }
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

        public int? BD_BatteryBOMNumberId { get; set; }
        public string? BD_BatteryBOMNumber { get; set; }
        public int? BD_BatteryProductCategoryId { get; set; }
        public string? BD_ProductCategory { get; set; }
        public int? BD_BatterySegmentId { get; set; }
        public string? BD_Segment { get; set; }
        public int? BD_BatterySubSegmentId { get; set; }
        public string? BD_SubSegment { get; set; }
        public int? BD_BatteryProductModelId { get; set; }
        public string? BD_ProductModel { get; set; }
        public int? BD_BatteryCellChemistryId { get; set; }
        public string? BD_CellChemistry { get; set; }
        public DateTime? BD_DateofManufacturing { get; set; }
        public int? BD_ProbReportedByCustId { get; set; }
        public string? BD_ProbReportedByCust { get; set; }
        public string? BD_ProblemDescription { get; set; }
        public DateTime? BD_WarrantyStartDate { get; set; }
        public DateTime? BD_WarrantyEndDate { get; set; }
        public int? BD_WarrantyStatusId { get; set; }
        public string? BD_WarrantyStatus { get; set; }
        public int? BD_WarrantyTypeId { get; set; }
        public string? BD_WarrantyType { get; set; }

        public bool? BD_IsTrackingDeviceRequired { get; set; }
        public int? BD_TrackingDeviceId { get; set; }
        public string? BD_TrackingDeviceName { get; set; }
        public int? BD_MakeId { get; set; }
        public string? BD_MakeName { get; set; }
        public string? BD_DeviceID { get; set; }
        public string? BD_IMEINo { get; set; }
        public string? BD_SIMNo { get; set; }
        public int? BD_SIMProviderId { get; set; }
        public string? BD_SIMProviderName { get; set; }
        public int? BD_PlatformId { get; set; }
        public string? BD_PlatformName { get; set; }
        public int? BD_TechnicalSupportEnggId { get; set; }
        public string? BD_TechnicalSupportEngg { get; set; }

        public int? TSAD_Visual { get; set; }
        public string? TSAD_CurrentChargingValue { get; set; }
        public string? TSAD_CurrentDischargingValue { get; set; }
        public string? TSAD_BatteryTemperature { get; set; }
        public string? TSAD_BatterVoltage { get; set; }
        public string? TSAD_CellDiffrence { get; set; }
        public int? TSAD_ProtectionsId { get; set; }

        public string? TSAD_Protections { get; set; }
        public string? TSAD_CycleCount { get; set; }
        public int TSAD_ProblemObservedByEngId { get; set; }
        public string? TSAD_ProblemObservedByEng { get; set; }
        public string? TSAD_ProblemObservedDesc { get; set; }

        public string? TSAD_Gravity { get; set; }
        public string? TSAD_IP_VoltageAC { get; set; }
        public string? TSAD_IP_VoltageDC { get; set; }
        public string? TSAD_OutputAC { get; set; }
        public string? TSAD_Protection { get; set; }
        public int? TSAD_FanStatus { get; set; }
        public string? TSPD_AnyPhysicalDamage { get; set; }
        public string? TSPD_Other { get; set; }
        public bool? TSPD_IsWarrantyVoid { get; set; }
        public string? TSPD_ReasonforVoid { get; set; }
        public int? TSPD_TypeOfBMSId { get; set; }
        public string? TSPD_TypeOfBMS { get; set; }

        public int? TS_AbnormalNoise { get; set; }
        public int? TS_ConnectorDamage { get; set; }
        public int? TS_AnyBrunt { get; set; }
        public int? TS_PhysicalDamage { get; set; }
        public string? TS_ProblemRemark { get; set; }
        public string? TS_IPCurrentAC_A { get; set; }
        public string? TS_OutputCurrentDC_A { get; set; }
        public string? TS_OutputVoltageDC_V { get; set; }
        public int? TS_Type { get; set; }
        public int? TS_Heating { get; set; }
        public string? TS_OutputVoltageAC_V { get; set; }
        public string? TS_OutputCurrentAC_A { get; set; }
        public string? TS_IPCurrentDC_A { get; set; }
        public string? TS_SpecificGravityC2 { get; set; }
        public string? TS_SpecificGravityC3 { get; set; }
        public string? TS_SpecificGravityC4 { get; set; }
        public string? TS_SpecificGravityC5 { get; set; }
        public string? TS_SpecificGravityC6 { get; set; }

        public int? CP_Visual { get; set; }
        public string? CP_TerminalVoltage { get; set; }
        public int? CP_CommunicationWithBattery { get; set; }
        public int? CP_TerminalWire { get; set; }
        public string? CP_LifeCycle { get; set; }
        public string? CP_StringVoltageVariation { get; set; }
        public int? CP_BatteryParametersSetting { get; set; }
        public int? CP_Spare { get; set; }
        public int? CP_BMSStatus { get; set; }
        public int? CP_BMSType { get; set; }
        public string? CP_BatteryTemp { get; set; }
        public string? CP_BMSSerialNumber { get; set; }
        public string? CP_ProblemObserved { get; set; }
        public int? CP_ProblemObservedByEngId { get; set; }
        public string? CP_ProblemObservedByEng { get; set; }
        public bool? CP_IsWarrantyVoid { get; set; }
        public string? CP_ReasonForVoid { get; set; }

        public int? CC_BatteryRepairedOnSite { get; set; }
        public int? CC_BatteryRepairedToPlant { get; set; }

        public int? TSSP_SolutionProvider { get; set; }
        public int? TSSP_AllocateToServiceEnggId { get; set; }
        public string? TSSP_AllocateToServiceEngg { get; set; }
        public string? TSSP_Remarks { get; set; }
        public int? TSSP_BranchId { get; set; }
        public string? TSSP_BranchName { get; set; }
        public int? TSSP_RectificationActionId { get; set; }
        public string? TSSP_RectificationAction { get; set; }
        public string? TSSP_ResolutionSummary { get; set; }

        public bool? OV_IsCustomerAvailable { get; set; }
        public string? OV_EngineerName { get; set; }
        public string? OV_EngineerNumber { get; set; }
        public string? OV_CustomerName { get; set; }
        public string? OV_CustomerNameSecondary { get; set; }
        public string? OV_CustomerMobileNumber { get; set; }
        public string? OV_RequestOTP { get; set; }
        public string? OV_Signature { get; set; }

        public bool? IsReopen { get; set; }

        public int? RO_TSAD_Visual { get; set; }
        public string? RO_TSAD_CurrentChargingValue { get; set; }
        public string? RO_TSAD_CurrentDischargingValue { get; set; }
        public string? RO_TSAD_BatteryTemperature { get; set; }
        public string? RO_TSAD_BatterVoltage { get; set; }
        public string? RO_TSAD_CellDiffrence { get; set; }
        public int? RO_TSAD_ProtectionsId { get; set; }
        public string? RO_TSAD_Protections { get; set; }
        public string? RO_TSAD_CycleCount { get; set; }
        public int RO_TSAD_ProblemObservedByEngId { get; set; }
        public string? RO_TSAD_ProblemObservedByEng { get; set; }
        public string? RO_TSAD_ProblemObservedDesc { get; set; }
        public string? RO_TSAD_Gravity { get; set; }
        public string? RO_TSAD_IP_VoltageAC { get; set; }
        public string? RO_TSAD_IP_VoltageDC { get; set; }
        public string? RO_TSAD_OutputAC { get; set; }
        public string? RO_TSAD_Protection { get; set; }
        public int? RO_TSAD_FanStatus { get; set; }

        public string RO_TSPD_AnyPhysicalDamage { get; set; }
        public string RO_TSPD_Other { get; set; }
        public bool? RO_TSPD_IsWarrantyVoid { get; set; }
        public string? RO_TSPD_ReasonforVoid { get; set; }
        public int? RO_TSPD_TypeOfBMSId { get; set; }
        public string? RO_TSPD_TypeOfBMS { get; set; }
        public int? RO_BD_TechnicalSupportEnggId { get; set; }
        public string? RO_BD_TechnicalSupportEngg { get; set; }

        public int? RO_TS_AbnormalNoise { get; set; }
        public int? RO_TS_ConnectorDamage { get; set; }
        public int? RO_TS_AnyBrunt { get; set; }
        public int? RO_TS_PhysicalDamage { get; set; }
        public string? RO_TS_ProblemRemark { get; set; }
        public string? RO_TS_IPCurrentAC_A { get; set; }
        public string? RO_TS_OutputCurrentDC_A { get; set; }
        public string? RO_TS_OutputVoltageDC_V { get; set; }
        public int? RO_TS_Type { get; set; }
        public int? RO_TS_Heating { get; set; }
        public string? RO_TS_OutputVoltageAC_V { get; set; }
        public string? RO_TS_OutputCurrentAC_A { get; set; }
        public string? RO_TS_IPCurrentDC_A { get; set; }
        public string? RO_TS_SpecificGravityC2 { get; set; }
        public string? RO_TS_SpecificGravityC3 { get; set; }
        public string? RO_TS_SpecificGravityC4 { get; set; }
        public string? RO_TS_SpecificGravityC5 { get; set; }
        public string? RO_TS_SpecificGravityC6 { get; set; }

        public int? RO_CP_Visual { get; set; }
        public string? RO_CP_TerminalVoltage { get; set; }
        public int? RO_CP_CommunicationWithBattery { get; set; }
        public int? RO_CP_TerminalWire { get; set; }
        public string? RO_CP_LifeCycle { get; set; }
        public string? RO_CP_StringVoltageVariation { get; set; }
        public int? RO_CP_BatteryParametersSetting { get; set; }
        public int? RO_CP_Spare { get; set; }
        public int? RO_CP_BMSStatus { get; set; }
        public int? RO_CP_BMSType { get; set; }
        public string? RO_CP_BatteryTemp { get; set; }
        public string? RO_CP_BMSSerialNumber { get; set; }
        public string? RO_CP_ProblemObserved { get; set; }
        public int? RO_CP_ProblemObservedByEngId { get; set; }
        public string? RO_CP_ProblemObservedByEng { get; set; }
        public bool? RO_CP_IsWarrantyVoid { get; set; }
        public string? RO_CP_ReasonForVoid { get; set; }

        public int? RO_CC_BatteryRepairedOnSite { get; set; }
        public int? RO_CC_BatteryRepairedToPlant { get; set; }

        public int? RO_TSSP_SolutionProvider { get; set; }
        public int? RO_TSSP_AllocateToServiceEnggId { get; set; }
        public string? RO_TSSP_AllocateToServiceEngg { get; set; }
        public string? RO_TSSP_Remarks { get; set; }
        public int? RO_TSSP_BranchId { get; set; }
        public string? RO_TSSP_BranchName { get; set; }
        public int? RO_TSSP_RectificationActionId { get; set; }
        public string? RO_TSSP_RectificationAction { get; set; }
        public string? RO_TSSP_ResolutionSummary { get; set; }

        public bool? RO_OV_IsCustomerAvailable { get; set; }
        public string? RO_OV_EngineerName { get; set; }
        public string? RO_OV_EngineerNumber { get; set; }
        public string? RO_OV_CustomerName { get; set; }
        public string? RO_OV_CustomerNameSecondary { get; set; }
        public string? RO_OV_CustomerMobileNumber { get; set; }
        public string? RO_OV_RequestOTP { get; set; }
        public string? RO_OV_Signature { get; set; }

        public int? TicketStatusId { get; set; }
        public string? TicketStatus { get; set; }
        public int? TicketStatusSequenceNo { get; set; }
        public int? TRC_EngineerId { get; set; }
        public string? TRC_Engineer { get; set; }

        public bool? IsResolvedWithoutOTP { get; set; }
        public bool? IsClosedWithoutOTP { get; set; }
        public bool? IsActive { get; set; }
    }

    public class InMaterialConsumptionReport_Response : BaseEntity
    {
        public int? SpareDetailsId { get; set; }
        public string? UniqueCode { get; set; }
        public string? SpareDesc { get; set; }
        public int? UOMId { get; set; }
        public string? UOMName { get; set; }
        public decimal? MinQty { get; set; }
        public decimal? AvailableQty { get; set; }
        public int? StatusId { get; set; }
        public string? StatusName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public string? CreatorName { get; set; }
    }
}
