using CLN.Domain.Entities;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class ManageTRC_Search : BaseSearchEntity
    {
        [DefaultValue(null)]
        public DateTime? FromDate { get; set; }

        [DefaultValue(null)]
        public DateTime? ToDate { get; set; }
        public int? EmployeeId { get; set; }
        public int? TRCStatusId { get; set; }

        [DefaultValue(null)]
        public bool? IsReplacement { get; set; }

        [DefaultValue(null)]
        public bool? IsPDIDone { get; set; }

        [DefaultValue(null)]
        public bool? IsAssignedToEnggForPDI { get; set; }

        [DefaultValue("All")]
        public string? FilterType { get; set; }

        [DefaultValue(false)]
        public bool? IsFeedback { get; set; }

        [DefaultValue("")]
        public string? BranchId { get; set; }

        public int? Filter_TRCStatusId { get; set; }
    }

    public class ManageTRC_Request : BaseEntity
    {
        public ManageTRC_Request()
        {
            PartDetail = new List<ManageTRCPartDetails_Request>();
        }

        public string? TRCNumber { get; set; }

        public DateTime? TRCDate { get; set; }

        public string? TRCTime { get; set; }


        public int? TicketId { get; set; }

        public string? TicketNumber { get; set; }

        public DateTime? TicketdDate { get; set; }

        public string? TicketdTime { get; set; }

        public int? TicketPriorityId { get; set; }


        public string? TicketSLADays { get; set; }

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

        [DefaultValue(false)]
        public bool? CD_IsSiteAddressSameAsCaller { get; set; }

        public string? CD_BatterySerialNumber { get; set; }

        public int? CD_CustomerTypeId { get; set; }

        public int? CD_CustomerNameId { get; set; }

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

        // TRC Detail
        public int? RP_IsCLNOrCustomer { get; set; }
        public int? RP_ProblemReportedByCustId { get; set; }
        public string? RP_ProblemDecription { get; set; }
        public string? RP_ProductPackingPhotoOriginalFileName1 { get; set; }
        public string? RP_ProductPackingPhotoFileName1 { get; set; }
        public string? RP_ProductPackingPhoto1_Base64 { get; set; }

        public string? RP_ProductPackingPhotoOriginalFileName2 { get; set; }
        public string? RP_ProductPackingPhotoFileName2 { get; set; }
        public string? RP_ProductPackingPhoto2_Base64 { get; set; }

        public string? RP_ProductPackingPhotoOriginalFileName3 { get; set; }
        public string? RP_ProductPackingPhotoFileName3 { get; set; }
        public string? RP_ProductPackingPhoto3_Base64 { get; set; }

        public string? RP_DeliveryChallanPhotoOriginalFileName { get; set; }
        public string? RP_DeliveryChallanPhotoFileName { get; set; }
        public string? RP_DeliveryChallanPhoto_Base64 { get; set; }

        public string? RP_ReservePickupFormatOriginalFileName { get; set; }
        public string? RP_ReservePickupFormatFileName { get; set; }
        public string? RP_ReservePickupFormat_Base64 { get; set; }

        public bool? RP_IsReservePickupMailToLogistic { get; set; }
        public string? RP_DocketDetails { get; set; }
        public bool? RP_IsBatteryInTransit { get; set; }

        public int? DNV_IsDeliveryChallanOrDebitNote { get; set; }
        public string? DNV_DeliveryChallanPhotoOriginalFileName { get; set; }
        public string? DNV_DeliveryChallanPhotoFileName { get; set; }
        public string? DNV_DeliveryChallanPhoto_Base64 { get; set; }

        public string? DNV_DebitNote { get; set; }
        public bool? DNV_IsHandoverToMainStore { get; set; }
        public string DNV_DeliveryChallanNumber { get; set; }
        public bool? DNV_IsBatteryReceivedInTRC { get; set; }
        public string? DNV_DebitNotePhotoOriginalFileName { get; set; }
        public string? DNV_DebitNotePhotoFileName { get; set; }
        public string? DNV_DebitNotePhoto_Base64 { get; set; }

        public int? ATE_AssignedToEngineerId { get; set; }

        public int? II_Visual { get; set; }
        public bool? II_IsIntact { get; set; }
        public bool? II_IsTempered { get; set; }
        public string? II_TemperedOriginalFileName1 { get; set; }
        public string? II_TemperedFileName1 { get; set; }
        public string? II_Tempered1_Base64 { get; set; }
        public string? II_TemperedOriginalFileName2 { get; set; }
        public string? II_TemperedFileName2 { get; set; }
        public string? II_Tempered2_Base64 { get; set; }
        public string? II_TemperedOriginalFileName3 { get; set; }
        public string? II_TemperedFileName3 { get; set; }
        public string? II_Tempered3_Base64 { get; set; }
        public string? II_PhysicallyDamageOriginalFileName1 { get; set; }
        public string? II_PhysicallyDamageFileName1 { get; set; }
        public string? II_PhysicallyDamage1_Base64 { get; set; }
        public string? II_PhysicallyDamageOriginalFileName2 { get; set; }
        public string? II_PhysicallyDamageFileName2 { get; set; }
        public string? II_PhysicallyDamage2_Base64 { get; set; }
        public string? II_PhysicallyDamageOriginalFileName3 { get; set; }
        public string? II_PhysicallyDamageFileName3 { get; set; }
        public string? II_PhysicallyDamage3_Base64 { get; set; }
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
        public int? II_BatteryParameterSetting { get; set; }

        public int? WS_IsWarrantyStatus { get; set; }
        public bool? WS_IsInformedToCustomerByEmail { get; set; }
        public bool? WS_IsCustomerAcceptance { get; set; }
        public bool? WS_IsPaymentClearance { get; set; }
        public string? WS_InvoiceOriginalFileName { get; set; }
        public string? WS_InvoiceFileName { get; set; }
        public string? WS_Invoice_Base64 { get; set; }
        public bool? WS_IsReplacement { get; set; }
        public int? WS_NewProductSerialNumberId { get; set; }
        public string? WS_SerialNumberDesc { get; set; }

        [DefaultValue(false)]
        public bool? WS_IsGeneratedQuotation { get; set; }

        public int? DA_ProblemObservedByEngId { get; set; }
        public string? DA_ProblemObservedDesc { get; set; }
        public int? DA_RectificationActionId { get; set; }
        public string? DA_ResolutionSummary { get; set; }
        public string? DA_CapacityAchieved { get; set; }

        public bool? ATEFP_IsAssignedToEnggForPDI { get; set; }
        public int? ATEFP_AssignedToEngineerId { get; set; }

        public DateTime? PI_BatteryReceivedDate { get; set; }
        public string? PI_BatteryReceivedTime { get; set; }
        public DateTime? PI_PDIDoneDate { get; set; }
        public string? PI_PDIDoneTime { get; set; }
        public int? PI_PDIDoneById { get; set; }

        public string? PI_SOCPercentageOriginalFileName { get; set; }
        public string? PI_SOCPercentageFileName { get; set; }
        public string? PI_SOCPercentage_Base64 { get; set; }

        public string? PI_VoltageDifference { get; set; }
        public string? PI_FinalVoltageOriginalFileName { get; set; }
        public string? PI_FinalVoltageFileName { get; set; }
        public string? PI_FinalVoltage_Base64 { get; set; }

        public string? PI_AmpereHour { get; set; }

        [DefaultValue(false)]
        public bool? PI_IsPDIDone { get; set; }

        public string? PIDD_DispatchedDeliveryChallan { get; set; }
        public DateTime? PIDD_DispatchedDate { get; set; }
        public int? PIDD_DispatchedCityId { get; set; }
        public string? DDB_DispatchedDoneBy { get; set; }
        public string? DDB_DocketDetails { get; set; }
        public string? DDB_CourierName { get; set; }
        public DateTime? CRD_CustomerReceivingDate { get; set; }


        //public int? TS_Visual { get; set; }

        //public string? TS_BatterTerminalVoltage { get; set; }

        //public string? TS_LifeCycle { get; set; }

        //public string? TS_StringVoltageVariation { get; set; }

        //public string? TS_BatteryTemperature { get; set; }

        //public string? TS_CurrentDischargingValue { get; set; }

        //public int? TS_ProtectionsId { get; set; }

        //public string? TS_CurrentChargingValue { get; set; }

        //public int? TS_AllocateToServiceEnggId { get; set; }

        //public DateTime? TS_TicketDate { get; set; }

        //public string? TS_TicketTime { get; set; }


        //public int? CP_Visual { get; set; }

        //public string? CP_VisualImageFileName { get; set; }

        //public string? CP_VisualImageOriginalFileName { get; set; }

        //public string? CP_VisualImage_Base64 { get; set; }


        //public string? CP_TerminalVoltage { get; set; }

        //public int? CP_CommunicationWithBattery { get; set; }

        //public int? CP_TerminalWire { get; set; }

        //public string? CP_TerminalWireImageFileName { get; set; }

        //public string? CP_TerminalWireImageOriginalFileName { get; set; }

        //public string? CP_TerminalWireImage_Base64 { get; set; }

        //public string? CP_LifeCycle { get; set; }

        //public string? CP_StringVoltageVariation { get; set; }

        //public int? CP_BatteryParametersSetting { get; set; }

        //public string? CP_BatteryParametersSettingImageFileName { get; set; }

        //public string? CP_BatteryParametersSettingImageOriginalFileName { get; set; }

        //public string? CP_BatteryParametersSettingImage_Base64 { get; set; }

        //public string? CP_BMSSoftwareImageFileName { get; set; }

        //public string? CP_BMSSoftwareImageOriginalFileName { get; set; }

        //public string? CP_BMSSoftwareImage_Base64 { get; set; }

        //public int? CP_Spare { get; set; }


        //public int? CC_BatteryRepairedOnSite { get; set; }

        //public int? CC_BatteryRepairedToPlant { get; set; }


        //public bool? OV_IsCustomerAvailable { get; set; }

        //public string? OV_EngineerName { get; set; }

        //public string? OV_EngineerNumber { get; set; }

        //public string? OV_CustomerName { get; set; }

        //public string? OV_CustomerNameSecondary { get; set; }

        //public string? OV_CustomerMobileNumber { get; set; }

        //public string? OV_RequestOTP { get; set; }

        //public string? OV_Signature { get; set; }

        //[DefaultValue(false)]
        //public bool? OV_IsMoveToTRC { get; set; }

        //public int? TicketdStatusId { get; set; }

        //public int? EnquiryId { get; set; }

        public int? TRCBranchId { get; set; }
        public int? TRCStatusId { get; set; }

        public bool? IsActive { get; set; }

        public List<ManageTRCPartDetails_Request> PartDetail { get; set; }
    }

    public class ManageTRCPartDetails_Request : BaseEntity
    {
        [JsonIgnore]
        public int? TRCId { get; set; }

        public int? SpareCategoryId { get; set; }
        public int? ProductMakeId { get; set; }
        public int? BMSMakeId { get; set; }
        public int? SpareDetailsId { get; set; }

        public decimal? Quantity { get; set; }

        public decimal? AvailableQty { get; set; }

        //public string? SparePartNo { get; set; }

        //public string? PartDescription { get; set; }

        //public int? Quantity { get; set; }

        //public string? Remarks { get; set; }

        //public int? PartStatusId { get; set; }
    }

    public class ManageTRCList_Response : BaseResponseEntity
    {
        public string? TRCNumber { get; set; }
        public DateTime? TRCDate { get; set; }
        public TimeSpan? TRCTime { get; set; }

        public int? TicketId { get; set; }
        public string? TicketNumber { get; set; }
        public DateTime? TicketDate { get; set; }
        public string? CD_CallerMobile { get; set; }

        public int? CD_CustomerTypeId { get; set; }
        public string? CD_CustomerType { get; set; }
        public int? CD_CustomerNameId { get; set; }
        public string? CD_CustomerName { get; set; }
        public string? CD_CustomerMobile { get; set; }

        public string? Address1 { get; set; }
        public string? RegionName { get; set; }
        public string? StateName { get; set; }
        public string? DistrictName { get; set; }
        public string? CityName { get; set; }

        public string? CD_SiteCustomerName { get; set; }
        public string? CD_SiteContactName { get; set; }
        public string? CD_SitContactMobile { get; set; }

        public int? ATE_AssignedToEngineerId { get; set; }
        public string? ATE_AssignedToEngineer { get; set; }

        public int? WS_IsWarrantyStatus { get; set; }
        public bool? WS_IsReplacement { get; set; }
        public int? CD_ProductSerialNumberId { get; set; }
        public string? CD_ProductSerialNumber { get; set; }
        public int? WS_NewProductSerialNumberId { get; set; }
        public string? WS_NewProductSerialNumber { get; set; }
        public string? WS_SerialNumberDesc { get; set; }
        public bool? WS_IsGeneratedQuotation { get; set; }

        public bool? ATEFP_IsAssignedToEnggForPDI { get; set; }
        public bool? PI_IsPDIDone { get; set; }
        public bool? IsFeedback { get; set; }

        public int? TRCStatusId { get; set; }
        public string? TRCStatus { get; set; }
        public bool? IsActive { get; set; }
    }

    public class ManageTRCDetail_Response : BaseResponseEntity
    {
        public ManageTRCDetail_Response()
        {
            PartDetails = new List<ManageTRCPartDetails_Response>();
        }

        public int? TicketId { get; set; }
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
        public DateTime? RP_BatteryInTransitDate { get; set; }

        public int? DNV_IsDeliveryChallanOrDebitNote { get; set; }
        public string? DNV_DeliveryChallanPhotoOriginalFileName { get; set; }
        public string? DNV_DeliveryChallanPhotoFileName { get; set; }
        public string? DNV_DeliveryChallanPhotoURL { get; set; }

        public string? DNV_DebitNote { get; set; }
        public bool? DNV_IsHandoverToMainStore { get; set; }
        public DateTime? DNV_HandoverToMainStoreDate { get; set; }
        public string DNV_DeliveryChallanNumber { get; set; }
        public bool? DNV_IsBatteryReceivedInTRC { get; set; }
        public string? DNV_DebitNotePhotoOriginalFileName { get; set; }
        public string? DNV_DebitNotePhotoFileName { get; set; }
        public string? DNV_DebitNotePhotoURL { get; set; }

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
        public int? II_BatteryParameterSetting { get; set; }

        public int? WS_IsWarrantyStatus { get; set; }
        public bool? WS_IsInformedToCustomerByEmail { get; set; }
        public DateTime? WS_InformedToCustomerByEmailDate { get; set; }
        public bool? WS_IsCustomerAcceptance { get; set; }
        public DateTime? WS_CustomerAcceptanceDate { get; set; }
        public bool? WS_IsPaymentClearance { get; set; }
        public DateTime? WS_PaymentClearanceDate { get; set; }
        public string? WS_InvoiceOriginalFileName { get; set; }
        public string? WS_InvoiceFileName { get; set; }
        public string? WS_InvoiceURL { get; set; }
        public bool? WS_IsReplacement { get; set; }
        public int? WS_NewProductSerialNumberId { get; set; }
        public string? WS_NewProductSerialNumber { get; set; }
        public string? WS_SerialNumberDesc { get; set; }
        public bool? WS_IsGeneratedQuotation { get; set; }
        public DateTime? WS_GeneratedQuotationDate { get; set; }

        public int? DA_ProblemObservedByEngId { get; set; }
        public string? DA_ProblemObservedByEng { get; set; }
        public string? DA_ProblemObservedDesc { get; set; }
        public int? DA_RectificationActionId { get; set; }
        public string? DA_RectificationAction { get; set; }
        public string? DA_ResolutionSummary { get; set; }
        public string? DA_CapacityAchieved { get; set; }

        public bool? ATEFP_IsAssignedToEnggForPDI { get; set; }
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

        public string? PI_AmpereHour { get; set; }
        public bool? PI_IsPDIDone { get; set; }

        public string? PIDD_DispatchedDeliveryChallan { get; set; }
        public DateTime? PIDD_DispatchedDate { get; set; }
        public int? PIDD_DispatchedCityId { get; set; }
        public string? PIDD_DispatchedCity { get; set; }
        public string? DDB_DispatchedDoneBy { get; set; }
        public string? DDB_DocketDetails { get; set; }
        public string? DDB_CourierName { get; set; }
        public DateTime? CRD_CustomerReceivingDate { get; set; }

        public int? TRCBranchId { get; set; }
        public string? TRCBranchName { get; set; }

        public int? TRCStatusId { get; set; }
        public string? TRCStatus { get; set; }

        public int? QuotationId { get; set; }
        public string? QuotationNumber { get; set; }

        public int? InvoiceId { get; set; }
        public string? InvoiceNumber { get; set; }

        public bool? IsActive { get; set; }

        public List<ManageTRCPartDetails_Response> PartDetails { get; set; }

        public ManageTicketDetail_Response TicketDetail { get; set; }
    }

    public class ManageTRCPartDetails_Response : BaseEntity
    {
        [JsonIgnore]
        public int? TRCId { get; set; }
        public int? SpareCategoryId { get; set; }
        public string? SpareCategory { get; set; }
        public int? ProductMakeId { get; set; }
        public string? ProductMake { get; set; }
        public int? BMSMakeId { get; set; }
        public string? BMSMake { get; set; }
        public int? SpareDetailsId { get; set; }
        public string? UniqueCode { get; set; }
        public string? SpareDesc { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? AvailableQty { get; set; }

        //public int? PartStatusId { get; set; }

        //public string? PartStatus { get; set; }

        public bool? RGP { get; set; }

        [JsonIgnore]
        public decimal? TentativeCost { get; set; }


        //public string? SparePartNo { get; set; }

        //public string? PartDescription { get; set; }

        //public int? Quantity { get; set; }

        //public string? Remarks { get; set; }

        //public int? PartStatusId { get; set; }
    }

    #region Quotation

    public class Quotation : BaseEntity
    {
        public Quotation()
        {
            partDetails = new List<QuotationPartDetails>();
        }

        public DateTime? QuotationDate { get; set; }
        public string? QuotationNumber { get; set; }
        public int? TicketId { get; set; }
        public string? TicketNumber { get; set; }
        public int? TRCId { get; set; }
        public string? TRCNumber { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerMobileNo { get; set; }
        public string? CustomerEmailId { get; set; }
        public int? CustomerAddressId { get; set; }
        public string? CustomerAddress { get; set; }
        public int? CustomerRegionId { get; set; }
        public string? CustomerRegionName { get; set; }
        public int? CustomerStateId { get; set; }
        public string? CustomerStateName { get; set; }
        public int? CustomerDistrictId { get; set; }
        public string? CustomerDistrictName { get; set; }
        public int? CustomerCityId { get; set; }
        public string? CustomerCityName { get; set; }
        public string? CustomerPinCode { get; set; }
        public int? ProductModelId { get; set; }
        public string? ProductModel { get; set; }
        public int? ProductSerialNumberId { get; set; }
        public string? ProductSerialNumber { get; set; }
        public decimal? SubTotal { get; set; }
        public int? TaxPerct { get; set; }
        public decimal? TaxValue { get; set; }
        public decimal? TotalAmount { get; set; }
        public int? BranchId { get; set; }
        public string? BranchName { get; set; }
        public int? StatusId { get; set; }
        public string? StatusName { get; set; }
        public List<QuotationPartDetails> partDetails { get; set; }
    }

    public class QuotationPartDetails : BaseEntity
    {
        [JsonIgnore]
        public int? QuotationId { get; set; }
        public int? SpareCategoryId { get; set; }
        public string? SpareCategory { get; set; }
        public int? ProductMakeId { get; set; }
        public string? ProductMake { get; set; }
        public int? BMSMakeId { get; set; }
        public string? BMSMake { get; set; }
        public int? SpareDetailsId { get; set; }
        public string? SpareDesc { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalPrice { get; set; }
    }

    public class Quotation_Search : BaseSearchEntity
    {
        public int StatusId { get; set; }
        public int TRCId { get; set; }

        [DefaultValue("")]
        public string? BranchId { get; set; }
    }

    public class QuotationList_Response : BaseResponseEntity
    {
        public int? TicketId { get; set; }
        public string? TicketNumber { get; set; }
        public int? TRCId { get; set; }
        public string? TRCNumber { get; set; }
        public DateTime? QuotationDate { get; set; }
        public string? QuotationNumber { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public decimal? SubTotal { get; set; }
        public int? TaxPerct { get; set; }
        public decimal? TaxValue { get; set; }
        public decimal? TotalAmount { get; set; }
        public int? TRCBranchId { get; set; }
        public string? BranchName { get; set; }
        public int? StatusId { get; set; }
        public string? StatusName { get; set; }
    }

    public class Quotation_ApproveNReject
    {
        public int? Id { get; set; }
        public int? StatusId { get; set; }
    }
    #endregion

    #region Invoice

    public class Invoice_Request : BaseEntity
    {
        public Invoice_Request()
        {
            partDetails = new List<InvoicePartDetails_Request>();
        }

        public DateTime? InvoiceDate { get; set; }
        public string? InvoiceNumber { get; set; }
        public int? TRCId { get; set; }
        public decimal? SubTotal { get; set; }
        public int? TaxPerct { get; set; }
        public decimal? TaxValue { get; set; }
        public decimal? TotalAmount { get; set; }
        public List<InvoicePartDetails_Request> partDetails { get; set; }
    }

    public class InvoicePartDetails_Request : BaseEntity
    {
        [JsonIgnore]
        public int? InvoiceId { get; set; }
        public int? SpareCategoryId { get; set; }
        public int? ProductMakeId { get; set; }
        public int? BMSMakeId { get; set; }
        public int? SpareDetailsId { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalPrice { get; set; }
    }

    public class Invoice_Search : BaseSearchEntity
    {
        public int TRCId { get; set; }

        [DefaultValue("")]
        public string? BranchId { get; set; }
    }

    public class Invoice_Response : BaseEntity
    {
        public Invoice_Response()
        {
            partDetails = new List<InvoicePartDetails_Response>();
        }

        public DateTime? InvoiceDate { get; set; }
        public string? InvoiceNumber { get; set; }
        public int? TRCId { get; set; }
        public string? TRCNumber { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerMobileNo { get; set; }
        public string? CustomerEmailId { get; set; }
        public int? CustomerAddressId { get; set; }
        public string? CustomerAddress { get; set; }
        public int? CustomerRegionId { get; set; }
        public string? CustomerRegionName { get; set; }
        public int? CustomerStateId { get; set; }
        public string? CustomerStateName { get; set; }
        public int? CustomerDistrictId { get; set; }
        public string? CustomerDistrictName { get; set; }
        public int? CustomerCityId { get; set; }
        public string? CustomerCityName { get; set; }
        public string? CustomerPinCode { get; set; }
        public int? ProductModelId { get; set; }
        public string? ProductModel { get; set; }
        public int? ProductSerialNumberId { get; set; }
        public string? ProductSerialNumber { get; set; }
        public decimal? SubTotal { get; set; }
        public int? TaxPerct { get; set; }
        public decimal? TaxValue { get; set; }
        public decimal? TotalAmount { get; set; }
        public int? BranchId { get; set; }
        public string? BranchName { get; set; }
        public List<InvoicePartDetails_Response> partDetails { get; set; }
    }

    public class InvoicePartDetails_Response : BaseEntity
    {
        [JsonIgnore]
        public int? InvoiceId { get; set; }
        public int? SpareCategoryId { get; set; }
        public string? SpareCategory { get; set; }
        public int? ProductMakeId { get; set; }
        public string? ProductMake { get; set; }
        public int? BMSMakeId { get; set; }
        public string? BMSMake { get; set; }
        public int? SpareDetailsId { get; set; }
        public string? SpareDesc { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalPrice { get; set; }
    }

    public class InvoiceList_Response : BaseResponseEntity
    {
        public int? TicketId { get; set; }
        public string? TicketNumber { get; set; }
        public int? TRCId { get; set; }
        public string? TRCNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string? InvoiceNumber { get; set; }
        public int? QuotationId { get; set; }
        public string? QuotationNumber { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public int? BranchId { get; set; }
        public string? BranchName { get; set; }
        public decimal? SubTotal { get; set; }
        public int? TaxPerct { get; set; }
        public decimal? TaxValue { get; set; }
        public decimal? TotalAmount { get; set; }
    }
    #endregion
}