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
    public class ManageTicketController : CustomBaseController
    {
        private ResponseModel _response;
        private IFileManager _fileManager;

        private readonly IManageTicketRepository _manageTicketRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IManageEnquiryRepository _manageEnquiryRepository;
        private readonly IManageTRCRepository _manageTRCRepository;
        private readonly ICustomerRepository _customerRepository;

        public ManageTicketController(IManageTicketRepository manageTicketRepository, IManageTRCRepository manageTRCRepository, IFileManager fileManager, IAddressRepository addressRepository, IManageEnquiryRepository manageEnquiryRepository, ICustomerRepository customerRepository)
        {
            _fileManager = fileManager;

            _manageTicketRepository = manageTicketRepository;
            _addressRepository = addressRepository;
            _manageEnquiryRepository = manageEnquiryRepository;
            _manageTRCRepository = manageTRCRepository;
            _customerRepository = customerRepository;

            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        #region Manage Ticket

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveManageTicket(ManageTicket_Request parameters)
        {
            // Image Upload
            if (parameters! != null && !string.IsNullOrWhiteSpace(parameters.CP_VisualImage_Base64))
            {
                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(parameters.CP_VisualImage_Base64, "\\Uploads\\Ticket\\", parameters.CP_VisualImageOriginalFileName);

                if (!string.IsNullOrWhiteSpace(vUploadFile))
                {
                    parameters.CP_VisualImageFileName = vUploadFile;
                }
            }

            if (parameters! != null && !string.IsNullOrWhiteSpace(parameters.CP_TerminalWireImage_Base64))
            {
                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(parameters.CP_TerminalWireImage_Base64, "\\Uploads\\Ticket\\", parameters.CP_TerminalWireImageOriginalFileName);

                if (!string.IsNullOrWhiteSpace(vUploadFile))
                {
                    parameters.CP_TerminalWireImageFileName = vUploadFile;
                }
            }

            if (parameters! != null && !string.IsNullOrWhiteSpace(parameters.CP_BatteryParametersSettingImage_Base64))
            {
                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(parameters.CP_BatteryParametersSettingImage_Base64, "\\Uploads\\Ticket\\", parameters.CP_BatteryParametersSettingImageOriginalFileName);

                if (!string.IsNullOrWhiteSpace(vUploadFile))
                {
                    parameters.CP_BatteryParametersSettingImageFileName = vUploadFile;
                }
            }

            if (parameters! != null && !string.IsNullOrWhiteSpace(parameters.CP_BMSSoftwareImage_Base64))
            {
                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(parameters.CP_BMSSoftwareImage_Base64, "\\Uploads\\Ticket\\", parameters.CP_BMSSoftwareImageOriginalFileName);

                if (!string.IsNullOrWhiteSpace(vUploadFile))
                {
                    parameters.CP_BMSSoftwareImageFileName = vUploadFile;
                }
            }

            // Set Status if id zero for new record
            if (parameters.Id == 0)
            {
                parameters.TicketStatusFromId = 1;
            }

            // Save/Update
            int result = await _manageTicketRepository.SaveManageTicket(parameters);

            if (result == (int)SaveOperationEnums.NoRecordExists)
            {
                _response.Message = "No record exists";
            }
            else if (result == (int)SaveOperationEnums.ReocrdExists)
            {
                _response.Message = "Record is already exists";
            }
            else if (result == (int)SaveOperationEnums.NoResult)
            {
                _response.Message = "Something went wrong, please try again";
            }
            else
            {
                _response.Message = "Record details saved sucessfully";
            }

            // Add/Update Ticket Details
            if (result > 0)
            {
                // Caller Address Detail
                var CallerAddressDetail = new Address_Request()
                {
                    Id = Convert.ToInt32(parameters.CD_CallerAddressId),
                    RefId = result,
                    RefType = "Ticket",
                    Address1 = parameters.CD_CallerAddress1,
                    RegionId = parameters.CD_CallerRegionId,
                    StateId = parameters.CD_CallerStateId,
                    DistrictId = parameters.CD_CallerDistrictId,
                    CityId = parameters.CD_CallerCityId,
                    PinCode = parameters.CD_CallerPinCode,
                    IsDeleted = false,
                    IsDefault = false,
                    IsActive = true,
                };

                int resultAddressDetail = await _addressRepository.SaveAddress(CallerAddressDetail);

                if (resultAddressDetail > 0)
                {
                    parameters.CD_CallerAddressId = resultAddressDetail;

                    if (parameters.CD_IsSiteAddressSameAsCaller == true)
                    {
                        parameters.CD_SiteAddressId = resultAddressDetail;
                    }
                }

                // Battery Customer Address Detail
                var BatteryCustomerAddressDetail = new Address_Request()
                {
                    Id = Convert.ToInt32(parameters.CD_CustomerAddressId),
                    RefId = result,
                    RefType = "Ticket",
                    Address1 = parameters.CD_CustomerAddress1,
                    RegionId = parameters.CD_CustomerRegionId,
                    StateId = parameters.CD_CustomerStateId,
                    DistrictId = parameters.CD_CustomerDistrictId,
                    CityId = parameters.CD_CustomerCityId,
                    PinCode = parameters.CD_CustomerPinCode,
                    IsDeleted = false,
                    IsDefault = false,
                    IsActive = true,
                };

                int resultBatteryCustomerAddressDetail = await _addressRepository.SaveAddress(BatteryCustomerAddressDetail);

                if (resultBatteryCustomerAddressDetail > 0)
                {
                    parameters.CD_CustomerAddressId = resultBatteryCustomerAddressDetail;
                }

                // Site Customer Address Detail
                if (parameters.CD_IsSiteAddressSameAsCaller == false)
                {
                    var SiteCustomerAddressDetail = new Address_Request()
                    {
                        Id = Convert.ToInt32(parameters.CD_SiteAddressId),
                        RefId = result,
                        RefType = "Ticket",
                        Address1 = parameters.CD_SiteCustomerAddress1,
                        RegionId = parameters.CD_SiteCustomerRegionId,
                        StateId = parameters.CD_SiteCustomerStateId,
                        DistrictId = parameters.CD_SiteCustomerDistrictId,
                        CityId = parameters.CD_SiteCustomerCityId,
                        PinCode = parameters.CD_SiteCustomerPinCode,
                        IsDeleted = false,
                        IsDefault = false,
                        IsActive = true,
                    };

                    int resultSiteCustomerAddressDetail = await _addressRepository.SaveAddress(SiteCustomerAddressDetail);

                    if (resultSiteCustomerAddressDetail > 0)
                    {
                        parameters.CD_SiteAddressId = resultSiteCustomerAddressDetail;
                    }
                }

                // Add/Update Ticket Details
                parameters.Id = result;

                int resultTicketDetail = await _manageTicketRepository.SaveManageTicket(parameters);
            }

            // Add/Update Ticket Part Details
            if (result > 0)
            {
                foreach (var item in parameters.PartDetail)
                {
                    var vManageTicketPartDetails_Request = new ManageTicketPartDetails_Request()
                    {
                        Id = Convert.ToInt32(item.Id),
                        TicketId = result,
                        SpareDetailsId= item.SpareDetailsId,
                        Quantity = item.Quantity,
                        PartStatusId = item.PartStatusId,
                    };
                    int resultTechnicalSupportAddUpdate = await _manageTicketRepository.SaveManageTicketPartDetail(vManageTicketPartDetails_Request);
                }
            }

            // Add Move Ticket To TRC
            if (result > 0 && parameters.OV_IsMoveToTRC == true)
            {
                var vManageTRC_Request = new ManageTRC_Request()
                {
                    TicketId = result,

                    TRCDate = DateTime.Now,
                    TRCTime = DateTime.Now.ToString("hh:mm tt"),

                    TRCStatusId = 1,
                    IsActive = true,
                };

                int resultManageTRC = await _manageTRCRepository.SaveManageTRC(vManageTRC_Request);
            }

            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetManageTicketList(ManageTicket_Search parameters)
        {
            var objList = await _manageTicketRepository.GetManageTicketList(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetManageTicketById(int TicketId)
        {
            var vManageTicketDetail_Response = new ManageTicketDetail_Response();

            if (TicketId <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _manageTicketRepository.GetManageTicketById(TicketId);
                if (vResultObj != null)
                {
                    vManageTicketDetail_Response.Id = vResultObj.Id;
                    vManageTicketDetail_Response.TicketNumber = vResultObj.TicketNumber;
                    vManageTicketDetail_Response.TicketDate = vResultObj.TicketDate;
                    vManageTicketDetail_Response.TicketTime = vResultObj.TicketTime;
                    vManageTicketDetail_Response.TicketPriorityId = vResultObj.TicketPriorityId;
                    vManageTicketDetail_Response.TicketPriority = vResultObj.TicketPriority;
                    vManageTicketDetail_Response.TicketSLADays = vResultObj.TicketSLADays;
                    vManageTicketDetail_Response.TicketAging = vResultObj.TicketAging;

                    vManageTicketDetail_Response.CD_LoggingSourceId = vResultObj.CD_LoggingSourceId;
                    vManageTicketDetail_Response.CD_LoggingSourceChannel = vResultObj.CD_LoggingSourceChannel;
                    
                    vManageTicketDetail_Response.CD_CallerTypeId = vResultObj.CD_CallerTypeId;
                    vManageTicketDetail_Response.CD_CallerType = vResultObj.CD_CallerType;
                    vManageTicketDetail_Response.CD_CallerName = vResultObj.CD_CallerName;
                    vManageTicketDetail_Response.CD_CallerMobile = vResultObj.CD_CallerMobile;
                    vManageTicketDetail_Response.CD_CallerEmailId = vResultObj.CD_CallerEmailId;

                    vManageTicketDetail_Response.CD_CallerAddressId = vResultObj.CD_CallerAddressId;
                    vManageTicketDetail_Response.CD_CallerAddress1 = vResultObj.CD_CallerAddress1;
                    vManageTicketDetail_Response.CD_CallerRegionId = vResultObj.CD_CallerRegionId;
                    vManageTicketDetail_Response.CD_CallerRegionName = vResultObj.CD_CallerRegionName;
                    vManageTicketDetail_Response.CD_CallerStateId = vResultObj.CD_CallerStateId;
                    vManageTicketDetail_Response.CD_CallerStateName = vResultObj.CD_CallerStateName;
                    vManageTicketDetail_Response.CD_CallerDistrictId = vResultObj.CD_CallerDistrictId;
                    vManageTicketDetail_Response.CD_CallerDistrictName = vResultObj.CD_CallerDistrictName;
                    vManageTicketDetail_Response.CD_CallerCityId = vResultObj.CD_CallerCityId;
                    vManageTicketDetail_Response.CD_CallerCityName = vResultObj.CD_CallerCityName;
                    vManageTicketDetail_Response.CD_CallerPinCode = vResultObj.CD_CallerPinCode;
                    vManageTicketDetail_Response.CD_CallerRemarks = vResultObj.CD_CallerRemarks;

                    vManageTicketDetail_Response.CD_IsSiteAddressSameAsCaller = vResultObj.CD_IsSiteAddressSameAsCaller;
                    vManageTicketDetail_Response.CD_ComplaintTypeId = vResultObj.CD_ComplaintTypeId;
                    vManageTicketDetail_Response.CD_ComplaintType = vResultObj.CD_ComplaintType;
                    vManageTicketDetail_Response.CD_IsNewBattery = vResultObj.CD_IsNewBattery;
                    vManageTicketDetail_Response.CD_BatterySerialNumber = vResultObj.CD_BatterySerialNumber;
                    vManageTicketDetail_Response.CD_CustomerTypeId = vResultObj.CD_CustomerTypeId;
                    vManageTicketDetail_Response.CD_CustomerType = vResultObj.CD_CustomerType;
                    vManageTicketDetail_Response.CD_CustomerNameId = vResultObj.CD_CustomerNameId;
                    vManageTicketDetail_Response.CD_CustomerName = vResultObj.CD_CustomerName;
                    vManageTicketDetail_Response.CD_CustomerMobile = vResultObj.CD_CustomerMobile;
                    vManageTicketDetail_Response.CD_CustomerEmail = vResultObj.CD_CustomerEmail;

                    vManageTicketDetail_Response.CD_CustomerAddressId = vResultObj.CD_CustomerAddressId;
                    vManageTicketDetail_Response.CD_CustomerAddress1 = vResultObj.CD_CustomerAddress1;
                    vManageTicketDetail_Response.CD_CustomerRegionId = vResultObj.CD_CustomerRegionId;
                    vManageTicketDetail_Response.CD_CustomerRegionName = vResultObj.CD_CustomerRegionName;
                    vManageTicketDetail_Response.CD_CustomerStateId = vResultObj.CD_CustomerStateId;
                    vManageTicketDetail_Response.CD_CustomerStateName = vResultObj.CD_CustomerStateName;
                    vManageTicketDetail_Response.CD_CustomerDistrictId = vResultObj.CD_CustomerDistrictId;
                    vManageTicketDetail_Response.CD_CustomerDistrictName = vResultObj.CD_CustomerDistrictName;
                    vManageTicketDetail_Response.CD_CustomerCityId = vResultObj.CD_CustomerCityId;
                    vManageTicketDetail_Response.CD_CustomerCityName = vResultObj.CD_CustomerCityName;
                    vManageTicketDetail_Response.CD_CustomerPinCode = vResultObj.CD_CustomerPinCode;

                    vManageTicketDetail_Response.CD_SiteCustomerName = vResultObj.CD_SiteCustomerName;
                    vManageTicketDetail_Response.CD_SiteContactName = vResultObj.CD_SiteContactName;
                    vManageTicketDetail_Response.CD_SitContactMobile = vResultObj.CD_SitContactMobile;

                    vManageTicketDetail_Response.CD_SiteAddressId = vResultObj.CD_SiteAddressId;
                    vManageTicketDetail_Response.CD_SiteCustomerAddress1 = vResultObj.CD_SiteCustomerAddress1;
                    vManageTicketDetail_Response.CD_SiteCustomerRegionId = vResultObj.CD_SiteCustomerRegionId;
                    vManageTicketDetail_Response.CD_SiteCustomerRegionName = vResultObj.CD_SiteCustomerRegionName;
                    vManageTicketDetail_Response.CD_SiteCustomerStateId = vResultObj.CD_SiteCustomerStateId;
                    vManageTicketDetail_Response.CD_SiteCustomerStateName = vResultObj.CD_SiteCustomerStateName;
                    vManageTicketDetail_Response.CD_SiteCustomerDistrictId = vResultObj.CD_SiteCustomerDistrictId;
                    vManageTicketDetail_Response.CD_SiteCustomerDistrictName = vResultObj.CD_SiteCustomerDistrictName;
                    vManageTicketDetail_Response.CD_SiteCustomerCityId = vResultObj.CD_SiteCustomerCityId;
                    vManageTicketDetail_Response.CD_SiteCustomerCityName = vResultObj.CD_SiteCustomerCityName;
                    vManageTicketDetail_Response.CD_SiteCustomerPinCode = vResultObj.CD_SiteCustomerPinCode;

                    vManageTicketDetail_Response.BD_BatteryPartCode = vResultObj.BD_BatteryPartCode;
                    vManageTicketDetail_Response.BD_BatterySegmentId = vResultObj.BD_BatterySegmentId;
                    vManageTicketDetail_Response.BD_Segment = vResultObj.BD_Segment;
                    vManageTicketDetail_Response.BD_BatterySubSegmentId = vResultObj.BD_BatterySubSegmentId;
                    vManageTicketDetail_Response.BD_SubSegment = vResultObj.BD_SubSegment;
                    vManageTicketDetail_Response.BD_BatterySpecificationId = vResultObj.BD_BatterySpecificationId;
                    vManageTicketDetail_Response.BD_BatterySpecification = vResultObj.BD_BatterySpecification;
                    vManageTicketDetail_Response.BD_BatteryCellChemistryId = vResultObj.BD_BatteryCellChemistryId;
                    vManageTicketDetail_Response.BD_CellChemistry = vResultObj.BD_CellChemistry;
                    vManageTicketDetail_Response.BD_DateofManufacturing = vResultObj.BD_DateofManufacturing;
                    vManageTicketDetail_Response.BD_ProbReportedByCustId = vResultObj.BD_ProbReportedByCustId;
                    vManageTicketDetail_Response.BD_ProbReportedByCust = vResultObj.BD_ProbReportedByCust;
                    vManageTicketDetail_Response.BD_WarrantyStartDate = vResultObj.BD_WarrantyStartDate;
                    vManageTicketDetail_Response.BD_WarrantyEndDate = vResultObj.BD_WarrantyEndDate;
                    vManageTicketDetail_Response.BD_WarrantyStatusId = vResultObj.BD_WarrantyStatusId;
                    vManageTicketDetail_Response.BD_WarrantyStatus = vResultObj.BD_WarrantyStatus;
                    vManageTicketDetail_Response.BD_TechnicalSupportEnggId = vResultObj.BD_TechnicalSupportEnggId;
                    vManageTicketDetail_Response.BD_TechnicalSupportEngg = vResultObj.BD_TechnicalSupportEngg;

                    vManageTicketDetail_Response.TS_Visual = vResultObj.TS_Visual;
                    vManageTicketDetail_Response.TS_BatterTerminalVoltage = vResultObj.TS_BatterTerminalVoltage;
                    vManageTicketDetail_Response.TS_LifeCycle = vResultObj.TS_LifeCycle;
                    vManageTicketDetail_Response.TS_StringVoltageVariation = vResultObj.TS_StringVoltageVariation;
                    vManageTicketDetail_Response.TS_BatteryTemperature = vResultObj.TS_BatteryTemperature;
                    vManageTicketDetail_Response.TS_CurrentDischargingValue = vResultObj.TS_CurrentDischargingValue;
                    vManageTicketDetail_Response.TS_ProtectionsId = vResultObj.TS_ProtectionsId;
                    vManageTicketDetail_Response.TS_Protections = vResultObj.TS_Protections;
                    vManageTicketDetail_Response.TS_CurrentChargingValue = vResultObj.TS_CurrentChargingValue;
                    vManageTicketDetail_Response.TS_AllocateToServiceEnggId = vResultObj.TS_AllocateToServiceEnggId;
                    vManageTicketDetail_Response.TS_AllocateToServiceEngg = vResultObj.TS_AllocateToServiceEngg;
                    vManageTicketDetail_Response.TS_TicketDate = vResultObj.TS_TicketDate;
                    vManageTicketDetail_Response.TS_TicketTime = vResultObj.TS_TicketTime;

                    vManageTicketDetail_Response.CP_Visual = vResultObj.CP_Visual;
                    vManageTicketDetail_Response.CP_VisualImageFileName = vResultObj.CP_VisualImageFileName;
                    vManageTicketDetail_Response.CP_VisualImageOriginalFileName = vResultObj.CP_VisualImageOriginalFileName;
                    vManageTicketDetail_Response.CP_VisualImageURL = vResultObj.CP_VisualImageURL;

                    vManageTicketDetail_Response.CP_TerminalVoltage = vResultObj.CP_TerminalVoltage;
                    vManageTicketDetail_Response.CP_CommunicationWithBattery = vResultObj.CP_CommunicationWithBattery;
                    vManageTicketDetail_Response.CP_TerminalWire = vResultObj.CP_TerminalWire;
                    vManageTicketDetail_Response.CP_TerminalWireImageFileName = vResultObj.CP_TerminalWireImageFileName;
                    vManageTicketDetail_Response.CP_TerminalWireImageOriginalFileName = vResultObj.CP_TerminalWireImageOriginalFileName;
                    vManageTicketDetail_Response.CP_TerminalWireImageURL = vResultObj.CP_TerminalWireImageURL;

                    vManageTicketDetail_Response.CP_LifeCycle = vResultObj.CP_LifeCycle;
                    vManageTicketDetail_Response.CP_StringVoltageVariation = vResultObj.CP_StringVoltageVariation;
                    vManageTicketDetail_Response.CP_BatteryParametersSetting = vResultObj.CP_BatteryParametersSetting;
                    vManageTicketDetail_Response.CP_BatteryParametersSettingImageFileName = vResultObj.CP_BatteryParametersSettingImageFileName;
                    vManageTicketDetail_Response.CP_BatteryParametersSettingImageOriginalFileName = vResultObj.CP_BatteryParametersSettingImageOriginalFileName;
                    vManageTicketDetail_Response.CP_BatteryParametersSettingImageURL = vResultObj.CP_BatteryParametersSettingImageURL;


                    vManageTicketDetail_Response.CP_BMSSoftwareImageFileName = vResultObj.CP_BMSSoftwareImageFileName;
                    vManageTicketDetail_Response.CP_BMSSoftwareImageOriginalFileName = vResultObj.CP_BMSSoftwareImageOriginalFileName;
                    vManageTicketDetail_Response.CP_BMSSoftwareImageURL = vResultObj.CP_BMSSoftwareImageURL;

                    vManageTicketDetail_Response.CP_Spare = vResultObj.CP_Spare;

                    vManageTicketDetail_Response.CC_BatteryRepairedOnSite = vResultObj.CC_BatteryRepairedOnSite;
                    vManageTicketDetail_Response.CC_BatteryRepairedToPlant = vResultObj.CC_BatteryRepairedToPlant;

                    vManageTicketDetail_Response.OV_IsCustomerAvailable = vResultObj.OV_IsCustomerAvailable;
                    vManageTicketDetail_Response.OV_EngineerName = vResultObj.OV_EngineerName;
                    vManageTicketDetail_Response.OV_EngineerNumber = vResultObj.OV_EngineerNumber;
                    vManageTicketDetail_Response.OV_CustomerName = vResultObj.OV_CustomerName;
                    vManageTicketDetail_Response.OV_CustomerNameSecondary = vResultObj.OV_CustomerNameSecondary;
                    vManageTicketDetail_Response.OV_CustomerMobileNumber = vResultObj.OV_CustomerMobileNumber;
                    vManageTicketDetail_Response.OV_RequestOTP = vResultObj.OV_RequestOTP;
                    vManageTicketDetail_Response.OV_Signature = vResultObj.OV_Signature;

                    vManageTicketDetail_Response.OV_IsMoveToTRC = vResultObj.OV_IsMoveToTRC;

                    vManageTicketDetail_Response.TicketStatusFromId = vResultObj.TicketStatusFromId;
                    vManageTicketDetail_Response.TicketStatusFrom = vResultObj.TicketStatusFrom;

                    vManageTicketDetail_Response.TicketStatusToId = vResultObj.TicketStatusToId;
                    vManageTicketDetail_Response.TicketStatusTo = vResultObj.TicketStatusTo;

                    vManageTicketDetail_Response.IsActive = vResultObj.IsActive;

                    vManageTicketDetail_Response.CreatorName = vResultObj.CreatorName;
                    vManageTicketDetail_Response.CreatedBy = vResultObj.CreatedBy;
                    vManageTicketDetail_Response.CreatedDate = vResultObj.CreatedDate;
                    ;
                    vManageTicketDetail_Response.ModifierName = vResultObj.ModifierName;
                    vManageTicketDetail_Response.ModifiedBy = vResultObj.ModifiedBy;
                    vManageTicketDetail_Response.ModifiedDate = vResultObj.ModifiedDate;


                    var vResultPartListObj = await _manageTicketRepository.GetManageTicketPartDetailById(TicketId);
                    foreach (var item in vResultPartListObj)
                    {
                        var vManageTicketPartDetails_Response = new ManageTicketPartDetails_Response()
                        {
                            Id = item.Id,
                            TicketId = item.TicketId,
                            SpareDetailsId= item.SpareDetailsId,
                            UniqueCode = item.UniqueCode,
                            SpareDesc = item.SpareDesc,
                            Quantity = item.Quantity,
                            PartStatusId = item.PartStatusId,
                            PartStatus = item.PartStatus
                        };

                        vManageTicketDetail_Response.PartDetails.Add(vManageTicketPartDetails_Response);
                    }
                }

                _response.Data = vManageTicketDetail_Response;
            }
            return _response;
        }



        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetCustomerMobileNumberList(string SearchText)
        {
            var objList = await _manageTicketRepository.GetCustomerMobileNumberList(SearchText);
            _response.Data = objList.ToList();
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetCustomerDetailByMobileNumber(string mobile)
        {
            var vCustomerDetail_Response = new ManageTicketCustomerDetail_Response();

            if (string.IsNullOrWhiteSpace(mobile))
            {
                _response.Message = "Mobile is required";
            }
            else
            {
                var vResultObj = await _manageTicketRepository.GetCustomerDetailByMobileNumber(mobile);
                if (vResultObj != null)
                {
                    vCustomerDetail_Response.Id = Convert.ToInt32(vResultObj.Id);
                    vCustomerDetail_Response.CustomerTypeId = vResultObj.CustomerTypeId;
                    vCustomerDetail_Response.CustomerName = vResultObj.CustomerName;
                    vCustomerDetail_Response.LandLineNumber = vResultObj.LandLineNumber;
                    vCustomerDetail_Response.MobileNumber = vResultObj.MobileNumber;
                    vCustomerDetail_Response.EmailId = vResultObj.EmailId;
                    vCustomerDetail_Response.Website = vResultObj.Website;
                    vCustomerDetail_Response.Remark = vResultObj.Remark;
                    vCustomerDetail_Response.RefParty = vResultObj.RefParty;
                    vCustomerDetail_Response.GSTImage = vResultObj.GSTImage;
                    vCustomerDetail_Response.GSTImageOriginalFileName = vResultObj.GSTImageOriginalFileName;
                    vCustomerDetail_Response.GSTImageURL = vResultObj.GSTImageURL;
                    vCustomerDetail_Response.PanCardImage = vResultObj.PanCardImage;
                    vCustomerDetail_Response.PanCardOriginalFileName = vResultObj.PanCardOriginalFileName;
                    vCustomerDetail_Response.PanCardImageURL = vResultObj.PanCardImageURL;
                    vCustomerDetail_Response.Address1 = vResultObj.Address1;
                    vCustomerDetail_Response.Address2 = vResultObj.Address2;
                    vCustomerDetail_Response.RegionId = vResultObj.RegionId;
                    vCustomerDetail_Response.RegionName = vResultObj.RegionName;
                    vCustomerDetail_Response.StateId = vResultObj.StateId;
                    vCustomerDetail_Response.StateName = vResultObj.StateName;
                    vCustomerDetail_Response.DistrictId = vResultObj.DistrictId;
                    vCustomerDetail_Response.DistrictName = vResultObj.DistrictName;
                    vCustomerDetail_Response.CityId = vResultObj.CityId;
                    vCustomerDetail_Response.CityName = vResultObj.CityName;

                    vCustomerDetail_Response.IsActive = vResultObj.IsActive;
                }

                _response.Data = vCustomerDetail_Response;
            }
            return _response;
        }

        #endregion

        #region Manage Enquiry

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveEnquiry(ManageEnquiry_Request parameters)
        {
            // Save/Update
            int result = await _manageEnquiryRepository.SaveManageEnquiry(parameters);

            if (result == (int)SaveOperationEnums.NoRecordExists)
            {
                _response.Message = "No record exists";
            }
            else if (result == (int)SaveOperationEnums.ReocrdExists)
            {
                _response.Message = "Record is already exists";
            }
            else if (result == (int)SaveOperationEnums.NoResult)
            {
                _response.Message = "Something went wrong, please try again";
            }
            else
            {
                _response.Message = "Record details saved sucessfully";
            }

            // Add/Update 
            if (result > 0)
            {
                // Caller Address Detail
                var CallerAddressDetail = new Address_Request()
                {
                    Id = Convert.ToInt32(parameters.CD_CallerAddressId),
                    RefId = result,
                    RefType = "Enquiry",
                    Address1 = parameters.CD_CallerAddress1,
                    RegionId = parameters.CD_CallerRegionId,
                    StateId = parameters.CD_CallerStateId,
                    DistrictId = parameters.CD_CallerDistrictId,
                    CityId = parameters.CD_CallerCityId,
                    PinCode = parameters.CD_CallerPinCode,
                    IsDeleted = false,
                    IsDefault = false,
                    IsActive = true,
                };

                int resultAddressDetail = await _addressRepository.SaveAddress(CallerAddressDetail);

                if (resultAddressDetail > 0)
                {
                    parameters.CD_CallerAddressId = resultAddressDetail;

                    if (parameters.CD_IsSiteAddressSameAsCaller == true)
                    {
                        parameters.CD_SiteAddressId = resultAddressDetail;
                    }
                }

                // Battery Customer Address Detail
                var BatteryCustomerAddressDetail = new Address_Request()
                {
                    Id = Convert.ToInt32(parameters.CD_CustomerAddressId),
                    RefId = result,
                    RefType = "Enquiry",
                    Address1 = parameters.CD_CustomerAddress1,
                    RegionId = parameters.CD_CustomerRegionId,
                    StateId = parameters.CD_CustomerStateId,
                    DistrictId = parameters.CD_CustomerDistrictId,
                    CityId = parameters.CD_CustomerCityId,
                    PinCode = parameters.CD_CustomerPinCode,
                    IsDeleted = false,
                    IsDefault = false,
                    IsActive = true,
                };

                int resultBatteryCustomerAddressDetail = await _addressRepository.SaveAddress(BatteryCustomerAddressDetail);

                if (resultBatteryCustomerAddressDetail > 0)
                {
                    parameters.CD_CustomerAddressId = resultBatteryCustomerAddressDetail;
                }

                // Site Customer Address Detail
                if (parameters.CD_IsSiteAddressSameAsCaller == false)
                {
                    var SiteCustomerAddressDetail = new Address_Request()
                    {
                        Id = Convert.ToInt32(parameters.CD_SiteAddressId),
                        RefId = result,
                        RefType = "Enquiry",
                        Address1 = parameters.CD_SiteCustomerAddress1,
                        RegionId = parameters.CD_SiteCustomerRegionId,
                        StateId = parameters.CD_SiteCustomerStateId,
                        DistrictId = parameters.CD_SiteCustomerDistrictId,
                        CityId = parameters.CD_SiteCustomerCityId,
                        PinCode = parameters.CD_SiteCustomerPinCode,
                        IsDeleted = false,
                        IsDefault = false,
                        IsActive = true,
                    };

                    int resultSiteCustomerAddressDetail = await _addressRepository.SaveAddress(SiteCustomerAddressDetail);

                    if (resultSiteCustomerAddressDetail > 0)
                    {
                        parameters.CD_SiteAddressId = resultSiteCustomerAddressDetail;
                    }
                }

                // Add/Update Ticket Details
                parameters.Id = result;

                int resultEnquiry = await _manageEnquiryRepository.SaveManageEnquiry(parameters);
            }

            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetEnquiryList(ManageEnquiry_Search parameters)
        {
            var objList = await _manageEnquiryRepository.GetManageEnquiryList(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetEnquiryById(int EnquiryId)
        {
            if (EnquiryId <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _manageEnquiryRepository.GetManageEnquiryById(EnquiryId);

                _response.Data = vResultObj;
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveEnquiryConvertToTicket(int EnquiryId)
        {
            if (EnquiryId <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultEnquiryObj = await _manageEnquiryRepository.GetManageEnquiryById(EnquiryId);
                if (vResultEnquiryObj != null)
                {
                    if (vResultEnquiryObj.IsConvertToTicket == true)
                    {
                        _response.Message = "The Enquiry already converted into tickets. Please try another enquiry";

                        return _response;
                    }

                    var vManageTicket_Request = new ManageTicket_Request()
                    {
                        TicketdDate = DateTime.Now,
                        TicketdTime = DateTime.Now.ToLongTimeString(),

                        CD_LoggingSourceId = vResultEnquiryObj.CD_LoggingSourceId,
                        CD_CallerTypeId = vResultEnquiryObj.CD_CallerTypeId,
                        CD_CallerName = vResultEnquiryObj.CD_CallerName,
                        CD_CallerMobile = vResultEnquiryObj.CD_CallerMobile,
                        CD_CallerEmailId = vResultEnquiryObj.CD_CallerEmailId,

                        CD_CallerAddressId = vResultEnquiryObj.CD_CallerAddressId,
                        CD_CallerAddress1 = vResultEnquiryObj.CD_CallerAddress1,
                        CD_CallerRegionId = vResultEnquiryObj.CD_CallerRegionId,
                        CD_CallerStateId = vResultEnquiryObj.CD_CallerStateId,
                        CD_CallerDistrictId = vResultEnquiryObj.CD_CallerDistrictId,
                        CD_CallerCityId = vResultEnquiryObj.CD_CallerCityId,
                        CD_CallerPinCode = vResultEnquiryObj.CD_CallerPinCode,
                        CD_CallerRemarks = vResultEnquiryObj.CD_CallerRemarks,

                        CD_IsSiteAddressSameAsCaller = vResultEnquiryObj.CD_IsSiteAddressSameAsCaller,
                        CD_BatterySerialNumber = vResultEnquiryObj.CD_BatterySerialNumber,
                        CD_CustomerTypeId = vResultEnquiryObj.CD_CustomerTypeId,
                        CD_CustomerNameId = vResultEnquiryObj.CD_CustomerNameId,
                        CD_CustomerMobile = vResultEnquiryObj.CD_CustomerMobile,

                        CD_CustomerAddressId = vResultEnquiryObj.CD_CustomerAddressId,
                        CD_CustomerAddress1 = vResultEnquiryObj.CD_CustomerAddress1,
                        CD_CustomerRegionId = vResultEnquiryObj.CD_CustomerRegionId,
                        CD_CustomerStateId = vResultEnquiryObj.CD_CustomerStateId,
                        CD_CustomerDistrictId = vResultEnquiryObj.CD_CustomerDistrictId,
                        CD_CustomerCityId = vResultEnquiryObj.CD_CustomerCityId,
                        CD_CustomerPinCode = vResultEnquiryObj.CD_CustomerPinCode,

                        CD_SiteCustomerName = vResultEnquiryObj.CD_SiteCustomerName,
                        CD_SiteContactName = vResultEnquiryObj.CD_SiteContactName,
                        CD_SitContactMobile = vResultEnquiryObj.CD_SitContactMobile,

                        CD_SiteAddressId = vResultEnquiryObj.CD_SiteAddressId,
                        CD_SiteCustomerAddress1 = vResultEnquiryObj.CD_SiteCustomerAddress1,
                        CD_SiteCustomerRegionId = vResultEnquiryObj.CD_SiteCustomerRegionId,
                        CD_SiteCustomerStateId = vResultEnquiryObj.CD_SiteCustomerStateId,
                        CD_SiteCustomerDistrictId = vResultEnquiryObj.CD_SiteCustomerDistrictId,
                        CD_SiteCustomerCityId = vResultEnquiryObj.CD_SiteCustomerCityId,
                        CD_SiteCustomerPinCode = vResultEnquiryObj.CD_SiteCustomerPinCode,

                        BD_BatteryPartCode = vResultEnquiryObj.BD_BatteryPartCode,
                        BD_BatterySegmentId = vResultEnquiryObj.BD_BatterySegmentId,
                        BD_BatterySubSegmentId = vResultEnquiryObj.BD_BatterySubSegmentId,
                        BD_BatterySpecificationId = vResultEnquiryObj.BD_BatterySpecificationId,
                        BD_BatteryCellChemistryId = vResultEnquiryObj.BD_BatteryCellChemistryId,
                        BD_DateofManufacturing = vResultEnquiryObj.BD_DateofManufacturing,
                        BD_ProbReportedByCustId = vResultEnquiryObj.BD_ProbReportedByCustId,
                        BD_WarrantyStartDate = vResultEnquiryObj.BD_WarrantyStartDate,
                        BD_WarrantyEndDate = vResultEnquiryObj.BD_WarrantyEndDate,
                        BD_WarrantyStatusId = vResultEnquiryObj.BD_WarrantyStatusId,
                        BD_TechnicalSupportEnggId = vResultEnquiryObj.BD_TechnicalSupportEnggId,

                        TicketStatusFromId = 1,
                        EnquiryId = vResultEnquiryObj.Id,
                    };


                    int result = await _manageTicketRepository.SaveManageTicket(vManageTicket_Request);

                    if (result == (int)SaveOperationEnums.NoRecordExists)
                    {
                        _response.Message = "No record exists";
                    }
                    else if (result == (int)SaveOperationEnums.ReocrdExists)
                    {
                        _response.Message = "Record is already exists";
                    }
                    else if (result == (int)SaveOperationEnums.NoResult)
                    {
                        _response.Message = "Something went wrong, please try again";
                    }
                    else
                    {
                        var vManageEnquiry_Request = new ManageEnquiry_Request()
                        {
                            Id = vResultEnquiryObj.Id,
                            EnquiryNumber = vResultEnquiryObj.EnquiryNumber,
                            EnquiryDate = DateTime.Now,
                            EnquiryTime = DateTime.Now.ToLongTimeString(),

                            CD_LoggingSourceId = vResultEnquiryObj.CD_LoggingSourceId,
                            CD_CallerTypeId = vResultEnquiryObj.CD_CallerTypeId,
                            CD_CallerName = vResultEnquiryObj.CD_CallerName,
                            CD_CallerMobile = vResultEnquiryObj.CD_CallerMobile,
                            CD_CallerEmailId = vResultEnquiryObj.CD_CallerEmailId,

                            CD_CallerAddressId = vResultEnquiryObj.CD_CallerAddressId,
                            CD_CallerAddress1 = vResultEnquiryObj.CD_CallerAddress1,
                            CD_CallerRegionId = vResultEnquiryObj.CD_CallerRegionId,
                            CD_CallerStateId = vResultEnquiryObj.CD_CallerStateId,
                            CD_CallerDistrictId = vResultEnquiryObj.CD_CallerDistrictId,
                            CD_CallerCityId = vResultEnquiryObj.CD_CallerCityId,
                            CD_CallerPinCode = vResultEnquiryObj.CD_CallerPinCode,
                            CD_CallerRemarks = vResultEnquiryObj.CD_CallerRemarks,

                            CD_IsSiteAddressSameAsCaller = vResultEnquiryObj.CD_IsSiteAddressSameAsCaller,
                            CD_BatterySerialNumber = vResultEnquiryObj.CD_BatterySerialNumber,
                            CD_CustomerTypeId = vResultEnquiryObj.CD_CustomerTypeId,
                            CD_CustomerNameId = vResultEnquiryObj.CD_CustomerNameId,
                            CD_CustomerMobile = vResultEnquiryObj.CD_CustomerMobile,

                            CD_CustomerAddressId = vResultEnquiryObj.CD_CustomerAddressId,
                            CD_CustomerAddress1 = vResultEnquiryObj.CD_CustomerAddress1,
                            CD_CustomerRegionId = vResultEnquiryObj.CD_CustomerRegionId,
                            CD_CustomerStateId = vResultEnquiryObj.CD_CustomerStateId,
                            CD_CustomerDistrictId = vResultEnquiryObj.CD_CustomerDistrictId,
                            CD_CustomerCityId = vResultEnquiryObj.CD_CustomerCityId,
                            CD_CustomerPinCode = vResultEnquiryObj.CD_CustomerPinCode,

                            CD_SiteCustomerName = vResultEnquiryObj.CD_SiteCustomerName,
                            CD_SiteContactName = vResultEnquiryObj.CD_SiteContactName,
                            CD_SitContactMobile = vResultEnquiryObj.CD_SitContactMobile,

                            CD_SiteAddressId = vResultEnquiryObj.CD_SiteAddressId,
                            CD_SiteCustomerAddress1 = vResultEnquiryObj.CD_SiteCustomerAddress1,
                            CD_SiteCustomerRegionId = vResultEnquiryObj.CD_SiteCustomerRegionId,
                            CD_SiteCustomerStateId = vResultEnquiryObj.CD_SiteCustomerStateId,
                            CD_SiteCustomerDistrictId = vResultEnquiryObj.CD_SiteCustomerDistrictId,
                            CD_SiteCustomerCityId = vResultEnquiryObj.CD_SiteCustomerCityId,
                            CD_SiteCustomerPinCode = vResultEnquiryObj.CD_SiteCustomerPinCode,

                            BD_BatteryPartCode = vResultEnquiryObj.BD_BatteryPartCode,
                            BD_BatterySegmentId = vResultEnquiryObj.BD_BatterySegmentId,
                            BD_BatterySubSegmentId = vResultEnquiryObj.BD_BatterySubSegmentId,
                            BD_BatterySpecificationId = vResultEnquiryObj.BD_BatterySpecificationId,
                            BD_BatteryCellChemistryId = vResultEnquiryObj.BD_BatteryCellChemistryId,
                            BD_DateofManufacturing = vResultEnquiryObj.BD_DateofManufacturing,
                            BD_ProbReportedByCustId = vResultEnquiryObj.BD_ProbReportedByCustId,
                            BD_WarrantyStartDate = vResultEnquiryObj.BD_WarrantyStartDate,
                            BD_WarrantyEndDate = vResultEnquiryObj.BD_WarrantyEndDate,
                            BD_WarrantyStatusId = vResultEnquiryObj.BD_WarrantyStatusId,
                            BD_TechnicalSupportEnggId = vResultEnquiryObj.BD_TechnicalSupportEnggId,

                            IsConvertToTicket = true,
                            IsActive = vResultEnquiryObj.IsActive,
                        };

                        int resultManageEnquiry = await _manageEnquiryRepository.SaveManageEnquiry(vManageEnquiry_Request);

                        _response.Message = "Record details saved sucessfully";
                    }
                }
            }

            return _response;
        }

        #endregion
    }
}
