﻿using CLN.Application.Enums;
using CLN.Application.Helpers;
using CLN.Application.Interfaces;
using CLN.Application.Models;
using CLN.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace CLN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageTRCController : CustomBaseController
    {
        private ResponseModel _response;
        private IFileManager _fileManager;

        private readonly IManageTRCRepository _manageTRCRepository;
        private readonly IManageTicketRepository _manageTicketRepository;
        private readonly IAddressRepository _addressRepository;

        public ManageTRCController(IManageTRCRepository manageTRCRepository, IManageTicketRepository manageTicketRepository, IFileManager fileManager, IAddressRepository addressRepository)
        {
            _fileManager = fileManager;

            _manageTRCRepository = manageTRCRepository;
            _manageTicketRepository = manageTicketRepository;
            _addressRepository = addressRepository;

            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        #region Manage Ticket

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveManageTRC(ManageTRC_Request parameters)
        {
            // Save/Update
            int result = await _manageTRCRepository.SaveManageTRC(parameters);

            if (result == (int)SaveOperationEnums.NoRecordExists)
            {
                _response.Message = "No record exists";
            }
            else if (result == (int)SaveOperationEnums.ReocrdExists)
            {
                _response.Message = "Record already exists";
            }
            else if (result == (int)SaveOperationEnums.NoResult)
            {
                _response.Message = "Something went wrong, please try again";
            }
            else
            {
                _response.Message = "Record details saved sucessfully";
            }

            // Add/Update Ticket Part Details
            if (result > 0)
            {
                foreach (var item in parameters.PartDetail)
                {
                    var vManageTRCPartDetails_Request = new ManageTRCPartDetails_Request()
                    {
                        Id = Convert.ToInt32(item.Id),
                        TRCId = result,
                        SparePartNo = item.SparePartNo,
                        PartDescription = item.PartDescription,
                        Quantity = item.Quantity,
                        Remarks = item.Remarks,
                        PartStatusId = item.PartStatusId,
                    };
                    int resultTechnicalSupportAddUpdate = await _manageTRCRepository.SaveManageTRCPartDetail(vManageTRCPartDetails_Request);
                }
            }

            _response.Id = result;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetManageTRCList(ManageTRC_Search parameters)
        {
            var objList = await _manageTRCRepository.GetManageTRCList(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetManageTRCById(int TicketId)
        {
            var vManageTRCDetail_Response = new ManageTRCDetail_Response();

            if (TicketId <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _manageTRCRepository.GetManageTRCById(TicketId);
                if (vResultObj != null)
                {
                    // TRC Deatil Bind
                    vManageTRCDetail_Response.Id = vResultObj.Id;
                    vManageTRCDetail_Response.TicketId = vResultObj.TicketId;

                    vManageTRCDetail_Response.TRCNumber = vResultObj.TRCNumber;
                    vManageTRCDetail_Response.TRCDate = vResultObj.TRCDate;
                    vManageTRCDetail_Response.TRCTime = vResultObj.TRCTime;

                    vManageTRCDetail_Response.DA_DefectObserved = vResultObj.DA_DefectObserved;
                    vManageTRCDetail_Response.DA_ActionTaken = vResultObj.DA_ActionTaken;
                    vManageTRCDetail_Response.DA_Remarks = vResultObj.DA_Remarks;

                    vManageTRCDetail_Response.PI_BatteryReceivedDate = vResultObj.PI_BatteryReceivedDate;
                    vManageTRCDetail_Response.PI_BatteryReceivedTime = vResultObj.PI_BatteryReceivedTime;
                    vManageTRCDetail_Response.PI_PDIDoneDate = vResultObj.PI_PDIDoneDate;
                    vManageTRCDetail_Response.PI_PDIDoneTime = vResultObj.PI_PDIDoneTime;
                    vManageTRCDetail_Response.PI_PDIDoneById = vResultObj.PI_PDIDoneById;
                    vManageTRCDetail_Response.PI_PDIDoneByEngg = vResultObj.PI_PDIDoneByEngg;
                    vManageTRCDetail_Response.PI_Note = vResultObj.PI_Note;

                    vManageTRCDetail_Response.TRCStatusId = vResultObj.TRCStatusId;
                    vManageTRCDetail_Response.TRCSStatus = vResultObj.TRCSStatus;

                    vManageTRCDetail_Response.IsActive = vResultObj.IsActive;

                    vManageTRCDetail_Response.CreatorName = vResultObj.CreatorName;
                    vManageTRCDetail_Response.CreatedBy = vResultObj.CreatedBy;
                    vManageTRCDetail_Response.CreatedDate = vResultObj.CreatedDate;
                    ;
                    vManageTRCDetail_Response.ModifierName = vResultObj.ModifierName;
                    vManageTRCDetail_Response.ModifiedBy = vResultObj.ModifiedBy;
                    vManageTRCDetail_Response.ModifiedDate = vResultObj.ModifiedDate;


                    var vResultPartListObj = await _manageTRCRepository.GetManageTRCPartDetailById(TicketId);
                    foreach (var item in vResultPartListObj)
                    {
                        var vManageTRCPartDetails_Response = new ManageTRCPartDetails_Response()
                        {
                            Id = item.Id,
                            TRCId = item.TRCId,
                            SparePartNo = item.SparePartNo,
                            PartDescription = item.PartDescription,
                            Quantity = item.Quantity,
                            Remarks = item.Remarks,
                            PartStatusId = item.PartStatusId
                        };

                        vManageTRCDetail_Response.PartDetails.Add(vManageTRCPartDetails_Response);
                    }
                }


                // Ticket Detail Bind
                vManageTRCDetail_Response.TicketDetail = new ManageTicketDetail_Response();
                var vResultTicketDetailObj = await _manageTicketRepository.GetManageTicketById(TicketId);
                if (vResultTicketDetailObj != null)
                {
                    vManageTRCDetail_Response.TicketDetail.Id = vResultTicketDetailObj.Id;
                    vManageTRCDetail_Response.TicketDetail.TicketNumber = vResultTicketDetailObj.TicketNumber;
                    vManageTRCDetail_Response.TicketDetail.TicketDate = vResultTicketDetailObj.TicketDate;
                    vManageTRCDetail_Response.TicketDetail.TicketTime = vResultTicketDetailObj.TicketTime;
                    vManageTRCDetail_Response.TicketDetail.TicketPriorityId = vResultTicketDetailObj.TicketPriorityId;
                    vManageTRCDetail_Response.TicketDetail.TicketPriority = vResultTicketDetailObj.TicketPriority;
                    vManageTRCDetail_Response.TicketDetail.TicketSLADays = vResultTicketDetailObj.TicketSLADays;
                    vManageTRCDetail_Response.TicketDetail.TicketSLAHours = vResultTicketDetailObj.TicketSLAHours;
                    vManageTRCDetail_Response.TicketDetail.TicketSLAMin = vResultTicketDetailObj.TicketSLAMin;
                    vManageTRCDetail_Response.TicketDetail.SLAStatus = vResultTicketDetailObj.SLAStatus;
                    vManageTRCDetail_Response.TicketDetail.TicketAging = vResultTicketDetailObj.TicketAging;


                    vManageTRCDetail_Response.TicketDetail.CD_LoggingSourceId = vResultTicketDetailObj.CD_LoggingSourceId;
                    vManageTRCDetail_Response.TicketDetail.CD_LoggingSourceChannel = vResultTicketDetailObj.CD_LoggingSourceChannel;

                    vManageTRCDetail_Response.TicketDetail.CD_CallerTypeId = vResultTicketDetailObj.CD_CallerTypeId;
                    vManageTRCDetail_Response.TicketDetail.CD_CallerType = vResultTicketDetailObj.CD_CallerType;
                    vManageTRCDetail_Response.TicketDetail.CD_CallerName = vResultTicketDetailObj.CD_CallerName;
                    vManageTRCDetail_Response.TicketDetail.CD_CallerMobile = vResultTicketDetailObj.CD_CallerMobile;
                    vManageTRCDetail_Response.TicketDetail.CD_CallerEmailId = vResultTicketDetailObj.CD_CallerEmailId;

                    vManageTRCDetail_Response.TicketDetail.CD_CallerAddressId = vResultTicketDetailObj.CD_CallerAddressId;
                    vManageTRCDetail_Response.TicketDetail.CD_CallerAddress1 = vResultTicketDetailObj.CD_CallerAddress1;
                    vManageTRCDetail_Response.TicketDetail.CD_CallerRegionId = vResultTicketDetailObj.CD_CallerRegionId;
                    vManageTRCDetail_Response.TicketDetail.CD_CallerRegionName = vResultTicketDetailObj.CD_CallerRegionName;
                    vManageTRCDetail_Response.TicketDetail.CD_CallerStateId = vResultTicketDetailObj.CD_CallerStateId;
                    vManageTRCDetail_Response.TicketDetail.CD_CallerStateName = vResultTicketDetailObj.CD_CallerStateName;
                    vManageTRCDetail_Response.TicketDetail.CD_CallerDistrictId = vResultTicketDetailObj.CD_CallerDistrictId;
                    vManageTRCDetail_Response.TicketDetail.CD_CallerDistrictName = vResultTicketDetailObj.CD_CallerDistrictName;
                    vManageTRCDetail_Response.TicketDetail.CD_CallerCityId = vResultTicketDetailObj.CD_CallerCityId;
                    vManageTRCDetail_Response.TicketDetail.CD_CallerCityName = vResultTicketDetailObj.CD_CallerCityName;
                    vManageTRCDetail_Response.TicketDetail.CD_CallerPinCode = vResultTicketDetailObj.CD_CallerPinCode;
                    vManageTRCDetail_Response.TicketDetail.CD_CallerRemarks = vResultTicketDetailObj.CD_CallerRemarks;

                    vManageTRCDetail_Response.TicketDetail.CD_IsSiteAddressSameAsCaller = vResultTicketDetailObj.CD_IsSiteAddressSameAsCaller;
                    vManageTRCDetail_Response.TicketDetail.CD_ComplaintTypeId = vResultTicketDetailObj.CD_ComplaintTypeId;
                    vManageTRCDetail_Response.TicketDetail.CD_ComplaintType = vResultTicketDetailObj.CD_ComplaintType;
                    vManageTRCDetail_Response.TicketDetail.CD_IsOldProduct = vResultTicketDetailObj.CD_IsOldProduct;
                    vManageTRCDetail_Response.TicketDetail.CD_ProductSerialNumberId = vResultTicketDetailObj.CD_ProductSerialNumberId;
                    vManageTRCDetail_Response.TicketDetail.CD_ProductSerialNumber = vResultTicketDetailObj.CD_ProductSerialNumber;

                    vManageTRCDetail_Response.TicketDetail.CD_CustomerTypeId = vResultTicketDetailObj.CD_CustomerTypeId;
                    vManageTRCDetail_Response.TicketDetail.CD_CustomerTypeId = vResultTicketDetailObj.CD_CustomerTypeId;
                    vManageTRCDetail_Response.TicketDetail.CD_CustomerNameId = vResultTicketDetailObj.CD_CustomerNameId;
                    vManageTRCDetail_Response.TicketDetail.CD_CustomerName = vResultTicketDetailObj.CD_CustomerName;
                    vManageTRCDetail_Response.TicketDetail.CD_CustomerMobile = vResultTicketDetailObj.CD_CustomerMobile;
                    vManageTRCDetail_Response.TicketDetail.CD_CustomerEmail = vResultTicketDetailObj.CD_CustomerEmail;

                    vManageTRCDetail_Response.TicketDetail.CD_CustomerAddressId = vResultTicketDetailObj.CD_CustomerAddressId;
                    vManageTRCDetail_Response.TicketDetail.CD_CustomerAddress1 = vResultTicketDetailObj.CD_CustomerAddress1;
                    vManageTRCDetail_Response.TicketDetail.CD_CustomerRegionId = vResultTicketDetailObj.CD_CustomerRegionId;
                    vManageTRCDetail_Response.TicketDetail.CD_CustomerRegionName = vResultTicketDetailObj.CD_CustomerRegionName;
                    vManageTRCDetail_Response.TicketDetail.CD_CustomerStateId = vResultTicketDetailObj.CD_CustomerStateId;
                    vManageTRCDetail_Response.TicketDetail.CD_CustomerStateName = vResultTicketDetailObj.CD_CustomerStateName;
                    vManageTRCDetail_Response.TicketDetail.CD_CustomerDistrictId = vResultTicketDetailObj.CD_CustomerDistrictId;
                    vManageTRCDetail_Response.TicketDetail.CD_CustomerDistrictName = vResultTicketDetailObj.CD_CustomerDistrictName;
                    vManageTRCDetail_Response.TicketDetail.CD_CustomerCityId = vResultTicketDetailObj.CD_CustomerCityId;
                    vManageTRCDetail_Response.TicketDetail.CD_CustomerCityName = vResultTicketDetailObj.CD_CustomerCityName;
                    vManageTRCDetail_Response.TicketDetail.CD_CustomerPinCode = vResultTicketDetailObj.CD_CustomerPinCode;

                    vManageTRCDetail_Response.TicketDetail.CD_SiteCustomerName = vResultTicketDetailObj.CD_SiteCustomerName;
                    vManageTRCDetail_Response.TicketDetail.CD_SiteContactName = vResultTicketDetailObj.CD_SiteContactName;
                    vManageTRCDetail_Response.TicketDetail.CD_SitContactMobile = vResultTicketDetailObj.CD_SitContactMobile;

                    vManageTRCDetail_Response.TicketDetail.CD_SiteAddressId = vResultTicketDetailObj.CD_SiteAddressId;
                    vManageTRCDetail_Response.TicketDetail.CD_SiteCustomerAddress1 = vResultTicketDetailObj.CD_SiteCustomerAddress1;
                    vManageTRCDetail_Response.TicketDetail.CD_SiteCustomerRegionId = vResultTicketDetailObj.CD_SiteCustomerRegionId;
                    vManageTRCDetail_Response.TicketDetail.CD_SiteCustomerRegionName = vResultTicketDetailObj.CD_SiteCustomerRegionName;
                    vManageTRCDetail_Response.TicketDetail.CD_SiteCustomerStateId = vResultTicketDetailObj.CD_SiteCustomerStateId;
                    vManageTRCDetail_Response.TicketDetail.CD_SiteCustomerStateName = vResultTicketDetailObj.CD_SiteCustomerStateName;
                    vManageTRCDetail_Response.TicketDetail.CD_SiteCustomerDistrictId = vResultTicketDetailObj.CD_SiteCustomerDistrictId;
                    vManageTRCDetail_Response.TicketDetail.CD_SiteCustomerDistrictName = vResultTicketDetailObj.CD_SiteCustomerDistrictName;
                    vManageTRCDetail_Response.TicketDetail.CD_SiteCustomerCityId = vResultTicketDetailObj.CD_SiteCustomerCityId;
                    vManageTRCDetail_Response.TicketDetail.CD_SiteCustomerCityName = vResultTicketDetailObj.CD_SiteCustomerCityName;
                    vManageTRCDetail_Response.TicketDetail.CD_SiteCustomerPinCode = vResultTicketDetailObj.CD_SiteCustomerPinCode;

                    vManageTRCDetail_Response.TicketDetail.BD_BatteryBOMNumberId = vResultTicketDetailObj.BD_BatteryBOMNumberId;
                    vManageTRCDetail_Response.TicketDetail.BD_BatteryBOMNumber = vResultTicketDetailObj.BD_BatteryBOMNumber;
                    vManageTRCDetail_Response.TicketDetail.BD_BatteryProductCategoryId = vResultTicketDetailObj.BD_BatteryProductCategoryId;
                    vManageTRCDetail_Response.TicketDetail.BD_ProductCategory = vResultTicketDetailObj.BD_ProductCategory;
                    vManageTRCDetail_Response.TicketDetail.BD_BatterySegmentId = vResultTicketDetailObj.BD_BatterySegmentId;
                    vManageTRCDetail_Response.TicketDetail.BD_Segment = vResultTicketDetailObj.BD_Segment;
                    vManageTRCDetail_Response.TicketDetail.BD_BatterySubSegmentId = vResultTicketDetailObj.BD_BatterySubSegmentId;
                    vManageTRCDetail_Response.TicketDetail.BD_SubSegment = vResultTicketDetailObj.BD_SubSegment;
                    vManageTRCDetail_Response.TicketDetail.BD_BatteryProductModelId = vResultTicketDetailObj.BD_BatteryProductModelId;
                    vManageTRCDetail_Response.TicketDetail.BD_ProductModel = vResultTicketDetailObj.BD_ProductModel;
                    vManageTRCDetail_Response.TicketDetail.BD_BatteryCellChemistryId = vResultTicketDetailObj.BD_BatteryCellChemistryId;
                    vManageTRCDetail_Response.TicketDetail.BD_CellChemistry = vResultTicketDetailObj.BD_CellChemistry;
                    vManageTRCDetail_Response.TicketDetail.BD_DateofManufacturing = vResultTicketDetailObj.BD_DateofManufacturing;
                    vManageTRCDetail_Response.TicketDetail.BD_ProbReportedByCustId = vResultTicketDetailObj.BD_ProbReportedByCustId;
                    vManageTRCDetail_Response.TicketDetail.BD_ProbReportedByCust = vResultTicketDetailObj.BD_ProbReportedByCust;
                    vManageTRCDetail_Response.TicketDetail.BD_ProblemDescription = vResultTicketDetailObj.BD_ProblemDescription;

                    vManageTRCDetail_Response.TicketDetail.BD_WarrantyStartDate = vResultTicketDetailObj.BD_WarrantyStartDate;
                    vManageTRCDetail_Response.TicketDetail.BD_WarrantyEndDate = vResultTicketDetailObj.BD_WarrantyEndDate;
                    vManageTRCDetail_Response.TicketDetail.BD_WarrantyStatusId = vResultTicketDetailObj.BD_WarrantyStatusId;
                    vManageTRCDetail_Response.TicketDetail.BD_WarrantyStatus = vResultTicketDetailObj.BD_WarrantyStatus;
                    vManageTRCDetail_Response.TicketDetail.BD_TechnicalSupportEnggId = vResultTicketDetailObj.BD_TechnicalSupportEnggId;
                    vManageTRCDetail_Response.TicketDetail.BD_TechnicalSupportEngg = vResultTicketDetailObj.BD_TechnicalSupportEngg;

                    vManageTRCDetail_Response.TicketDetail.TSAD_Visual = vResultTicketDetailObj.TSAD_Visual;
                    vManageTRCDetail_Response.TicketDetail.TSAD_VisualImageFileName = vResultTicketDetailObj.TSAD_VisualImageFileName;
                    vManageTRCDetail_Response.TicketDetail.TSAD_VisualImageOriginalFileName = vResultTicketDetailObj.TSAD_VisualImageOriginalFileName;
                    vManageTRCDetail_Response.TicketDetail.TSAD_VisualImageURL = vResultTicketDetailObj.TSAD_VisualImageURL;
                    vManageTRCDetail_Response.TicketDetail.TSAD_BatteryTemperature = vResultTicketDetailObj.TSAD_BatteryTemperature;
                    vManageTRCDetail_Response.TicketDetail.TSAD_CurrentChargingValue = vResultTicketDetailObj.TSAD_CurrentChargingValue;
                    vManageTRCDetail_Response.TicketDetail.TSAD_CurrentDischargingValue = vResultTicketDetailObj.TSAD_CurrentDischargingValue;
                    vManageTRCDetail_Response.TicketDetail.TSAD_BatteryTemperature = vResultTicketDetailObj.TSAD_BatteryTemperature;
                    vManageTRCDetail_Response.TicketDetail.TSAD_BatterVoltage = vResultTicketDetailObj.TSAD_BatterVoltage;
                    vManageTRCDetail_Response.TicketDetail.TSAD_CellDiffrence = vResultTicketDetailObj.TSAD_CellDiffrence;
                    vManageTRCDetail_Response.TicketDetail.TSAD_ProtectionsId = vResultTicketDetailObj.TSAD_ProtectionsId;
                    vManageTRCDetail_Response.TicketDetail.TSAD_Protections = vResultTicketDetailObj.TSAD_Protections;

                    vManageTRCDetail_Response.TicketDetail.TSAD_CycleCount = vResultTicketDetailObj.TSAD_CycleCount;
                    vManageTRCDetail_Response.TicketDetail.TSPD_PhysicaImageFileName = vResultTicketDetailObj.TSPD_PhysicaImageFileName;
                    vManageTRCDetail_Response.TicketDetail.TSPD_PhysicaImageOriginalFileName = vResultTicketDetailObj.TSPD_PhysicaImageOriginalFileName;
                    vManageTRCDetail_Response.TicketDetail.TSPD_PhysicaImageURL = vResultTicketDetailObj.TSPD_PhysicaImageURL;
                    vManageTRCDetail_Response.TicketDetail.TSPD_AnyPhysicalDamage = vResultTicketDetailObj.TSPD_AnyPhysicalDamage;
                    vManageTRCDetail_Response.TicketDetail.TSPD_Other = vResultTicketDetailObj.TSPD_Other;
                    vManageTRCDetail_Response.TicketDetail.TSPD_IsWarrantyVoid = vResultTicketDetailObj.TSPD_IsWarrantyVoid;
                    vManageTRCDetail_Response.TicketDetail.TSSP_SolutionProvider = vResultTicketDetailObj.TSSP_SolutionProvider;
                    vManageTRCDetail_Response.TicketDetail.TSSP_AllocateToServiceEnggId = vResultTicketDetailObj.TSSP_AllocateToServiceEnggId;
                    vManageTRCDetail_Response.TicketDetail.TSSP_AllocateToServiceEngg = vResultTicketDetailObj.TSSP_AllocateToServiceEngg;
                    vManageTRCDetail_Response.TicketDetail.TSSP_Remarks = vResultTicketDetailObj.TSSP_Remarks;

                    vManageTRCDetail_Response.TicketDetail.CP_Visual = vResultTicketDetailObj.CP_Visual;
                    vManageTRCDetail_Response.TicketDetail.CP_VisualImageFileName = vResultTicketDetailObj.CP_VisualImageFileName;
                    vManageTRCDetail_Response.TicketDetail.CP_VisualImageOriginalFileName = vResultTicketDetailObj.CP_VisualImageOriginalFileName;
                    vManageTRCDetail_Response.TicketDetail.CP_VisualImageURL = vResultTicketDetailObj.CP_VisualImageURL;
                    vManageTRCDetail_Response.TicketDetail.CP_TerminalVoltage = vResultTicketDetailObj.CP_TerminalVoltage;
                    vManageTRCDetail_Response.TicketDetail.CP_CommunicationWithBattery = vResultTicketDetailObj.CP_CommunicationWithBattery;
                    vManageTRCDetail_Response.TicketDetail.CP_TerminalWire = vResultTicketDetailObj.CP_TerminalWire;
                    vManageTRCDetail_Response.TicketDetail.CP_TerminalWireImageFileName = vResultTicketDetailObj.CP_TerminalWireImageFileName;
                    vManageTRCDetail_Response.TicketDetail.CP_TerminalWireImageOriginalFileName = vResultTicketDetailObj.CP_TerminalWireImageOriginalFileName;
                    vManageTRCDetail_Response.TicketDetail.CP_TerminalWireImageURL = vResultTicketDetailObj.CP_TerminalWireImageURL;
                    vManageTRCDetail_Response.TicketDetail.CP_LifeCycle = vResultTicketDetailObj.CP_LifeCycle;
                    vManageTRCDetail_Response.TicketDetail.CP_StringVoltageVariation = vResultTicketDetailObj.CP_StringVoltageVariation;
                    vManageTRCDetail_Response.TicketDetail.CP_BatteryParametersSetting = vResultTicketDetailObj.CP_BatteryParametersSetting;
                    vManageTRCDetail_Response.TicketDetail.CP_BatteryParametersSettingImageFileName = vResultTicketDetailObj.CP_BatteryParametersSettingImageFileName;
                    vManageTRCDetail_Response.TicketDetail.CP_BatteryParametersSettingImageOriginalFileName = vResultTicketDetailObj.CP_BatteryParametersSettingImageOriginalFileName;
                    vManageTRCDetail_Response.TicketDetail.CP_BatteryParametersSettingImageURL = vResultTicketDetailObj.CP_BatteryParametersSettingImageURL;
                    vManageTRCDetail_Response.TicketDetail.CP_Spare = vResultTicketDetailObj.CP_Spare;
                    vManageTRCDetail_Response.TicketDetail.CP_BMSStatus = vResultTicketDetailObj.CP_BMSStatus;
                    vManageTRCDetail_Response.TicketDetail.CP_BMSSoftwareImageFileName = vResultTicketDetailObj.CP_BMSSoftwareImageFileName;
                    vManageTRCDetail_Response.TicketDetail.CP_BMSSoftwareImageOriginalFileName = vResultTicketDetailObj.CP_BMSSoftwareImageOriginalFileName;
                    vManageTRCDetail_Response.TicketDetail.CP_BMSSoftwareImageURL = vResultTicketDetailObj.CP_BMSSoftwareImageURL;
                    vManageTRCDetail_Response.TicketDetail.CP_BMSType = vResultTicketDetailObj.CP_BMSType;
                    vManageTRCDetail_Response.TicketDetail.CP_BatteryTemp = vResultTicketDetailObj.CP_BatteryTemp;
                    vManageTRCDetail_Response.TicketDetail.CP_BMSSerialNumber = vResultTicketDetailObj.CP_BMSSerialNumber;
                    vManageTRCDetail_Response.TicketDetail.CP_ProblemObserved = vResultTicketDetailObj.CP_ProblemObserved;

                    vManageTRCDetail_Response.TicketDetail.CC_BatteryRepairedOnSite = vResultTicketDetailObj.CC_BatteryRepairedOnSite;
                    vManageTRCDetail_Response.TicketDetail.CC_BatteryRepairedToPlant = vResultTicketDetailObj.CC_BatteryRepairedToPlant;

                    vManageTRCDetail_Response.TicketDetail.OV_IsCustomerAvailable = vResultTicketDetailObj.OV_IsCustomerAvailable;
                    vManageTRCDetail_Response.TicketDetail.OV_EngineerName = vResultTicketDetailObj.OV_EngineerName;
                    vManageTRCDetail_Response.TicketDetail.OV_EngineerNumber = vResultTicketDetailObj.OV_EngineerNumber;
                    vManageTRCDetail_Response.TicketDetail.OV_CustomerName = vResultTicketDetailObj.OV_CustomerName;
                    vManageTRCDetail_Response.TicketDetail.OV_CustomerNameSecondary = vResultTicketDetailObj.OV_CustomerNameSecondary;
                    vManageTRCDetail_Response.TicketDetail.OV_CustomerMobileNumber = vResultTicketDetailObj.OV_CustomerMobileNumber;
                    vManageTRCDetail_Response.TicketDetail.OV_RequestOTP = vResultTicketDetailObj.OV_RequestOTP;
                    vManageTRCDetail_Response.TicketDetail.OV_Signature = vResultTicketDetailObj.OV_Signature;

                    vManageTRCDetail_Response.TicketDetail.TicketStatusId = vResultTicketDetailObj.TicketStatusId;
                    vManageTRCDetail_Response.TicketDetail.TicketStatus = vResultTicketDetailObj.TicketStatus;
                    vManageTRCDetail_Response.TicketDetail.TicketStatusSequenceNo = vResultTicketDetailObj.TicketStatusSequenceNo;

                    vManageTRCDetail_Response.TicketDetail.IsActive = vResultTicketDetailObj.IsActive;

                    vManageTRCDetail_Response.TicketDetail.CreatorName = vResultTicketDetailObj.CreatorName;
                    vManageTRCDetail_Response.TicketDetail.CreatedBy = vResultTicketDetailObj.CreatedBy;
                    vManageTRCDetail_Response.TicketDetail.CreatedDate = vResultTicketDetailObj.CreatedDate;
                    ;
                    vManageTRCDetail_Response.TicketDetail.ModifierName = vResultTicketDetailObj.ModifierName;
                    vManageTRCDetail_Response.TicketDetail.ModifiedBy = vResultTicketDetailObj.ModifiedBy;
                    vManageTRCDetail_Response.TicketDetail.ModifiedDate = vResultTicketDetailObj.ModifiedDate;


                    var vResultTicketPartListObj = await _manageTicketRepository.GetManageTicketPartDetailById(TicketId);
                    foreach (var item in vResultTicketPartListObj)
                    {
                        var vManageTicketPartDetails_Response = new ManageTicketPartDetails_Response()
                        {
                            Id = item.Id,
                            TicketId = item.TicketId,
                            SpareCategoryId = item.SpareCategoryId,
                            SpareCategory = item.SpareCategory,
                            SpareDetailsId = item.SpareDetailsId,
                            UniqueCode = item.UniqueCode,
                            SpareDesc = item.SpareDesc,
                            Quantity = item.Quantity,
                            AvailableQty = item.AvailableQty,
                            //PartStatusId = item.PartStatusId,
                            //PartStatus = item.PartStatus
                        };

                        vManageTRCDetail_Response.TicketDetail.PartDetails.Add(vManageTicketPartDetails_Response);
                    }

                    // Status Log
                    var vTicketStatusLogListObj = await _manageTicketRepository.GetManageTicketStatusLogById(TicketId);
                    foreach (var item in vTicketStatusLogListObj)
                    {
                        var vManageTicketStatusLog_Response = new ManageTicketStatusLog_Response()
                        {
                            TicketId = item.TicketId,
                            StartDate = item.StartDate,
                            EndDate = item.EndDate,
                            StatusId = item.StatusId,
                            TicketStatus = item.TicketStatus,
                            PriorityId = item.PriorityId,
                            Priority = item.Priority,
                            Agging = item.Agging
                        };

                        vManageTRCDetail_Response.TicketDetail.TicketStatusLog.Add(vManageTicketStatusLog_Response);
                    }
                }

                _response.Data = vManageTRCDetail_Response;
            }
            return _response;
        }

        #endregion
    }
}
