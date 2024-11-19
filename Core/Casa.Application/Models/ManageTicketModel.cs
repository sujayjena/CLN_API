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
    public class ManageTicket_Search : BaseSearchEntity
    {
        [DefaultValue(null)]
        public DateTime? FromDate { get; set; }

        [DefaultValue(null)]
        public DateTime? ToDate { get; set; }

        public int? EmployeeId { get; set; }

        [DefaultValue(0)]
        public int? TicketStatusId { get; set; }

        [DefaultValue(0)]
        public int? Filter_TicketStatusId { get; set; }

        [DefaultValue(false)]
        public bool? IsChangeStatus_LogTicket { get; set; }

        [DefaultValue(0)]
        public int? IsEngineerType { get; set; }

        [DefaultValue(null)]
        public bool? IsReopen { get; set; }

        [DefaultValue("")]
        public string? BranchId { get; set; }

        //[DefaultValue("")]
        //public string? BranchRegionId { get; set; }

        //[DefaultValue("")]
        //public string? BranchStateId { get; set; }

        [DefaultValue("All")]
        public string? FilterType { get; set; }

        [DefaultValue("")]
        public string? SortBy { get; set; }

        [DefaultValue("")]
        public string? OrderBy { get; set; }
    }

    public class ManageTicketLogHistory_Search : BaseSearchEntity
    {
        [DefaultValue(0)]
        public int? TicketId { get; set; }

        [DefaultValue("All")]
        public string? FilterType { get; set; }
    }

    public class ManageTicket_Request : BaseEntity
    {
        public ManageTicket_Request()
        {
            PartDetail = new List<ManageTicketPartDetails_Request>();
        }

        public string? TicketNumber { get; set; }
        public DateTime? TicketDate { get; set; }
        public string? TicketTime { get; set; }
        public int? TicketPriorityId { get; set; }
        public string? TicketSLADays { get; set; }
        public string? TicketSLAHours { get; set; }
        public string? TicketSLAMin { get; set; }

        public int? CD_LoggingSourceId { get; set; }
        public int? CD_CallerTypeId { get; set; }
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
        [DefaultValue(false)]
        public bool? CD_IsSiteAddressSameAsCaller { get; set; }
        public int? CD_ComplaintTypeId { get; set; }
        [DefaultValue(false)]
        public bool? CD_IsOldProduct { get; set; }
        public int? CD_ProductSerialNumberId { get; set; }
        public string? CD_ProductSerialNumber { get; set; }
        public int? CD_CustomerTypeId { get; set; }
        public int? CD_CustomerNameId { get; set; }
        public string? CD_CustomerMobile { get; set; }
        public string? CD_CustomerEmail { get; set; }

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

        public int? BD_BatteryBOMNumberId { get; set; }
        public int? BD_BatteryProductCategoryId { get; set; }
        public int? BD_BatterySegmentId { get; set; }
        public int? BD_BatterySubSegmentId { get; set; }
        public int? BD_BatteryProductModelId { get; set; }
        public int? BD_BatteryCellChemistryId { get; set; }
        public DateTime? BD_DateofManufacturing { get; set; }
        public int? BD_ProbReportedByCustId { get; set; }
        public string? BD_ProblemDescription { get; set; }
        public DateTime? BD_WarrantyStartDate { get; set; }
        public DateTime? BD_WarrantyEndDate { get; set; }
        public int? BD_WarrantyStatusId { get; set; }
        public int? BD_WarrantyTypeId { get; set; }

        public bool? BD_IsTrackingDeviceRequired { get; set; }
        public int? BD_TrackingDeviceId { get; set; }
        public int? BD_MakeId { get; set; }
        public string? BD_DeviceID { get; set; }
        public string? BD_IMEINo { get; set; }
        public string? BD_SIMNo { get; set; }
        public int? BD_SIMProviderId { get; set; }
        public int? BD_PlatformId { get; set; }
        public int? BD_TechnicalSupportEnggId { get; set; }

        public int? TSAD_Visual { get; set; }
        public string? TSAD_VisualImageFileName { get; set; }
        public string? TSAD_VisualImageOriginalFileName { get; set; }
        public string? TSAD_VisualImage_Base64 { get; set; }

        public string? TSAD_CurrentChargingValue { get; set; }
        public string? TSAD_CurrentDischargingValue { get; set; }
        public string? TSAD_BatteryTemperature { get; set; }
        public string? TSAD_BatterVoltage { get; set; }
        public string? TSAD_CellDiffrence { get; set; }
        public int? TSAD_ProtectionsId { get; set; }
        public string? TSAD_CycleCount { get; set; }
        public int? TSAD_ProblemObservedByEngId { get; set; }
        public string? TSAD_ProblemObservedDesc { get; set; }

        public string? TSAD_Gravity { get; set; }
        public string? TSAD_IP_VoltageAC { get; set; }
        public string? TSAD_IP_VoltageDC { get; set; }
        public string? TSAD_OutputAC { get; set; }
        public string? TSAD_Protection { get; set; }
        public string? TSAD_AttachPhotoFileName { get; set; }
        public string? TSAD_AttachPhotoOriginalFileName { get; set; }
        public string? TSAD_AttachPhoto_Base64 { get; set; }
        public int? TSAD_FanStatus { get; set; }
        public string? TSAD_PhysicalPhotoFileName { get; set; }
        public string? TSAD_PhysicalPhotoOriginalFileName { get; set; }
        public string? TSAD_PhysicalPhoto_Base64 { get; set; }
        public string? TSAD_IssueImageFileName { get; set; }
        public string? TSAD_IssueImageOriginalFileName { get; set; }
        public string? TSAD_IssueImage_Base64 { get; set; }

        public string? TSPD_PhysicaImageFileName { get; set; }
        public string? TSPD_PhysicaImageOriginalFileName { get; set; }
        public string? TSPD_PhysicaImage_Base64 { get; set; }

        public string? TSPD_AnyPhysicalDamage { get; set; }
        public string? TSPD_Other { get; set; }
        [DefaultValue(false)]
        public bool? TSPD_IsWarrantyVoid { get; set; }
        public string? TSPD_ReasonforVoid { get; set; }
        public int? TSPD_TypeOfBMSId { get; set; }

        public int? TSSP_SolutionProvider { get; set; }
        public int? TSSP_AllocateToServiceEnggId { get; set; }
        public string? TSSP_Remarks { get; set; }
        public int? TSSP_BranchId { get; set; }
        public int? TSSP_RectificationActionId { get; set; }
        public string? TSSP_ResolutionSummary { get; set; }

        public int? CP_Visual { get; set; }
        public string? CP_VisualImageFileName { get; set; }
        public string? CP_VisualImageOriginalFileName { get; set; }
        public string? CP_VisualImage_Base64 { get; set; }

        public string? CP_TerminalVoltage { get; set; }
        public int? CP_CommunicationWithBattery { get; set; }
        public int? CP_TerminalWire { get; set; }
        public string? CP_TerminalWireImageFileName { get; set; }
        public string? CP_TerminalWireImageOriginalFileName { get; set; }
        public string? CP_TerminalWireImage_Base64 { get; set; }

        public string? CP_LifeCycle { get; set; }
        public string? CP_StringVoltageVariation { get; set; }
        public int? CP_BatteryParametersSetting { get; set; }
        public string? CP_BatteryParametersSettingImageFileName { get; set; }
        public string? CP_BatteryParametersSettingImageOriginalFileName { get; set; }
        public string? CP_BatteryParametersSettingImage_Base64 { get; set; }

        public int? CP_Spare { get; set; }
        public int? CP_BMSStatus { get; set; }
        public string? CP_BMSSoftwareImageFileName { get; set; }
        public string? CP_BMSSoftwareImageOriginalFileName { get; set; }
        public string? CP_BMSSoftwareImage_Base64 { get; set; }

        public int? CP_BMSType { get; set; }
        public string? CP_BatteryTemp { get; set; }
        public string? CP_BMSSerialNumber { get; set; }
        public string? CP_ProblemObserved { get; set; }
        public int? CP_ProblemObservedByEngId { get; set; }

        public int? CC_BatteryRepairedOnSite { get; set; }
        public int? CC_BatteryRepairedToPlant { get; set; }


        [DefaultValue(false)]
        public bool? OV_IsCustomerAvailable { get; set; }
        public string? OV_EngineerName { get; set; }
        public string? OV_EngineerNumber { get; set; }
        public string? OV_CustomerName { get; set; }
        public string? OV_CustomerNameSecondary { get; set; }
        public string? OV_CustomerMobileNumber { get; set; }
        public string? OV_RequestOTP { get; set; }
        public string? OV_Signature { get; set; }

        public string? TicketRemarks { get; set; }

        public int? EnquiryId { get; set; }
        public int? TicketStatusId { get; set; }
        public int? TRC_EngineerId { get; set; }

        [DefaultValue(null)]
        public bool? IsResolvedWithoutOTP { get; set; }

        [DefaultValue(null)]
        public bool? IsClosedWithoutOTP { get; set; }

        [DefaultValue(false)]
        public bool? IsReopen { get; set; }

        public int? RO_TSAD_Visual { get; set; }
        public string? RO_TSAD_VisualImageFileName { get; set; }
        public string? RO_TSAD_VisualImageOriginalFileName { get; set; }
        public string? RO_TSAD_VisualImage_Base64 { get; set; }
        public string? RO_TSAD_CurrentChargingValue { get; set; }
        public string? RO_TSAD_CurrentDischargingValue { get; set; }
        public string? RO_TSAD_BatteryTemperature { get; set; }
        public string? RO_TSAD_BatterVoltage { get; set; }
        public string? RO_TSAD_CellDiffrence { get; set; }
        public int? RO_TSAD_ProtectionsId { get; set; }
        public string? RO_TSAD_CycleCount { get; set; }
        public int? RO_TSAD_ProblemObservedByEngId { get; set; }
        public string? RO_TSAD_ProblemObservedDesc { get; set; }
        public string? RO_TSAD_Gravity { get; set; }
        public string? RO_TSAD_IP_VoltageAC { get; set; }
        public string? RO_TSAD_IP_VoltageDC { get; set; }
        public string? RO_TSAD_OutputAC { get; set; }
        public string? RO_TSAD_Protection { get; set; }
        public string? RO_TSAD_AttachPhotoFileName { get; set; }
        public string? RO_TSAD_AttachPhotoOriginalFileName { get; set; }
        public string? RO_TSAD_AttachPhoto_Base64 { get; set; }
        public int? RO_TSAD_FanStatus { get; set; }
        public string? RO_TSAD_PhysicalPhotoFileName { get; set; }
        public string? RO_TSAD_PhysicalPhotoOriginalFileName { get; set; }
        public string? RO_TSAD_PhysicalPhoto_Base64 { get; set; }
        public string? RO_TSAD_IssueImageFileName { get; set; }
        public string? RO_TSAD_IssueImageOriginalFileName { get; set; }
        public string? RO_TSAD_IssueImage_Base64 { get; set; }

        public string? RO_TSPD_PhysicaImageFileName { get; set; }
        public string? RO_TSPD_PhysicaImageOriginalFileName { get; set; }
        public string? RO_TSPD_PhysicaImage_Base64 { get; set; }
        public string? RO_TSPD_AnyPhysicalDamage { get; set; }
        public string? RO_TSPD_Other { get; set; }
        [DefaultValue(false)]
        public bool? RO_TSPD_IsWarrantyVoid { get; set; }
        public string? RO_TSPD_ReasonforVoid { get; set; }
        public int? RO_TSPD_TypeOfBMSId { get; set; }
        public int? RO_BD_TechnicalSupportEnggId { get; set; }
        public int? RO_TSSP_SolutionProvider { get; set; }
        public int? RO_TSSP_AllocateToServiceEnggId { get; set; }
        public string? RO_TSSP_Remarks { get; set; }
        public int? RO_TSSP_BranchId { get; set; }
        public int? RO_TSSP_RectificationActionId { get; set; }
        public string? RO_TSSP_ResolutionSummary { get; set; }

        public int? RO_CP_Visual { get; set; }
        public string? RO_CP_VisualImageFileName { get; set; }
        public string? RO_CP_VisualImageOriginalFileName { get; set; }
        public string? RO_CP_VisualImage_Base64 { get; set; }
        public string? RO_CP_TerminalVoltage { get; set; }
        public int? RO_CP_CommunicationWithBattery { get; set; }
        public int? RO_CP_TerminalWire { get; set; }
        public string? RO_CP_TerminalWireImageFileName { get; set; }
        public string? RO_CP_TerminalWireImageOriginalFileName { get; set; }
        public string? RO_CP_TerminalWireImage_Base64 { get; set; }
        public string? RO_CP_LifeCycle { get; set; }
        public string? RO_CP_StringVoltageVariation { get; set; }
        public int? RO_CP_BatteryParametersSetting { get; set; }
        public string? RO_CP_BatteryParametersSettingImageFileName { get; set; }
        public string? RO_CP_BatteryParametersSettingImageOriginalFileName { get; set; }
        public string? RO_CP_BatteryParametersSettingImage_Base64 { get; set; }
        public int? RO_CP_Spare { get; set; }
        public int? RO_CP_BMSStatus { get; set; }
        public string? RO_CP_BMSSoftwareImageFileName { get; set; }
        public string? RO_CP_BMSSoftwareImageOriginalFileName { get; set; }
        public string? RO_CP_BMSSoftwareImage_Base64 { get; set; }
        public int? RO_CP_BMSType { get; set; }
        public string? RO_CP_BatteryTemp { get; set; }
        public string? RO_CP_BMSSerialNumber { get; set; }
        public string? RO_CP_ProblemObserved { get; set; }
        public int? RO_CP_ProblemObservedByEngId { get; set; }

        public int? RO_CC_BatteryRepairedOnSite { get; set; }
        public int? RO_CC_BatteryRepairedToPlant { get; set; }

        [DefaultValue(false)]
        public bool? RO_OV_IsCustomerAvailable { get; set; }
        public string? RO_OV_EngineerName { get; set; }
        public string? RO_OV_EngineerNumber { get; set; }
        public string? RO_OV_CustomerName { get; set; }
        public string? RO_OV_CustomerNameSecondary { get; set; }
        public string? RO_OV_CustomerMobileNumber { get; set; }
        public string? RO_OV_RequestOTP { get; set; }
        public string? RO_OV_Signature { get; set; }


        [DefaultValue(false)]
        public bool? IsActive { get; set; }

        public List<ManageTicketPartDetails_Request> PartDetail { get; set; }
    }

    public class ManageTicketPartDetails_Request : BaseEntity
    {
        [JsonIgnore]
        public int? TicketId { get; set; }

        public int? SpareCategoryId { get; set; }

        public int? SpareDetailsId { get; set; }

        public int? Quantity { get; set; }

        public int? AvailableQty { get; set; }

        //public int? PartStatusId { get; set; }
    }

    public class ManageTicketList_Response : BaseResponseEntity
    {
        public string TicketNumber { get; set; }
        public DateTime? TicketDate { get; set; }
        public TimeSpan? TicketTime { get; set; }
        public int? TicketPriorityId { get; set; }
        public string TicketPriority { get; set; }
        public string TicketSLADays { get; set; }
        public string TicketSLAHours { get; set; }
        public string TicketSLAMin { get; set; }
        public string SLAStatus { get; set; }
        public string TicketAging { get; set; }

        public int? CD_LoggingSourceId { get; set; }
        public string CD_LoggingSourceChannel { get; set; }
        public int? CD_CallerTypeId { get; set; }
        public string CD_CallerType { get; set; }
        public string CD_CallerName { get; set; }
        public string CD_CallerMobile { get; set; }
        public string CD_CallerEmailId { get; set; }
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
        public string CD_CallerRemarks { get; set; }
        public bool? CD_IsSiteAddressSameAsCaller { get; set; }
        public int? CD_ComplaintTypeId { get; set; }
        public string CD_ComplaintType { get; set; }
        public bool? CD_IsOldProduct { get; set; }
        public int? CD_ProductSerialNumberId { get; set; }
        public string CD_ProductSerialNumber { get; set; }
        public int? CD_CustomerTypeId { get; set; }
        public string CustomerType { get; set; }
        public int? CD_CustomerNameId { get; set; }
        public string CD_CustomerName { get; set; }
        public string CD_CustomerMobile { get; set; }
        public string CD_CustomerEmail { get; set; }
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

        public int? BD_BatteryBOMNumberId { get; set; }
        public string BD_BatteryBOMNumber { get; set; }
        public int? BD_BatteryProductCategoryId { get; set; }
        public string BD_ProductCategory { get; set; }
        public int? BD_BatterySegmentId { get; set; }
        public string BD_Segment { get; set; }
        public int? BD_BatterySubSegmentId { get; set; }
        public string BD_SubSegment { get; set; }
        public int? BD_BatteryProductModelId { get; set; }
        public string BD_ProductModel { get; set; }
        public int? BD_BatteryCellChemistryId { get; set; }
        public string BD_CellChemistry { get; set; }
        public DateTime? BD_DateofManufacturing { get; set; }
        public int? BD_ProbReportedByCustId { get; set; }
        public DateTime? BD_WarrantyStartDate { get; set; }
        public DateTime? BD_WarrantyEndDate { get; set; }
        public int? BD_WarrantyStatusId { get; set; }
        public string BD_WarrantyStatus { get; set; }
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
        public string BD_TechnicalSupportEngg { get; set; }

        public int? TSSP_AllocateToServiceEnggId { get; set; }
        public string TSSP_AllocateToServiceEngg { get; set; }

        public int? EnquiryId { get; set; }
        public int? TicketStatusId { get; set; }
        public string TicketStatus { get; set; }
        public bool? IsDuplicateTicket { get; set; }
        public int? TRC_EngineerId { get; set; }
        public string TRC_Engineer { get; set; }

        public bool? IsResolvedWithoutOTP { get; set; }
        public bool? IsClosedWithoutOTP { get; set; }
        public bool? IsReopen { get; set; }

        public bool? IsActive { get; set; }

        public ManageTicketEngineerVisitHistory_Response manageTicketEngineerVisitHistory { get; set; }
    }

    public class ManageTicketLogHistory_Response : BaseResponseEntity
    {
        public ManageTicketLogHistory_Response()
        {
            PartDetails = new List<ManageTicketPartDetails_Response>();
        }
        public int? TicketId { get; set; }

        public string TicketNumber { get; set; }
        public DateTime? TicketDate { get; set; }
        public TimeSpan? TicketTime { get; set; }
        public int? TicketPriorityId { get; set; }
        public string TicketPriority { get; set; }
        public string TicketSLADays { get; set; }
        public string TicketSLAHours { get; set; }
        public string TicketSLAMin { get; set; }
        public string SLAStatus { get; set; }
        public string TicketAging { get; set; }

        public int? CD_LoggingSourceId { get; set; }
        public string CD_LoggingSourceChannel { get; set; }
        public int? CD_CallerTypeId { get; set; }
        public string CD_CallerType { get; set; }
        public string CD_CallerName { get; set; }
        public string CD_CallerMobile { get; set; }
        public string CD_CallerEmailId { get; set; }
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
        public string CD_CallerRemarks { get; set; }
        public bool? CD_IsSiteAddressSameAsCaller { get; set; }
        public int? CD_ComplaintTypeId { get; set; }
        public string CD_ComplaintType { get; set; }
        public bool? CD_IsOldProduct { get; set; }
        public int? CD_ProductSerialNumberId { get; set; }
        public string CD_ProductSerialNumber { get; set; }
        public int? CD_CustomerTypeId { get; set; }
        public string CustomerType { get; set; }
        public int? CD_CustomerNameId { get; set; }
        public string CD_CustomerName { get; set; }
        public string CD_CustomerMobile { get; set; }
        public string CD_CustomerEmail { get; set; }
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

        public int? BD_BatteryBOMNumberId { get; set; }
        public string BD_BatteryBOMNumber { get; set; }
        public int? BD_BatteryProductCategoryId { get; set; }
        public string BD_ProductCategory { get; set; }
        public int? BD_BatterySegmentId { get; set; }
        public string BD_Segment { get; set; }
        public int? BD_BatterySubSegmentId { get; set; }
        public string BD_SubSegment { get; set; }
        public int? BD_BatteryProductModelId { get; set; }
        public string BD_ProductModel { get; set; }
        public int? BD_BatteryCellChemistryId { get; set; }
        public string BD_CellChemistry { get; set; }
        public DateTime? BD_DateofManufacturing { get; set; }
        public int? BD_ProbReportedByCustId { get; set; }
        public string BD_ProbReportedByCust { get; set; }
        public string? BD_ProblemDescription { get; set; }
        public DateTime? BD_WarrantyStartDate { get; set; }
        public DateTime? BD_WarrantyEndDate { get; set; }
        public int? BD_WarrantyStatusId { get; set; }
        public string BD_WarrantyStatus { get; set; }
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
        public string BD_TechnicalSupportEngg { get; set; }

        public int? TSAD_Visual { get; set; }
        public string TSAD_VisualImageFileName { get; set; }
        public string TSAD_VisualImageOriginalFileName { get; set; }
        public string TSAD_VisualImageURL { get; set; }
        public string TSAD_CurrentChargingValue { get; set; }
        public string TSAD_CurrentDischargingValue { get; set; }
        public string TSAD_BatteryTemperature { get; set; }
        public string TSAD_BatterVoltage { get; set; }
        public string TSAD_CellDiffrence { get; set; }
        public int? TSAD_ProtectionsId { get; set; }

        public string TSAD_Protections { get; set; }
        public string TSAD_CycleCount { get; set; }
        public int TSAD_ProblemObservedByEngId { get; set; }
        public string TSAD_ProblemObservedByEng { get; set; }
        public string TSAD_ProblemObservedDesc { get; set; }

        public string? TSAD_Gravity { get; set; }
        public string? TSAD_IP_VoltageAC { get; set; }
        public string? TSAD_IP_VoltageDC { get; set; }
        public string? TSAD_OutputAC { get; set; }
        public string? TSAD_Protection { get; set; }
        public string? TSAD_AttachPhotoFileName { get; set; }
        public string? TSAD_AttachPhotoOriginalFileName { get; set; }
        public string? TSAD_AttachPhotoURL { get; set; }
        public int? TSAD_FanStatus { get; set; }
        public string? TSAD_PhysicalPhotoFileName { get; set; }
        public string? TSAD_PhysicalPhotoOriginalFileName { get; set; }
        public string? TSAD_PhysicalPhotoURL { get; set; }
        public string? TSAD_IssueImageFileName { get; set; }
        public string? TSAD_IssueImageOriginalFileName { get; set; }
        public string? TSAD_IssueImageURL { get; set; }

        public string TSPD_PhysicaImageFileName { get; set; }
        public string TSPD_PhysicaImageOriginalFileName { get; set; }
        public string TSPD_PhysicaImageURL { get; set; }
        public string TSPD_AnyPhysicalDamage { get; set; }
        public string TSPD_Other { get; set; }
        public bool? TSPD_IsWarrantyVoid { get; set; }
        public string? TSPD_ReasonforVoid { get; set; }
        public int? TSPD_TypeOfBMSId { get; set; }
        public string? TSPD_TypeOfBMS { get; set; }

        public int? TSSP_SolutionProvider { get; set; }
        public int? TSSP_AllocateToServiceEnggId { get; set; }
        public string TSSP_AllocateToServiceEngg { get; set; }
        public string? TSSP_Remarks { get; set; }
        public int? TSSP_BranchId { get; set; }
        public string? TSSP_BranchName { get; set; }
        public int? TSSP_RectificationActionId { get; set; }
        public string? TSSP_RectificationAction { get; set; }
        public string? TSSP_ResolutionSummary { get; set; }

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
        public int? CP_Spare { get; set; }
        public int? CP_BMSStatus { get; set; }
        public string CP_BMSSoftwareImageFileName { get; set; }
        public string CP_BMSSoftwareImageOriginalFileName { get; set; }
        public string CP_BMSSoftwareImageURL { get; set; }
        public int? CP_BMSType { get; set; }
        public string CP_BatteryTemp { get; set; }
        public string CP_BMSSerialNumber { get; set; }
        public string CP_ProblemObserved { get; set; }
        public int? CP_ProblemObservedByEngId { get; set; }
        public string? CP_ProblemObservedByEng { get; set; }

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

        public int? EnquiryId { get; set; }
        public int? TicketStatusId { get; set; }
        public string TicketStatus { get; set; }
        public int? TicketStatusSequenceNo { get; set; }
        public int? TRC_EngineerId { get; set; }
        public string TRC_Engineer { get; set; }

        public bool? IsResolvedWithoutOTP { get; set; }
        public bool? IsClosedWithoutOTP { get; set; }
        public bool? IsReopen { get; set; }

        public int? RO_TSAD_Visual { get; set; }
        public string RO_TSAD_VisualImageFileName { get; set; }
        public string RO_TSAD_VisualImageOriginalFileName { get; set; }
        public string RO_TSAD_VisualImageURL { get; set; }
        public string RO_TSAD_CurrentChargingValue { get; set; }
        public string RO_TSAD_CurrentDischargingValue { get; set; }
        public string RO_TSAD_BatteryTemperature { get; set; }
        public string RO_TSAD_BatterVoltage { get; set; }
        public string RO_TSAD_CellDiffrence { get; set; }
        public int? RO_TSAD_ProtectionsId { get; set; }
        public string RO_TSAD_Protections { get; set; }
        public string RO_TSAD_CycleCount { get; set; }
        public int RO_TSAD_ProblemObservedByEngId { get; set; }
        public string RO_TSAD_ProblemObservedByEng { get; set; }
        public string RO_TSAD_ProblemObservedDesc { get; set; }
        public string? RO_TSAD_Gravity { get; set; }
        public string? RO_TSAD_IP_VoltageAC { get; set; }
        public string? RO_TSAD_IP_VoltageDC { get; set; }
        public string? RO_TSAD_OutputAC { get; set; }
        public string? RO_TSAD_Protection { get; set; }
        public string? RO_TSAD_AttachPhotoFileName { get; set; }
        public string? RO_TSAD_AttachPhotoOriginalFileName { get; set; }
        public string? RO_TSAD_AttachPhotoURL { get; set; }
        public int? RO_TSAD_FanStatus { get; set; }
        public string? RO_TSAD_PhysicalPhotoFileName { get; set; }
        public string? RO_TSAD_PhysicalPhotoOriginalFileName { get; set; }
        public string? RO_TSAD_PhysicalPhotoURL { get; set; }
        public string? RO_TSAD_IssueImageFileName { get; set; }
        public string? RO_TSAD_IssueImageOriginalFileName { get; set; }
        public string? RO_TSAD_IssueImageURL { get; set; }

        public string RO_TSPD_PhysicaImageFileName { get; set; }
        public string RO_TSPD_PhysicaImageOriginalFileName { get; set; }
        public string RO_TSPD_PhysicaImageURL { get; set; }
        public string RO_TSPD_AnyPhysicalDamage { get; set; }
        public string RO_TSPD_Other { get; set; }
        public bool? RO_TSPD_IsWarrantyVoid { get; set; }
        public string? RO_TSPD_ReasonforVoid { get; set; }
        public int? RO_TSPD_TypeOfBMSId { get; set; }
        public string? RO_TSPD_TypeOfBMS { get; set; }
        public int? RO_BD_TechnicalSupportEnggId { get; set; }
        public string RO_BD_TechnicalSupportEngg { get; set; }
        public int? RO_TSSP_SolutionProvider { get; set; }
        public int? RO_TSSP_AllocateToServiceEnggId { get; set; }
        public string RO_TSSP_AllocateToServiceEngg { get; set; }
        public string? RO_TSSP_Remarks { get; set; }
        public int? RO_TSSP_BranchId { get; set; }
        public string? RO_TSSP_BranchName { get; set; }
        public int? RO_TSSP_RectificationActionId { get; set; }
        public string? RO_TSSP_RectificationAction { get; set; }
        public string? RO_TSSP_ResolutionSummary { get; set; }

        public int? RO_CP_Visual { get; set; }
        public string RO_CP_VisualImageFileName { get; set; }
        public string RO_CP_VisualImageOriginalFileName { get; set; }
        public string RO_CP_VisualImageURL { get; set; }
        public string RO_CP_TerminalVoltage { get; set; }
        public int? RO_CP_CommunicationWithBattery { get; set; }
        public int? RO_CP_TerminalWire { get; set; }
        public string RO_CP_TerminalWireImageOriginalFileName { get; set; }
        public string RO_CP_TerminalWireImageFileName { get; set; }
        public string RO_CP_TerminalWireImageURL { get; set; }
        public string RO_CP_LifeCycle { get; set; }
        public string RO_CP_StringVoltageVariation { get; set; }
        public int? RO_CP_BatteryParametersSetting { get; set; }
        public string RO_CP_BatteryParametersSettingImageFileName { get; set; }
        public string RO_CP_BatteryParametersSettingImageOriginalFileName { get; set; }
        public string RO_CP_BatteryParametersSettingImageURL { get; set; }
        public int? RO_CP_Spare { get; set; }
        public int? RO_CP_BMSStatus { get; set; }
        public string RO_CP_BMSSoftwareImageFileName { get; set; }
        public string RO_CP_BMSSoftwareImageOriginalFileName { get; set; }
        public string RO_CP_BMSSoftwareImageURL { get; set; }
        public int? RO_CP_BMSType { get; set; }
        public string RO_CP_BatteryTemp { get; set; }
        public string RO_CP_BMSSerialNumber { get; set; }
        public string RO_CP_ProblemObserved { get; set; }
        public int? RO_CP_ProblemObservedByEngId { get; set; }
        public string? RO_CP_ProblemObservedByEng { get; set; }

        public int? RO_CC_BatteryRepairedOnSite { get; set; }
        public int? RO_CC_BatteryRepairedToPlant { get; set; }

        public bool? RO_OV_IsCustomerAvailable { get; set; }
        public string RO_OV_EngineerName { get; set; }
        public string RO_OV_EngineerNumber { get; set; }
        public string RO_OV_CustomerName { get; set; }
        public string RO_OV_CustomerNameSecondary { get; set; }
        public string RO_OV_CustomerMobileNumber { get; set; }
        public string RO_OV_RequestOTP { get; set; }
        public string RO_OV_Signature { get; set; }

        public string? TRCNumber { get; set; }
        public DateTime? TRCDate { get; set; }
        public TimeSpan? TRCTime { get; set; }
        public int? RP_IsCLNOrCustomer { get; set; }
        public int? RP_ProblemReportedByCustId { get; set; }
        public string? RP_ProblemReportedByCust { get; set; }
        public string? RP_ProblemDecription { get; set; }
        public string? RP_ProductPackingPhotoOriginalFileName1 { get; set; }
        public string? RP_ProductPackingPhotoFileName1 { get; set; }
        public string? RP_ProductPackingPhotoURL1 { get; set; }
        public string? RP_ProductPackingPhotoOriginalFileName2 { get; set; }
        public string? RP_ProductPackingPhotoFileName2 { get; set; }
        public string? RP_ProductPackingPhotoURL2 { get; set; }
        public string? RP_ProductPackingPhotoOriginalFileName3 { get; set; }
        public string? RP_ProductPackingPhotoFileName3 { get; set; }
        public string? RP_ProductPackingPhotoURL3 { get; set; }
        public string? RP_DeliveryChallanPhotoOriginalFileName { get; set; }
        public string? RP_DeliveryChallanPhotoFileName { get; set; }
        public string? RP_DeliveryChallanPhotoURL { get; set; }
        public string? RP_ReservePickupFormatOriginalFileName { get; set; }
        public string? RP_ReservePickupFormatFileName { get; set; }
        public string? RP_ReservePickupFormatURL { get; set; }
        public bool? RP_IsReservePickupMailToLogistic { get; set; }
        public string? RP_DocketDetails { get; set; }
        public bool? RP_IsBatteryInTransit { get; set; }
        public string? DNV_DeliveryChallanPhotoOriginalFileName { get; set; }
        public string? DNV_DeliveryChallanPhotoFileName { get; set; }
        public string? DNV_DeliveryChallanPhotoURL { get; set; }
        public string? DNV_DebitNote { get; set; }
        public bool? DNV_IsHandoverToMainStore { get; set; }
        public string DNV_DeliveryChallanNumber { get; set; }
        public bool? DNV_IsBatteryReceivedInTRC { get; set; }
        public int? ATE_AssignedToEngineerId { get; set; }
        public string? ATE_AssignedToEngineer { get; set; }
        public int? II_Visual { get; set; }
        public bool? II_IsIntact { get; set; }
        public bool? II_IsTempered { get; set; }
        public string? II_TemperedOriginalFileName1 { get; set; }
        public string? II_TemperedFileName1 { get; set; }
        public string? II_TemperedURL1 { get; set; }
        public string? II_TemperedOriginalFileName2 { get; set; }
        public string? II_TemperedFileName2 { get; set; }
        public string? II_TemperedURL2 { get; set; }
        public string? II_TemperedOriginalFileName3 { get; set; }
        public string? II_TemperedFileName3 { get; set; }
        public string? II_TemperedURL3 { get; set; }
        public string? II_PhysicallyDamageOriginalFileName1 { get; set; }
        public string? II_PhysicallyDamageFileName1 { get; set; }
        public string? II_PhysicallyDamageURL1 { get; set; }
        public string? II_PhysicallyDamageOriginalFileName2 { get; set; }
        public string? II_PhysicallyDamageFileName2 { get; set; }
        public string? II_PhysicallyDamageURL2 { get; set; }
        public string? II_PhysicallyDamageOriginalFileName3 { get; set; }
        public string? II_PhysicallyDamageFileName3 { get; set; }
        public string? II_PhysicallyDamageURL3 { get; set; }
        public string? II_StringVoltageVariation { get; set; }
        public string? II_BatteryTerminalVoltage { get; set; }
        public string? II_LifeCycle { get; set; }
        public string? II_BatteryTemperature { get; set; }
        public string? II_BMSSpecification { get; set; }
        public string? II_BMSBrand { get; set; }
        public string? II_CellSpecification { get; set; }
        public string? II_CellBrand { get; set; }
        public string? II_BMSSerialNumber { get; set; }
        public int? II_CellChemistryId { get; set; }
        public string? II_CellChemistry { get; set; }
        public int? WS_IsWarrantyStatus { get; set; }
        public bool? WS_IsInformedToCustomerByEmail { get; set; }
        public bool? WS_IsCustomerAcceptance { get; set; }
        public bool? WS_IsPaymentClearance { get; set; }
        public string? WS_InvoiceOriginalFileName { get; set; }
        public string? WS_InvoiceFileName { get; set; }
        public string? WS_InvoiceURL { get; set; }
        public int? DA_ProblemObservedByEngId { get; set; }
        public string? DA_ProblemObservedByEng { get; set; }
        public string? DA_ProblemObservedDesc { get; set; }
        public int? DA_RectificationActionId { get; set; }
        public string? DA_RectificationAction { get; set; }
        public string? DA_ResolutionSummary { get; set; }
        public int? ATEFP_AssignedToEngineerId { get; set; }
        public string? ATEFP_AssignedToEngineer { get; set; }
        public DateTime? PI_BatteryReceivedDate { get; set; }
        public TimeSpan? PI_BatteryReceivedTime { get; set; }
        public DateTime? PI_PDIDoneDate { get; set; }
        public TimeSpan? PI_PDIDoneTime { get; set; }
        public int? PI_PDIDoneById { get; set; }
        public string? PI_PDIDoneByEngg { get; set; }
        public string? PI_SOCPercentageOriginalFileName { get; set; }
        public string? PI_SOCPercentageFileName { get; set; }
        public string PI_SOCPercentageURL { get; set; }
        public string? PI_VoltageDifference { get; set; }
        public string? PI_FinalVoltageOriginalFileName { get; set; }
        public string? PI_FinalVoltageFileName { get; set; }
        public string? PI_FinalVoltageURL { get; set; }
        public string? PIDD_DispatchedDeliveryChallan { get; set; }
        public DateTime? PIDD_DispatchedDate { get; set; }
        public string? PIDD_DispatchedCity { get; set; }
        public string? DDB_DispatchedDoneBy { get; set; }
        public string? DDB_DocketDetails { get; set; }
        public string? DDB_CourierName { get; set; }
        public DateTime? CRD_CustomerReceivingDate { get; set; }
        public int? TRCBranchId { get; set; }
        public string? TRCBranchName { get; set; }
        public int? TRCStatusId { get; set; }
        public string? TRCSStatus { get; set; }

        public bool? IsActive { get; set; }

        public List<ManageTicketPartDetails_Response> PartDetails { get; set; }
    }

    public class ManageTicketDetail_Response : BaseResponseEntity
    {
        public ManageTicketDetail_Response()
        {
            PartDetails = new List<ManageTicketPartDetails_Response>();
            TicketStatusLog = new List<ManageTicketStatusLog_Response>();
            TicketRemarksList = new List<ManageTicketRemarks_Response>();
        }

        public string TicketNumber { get; set; }
        public DateTime? TicketDate { get; set; }
        public TimeSpan? TicketTime { get; set; }
        public int? TicketPriorityId { get; set; }
        public string TicketPriority { get; set; }
        public string TicketSLADays { get; set; }
        public string TicketSLAHours { get; set; }
        public string TicketSLAMin { get; set; }
        public string SLAStatus { get; set; }
        public string TicketAging { get; set; }

        public int? CD_LoggingSourceId { get; set; }
        public string CD_LoggingSourceChannel { get; set; }
        public int? CD_CallerTypeId { get; set; }
        public string CD_CallerType { get; set; }
        public string CD_CallerName { get; set; }
        public string CD_CallerMobile { get; set; }
        public string CD_CallerEmailId { get; set; }
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
        public string CD_CallerRemarks { get; set; }
        public bool? CD_IsSiteAddressSameAsCaller { get; set; }
        public int? CD_ComplaintTypeId { get; set; }
        public string CD_ComplaintType { get; set; }
        public bool? CD_IsOldProduct { get; set; }
        public int? CD_ProductSerialNumberId { get; set; }
        public string CD_ProductSerialNumber { get; set; }
        public int? CD_CustomerTypeId { get; set; }
        public string CustomerType { get; set; }
        public int? CD_CustomerNameId { get; set; }
        public string CD_CustomerName { get; set; }
        public string CD_CustomerMobile { get; set; }
        public string CD_CustomerEmail { get; set; }
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

        public int? BD_BatteryBOMNumberId { get; set; }
        public string BD_BatteryBOMNumber { get; set; }
        public int? BD_BatteryProductCategoryId { get; set; }
        public string BD_ProductCategory { get; set; }
        public int? BD_BatterySegmentId { get; set; }
        public string BD_Segment { get; set; }
        public int? BD_BatterySubSegmentId { get; set; }
        public string BD_SubSegment { get; set; }
        public int? BD_BatteryProductModelId { get; set; }
        public string BD_ProductModel { get; set; }
        public int? BD_BatteryCellChemistryId { get; set; }
        public string BD_CellChemistry { get; set; }
        public DateTime? BD_DateofManufacturing { get; set; }
        public int? BD_ProbReportedByCustId { get; set; }
        public string BD_ProbReportedByCust { get; set; }
        public string? BD_ProblemDescription { get; set; }
        public DateTime? BD_WarrantyStartDate { get; set; }
        public DateTime? BD_WarrantyEndDate { get; set; }
        public int? BD_WarrantyStatusId { get; set; }
        public string BD_WarrantyStatus { get; set; }
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
        public string BD_TechnicalSupportEngg { get; set; }

        public int? TSAD_Visual { get; set; }
        public string TSAD_VisualImageFileName { get; set; }
        public string TSAD_VisualImageOriginalFileName { get; set; }
        public string TSAD_VisualImageURL { get; set; }
        public string TSAD_CurrentChargingValue { get; set; }
        public string TSAD_CurrentDischargingValue { get; set; }
        public string TSAD_BatteryTemperature { get; set; }
        public string TSAD_BatterVoltage { get; set; }
        public string TSAD_CellDiffrence { get; set; }
        public int? TSAD_ProtectionsId { get; set; }

        public string TSAD_Protections { get; set; }
        public string TSAD_CycleCount { get; set; }
        public int TSAD_ProblemObservedByEngId { get; set; }
        public string TSAD_ProblemObservedByEng { get; set; }
        public string TSAD_ProblemObservedDesc { get; set; }

        public string? TSAD_Gravity { get; set; }
        public string? TSAD_IP_VoltageAC { get; set; }
        public string? TSAD_IP_VoltageDC { get; set; }
        public string? TSAD_OutputAC { get; set; }
        public string? TSAD_Protection { get; set; }
        public string? TSAD_AttachPhotoFileName { get; set; }
        public string? TSAD_AttachPhotoOriginalFileName { get; set; }
        public string? TSAD_AttachPhotoURL { get; set; }
        public int? TSAD_FanStatus { get; set; }
        public string? TSAD_PhysicalPhotoFileName { get; set; }
        public string? TSAD_PhysicalPhotoOriginalFileName { get; set; }
        public string? TSAD_PhysicalPhotoURL { get; set; }
        public string? TSAD_IssueImageFileName { get; set; }
        public string? TSAD_IssueImageOriginalFileName { get; set; }
        public string? TSAD_IssueImageURL { get; set; }

        public string TSPD_PhysicaImageFileName { get; set; }
        public string TSPD_PhysicaImageOriginalFileName { get; set; }
        public string TSPD_PhysicaImageURL { get; set; }
        public string TSPD_AnyPhysicalDamage { get; set; }
        public string TSPD_Other { get; set; }
        public bool? TSPD_IsWarrantyVoid { get; set; }
        public string? TSPD_ReasonforVoid { get; set; }
        public int? TSPD_TypeOfBMSId { get; set; }
        public string? TSPD_TypeOfBMS { get; set; }

        public int? TSSP_SolutionProvider { get; set; }
        public int? TSSP_AllocateToServiceEnggId { get; set; }
        public string TSSP_AllocateToServiceEngg { get; set; }
        public string? TSSP_Remarks { get; set; }
        public int? TSSP_BranchId { get; set; }
        public string? TSSP_BranchName { get; set; }
        public int? TSSP_RectificationActionId { get; set; }
        public string? TSSP_RectificationAction { get; set; }
        public string? TSSP_ResolutionSummary { get; set; }

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
        public int? CP_Spare { get; set; }
        public int? CP_BMSStatus { get; set; }
        public string CP_BMSSoftwareImageFileName { get; set; }
        public string CP_BMSSoftwareImageOriginalFileName { get; set; }
        public string CP_BMSSoftwareImageURL { get; set; }
        public int? CP_BMSType { get; set; }
        public string CP_BatteryTemp { get; set; }
        public string CP_BMSSerialNumber { get; set; }
        public string CP_ProblemObserved { get; set; }
        public int? CP_ProblemObservedByEngId { get; set; }
        public string? CP_ProblemObservedByEng { get; set; }

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

        public string? TicketRemarks { get; set; }
        public int? EnquiryId { get; set; }
        public int? TicketStatusId { get; set; }
        public string TicketStatus { get; set; }
        public int? TicketStatusSequenceNo { get; set; }
        public int? TRC_EngineerId { get; set; }
        public string TRC_Engineer { get; set; }

        public bool? IsResolvedWithoutOTP { get; set; }
        public bool? IsClosedWithoutOTP { get; set; }
        public bool? IsReopen { get; set; }

        public int? RO_TSAD_Visual { get; set; }
        public string RO_TSAD_VisualImageFileName { get; set; }
        public string RO_TSAD_VisualImageOriginalFileName { get; set; }
        public string RO_TSAD_VisualImageURL { get; set; }
        public string RO_TSAD_CurrentChargingValue { get; set; }
        public string RO_TSAD_CurrentDischargingValue { get; set; }
        public string RO_TSAD_BatteryTemperature { get; set; }
        public string RO_TSAD_BatterVoltage { get; set; }
        public string RO_TSAD_CellDiffrence { get; set; }
        public int? RO_TSAD_ProtectionsId { get; set; }
        public string RO_TSAD_Protections { get; set; }
        public string RO_TSAD_CycleCount { get; set; }
        public int RO_TSAD_ProblemObservedByEngId { get; set; }
        public string RO_TSAD_ProblemObservedByEng { get; set; }
        public string RO_TSAD_ProblemObservedDesc { get; set; }
        public string? RO_TSAD_Gravity { get; set; }
        public string? RO_TSAD_IP_VoltageAC { get; set; }
        public string? RO_TSAD_IP_VoltageDC { get; set; }
        public string? RO_TSAD_OutputAC { get; set; }
        public string? RO_TSAD_Protection { get; set; }
        public string? RO_TSAD_AttachPhotoFileName { get; set; }
        public string? RO_TSAD_AttachPhotoOriginalFileName { get; set; }
        public string? RO_TSAD_AttachPhotoURL { get; set; }
        public int? RO_TSAD_FanStatus { get; set; }
        public string? RO_TSAD_PhysicalPhotoFileName { get; set; }
        public string? RO_TSAD_PhysicalPhotoOriginalFileName { get; set; }
        public string? RO_TSAD_PhysicalPhotoURL { get; set; }
        public string? RO_TSAD_IssueImageFileName { get; set; }
        public string? RO_TSAD_IssueImageOriginalFileName { get; set; }
        public string? RO_TSAD_IssueImageURL { get; set; }

        public string RO_TSPD_PhysicaImageFileName { get; set; }
        public string RO_TSPD_PhysicaImageOriginalFileName { get; set; }
        public string RO_TSPD_PhysicaImageURL { get; set; }
        public string RO_TSPD_AnyPhysicalDamage { get; set; }
        public string RO_TSPD_Other { get; set; }
        public bool? RO_TSPD_IsWarrantyVoid { get; set; }
        public string? RO_TSPD_ReasonforVoid { get; set; }
        public int? RO_TSPD_TypeOfBMSId { get; set; }
        public string? RO_TSPD_TypeOfBMS { get; set; }
        public int? RO_BD_TechnicalSupportEnggId { get; set; }
        public string RO_BD_TechnicalSupportEngg { get; set; }
        public int? RO_TSSP_SolutionProvider { get; set; }
        public int? RO_TSSP_AllocateToServiceEnggId { get; set; }
        public string RO_TSSP_AllocateToServiceEngg { get; set; }
        public string? RO_TSSP_Remarks { get; set; }
        public int? RO_TSSP_BranchId { get; set; }
        public string? RO_TSSP_BranchName { get; set; }
        public int? RO_TSSP_RectificationActionId { get; set; }
        public string? RO_TSSP_RectificationAction { get; set; }
        public string? RO_TSSP_ResolutionSummary { get; set; }

        public int? RO_CP_Visual { get; set; }
        public string RO_CP_VisualImageFileName { get; set; }
        public string RO_CP_VisualImageOriginalFileName { get; set; }
        public string RO_CP_VisualImageURL { get; set; }
        public string RO_CP_TerminalVoltage { get; set; }
        public int? RO_CP_CommunicationWithBattery { get; set; }
        public int? RO_CP_TerminalWire { get; set; }
        public string RO_CP_TerminalWireImageOriginalFileName { get; set; }
        public string RO_CP_TerminalWireImageFileName { get; set; }
        public string RO_CP_TerminalWireImageURL { get; set; }
        public string RO_CP_LifeCycle { get; set; }
        public string RO_CP_StringVoltageVariation { get; set; }
        public int? RO_CP_BatteryParametersSetting { get; set; }
        public string RO_CP_BatteryParametersSettingImageFileName { get; set; }
        public string RO_CP_BatteryParametersSettingImageOriginalFileName { get; set; }
        public string RO_CP_BatteryParametersSettingImageURL { get; set; }
        public int? RO_CP_Spare { get; set; }
        public int? RO_CP_BMSStatus { get; set; }
        public string RO_CP_BMSSoftwareImageFileName { get; set; }
        public string RO_CP_BMSSoftwareImageOriginalFileName { get; set; }
        public string RO_CP_BMSSoftwareImageURL { get; set; }
        public int? RO_CP_BMSType { get; set; }
        public string RO_CP_BatteryTemp { get; set; }
        public string RO_CP_BMSSerialNumber { get; set; }
        public string RO_CP_ProblemObserved { get; set; }
        public int? RO_CP_ProblemObservedByEngId { get; set; }
        public string? RO_CP_ProblemObservedByEng { get; set; }

        public int? RO_CC_BatteryRepairedOnSite { get; set; }
        public int? RO_CC_BatteryRepairedToPlant { get; set; }

        public bool? RO_OV_IsCustomerAvailable { get; set; }
        public string RO_OV_EngineerName { get; set; }
        public string RO_OV_EngineerNumber { get; set; }
        public string RO_OV_CustomerName { get; set; }
        public string RO_OV_CustomerNameSecondary { get; set; }
        public string RO_OV_CustomerMobileNumber { get; set; }
        public string RO_OV_RequestOTP { get; set; }
        public string RO_OV_Signature { get; set; }

        public bool? IsActive { get; set; }

        public List<ManageTicketPartDetails_Response> PartDetails { get; set; }
        public List<ManageTicketStatusLog_Response> TicketStatusLog { get; set; }
        public List<ManageTicketRemarks_Response> TicketRemarksList { get; set; }

    }



    public class ManageTicketPartDetails_Response : BaseEntity
    {
        [JsonIgnore]
        public int? TicketId { get; set; }

        public int? SpareCategoryId { get; set; }

        public string? SpareCategory { get; set; }

        public int? SpareDetailsId { get; set; }

        public string? UniqueCode { get; set; }

        public string? SpareDesc { get; set; }

        public int? Quantity { get; set; }

        public int? AvailableQty { get; set; }

        //public int? PartStatusId { get; set; }

        //public string? PartStatus { get; set; }

        public bool? RGP { get; set; }
    }

    public class ManageTicketRemarks_Search
    {
        public int? TicketId { get; set; }
    }

    public class ManageTicketRemarks_Response : BaseResponseEntity
    {
        [JsonIgnore]
        public int? TicketId { get; set; }

        public string Remarks { get; set; }
    }

    public class ManageTicketStatusLog_Response
    {
        public int? TicketId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? StatusId { get; set; }
        public string TicketStatus { get; set; }
        public int? PriorityId { get; set; }
        public string Priority { get; set; }
        public string Agging { get; set; }
    }

    public class ManageTicketCustomerMobileNumber_Response
    {
        public string? MobileNumber { get; set; }
    }

    public class ManageTicketCustomerDetail_Response : BaseResponseEntity
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

        public string? GSTImage { get; set; }

        public string? GSTImageOriginalFileName { get; set; }

        public string? GSTImageURL { get; set; }

        public string? PanCardImage { get; set; }

        public string? PanCardOriginalFileName { get; set; }

        public string? PanCardImageURL { get; set; }

        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public int RegionId { get; set; }
        public string? RegionName { get; set; }
        public int StateId { get; set; }
        public string? StateName { get; set; }
        public int DistrictId { get; set; }
        public string? DistrictName { get; set; }
        public int CityId { get; set; }
        public string? CityName { get; set; }

        public bool? IsActive { get; set; }
    }


    public class ManageTicketEngineerVisitHistory_Search : BaseSearchEntity
    {
        public int? TicketId { get; set; }
        public int? EngineerId { get; set; }
    }

    public class ManageTicketEngineerVisitHistory_Request
    {
        public int Id { get; set; }
        public int? EngineerId { get; set; }
        public DateTime? VisitDate { get; set; }
        public int? TicketId { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? Address { get; set; }
        public string? Status { get; set; }
    }

    public class ManageTicketEngineerVisitHistory_Response : BaseResponseEntity
    {
        public int Id { get; set; }
        public int? EngineerId { get; set; }
        public string? EngineerName { get; set; }
        public int? TicketId { get; set; }
        public string? TicketNumber { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? StartLocation { get; set; }
        public string? StopLocation { get; set; }
        public DateTime? StartDateTime { get; set; }
        public string? StartStatus { get; set; }
        public DateTime? StopDateTime { get; set; }
        public string? StopStatus { get; set; }
    }

    public class ManageTicketOTPVerify
    {
        [DefaultValue(0)]
        public int? TicketId { get; set; }

        public string? Mobile { get; set; }
    }

    public class ValidateTicketProductSerialNumber_Response
    {
        public string? TicketNumber { get; set; }
    }

    public class FeedbackQuestionAnswer_Request
    {
        public int Id { get; set; }
        public int? TicketId { get; set; }
        public string? Question_Answer_Json_Format { get; set; }
    }

    public class FeedbackQuestionAnswerSearch_Request: BaseSearchEntity
    {
        public int? TicketId { get; set; }
    }

    public class FeedbackQuestionAnswer_Response : BaseResponseEntity
    {
        public int Id { get; set; }
        public int? TicketId { get; set; }
        public string? TicketNumber { get; set; }
        public string? Question_Answer_Json_Format { get; set; }
    }
}