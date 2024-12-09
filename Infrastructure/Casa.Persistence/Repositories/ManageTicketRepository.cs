using CLN.Application.Helpers;
using CLN.Application.Interfaces;
using CLN.Application.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Persistence.Repositories
{
    public class ManageTicketRepository : GenericRepository, IManageTicketRepository
    {
        private IConfiguration _configuration;

        public ManageTicketRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> SaveManageTicket(ManageTicket_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@TicketNumber", parameters.TicketNumber);
            queryParameters.Add("@TicketDate", parameters.TicketDate);
            queryParameters.Add("@TicketTime", parameters.TicketTime);
            queryParameters.Add("@TicketPriorityId", parameters.TicketPriorityId);
            queryParameters.Add("@TicketSLADays", parameters.TicketSLADays);
            queryParameters.Add("@TicketSLAHours", parameters.TicketSLAHours);
            queryParameters.Add("@TicketSLAMin", parameters.TicketSLAMin);

            queryParameters.Add("@CD_LoggingSourceId", parameters.CD_LoggingSourceId);
            queryParameters.Add("@CD_CallerTypeId", parameters.CD_CallerTypeId);
            queryParameters.Add("@CD_CallerName", parameters.CD_CallerName);
            queryParameters.Add("@CD_CallerMobile", parameters.CD_CallerMobile);
            queryParameters.Add("@CD_CallerEmailId", parameters.CD_CallerEmailId);
            queryParameters.Add("@CD_CallerAddressId", parameters.CD_CallerAddressId);
            queryParameters.Add("@CD_CallerRemarks", parameters.CD_CallerRemarks);
            queryParameters.Add("@CD_IsSiteAddressSameAsCaller", parameters.CD_IsSiteAddressSameAsCaller);
            queryParameters.Add("@CD_ComplaintTypeId", parameters.CD_ComplaintTypeId);
            queryParameters.Add("@CD_IsOldProduct", parameters.CD_IsOldProduct);
            queryParameters.Add("@CD_ProductSerialNumberId", parameters.CD_ProductSerialNumberId);
            queryParameters.Add("@CD_ProductSerialNumber", parameters.CD_ProductSerialNumber);
            queryParameters.Add("@CD_CustomerTypeId", parameters.CD_CustomerTypeId);
            queryParameters.Add("@CD_CustomerNameId", parameters.CD_CustomerNameId);
            queryParameters.Add("@CD_CustomerMobile", parameters.CD_CustomerMobile);
            queryParameters.Add("@CD_CustomerEmail", parameters.CD_CustomerEmail);
            queryParameters.Add("@CD_CustomerAddressId", parameters.CD_CustomerAddressId);
            queryParameters.Add("@CD_SiteCustomerName", parameters.CD_SiteCustomerName);
            queryParameters.Add("@CD_SiteContactName", parameters.CD_SiteContactName);
            queryParameters.Add("@CD_SitContactMobile", parameters.CD_SitContactMobile);
            queryParameters.Add("@CD_SiteAddressId", parameters.CD_SiteAddressId);

            queryParameters.Add("@BD_BatteryBOMNumberId", parameters.BD_BatteryBOMNumberId);
            queryParameters.Add("@BD_BatteryProductCategoryId", parameters.BD_BatteryProductCategoryId);
            queryParameters.Add("@BD_BatterySegmentId", parameters.BD_BatterySegmentId);
            queryParameters.Add("@BD_BatterySubSegmentId", parameters.BD_BatterySubSegmentId);
            queryParameters.Add("@BD_BatteryProductModelId", parameters.BD_BatteryProductModelId);
            queryParameters.Add("@BD_BatteryCellChemistryId", parameters.BD_BatteryCellChemistryId);
            queryParameters.Add("@BD_DateofManufacturing", parameters.BD_DateofManufacturing);
            queryParameters.Add("@BD_ProbReportedByCustId", parameters.BD_ProbReportedByCustId);
            queryParameters.Add("@BD_ProblemDescription", parameters.BD_ProblemDescription);
            queryParameters.Add("@BD_WarrantyStartDate", parameters.BD_WarrantyStartDate);
            queryParameters.Add("@BD_WarrantyEndDate", parameters.BD_WarrantyEndDate);
            queryParameters.Add("@BD_WarrantyStatusId", parameters.BD_WarrantyStatusId);
            queryParameters.Add("@BD_WarrantyTypeId", parameters.BD_WarrantyTypeId);

            queryParameters.Add("@BD_IsTrackingDeviceRequired", parameters.BD_IsTrackingDeviceRequired);
            queryParameters.Add("@BD_TrackingDeviceId", parameters.BD_TrackingDeviceId);
            queryParameters.Add("@BD_MakeId", parameters.BD_MakeId);
            queryParameters.Add("@BD_DeviceID", parameters.BD_DeviceID);
            queryParameters.Add("@BD_IMEINo", parameters.BD_IMEINo);
            queryParameters.Add("@BD_SIMNo", parameters.BD_SIMNo);
            queryParameters.Add("@BD_SIMProviderId", parameters.BD_SIMProviderId);
            queryParameters.Add("@BD_PlatformId", parameters.BD_PlatformId);
            queryParameters.Add("@BD_TechnicalSupportEnggId", parameters.BD_TechnicalSupportEnggId);

            queryParameters.Add("@TSAD_Visual", parameters.TSAD_Visual);
            queryParameters.Add("@TSAD_VisualImageFileName", parameters.TSAD_VisualImageFileName);
            queryParameters.Add("@TSAD_VisualImageOriginalFileName", parameters.TSAD_VisualImageOriginalFileName);
            queryParameters.Add("@TSAD_CurrentChargingValue", parameters.TSAD_CurrentChargingValue);
            queryParameters.Add("@TSAD_CurrentDischargingValue", parameters.TSAD_CurrentDischargingValue);
            queryParameters.Add("@TSAD_BatteryTemperature", parameters.TSAD_BatteryTemperature);
            queryParameters.Add("@TSAD_BatterVoltage", parameters.TSAD_BatterVoltage);
            queryParameters.Add("@TSAD_CellDiffrence", parameters.TSAD_CellDiffrence);
            queryParameters.Add("@TSAD_ProtectionsId", parameters.TSAD_ProtectionsId);
            queryParameters.Add("@TSAD_CycleCount", parameters.TSAD_CycleCount);
            queryParameters.Add("@TSAD_ProblemObservedByEngId", parameters.TSAD_ProblemObservedByEngId);
            queryParameters.Add("@TSAD_ProblemObservedDesc", parameters.TSAD_ProblemObservedDesc);

            queryParameters.Add("@TSAD_Gravity", parameters.TSAD_Gravity);
            queryParameters.Add("@TSAD_IP_VoltageAC", parameters.TSAD_IP_VoltageAC);
            queryParameters.Add("@TSAD_IP_VoltageDC", parameters.TSAD_IP_VoltageDC);
            queryParameters.Add("@TSAD_OutputAC", parameters.TSAD_OutputAC);
            queryParameters.Add("@TSAD_Protection", parameters.TSAD_Protection);
            queryParameters.Add("@TSAD_AttachPhotoFileName", parameters.TSAD_AttachPhotoFileName);
            queryParameters.Add("@TSAD_AttachPhotoOriginalFileName", parameters.TSAD_AttachPhotoOriginalFileName);
            queryParameters.Add("@TSAD_FanStatus", parameters.TSAD_FanStatus);
            queryParameters.Add("@TSAD_PhysicalPhotoFileName", parameters.TSAD_PhysicalPhotoFileName);
            queryParameters.Add("@TSAD_PhysicalPhotoOriginalFileName", parameters.TSAD_PhysicalPhotoOriginalFileName);
            queryParameters.Add("@TSAD_IssueImageFileName", parameters.TSAD_IssueImageFileName);
            queryParameters.Add("@TSAD_IssueImageOriginalFileName", parameters.TSAD_IssueImageOriginalFileName);

            queryParameters.Add("@TSPD_PhysicaImageFileName", parameters.TSPD_PhysicaImageFileName);
            queryParameters.Add("@TSPD_PhysicaImageOriginalFileName", parameters.TSPD_PhysicaImageOriginalFileName);
            queryParameters.Add("@TSPD_AnyPhysicalDamage", parameters.TSPD_AnyPhysicalDamage);
            queryParameters.Add("@TSPD_Other", parameters.TSPD_Other);
            queryParameters.Add("@TSPD_IsWarrantyVoid", parameters.TSPD_IsWarrantyVoid);
            queryParameters.Add("@TSPD_ReasonforVoid", parameters.TSPD_ReasonforVoid);
            queryParameters.Add("@TSPD_TypeOfBMSId", parameters.TSPD_TypeOfBMSId);

            queryParameters.Add("@TSSP_SolutionProvider", parameters.TSSP_SolutionProvider);
            queryParameters.Add("@TSSP_AllocateToServiceEnggId", parameters.TSSP_AllocateToServiceEnggId);
            queryParameters.Add("@TSSP_Remarks", parameters.TSSP_Remarks);
            queryParameters.Add("@TSSP_BranchId", parameters.TSSP_BranchId);
            queryParameters.Add("@TSSP_RectificationActionId", parameters.TSSP_RectificationActionId);
            queryParameters.Add("@TSSP_ResolutionSummary", parameters.TSSP_ResolutionSummary);

            queryParameters.Add("@TS_AbnormalNoise", parameters.TS_AbnormalNoise);
            queryParameters.Add("@TS_ConnectorDamage", parameters.TS_ConnectorDamage);
            queryParameters.Add("@TS_ConnectorDamageFileName", parameters.TS_ConnectorDamageFileName);
            queryParameters.Add("@TS_ConnectorDamageOriginalFileName", parameters.TS_ConnectorDamageOriginalFileName);
            queryParameters.Add("@TS_AnyBrunt", parameters.TS_AnyBrunt);
            queryParameters.Add("@TS_AnyBruntFileName", parameters.TS_AnyBruntFileName);
            queryParameters.Add("@TS_AnyBruntOriginalFileName", parameters.TS_AnyBruntOriginalFileName);
            queryParameters.Add("@TS_PhysicalDamage", parameters.TS_PhysicalDamage);
            queryParameters.Add("@TS_PhysicalDamageFileName", parameters.TS_PhysicalDamageFileName);
            queryParameters.Add("@TS_PhysicalDamageOriginalFileName", parameters.TS_PhysicalDamageOriginalFileName);
            queryParameters.Add("@TS_ProblemRemark", parameters.TS_ProblemRemark);
            queryParameters.Add("@TS_IPCurrentAC_A", parameters.TS_IPCurrentAC_A);
            queryParameters.Add("@TS_OutputCurrentDC_A", parameters.TS_OutputCurrentDC_A);
            queryParameters.Add("@TS_OutputVoltageDC_V", parameters.TS_OutputVoltageDC_V);
            queryParameters.Add("@TS_Type", parameters.TS_Type);
            queryParameters.Add("@TS_Heating", parameters.TS_Heating);
            queryParameters.Add("@TS_DisplayPhotoFileName", parameters.TS_DisplayPhotoFileName);
            queryParameters.Add("@TS_DisplayPhotoOriginalFileName", parameters.TS_DisplayPhotoOriginalFileName);
            queryParameters.Add("@TS_OutputVoltageAC_V", parameters.TS_OutputVoltageAC_V);
            queryParameters.Add("@TS_OutputCurrentAC_A", parameters.TS_OutputCurrentAC_A);
            queryParameters.Add("@TS_IPCurrentDC_A", parameters.TS_IPCurrentDC_A);
            queryParameters.Add("@TS_SpecificGravityC2", parameters.TS_SpecificGravityC2);
            queryParameters.Add("@TS_SpecificGravityC3", parameters.TS_SpecificGravityC3);
            queryParameters.Add("@TS_SpecificGravityC4", parameters.TS_SpecificGravityC4);
            queryParameters.Add("@TS_SpecificGravityC5", parameters.TS_SpecificGravityC5);
            queryParameters.Add("@TS_SpecificGravityC6", parameters.TS_SpecificGravityC6);

            queryParameters.Add("@CP_Visual", parameters.CP_Visual);
            queryParameters.Add("@CP_VisualImageFileName", parameters.CP_VisualImageFileName);
            queryParameters.Add("@CP_VisualImageOriginalFileName", parameters.CP_VisualImageOriginalFileName);
            queryParameters.Add("@CP_TerminalVoltage", parameters.CP_TerminalVoltage);
            queryParameters.Add("@CP_CommunicationWithBattery", parameters.CP_CommunicationWithBattery);
            queryParameters.Add("@CP_TerminalWire", parameters.CP_TerminalWire);
            queryParameters.Add("@CP_TerminalWireImageFileName", parameters.CP_TerminalWireImageFileName);
            queryParameters.Add("@CP_TerminalWireImageOriginalFileName", parameters.CP_TerminalWireImageOriginalFileName);
            queryParameters.Add("@CP_LifeCycle", parameters.CP_LifeCycle);
            queryParameters.Add("@CP_StringVoltageVariation", parameters.CP_StringVoltageVariation);
            queryParameters.Add("@CP_BatteryParametersSetting", parameters.CP_BatteryParametersSetting);
            queryParameters.Add("@CP_BatteryParametersSettingImageFileName", parameters.CP_BatteryParametersSettingImageFileName);
            queryParameters.Add("@CP_BatteryParametersSettingImageOriginalFileName", parameters.CP_BatteryParametersSettingImageOriginalFileName);
            queryParameters.Add("@CP_Spare", parameters.CP_Spare);
            queryParameters.Add("@CP_BMSStatus", parameters.CP_BMSStatus);
            queryParameters.Add("@CP_BMSSoftwareImageFileName", parameters.CP_BMSSoftwareImageFileName);
            queryParameters.Add("@CP_BMSSoftwareImageOriginalFileName", parameters.CP_BMSSoftwareImageOriginalFileName);
            queryParameters.Add("@CP_BMSType", parameters.CP_BMSType);
            queryParameters.Add("@CP_BatteryTemp", parameters.CP_BatteryTemp);
            queryParameters.Add("@CP_BMSSerialNumber", parameters.CP_BMSSerialNumber);
            queryParameters.Add("@CP_ProblemObserved", parameters.CP_ProblemObserved);
            queryParameters.Add("@CP_ProblemObservedByEngId", parameters.CP_ProblemObservedByEngId);
            queryParameters.Add("@CP_IsWarrantyVoid", parameters.CP_IsWarrantyVoid);
            queryParameters.Add("@CP_ReasonForVoid", parameters.CP_ReasonForVoid);
            queryParameters.Add("@CP_CommunicationWithMotorAndController", parameters.CP_CommunicationWithMotorAndController);
            queryParameters.Add("@CP_UnderWarranty", parameters.CP_UnderWarranty);
            queryParameters.Add("@CP_RPhaseVoltage", parameters.CP_RPhaseVoltage);
            queryParameters.Add("@CP_YPhaseVoltage", parameters.CP_YPhaseVoltage);
            queryParameters.Add("@CP_BPhaseVoltage", parameters.CP_BPhaseVoltage);
            queryParameters.Add("@CP_BatteryOCV", parameters.CP_BatteryOCV);
            queryParameters.Add("@CP_SystemVoltage", parameters.CP_SystemVoltage);
            queryParameters.Add("@CP_SpecificGravityC1", parameters.CP_SpecificGravityC1);
            queryParameters.Add("@CP_SpecificGravityC2", parameters.CP_SpecificGravityC2);
            queryParameters.Add("@CP_SpecificGravityC3", parameters.CP_SpecificGravityC3);
            queryParameters.Add("@CP_SpecificGravityC4", parameters.CP_SpecificGravityC4);
            queryParameters.Add("@CP_SpecificGravityC5", parameters.CP_SpecificGravityC5);
            queryParameters.Add("@CP_SpecificGravityC6", parameters.CP_SpecificGravityC6);
            queryParameters.Add("@CP_BatteryVoltageDropOnLoad", parameters.CP_BatteryVoltageDropOnLoad);
            queryParameters.Add("@CP_KmReading", parameters.CP_KmReading);
            queryParameters.Add("@CP_TotalRun", parameters.CP_TotalRun);
            queryParameters.Add("@CP_BatterySerialNo", parameters.CP_BatterySerialNo);

            queryParameters.Add("@CC_BatteryRepairedOnSite", parameters.CC_BatteryRepairedOnSite);
            queryParameters.Add("@CC_BatteryRepairedToPlant", parameters.CC_BatteryRepairedToPlant);

            queryParameters.Add("@OV_IsCustomerAvailable", parameters.OV_IsCustomerAvailable);
            queryParameters.Add("@OV_EngineerName", parameters.OV_EngineerName);
            queryParameters.Add("@OV_EngineerNumber", parameters.OV_EngineerNumber);
            queryParameters.Add("@OV_CustomerName", parameters.OV_CustomerName);
            queryParameters.Add("@OV_CustomerNameSecondary", parameters.OV_CustomerNameSecondary);
            queryParameters.Add("@OV_CustomerMobileNumber", parameters.OV_CustomerMobileNumber);
            queryParameters.Add("@OV_RequestOTP", parameters.OV_RequestOTP);
            queryParameters.Add("@OV_Signature", parameters.OV_Signature);

            queryParameters.Add("@TicketRemarks", parameters.TicketRemarks);

            queryParameters.Add("@EnquiryId", parameters.EnquiryId);
            queryParameters.Add("@TicketStatusId", parameters.TicketStatusId);
            queryParameters.Add("@TRC_EngineerId", parameters.TRC_EngineerId);

            queryParameters.Add("@IsResolvedWithoutOTP", parameters.IsResolvedWithoutOTP);
            queryParameters.Add("@IsClosedWithoutOTP", parameters.IsClosedWithoutOTP);
            queryParameters.Add("@IsReopen", parameters.IsReopen);

            queryParameters.Add("@RO_TSAD_Visual", parameters.RO_TSAD_Visual);
            queryParameters.Add("@RO_TSAD_VisualImageFileName", parameters.RO_TSAD_VisualImageFileName);
            queryParameters.Add("@RO_TSAD_VisualImageOriginalFileName", parameters.RO_TSAD_VisualImageOriginalFileName);
            queryParameters.Add("@RO_TSAD_CurrentChargingValue", parameters.RO_TSAD_CurrentChargingValue);
            queryParameters.Add("@RO_TSAD_CurrentDischargingValue", parameters.RO_TSAD_CurrentDischargingValue);
            queryParameters.Add("@RO_TSAD_BatteryTemperature", parameters.RO_TSAD_BatteryTemperature);
            queryParameters.Add("@RO_TSAD_BatterVoltage", parameters.RO_TSAD_BatterVoltage);
            queryParameters.Add("@RO_TSAD_CellDiffrence", parameters.RO_TSAD_CellDiffrence);
            queryParameters.Add("@RO_TSAD_ProtectionsId", parameters.RO_TSAD_ProtectionsId);
            queryParameters.Add("@RO_TSAD_CycleCount", parameters.RO_TSAD_CycleCount);
            queryParameters.Add("@RO_TSAD_ProblemObservedByEngId", parameters.RO_TSAD_ProblemObservedByEngId);
            queryParameters.Add("@RO_TSAD_ProblemObservedDesc", parameters.RO_TSAD_ProblemObservedDesc);
            queryParameters.Add("@RO_TSAD_Gravity", parameters.RO_TSAD_Gravity);
            queryParameters.Add("@RO_TSAD_IP_VoltageAC", parameters.RO_TSAD_IP_VoltageAC);
            queryParameters.Add("@RO_TSAD_IP_VoltageDC", parameters.RO_TSAD_IP_VoltageDC);
            queryParameters.Add("@RO_TSAD_OutputAC", parameters.RO_TSAD_OutputAC);
            queryParameters.Add("@RO_TSAD_Protection", parameters.RO_TSAD_Protection);
            queryParameters.Add("@RO_TSAD_AttachPhotoFileName", parameters.RO_TSAD_AttachPhotoFileName);
            queryParameters.Add("@RO_TSAD_AttachPhotoOriginalFileName", parameters.RO_TSAD_AttachPhotoOriginalFileName);
            queryParameters.Add("@RO_TSAD_FanStatus", parameters.RO_TSAD_FanStatus);
            queryParameters.Add("@RO_TSAD_PhysicalPhotoFileName", parameters.RO_TSAD_PhysicalPhotoFileName);
            queryParameters.Add("@RO_TSAD_PhysicalPhotoOriginalFileName", parameters.RO_TSAD_PhysicalPhotoOriginalFileName);
            queryParameters.Add("@RO_TSAD_IssueImageFileName", parameters.RO_TSAD_IssueImageFileName);
            queryParameters.Add("@RO_TSAD_IssueImageOriginalFileName", parameters.RO_TSAD_IssueImageOriginalFileName);
            queryParameters.Add("@RO_TSPD_PhysicaImageFileName", parameters.RO_TSPD_PhysicaImageFileName);
            queryParameters.Add("@RO_TSPD_PhysicaImageOriginalFileName", parameters.RO_TSPD_PhysicaImageOriginalFileName);
            queryParameters.Add("@RO_TSPD_AnyPhysicalDamage", parameters.RO_TSPD_AnyPhysicalDamage);
            queryParameters.Add("@RO_TSPD_Other", parameters.RO_TSPD_Other);
            queryParameters.Add("@RO_TSPD_IsWarrantyVoid", parameters.RO_TSPD_IsWarrantyVoid);
            queryParameters.Add("@RO_TSPD_ReasonforVoid", parameters.RO_TSPD_ReasonforVoid);
            queryParameters.Add("@RO_TSPD_TypeOfBMSId", parameters.RO_TSPD_TypeOfBMSId);
            queryParameters.Add("@RO_BD_TechnicalSupportEnggId", parameters.RO_BD_TechnicalSupportEnggId);
            queryParameters.Add("@RO_TSSP_SolutionProvider", parameters.RO_TSSP_SolutionProvider);
            queryParameters.Add("@RO_TSSP_AllocateToServiceEnggId", parameters.RO_TSSP_AllocateToServiceEnggId);
            queryParameters.Add("@RO_TSSP_Remarks", parameters.RO_TSSP_Remarks);
            queryParameters.Add("@RO_TSSP_BranchId", parameters.RO_TSSP_BranchId);
            queryParameters.Add("@RO_TSSP_RectificationActionId", parameters.RO_TSSP_RectificationActionId);
            queryParameters.Add("@RO_TSSP_ResolutionSummary", parameters.RO_TSSP_ResolutionSummary);

            queryParameters.Add("@RO_TS_AbnormalNoise", parameters.RO_TS_AbnormalNoise);
            queryParameters.Add("@RO_TS_ConnectorDamage", parameters.RO_TS_ConnectorDamage);
            queryParameters.Add("@RO_TS_ConnectorDamageFileName", parameters.RO_TS_ConnectorDamageFileName);
            queryParameters.Add("@RO_TS_ConnectorDamageOriginalFileName", parameters.RO_TS_ConnectorDamageOriginalFileName);
            queryParameters.Add("@RO_TS_AnyBrunt", parameters.RO_TS_AnyBrunt);
            queryParameters.Add("@RO_TS_AnyBruntFileName", parameters.RO_TS_AnyBruntFileName);
            queryParameters.Add("@RO_TS_AnyBruntOriginalFileName", parameters.RO_TS_AnyBruntOriginalFileName);
            queryParameters.Add("@RO_TS_PhysicalDamage", parameters.RO_TS_PhysicalDamage);
            queryParameters.Add("@RO_TS_PhysicalDamageFileName", parameters.RO_TS_PhysicalDamageFileName);
            queryParameters.Add("@RO_TS_PhysicalDamageOriginalFileName", parameters.RO_TS_PhysicalDamageOriginalFileName);
            queryParameters.Add("@RO_TS_ProblemRemark", parameters.RO_TS_ProblemRemark);
            queryParameters.Add("@RO_TS_IPCurrentAC_A", parameters.RO_TS_IPCurrentAC_A);
            queryParameters.Add("@RO_TS_OutputCurrentDC_A", parameters.RO_TS_OutputCurrentDC_A);
            queryParameters.Add("@RO_TS_OutputVoltageDC_V", parameters.RO_TS_OutputVoltageDC_V);
            queryParameters.Add("@RO_TS_Type", parameters.RO_TS_Type);
            queryParameters.Add("@RO_TS_Heating", parameters.RO_TS_Heating);
            queryParameters.Add("@RO_TS_DisplayPhotoFileName", parameters.RO_TS_DisplayPhotoFileName);
            queryParameters.Add("@RO_TS_DisplayPhotoOriginalFileName", parameters.RO_TS_DisplayPhotoOriginalFileName);
            queryParameters.Add("@RO_TS_OutputVoltageAC_V", parameters.RO_TS_OutputVoltageAC_V);
            queryParameters.Add("@RO_TS_OutputCurrentAC_A", parameters.RO_TS_OutputCurrentAC_A);
            queryParameters.Add("@RO_TS_IPCurrentDC_A", parameters.RO_TS_IPCurrentDC_A);
            queryParameters.Add("@RO_TS_SpecificGravityC2", parameters.RO_TS_SpecificGravityC2);
            queryParameters.Add("@RO_TS_SpecificGravityC3", parameters.RO_TS_SpecificGravityC3);
            queryParameters.Add("@RO_TS_SpecificGravityC4", parameters.RO_TS_SpecificGravityC4);
            queryParameters.Add("@RO_TS_SpecificGravityC5", parameters.RO_TS_SpecificGravityC5);
            queryParameters.Add("@RO_TS_SpecificGravityC6", parameters.RO_TS_SpecificGravityC6);

            queryParameters.Add("@RO_CP_Visual", parameters.RO_CP_Visual);
            queryParameters.Add("@RO_CP_VisualImageFileName", parameters.RO_CP_VisualImageFileName);
            queryParameters.Add("@RO_CP_VisualImageOriginalFileName", parameters.RO_CP_VisualImageOriginalFileName);
            queryParameters.Add("@RO_CP_TerminalVoltage", parameters.RO_CP_TerminalVoltage);
            queryParameters.Add("@RO_CP_CommunicationWithBattery", parameters.RO_CP_CommunicationWithBattery);
            queryParameters.Add("@RO_CP_TerminalWire", parameters.RO_CP_TerminalWire);
            queryParameters.Add("@RO_CP_TerminalWireImageFileName", parameters.RO_CP_TerminalWireImageFileName);
            queryParameters.Add("@RO_CP_TerminalWireImageOriginalFileName", parameters.RO_CP_TerminalWireImageOriginalFileName);
            queryParameters.Add("@RO_CP_LifeCycle", parameters.RO_CP_LifeCycle);
            queryParameters.Add("@RO_CP_StringVoltageVariation", parameters.RO_CP_StringVoltageVariation);
            queryParameters.Add("@RO_CP_BatteryParametersSetting", parameters.RO_CP_BatteryParametersSetting);
            queryParameters.Add("@RO_CP_BatteryParametersSettingImageFileName", parameters.RO_CP_BatteryParametersSettingImageFileName);
            queryParameters.Add("@RO_CP_BatteryParametersSettingImageOriginalFileName", parameters.RO_CP_BatteryParametersSettingImageOriginalFileName);
            queryParameters.Add("@RO_CP_Spare", parameters.RO_CP_Spare);
            queryParameters.Add("@RO_CP_BMSStatus", parameters.RO_CP_BMSStatus);
            queryParameters.Add("@RO_CP_BMSSoftwareImageFileName", parameters.RO_CP_BMSSoftwareImageFileName);
            queryParameters.Add("@RO_CP_BMSSoftwareImageOriginalFileName", parameters.RO_CP_BMSSoftwareImageOriginalFileName);
            queryParameters.Add("@RO_CP_BMSType", parameters.RO_CP_BMSType);
            queryParameters.Add("@RO_CP_BatteryTemp", parameters.RO_CP_BatteryTemp);
            queryParameters.Add("@RO_CP_BMSSerialNumber", parameters.RO_CP_BMSSerialNumber);
            queryParameters.Add("@RO_CP_ProblemObserved", parameters.RO_CP_ProblemObserved);
            queryParameters.Add("@RO_CP_ProblemObservedByEngId", parameters.RO_CP_ProblemObservedByEngId);
            queryParameters.Add("@RO_CP_IsWarrantyVoid", parameters.RO_CP_IsWarrantyVoid);
            queryParameters.Add("@RO_CP_ReasonForVoid", parameters.RO_CP_ReasonForVoid);
            queryParameters.Add("@RO_CP_CommunicationWithMotorAndController", parameters.RO_CP_CommunicationWithMotorAndController);
            queryParameters.Add("@RO_CP_UnderWarranty", parameters.RO_CP_UnderWarranty);
            queryParameters.Add("@RO_CP_RPhaseVoltage", parameters.RO_CP_RPhaseVoltage);
            queryParameters.Add("@RO_CP_YPhaseVoltage", parameters.RO_CP_YPhaseVoltage);
            queryParameters.Add("@RO_CP_BPhaseVoltage", parameters.RO_CP_BPhaseVoltage);
            queryParameters.Add("@RO_CP_BatteryOCV", parameters.RO_CP_BatteryOCV);
            queryParameters.Add("@RO_CP_SystemVoltage", parameters.RO_CP_SystemVoltage);
            queryParameters.Add("@RO_CP_SpecificGravityC1", parameters.RO_CP_SpecificGravityC1);
            queryParameters.Add("@RO_CP_SpecificGravityC2", parameters.RO_CP_SpecificGravityC2);
            queryParameters.Add("@RO_CP_SpecificGravityC3", parameters.RO_CP_SpecificGravityC3);
            queryParameters.Add("@RO_CP_SpecificGravityC4", parameters.RO_CP_SpecificGravityC4);
            queryParameters.Add("@RO_CP_SpecificGravityC5", parameters.RO_CP_SpecificGravityC5);
            queryParameters.Add("@RO_CP_SpecificGravityC6", parameters.RO_CP_SpecificGravityC6);
            queryParameters.Add("@RO_CP_BatteryVoltageDropOnLoad", parameters.RO_CP_BatteryVoltageDropOnLoad);
            queryParameters.Add("@RO_CP_KmReading", parameters.RO_CP_KmReading);
            queryParameters.Add("@RO_CP_TotalRun", parameters.RO_CP_TotalRun);
            queryParameters.Add("@RO_CP_BatterySerialNo", parameters.RO_CP_BatterySerialNo);

            queryParameters.Add("@RO_CC_BatteryRepairedOnSite", parameters.RO_CC_BatteryRepairedOnSite);
            queryParameters.Add("@RO_CC_BatteryRepairedToPlant", parameters.RO_CC_BatteryRepairedToPlant);
            queryParameters.Add("@RO_OV_IsCustomerAvailable", parameters.RO_OV_IsCustomerAvailable);
            queryParameters.Add("@RO_OV_EngineerName", parameters.RO_OV_EngineerName);
            queryParameters.Add("@RO_OV_EngineerNumber", parameters.RO_OV_EngineerNumber);
            queryParameters.Add("@RO_OV_CustomerName", parameters.RO_OV_CustomerName);
            queryParameters.Add("@RO_OV_CustomerNameSecondary", parameters.RO_OV_CustomerNameSecondary);
            queryParameters.Add("@RO_OV_CustomerMobileNumber", parameters.RO_OV_CustomerMobileNumber);
            queryParameters.Add("@RO_OV_RequestOTP", parameters.RO_OV_RequestOTP);
            queryParameters.Add("@RO_OV_Signature", parameters.RO_OV_Signature);

            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveTicket", queryParameters);
        }

        public async Task<int> CreateDuplicateTicket(int TicketId, int IsEngineerType = 0)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@TicketId", TicketId);
            queryParameters.Add("@IsEngineerType", IsEngineerType);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("CreateDuplicateTicket", queryParameters);
        }


        public async Task<IEnumerable<ManageTicketList_Response>> GetManageTicketList(ManageTicket_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@FromDate", parameters.FromDate);
            queryParameters.Add("@ToDate", parameters.ToDate);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@EmployeeId", parameters.EmployeeId);
            queryParameters.Add("@TicketStatusId", parameters.TicketStatusId);
            queryParameters.Add("@Filter_TicketStatusId", parameters.Filter_TicketStatusId);
            queryParameters.Add("@IsChangeStatus_LogTicket", parameters.IsChangeStatus_LogTicket);
            queryParameters.Add("@IsEngineerType", parameters.IsEngineerType);
            queryParameters.Add("@IsReopen", parameters.IsReopen);
            queryParameters.Add("@BranchId", parameters.BranchId);
            queryParameters.Add("@IsTRC_Ticket", parameters.IsTRC_Ticket);
            //queryParameters.Add("@BranchRegionId", parameters.BranchRegionId);
            //queryParameters.Add("@BranchStateId", parameters.BranchStateId);
            queryParameters.Add("@FilterType", parameters.FilterType);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@SortBy", parameters.SortBy);
            queryParameters.Add("@OrderBy", parameters.OrderBy);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<ManageTicketList_Response>("GetTicketList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<ManageTicketDetail_Response?> GetManageTicketById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", Id);

            return (await ListByStoredProcedure<ManageTicketDetail_Response>("GetTicketById", queryParameters)).FirstOrDefault();
        }


        public async Task<IEnumerable<ManageTicketRemarks_Response>> GetTicketRemarkListById(ManageTicketRemarks_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@TicketId", parameters.TicketId);

            var result = await ListByStoredProcedure<ManageTicketRemarks_Response>("GetTicketRemarksById", queryParameters);

            return result;
        }

        public async Task<IEnumerable<ManageTicketStatusLog_Response>> GetManageTicketStatusLogById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", Id);

            var result = await ListByStoredProcedure<ManageTicketStatusLog_Response>("GetTicketStatusLog", queryParameters);

            return result;
        }

        public async Task<int> SaveTicketVisitHistory(ManageTicketEngineerVisitHistory_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@EngineerId", parameters.EngineerId);
            queryParameters.Add("@VisitDate", parameters.VisitDate);
            queryParameters.Add("@TicketId", parameters.TicketId);
            queryParameters.Add("@Latitude", parameters.Latitude);
            queryParameters.Add("@Longitude", parameters.Longitude);
            queryParameters.Add("@Address", parameters.Address);
            queryParameters.Add("@Status", parameters.Status);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveTicketVisitHistory", queryParameters);
        }

        public async Task<IEnumerable<ManageTicketEngineerVisitHistory_Response>> GetTicketVisitHistoryList(ManageTicketEngineerVisitHistory_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@TicketId", parameters.TicketId);
            queryParameters.Add("@EngineerId", parameters.EngineerId);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<ManageTicketEngineerVisitHistory_Response>("GetTicketVisitHistoryList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }


        public async Task<int> SaveManageTicketPartDetail(ManageTicketPartDetails_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@TicketId", parameters.TicketId);
            queryParameters.Add("@SpareCategoryId", parameters.SpareCategoryId);
            queryParameters.Add("@SpareDetailsId", parameters.SpareDetailsId);
            queryParameters.Add("@Quantity", parameters.Quantity);
            queryParameters.Add("@AvailableQty", parameters.AvailableQty);
            //queryParameters.Add("@PartStatusId", parameters.PartStatusId);

            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveTicketPartDetails", queryParameters);
        }

        public async Task<int> DeleteManageTicketPartDetail(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", Id);

            return await SaveByStoredProcedure<int>("DeleteTicketPartDetails", queryParameters);
        }

        public async Task<IEnumerable<ManageTicketPartDetails_Response>> GetManageTicketPartDetailById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@TicketId", Id);

            var result = await ListByStoredProcedure<ManageTicketPartDetails_Response>("GetTicketPartDetailById", queryParameters);

            return result;
        }

        public async Task<IEnumerable<ManageTicketCustomerMobileNumber_Response>> GetCustomerMobileNumberList(string SearchText)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@SearchText", SearchText.SanitizeValue());

            var result = await ListByStoredProcedure<ManageTicketCustomerMobileNumber_Response>("GetCustomerMobileNumberList", queryParameters);
            return result;
        }

        public async Task<ManageTicketCustomerDetail_Response?> GetCustomerDetailByMobileNumber(string mobile)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@MobileNumber", mobile);

            return (await ListByStoredProcedure<ManageTicketCustomerDetail_Response>("GetCustomerDetailByMobileNumber", queryParameters)).FirstOrDefault();
        }

        public async Task<int> SaveManageTicketLogHistory(int TicketId)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@TicketId", TicketId);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveTicketLogHistory", queryParameters);
        }

        public async Task<IEnumerable<ManageTicketLogHistory_Response>> GetManageTicketLogHistoryList(ManageTicketLogHistory_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@TicketId", parameters.TicketId);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);

            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<ManageTicketLogHistory_Response>("GetTicketLogHistoryList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<IEnumerable<ValidateTicketProductSerialNumber_Response>> ValidateTicketProductSerialNumberById(string ProductSerialNumber, bool IsOldProduct, int TicketId)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@ProductSerialNumber", ProductSerialNumber);
            queryParameters.Add("@IsOldProduct", IsOldProduct);
            queryParameters.Add("@TicketId", TicketId);

            var result = await ListByStoredProcedure<ValidateTicketProductSerialNumber_Response>("ValidateTicketProductSerialNumberById", queryParameters);
            return result;
        }

        public async Task<int> SaveFeedbackQuestionAnswer(FeedbackQuestionAnswer_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@TicketId", parameters.TicketId);
            queryParameters.Add("@Question_Answer_Json_Format", parameters.Question_Answer_Json_Format);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveFeedbackQuestionAnswer", queryParameters);
        }

        public async Task<IEnumerable<FeedbackQuestionAnswer_Response>> GetFeedbackQuestionAnswerList(FeedbackQuestionAnswerSearch_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@TicketId", parameters.TicketId);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<FeedbackQuestionAnswer_Response>("GetFeedbackQuestionAnswerList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }
    }
}
