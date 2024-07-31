using CLN.Application.Enums;
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

            if (parameters! != null && !string.IsNullOrWhiteSpace(parameters.TSAD_VisualImage_Base64))
            {
                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(parameters.TSAD_VisualImage_Base64, "\\Uploads\\Ticket\\", parameters.TSAD_VisualImageOriginalFileName);

                if (!string.IsNullOrWhiteSpace(vUploadFile))
                {
                    parameters.TSAD_VisualImageFileName = vUploadFile;
                }
            }

            if (parameters! != null && !string.IsNullOrWhiteSpace(parameters.TSPD_PhysicaImage_Base64))
            {
                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(parameters.TSPD_PhysicaImage_Base64, "\\Uploads\\Ticket\\", parameters.TSPD_PhysicaImageOriginalFileName);

                if (!string.IsNullOrWhiteSpace(vUploadFile))
                {
                    parameters.TSPD_PhysicaImageFileName = vUploadFile;
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

            // Save/Update
            int result = await _manageTicketRepository.SaveManageTicket(parameters);

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
                        SpareDetailsId = item.SpareDetailsId,
                        Quantity = item.Quantity,
                        PartStatusId = item.PartStatusId,
                    };
                    int resultTechnicalSupportAddUpdate = await _manageTicketRepository.SaveManageTicketPartDetail(vManageTicketPartDetails_Request);
                }
            }

            // Add Move Ticket To TRC
            if (result > 0 && (parameters.TSSP_SolutionProvider == (int)TicketStatusEnums.ReferToTRC || parameters.TicketStatusId == (int)TicketStatusEnums.ReferToTRC)) // Refer To TRC
            {
                var vManageTRC_Request = new ManageTRC_Request()
                {
                    TicketId = result,

                    TRCDate = DateTime.Now,
                    TRCTime = DateTime.Now.ToString("hh:mm tt"),

                    TRCStatusId = (int)TicketStatusEnums.ReferToTRC,
                    IsActive = true,
                };

                int resultManageTRC = await _manageTRCRepository.SaveManageTRC(vManageTRC_Request);
            }

            // Save Ticket Log History
            if (result > 0)
            {
                int resultManageTicketLog = await _manageTicketRepository.SaveManageTicketLogHistory(result);
            }

            _response.Id = result;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> CreateDuplicateTicket(int TicketId)
        {
            // Save/Update
            int result = await _manageTicketRepository.CreateDuplicateTicket(TicketId);

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
                _response.Message = "Record created sucessfully";
            }

            // Add/Update Ticket Details
            if (result > 0)
            {
            }

            _response.Id = result;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetManageTicketList(ManageTicket_Search parameters)
        {
            var objList = await _manageTicketRepository.GetManageTicketList(parameters);
            foreach (var item in objList)
            {
                if (Convert.ToInt32(item.TSSP_AllocateToServiceEnggId) > 0)
                {
                    var vManageTicketEngineerVisitHistory_Search = new ManageTicketEngineerVisitHistory_Search()
                    {
                        TicketId = item.Id,
                        EngineerId = item.TSSP_AllocateToServiceEnggId
                    };

                    var vTicketHistoryListObj = _manageTicketRepository.GetTicketVisitHistoryList(vManageTicketEngineerVisitHistory_Search).Result.ToList().OrderByDescending(x => x.VisitDate).FirstOrDefault();
                    if (vTicketHistoryListObj != null)
                    {
                        item.manageTicketEngineerVisitHistory = new ManageTicketEngineerVisitHistory_Response();

                        item.manageTicketEngineerVisitHistory.Id = vTicketHistoryListObj.Id;
                        item.manageTicketEngineerVisitHistory.EngineerId = vTicketHistoryListObj.EngineerId;
                        item.manageTicketEngineerVisitHistory.VisitDate = vTicketHistoryListObj.VisitDate;
                        item.manageTicketEngineerVisitHistory.Latitude = vTicketHistoryListObj.Latitude;
                        item.manageTicketEngineerVisitHistory.Longitude = vTicketHistoryListObj.Longitude;
                        item.manageTicketEngineerVisitHistory.Address = vTicketHistoryListObj.Address;
                        item.manageTicketEngineerVisitHistory.Status = vTicketHistoryListObj.Status;
                    }
                }
            }

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
                    vManageTicketDetail_Response.TicketSLAHours = vResultObj.TicketSLAHours;
                    vManageTicketDetail_Response.TicketSLAMin = vResultObj.TicketSLAMin;
                    vManageTicketDetail_Response.SLAStatus = vResultObj.SLAStatus;
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
                    vManageTicketDetail_Response.CD_IsOldProduct = vResultObj.CD_IsOldProduct;
                    vManageTicketDetail_Response.CD_ProductSerialNumberId = vResultObj.CD_ProductSerialNumberId;
                    vManageTicketDetail_Response.CD_ProductSerialNumber = vResultObj.CD_ProductSerialNumber;

                    vManageTicketDetail_Response.CD_CustomerTypeId = vResultObj.CD_CustomerTypeId;
                    vManageTicketDetail_Response.CustomerType = vResultObj.CustomerType;
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

                    vManageTicketDetail_Response.BD_BatteryBOMNumberId = vResultObj.BD_BatteryBOMNumberId;
                    vManageTicketDetail_Response.BD_BatteryBOMNumber = vResultObj.BD_BatteryBOMNumber;
                    vManageTicketDetail_Response.BD_BatteryProductCategoryId = vResultObj.BD_BatteryProductCategoryId;
                    vManageTicketDetail_Response.BD_ProductCategory = vResultObj.BD_ProductCategory;
                    vManageTicketDetail_Response.BD_BatterySegmentId = vResultObj.BD_BatterySegmentId;
                    vManageTicketDetail_Response.BD_Segment = vResultObj.BD_Segment;
                    vManageTicketDetail_Response.BD_BatterySubSegmentId = vResultObj.BD_BatterySubSegmentId;
                    vManageTicketDetail_Response.BD_SubSegment = vResultObj.BD_SubSegment;
                    vManageTicketDetail_Response.BD_BatteryProductModelId = vResultObj.BD_BatteryProductModelId;
                    vManageTicketDetail_Response.BD_ProductModel = vResultObj.BD_ProductModel;
                    vManageTicketDetail_Response.BD_BatteryCellChemistryId = vResultObj.BD_BatteryCellChemistryId;
                    vManageTicketDetail_Response.BD_CellChemistry = vResultObj.BD_CellChemistry;
                    vManageTicketDetail_Response.BD_DateofManufacturing = vResultObj.BD_DateofManufacturing;
                    vManageTicketDetail_Response.BD_ProbReportedByCustId = vResultObj.BD_ProbReportedByCustId;
                    vManageTicketDetail_Response.BD_ProbReportedByCust = vResultObj.BD_ProbReportedByCust;
                    vManageTicketDetail_Response.BD_ProblemDescription = vResultObj.BD_ProblemDescription;

                    vManageTicketDetail_Response.BD_WarrantyStartDate = vResultObj.BD_WarrantyStartDate;
                    vManageTicketDetail_Response.BD_WarrantyEndDate = vResultObj.BD_WarrantyEndDate;
                    vManageTicketDetail_Response.BD_WarrantyStatusId = vResultObj.BD_WarrantyStatusId;
                    vManageTicketDetail_Response.BD_WarrantyStatus = vResultObj.BD_WarrantyStatus;
                    vManageTicketDetail_Response.BD_TechnicalSupportEnggId = vResultObj.BD_TechnicalSupportEnggId;
                    vManageTicketDetail_Response.BD_TechnicalSupportEngg = vResultObj.BD_TechnicalSupportEngg;

                    vManageTicketDetail_Response.TSAD_Visual = vResultObj.TSAD_Visual;
                    vManageTicketDetail_Response.TSAD_VisualImageFileName = vResultObj.TSAD_VisualImageFileName;
                    vManageTicketDetail_Response.TSAD_VisualImageOriginalFileName = vResultObj.TSAD_VisualImageOriginalFileName;
                    vManageTicketDetail_Response.TSAD_VisualImageURL = vResultObj.TSAD_VisualImageURL;
                    vManageTicketDetail_Response.TSAD_BatteryTemperature = vResultObj.TSAD_BatteryTemperature;
                    vManageTicketDetail_Response.TSAD_CurrentChargingValue = vResultObj.TSAD_CurrentChargingValue;
                    vManageTicketDetail_Response.TSAD_CurrentDischargingValue = vResultObj.TSAD_CurrentDischargingValue;
                    vManageTicketDetail_Response.TSAD_BatteryTemperature = vResultObj.TSAD_BatteryTemperature;
                    vManageTicketDetail_Response.TSAD_BatterVoltage = vResultObj.TSAD_BatterVoltage;
                    vManageTicketDetail_Response.TSAD_CellDiffrence = vResultObj.TSAD_CellDiffrence;
                    vManageTicketDetail_Response.TSAD_ProtectionsId = vResultObj.TSAD_ProtectionsId;
                    vManageTicketDetail_Response.TSAD_Protections = vResultObj.TSAD_Protections;

                    vManageTicketDetail_Response.TSAD_CycleCount = vResultObj.TSAD_CycleCount;
                    vManageTicketDetail_Response.TSPD_PhysicaImageFileName = vResultObj.TSPD_PhysicaImageFileName;
                    vManageTicketDetail_Response.TSPD_PhysicaImageOriginalFileName = vResultObj.TSPD_PhysicaImageOriginalFileName;
                    vManageTicketDetail_Response.TSPD_PhysicaImageURL = vResultObj.TSPD_PhysicaImageURL;
                    vManageTicketDetail_Response.TSPD_AnyPhysicalDamage = vResultObj.TSPD_AnyPhysicalDamage;
                    vManageTicketDetail_Response.TSPD_Other = vResultObj.TSPD_Other;
                    vManageTicketDetail_Response.TSPD_IsWarrantyVoid = vResultObj.TSPD_IsWarrantyVoid;
                    vManageTicketDetail_Response.TSSP_SolutionProvider = vResultObj.TSSP_SolutionProvider;
                    vManageTicketDetail_Response.TSSP_AllocateToServiceEnggId = vResultObj.TSSP_AllocateToServiceEnggId;
                    vManageTicketDetail_Response.TSSP_AllocateToServiceEngg = vResultObj.TSSP_AllocateToServiceEngg;
                    vManageTicketDetail_Response.TSSP_Remarks = vResultObj.TSSP_Remarks;

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
                    vManageTicketDetail_Response.CP_Spare = vResultObj.CP_Spare;
                    vManageTicketDetail_Response.CP_BMSStatus = vResultObj.CP_BMSStatus;
                    vManageTicketDetail_Response.CP_BMSSoftwareImageFileName = vResultObj.CP_BMSSoftwareImageFileName;
                    vManageTicketDetail_Response.CP_BMSSoftwareImageOriginalFileName = vResultObj.CP_BMSSoftwareImageOriginalFileName;
                    vManageTicketDetail_Response.CP_BMSSoftwareImageURL = vResultObj.CP_BMSSoftwareImageURL;
                    vManageTicketDetail_Response.CP_BMSType = vResultObj.CP_BMSType;
                    vManageTicketDetail_Response.CP_BatteryTemp = vResultObj.CP_BatteryTemp;
                    vManageTicketDetail_Response.CP_BMSSerialNumber = vResultObj.CP_BMSSerialNumber;
                    vManageTicketDetail_Response.CP_ProblemObserved = vResultObj.CP_ProblemObserved;

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

                    vManageTicketDetail_Response.TicketStatusId = vResultObj.TicketStatusId;
                    vManageTicketDetail_Response.TicketStatus = vResultObj.TicketStatus;
                    vManageTicketDetail_Response.TicketStatusSequenceNo = vResultObj.TicketStatusSequenceNo;
                    vManageTicketDetail_Response.TRC_EngineerId = vResultObj.TRC_EngineerId;
                    vManageTicketDetail_Response.TRC_Engineer = vResultObj.TRC_Engineer;

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
                            SpareDetailsId = item.SpareDetailsId,
                            UniqueCode = item.UniqueCode,
                            SpareDesc = item.SpareDesc,
                            Quantity = item.Quantity,
                            PartStatusId = item.PartStatusId,
                            PartStatus = item.PartStatus,
                            RGP = item.RGP,
                        };

                        vManageTicketDetail_Response.PartDetails.Add(vManageTicketPartDetails_Response);
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

                        vManageTicketDetail_Response.TicketStatusLog.Add(vManageTicketStatusLog_Response);
                    }
                }

                _response.Data = vManageTicketDetail_Response;
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> DeleteManageTicketPartDetails(int Id)
        {
            // Save/Update
            int result = await _manageTicketRepository.DeleteManageTicketPartDetail(Id);

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
                _response.Message = "Record deleted sucessfully";
            }

            _response.Id = result;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveTicketVisitHistory(ManageTicketEngineerVisitHistory_Request parameters)
        {
            //Save / Update
            int result = await _manageTicketRepository.SaveTicketVisitHistory(parameters);

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

            _response.Id = result;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetTicketVisitHistoryList(ManageTicketEngineerVisitHistory_Search parameters)
        {
            var objList = await _manageTicketRepository.GetTicketVisitHistoryList(parameters);
            _response.Data = objList.ToList();
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetManageTicketLogHistoryList(ManageTicketLogHistory_Search parameters)
        {
            var vManageTicketDetail_Response = new List<ManageTicketLogHistory_Response>();

            var vResultObj = await _manageTicketRepository.GetManageTicketLogHistoryList(parameters);
            foreach (var item in vResultObj)
            {
                //var vManageTicket_ResponseObj = new ManageTicketLogHistory_Response()
                //{
                //    Id = item.Id,
                //    TicketId = item.TicketId,
                //    TicketNumber = item.TicketNumber,
                //    TicketDate = item.TicketDate,
                //    TicketTime = item.TicketTime,
                //    TicketPriorityId = item.TicketPriorityId,
                //    TicketPriority = item.TicketPriority,
                //    TicketSLADays = item.TicketSLADays,
                //    TicketSLAHours = item.TicketSLAHours,
                //    TicketSLAMin = item.TicketSLAMin,
                //    SLAStatus = item.SLAStatus,
                //    TicketAging = item.TicketAging,


                //    CD_LoggingSourceId = item.CD_LoggingSourceId,
                //    CD_LoggingSourceChannel = item.CD_LoggingSourceChannel,

                //    CD_CallerTypeId = item.CD_CallerTypeId,
                //    CD_CallerType = item.CD_CallerType,
                //    CD_CallerName = item.CD_CallerName,
                //    CD_CallerMobile = item.CD_CallerMobile,
                //    CD_CallerEmailId = item.CD_CallerEmailId,

                //    CD_CallerAddressId = item.CD_CallerAddressId,
                //    CD_CallerAddress1 = item.CD_CallerAddress1,
                //    CD_CallerRegionId = item.CD_CallerRegionId,
                //    CD_CallerRegionName = item.CD_CallerRegionName,
                //    CD_CallerStateId = item.CD_CallerStateId,
                //    CD_CallerStateName = item.CD_CallerStateName,
                //    CD_CallerDistrictId = item.CD_CallerDistrictId,
                //    CD_CallerDistrictName = item.CD_CallerDistrictName,
                //    CD_CallerCityId = item.CD_CallerCityId,
                //    CD_CallerCityName = item.CD_CallerCityName,
                //    CD_CallerPinCode = item.CD_CallerPinCode,
                //    CD_CallerRemarks = item.CD_CallerRemarks,

                //    CD_IsSiteAddressSameAsCaller = item.CD_IsSiteAddressSameAsCaller,
                //    CD_ComplaintTypeId = item.CD_ComplaintTypeId,
                //    CD_ComplaintType = item.CD_ComplaintType,
                //    CD_IsOldProduct = item.CD_IsOldProduct,
                //    CD_ProductSerialNumberId = item.CD_ProductSerialNumberId,
                //    CD_ProductSerialNumber = item.CD_ProductSerialNumber,

                //    CD_CustomerTypeId = item.CD_CustomerTypeId,
                //    CustomerType = item.CustomerType,
                //    CD_CustomerNameId = item.CD_CustomerNameId,
                //    CD_CustomerName = item.CD_CustomerName,
                //    CD_CustomerMobile = item.CD_CustomerMobile,
                //    CD_CustomerEmail = item.CD_CustomerEmail,

                //    CD_CustomerAddressId = item.CD_CustomerAddressId,
                //    CD_CustomerAddress1 = item.CD_CustomerAddress1,
                //    CD_CustomerRegionId = item.CD_CustomerRegionId,
                //    CD_CustomerRegionName = item.CD_CustomerRegionName,
                //    CD_CustomerStateId = item.CD_CustomerStateId,
                //    CD_CustomerStateName = item.CD_CustomerStateName,
                //    CD_CustomerDistrictId = item.CD_CustomerDistrictId,
                //    CD_CustomerDistrictName = item.CD_CustomerDistrictName,
                //    CD_CustomerCityId = item.CD_CustomerCityId,
                //    CD_CustomerCityName = item.CD_CustomerCityName,
                //    CD_CustomerPinCode = item.CD_CustomerPinCode,

                //    CD_SiteCustomerName = item.CD_SiteCustomerName,
                //    CD_SiteContactName = item.CD_SiteContactName,
                //    CD_SitContactMobile = item.CD_SitContactMobile,

                //    CD_SiteAddressId = item.CD_SiteAddressId,
                //    CD_SiteCustomerAddress1 = item.CD_SiteCustomerAddress1,
                //    CD_SiteCustomerRegionId = item.CD_SiteCustomerRegionId,
                //    CD_SiteCustomerRegionName = item.CD_SiteCustomerRegionName,
                //    CD_SiteCustomerStateId = item.CD_SiteCustomerStateId,
                //    CD_SiteCustomerStateName = item.CD_SiteCustomerStateName,
                //    CD_SiteCustomerDistrictId = item.CD_SiteCustomerDistrictId,
                //    CD_SiteCustomerDistrictName = item.CD_SiteCustomerDistrictName,
                //    CD_SiteCustomerCityId = item.CD_SiteCustomerCityId,
                //    CD_SiteCustomerCityName = item.CD_SiteCustomerCityName,
                //    CD_SiteCustomerPinCode = item.CD_SiteCustomerPinCode,

                //    BD_BatteryBOMNumberId = item.BD_BatteryBOMNumberId,
                //    BD_BatteryBOMNumber = item.BD_BatteryBOMNumber,
                //    BD_BatteryProductCategoryId = item.BD_BatteryProductCategoryId,
                //    BD_ProductCategory = item.BD_ProductCategory,
                //    BD_BatterySegmentId = item.BD_BatterySegmentId,
                //    BD_Segment = item.BD_Segment,
                //    BD_BatterySubSegmentId = item.BD_BatterySubSegmentId,
                //    BD_SubSegment = item.BD_SubSegment,
                //    BD_BatteryProductModelId = item.BD_BatteryProductModelId,
                //    BD_ProductModel = item.BD_ProductModel,
                //    BD_BatteryCellChemistryId = item.BD_BatteryCellChemistryId,
                //    BD_CellChemistry = item.BD_CellChemistry,
                //    BD_DateofManufacturing = item.BD_DateofManufacturing,
                //    BD_ProbReportedByCustId = item.BD_ProbReportedByCustId,
                //    BD_ProbReportedByCust = item.BD_ProbReportedByCust,
                //    BD_ProblemDescription = item.BD_ProblemDescription,

                //    BD_WarrantyStartDate = item.BD_WarrantyStartDate,
                //    BD_WarrantyEndDate = item.BD_WarrantyEndDate,
                //    BD_WarrantyStatusId = item.BD_WarrantyStatusId,
                //    BD_WarrantyStatus = item.BD_WarrantyStatus,
                //    BD_TechnicalSupportEnggId = item.BD_TechnicalSupportEnggId,
                //    BD_TechnicalSupportEngg = item.BD_TechnicalSupportEngg,

                //    TSAD_Visual = item.TSAD_Visual,
                //    TSAD_VisualImageFileName = item.TSAD_VisualImageFileName,
                //    TSAD_VisualImageOriginalFileName = item.TSAD_VisualImageOriginalFileName,
                //    TSAD_VisualImageURL = item.TSAD_VisualImageURL,
                //    TSAD_BatteryTemperature = item.TSAD_BatteryTemperature,
                //    TSAD_CurrentChargingValue = item.TSAD_CurrentChargingValue,
                //    TSAD_CurrentDischargingValue = item.TSAD_CurrentDischargingValue,
                //    TSAD_BatterVoltage = item.TSAD_BatterVoltage,
                //    TSAD_CellDiffrence = item.TSAD_CellDiffrence,
                //    TSAD_ProtectionsId = item.TSAD_ProtectionsId,
                //    TSAD_Protections = item.TSAD_Protections,

                //    TSAD_CycleCount = item.TSAD_CycleCount,
                //    TSPD_PhysicaImageFileName = item.TSPD_PhysicaImageFileName,
                //    TSPD_PhysicaImageOriginalFileName = item.TSPD_PhysicaImageOriginalFileName,
                //    TSPD_PhysicaImageURL = item.TSPD_PhysicaImageURL,
                //    TSPD_AnyPhysicalDamage = item.TSPD_AnyPhysicalDamage,
                //    TSPD_Other = item.TSPD_Other,
                //    TSPD_IsWarrantyVoid = item.TSPD_IsWarrantyVoid,
                //    TSSP_SolutionProvider = item.TSSP_SolutionProvider,
                //    TSSP_AllocateToServiceEnggId = item.TSSP_AllocateToServiceEnggId,
                //    TSSP_AllocateToServiceEngg = item.TSSP_AllocateToServiceEngg,
                //    TSSP_Remarks = item.TSSP_Remarks,

                //    CP_Visual = item.CP_Visual,
                //    CP_VisualImageFileName = item.CP_VisualImageFileName,
                //    CP_VisualImageOriginalFileName = item.CP_VisualImageOriginalFileName,
                //    CP_VisualImageURL = item.CP_VisualImageURL,
                //    CP_TerminalVoltage = item.CP_TerminalVoltage,
                //    CP_CommunicationWithBattery = item.CP_CommunicationWithBattery,
                //    CP_TerminalWire = item.CP_TerminalWire,
                //    CP_TerminalWireImageFileName = item.CP_TerminalWireImageFileName,
                //    CP_TerminalWireImageOriginalFileName = item.CP_TerminalWireImageOriginalFileName,
                //    CP_TerminalWireImageURL = item.CP_TerminalWireImageURL,
                //    CP_LifeCycle = item.CP_LifeCycle,
                //    CP_StringVoltageVariation = item.CP_StringVoltageVariation,
                //    CP_BatteryParametersSetting = item.CP_BatteryParametersSetting,
                //    CP_BatteryParametersSettingImageFileName = item.CP_BatteryParametersSettingImageFileName,
                //    CP_BatteryParametersSettingImageOriginalFileName = item.CP_BatteryParametersSettingImageOriginalFileName,
                //    CP_BatteryParametersSettingImageURL = item.CP_BatteryParametersSettingImageURL,
                //    CP_Spare = item.CP_Spare,
                //    CP_BMSStatus = item.CP_BMSStatus,
                //    CP_BMSSoftwareImageFileName = item.CP_BMSSoftwareImageFileName,
                //    CP_BMSSoftwareImageOriginalFileName = item.CP_BMSSoftwareImageOriginalFileName,
                //    CP_BMSSoftwareImageURL = item.CP_BMSSoftwareImageURL,
                //    CP_BMSType = item.CP_BMSType,
                //    CP_BatteryTemp = item.CP_BatteryTemp,
                //    CP_BMSSerialNumber = item.CP_BMSSerialNumber,
                //    CP_ProblemObserved = item.CP_ProblemObserved,

                //    CC_BatteryRepairedOnSite = item.CC_BatteryRepairedOnSite,
                //    CC_BatteryRepairedToPlant = item.CC_BatteryRepairedToPlant,

                //    OV_IsCustomerAvailable = item.OV_IsCustomerAvailable,
                //    OV_EngineerName = item.OV_EngineerName,
                //    OV_EngineerNumber = item.OV_EngineerNumber,
                //    OV_CustomerName = item.OV_CustomerName,
                //    OV_CustomerNameSecondary = item.OV_CustomerNameSecondary,
                //    OV_CustomerMobileNumber = item.OV_CustomerMobileNumber,
                //    OV_RequestOTP = item.OV_RequestOTP,
                //    OV_Signature = item.OV_Signature,

                //    TicketStatusId = item.TicketStatusId,
                //    TicketStatus = item.TicketStatus,
                //    TicketStatusSequenceNo = item.TicketStatusSequenceNo,
                //    TRC_EngineerId = item.TRC_EngineerId,
                //    TRC_Engineer = item.TRC_Engineer,

                //    IsActive = item.IsActive,

                //    CreatorName = item.CreatorName,
                //    CreatedBy = item.CreatedBy,
                //    CreatedDate = item.CreatedDate,

                //    ModifierName = item.ModifierName,
                //    ModifiedBy = item.ModifiedBy,
                //    ModifiedDate = item.ModifiedDate,
                //};

                var vResultPartListObj = await _manageTicketRepository.GetManageTicketPartDetailById(Convert.ToInt32(item.TicketId));
                foreach (var itemPart in vResultPartListObj)
                {
                    var vManageTicketPartDetails_Response = new ManageTicketPartDetails_Response()
                    {
                        Id = item.Id,
                        TicketId = itemPart.TicketId,
                        SpareDetailsId = itemPart.SpareDetailsId,
                        UniqueCode = itemPart.UniqueCode,
                        SpareDesc = itemPart.SpareDesc,
                        Quantity = itemPart.Quantity,
                        PartStatusId = itemPart.PartStatusId,
                        PartStatus = itemPart.PartStatus,
                        RGP = itemPart.RGP,
                    };

                    item.PartDetails.Add(vManageTicketPartDetails_Response);
                }

                vManageTicketDetail_Response.Add(item);
            }

            _response.Data = vManageTicketDetail_Response;
            _response.Total = parameters.Total;
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

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> TicketOTPGenerate(string SearchText)
        {
            var objList = await _manageTicketRepository.GetCustomerMobileNumberList(SearchText);
            _response.Data = objList.ToList();
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

            _response.Id = result;
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

        //[Route("[action]")]
        //[HttpPost]
        //public async Task<ResponseModel> SaveEnquiryConvertToTicket(int EnquiryId)
        //{
        //    if (EnquiryId <= 0)
        //    {
        //        _response.Message = "Id is required";
        //    }
        //    else
        //    {
        //        var vResultEnquiryObj = await _manageEnquiryRepository.GetManageEnquiryById(EnquiryId);
        //        if (vResultEnquiryObj != null)
        //        {
        //            if (vResultEnquiryObj.IsConvertToTicket == true)
        //            {
        //                _response.Message = "The Enquiry already converted into tickets. Please try another enquiry";

        //                return _response;
        //            }

        //            var vManageTicket_Request = new ManageTicket_Request()
        //            {
        //                //TicketdDate = DateTime.Now,
        //                //TicketdTime = DateTime.Now.ToLongTimeString(),

        //                CD_LoggingSourceId = vResultEnquiryObj.CD_LoggingSourceId,
        //                CD_CallerTypeId = vResultEnquiryObj.CD_CallerTypeId,
        //                CD_CallerName = vResultEnquiryObj.CD_CallerName,
        //                CD_CallerMobile = vResultEnquiryObj.CD_CallerMobile,
        //                CD_CallerEmailId = vResultEnquiryObj.CD_CallerEmailId,

        //                CD_CallerAddressId = vResultEnquiryObj.CD_CallerAddressId,
        //                CD_CallerAddress1 = vResultEnquiryObj.CD_CallerAddress1,
        //                CD_CallerRegionId = vResultEnquiryObj.CD_CallerRegionId,
        //                CD_CallerStateId = vResultEnquiryObj.CD_CallerStateId,
        //                CD_CallerDistrictId = vResultEnquiryObj.CD_CallerDistrictId,
        //                CD_CallerCityId = vResultEnquiryObj.CD_CallerCityId,
        //                CD_CallerPinCode = vResultEnquiryObj.CD_CallerPinCode,
        //                CD_CallerRemarks = vResultEnquiryObj.CD_CallerRemarks,

        //                CD_IsSiteAddressSameAsCaller = vResultEnquiryObj.CD_IsSiteAddressSameAsCaller,
        //                CD_BatterySerialNumber = vResultEnquiryObj.CD_BatterySerialNumber,
        //                CD_CustomerTypeId = vResultEnquiryObj.CD_CustomerTypeId,
        //                CD_CustomerNameId = vResultEnquiryObj.CD_CustomerNameId,
        //                CD_CustomerMobile = vResultEnquiryObj.CD_CustomerMobile,

        //                CD_CustomerAddressId = vResultEnquiryObj.CD_CustomerAddressId,
        //                CD_CustomerAddress1 = vResultEnquiryObj.CD_CustomerAddress1,
        //                CD_CustomerRegionId = vResultEnquiryObj.CD_CustomerRegionId,
        //                CD_CustomerStateId = vResultEnquiryObj.CD_CustomerStateId,
        //                CD_CustomerDistrictId = vResultEnquiryObj.CD_CustomerDistrictId,
        //                CD_CustomerCityId = vResultEnquiryObj.CD_CustomerCityId,
        //                CD_CustomerPinCode = vResultEnquiryObj.CD_CustomerPinCode,

        //                CD_SiteCustomerName = vResultEnquiryObj.CD_SiteCustomerName,
        //                CD_SiteContactName = vResultEnquiryObj.CD_SiteContactName,
        //                CD_SitContactMobile = vResultEnquiryObj.CD_SitContactMobile,

        //                CD_SiteAddressId = vResultEnquiryObj.CD_SiteAddressId,
        //                CD_SiteCustomerAddress1 = vResultEnquiryObj.CD_SiteCustomerAddress1,
        //                CD_SiteCustomerRegionId = vResultEnquiryObj.CD_SiteCustomerRegionId,
        //                CD_SiteCustomerStateId = vResultEnquiryObj.CD_SiteCustomerStateId,
        //                CD_SiteCustomerDistrictId = vResultEnquiryObj.CD_SiteCustomerDistrictId,
        //                CD_SiteCustomerCityId = vResultEnquiryObj.CD_SiteCustomerCityId,
        //                CD_SiteCustomerPinCode = vResultEnquiryObj.CD_SiteCustomerPinCode,

        //                BD_BatteryBOMNumber = vResultEnquiryObj.BD_BatteryBOMNumber,
        //                BD_BatterySegmentId = vResultEnquiryObj.BD_BatterySegmentId,
        //                BD_BatterySubSegmentId = vResultEnquiryObj.BD_BatterySubSegmentId,
        //                BD_BatteryProductModelId = vResultEnquiryObj.BD_BatteryProductModelId,
        //                BD_BatteryCellChemistryId = vResultEnquiryObj.BD_BatteryCellChemistryId,
        //                BD_DateofManufacturing = vResultEnquiryObj.BD_DateofManufacturing,
        //                BD_ProbReportedByCustId = vResultEnquiryObj.BD_ProbReportedByCustId,
        //                BD_WarrantyStartDate = vResultEnquiryObj.BD_WarrantyStartDate,
        //                BD_WarrantyEndDate = vResultEnquiryObj.BD_WarrantyEndDate,
        //                BD_WarrantyStatusId = vResultEnquiryObj.BD_WarrantyStatusId,
        //                BD_TechnicalSupportEnggId = vResultEnquiryObj.BD_TechnicalSupportEnggId,

        //                TicketStatusFromId = 1,
        //                EnquiryId = vResultEnquiryObj.Id,
        //            };


        //            int result = await _manageTicketRepository.SaveManageTicket(vManageTicket_Request);

        //            if (result == (int)SaveOperationEnums.NoRecordExists)
        //            {
        //                _response.Message = "No record exists";
        //            }
        //            else if (result == (int)SaveOperationEnums.ReocrdExists)
        //            {
        //                _response.Message = "Record already exists";
        //            }
        //            else if (result == (int)SaveOperationEnums.NoResult)
        //            {
        //                _response.Message = "Something went wrong, please try again";
        //            }
        //            else
        //            {
        //                var vManageEnquiry_Request = new ManageEnquiry_Request()
        //                {
        //                    Id = vResultEnquiryObj.Id,
        //                    EnquiryNumber = vResultEnquiryObj.EnquiryNumber,
        //                    EnquiryDate = DateTime.Now,
        //                    EnquiryTime = DateTime.Now.ToLongTimeString(),

        //                    CD_LoggingSourceId = vResultEnquiryObj.CD_LoggingSourceId,
        //                    CD_CallerTypeId = vResultEnquiryObj.CD_CallerTypeId,
        //                    CD_CallerName = vResultEnquiryObj.CD_CallerName,
        //                    CD_CallerMobile = vResultEnquiryObj.CD_CallerMobile,
        //                    CD_CallerEmailId = vResultEnquiryObj.CD_CallerEmailId,

        //                    CD_CallerAddressId = vResultEnquiryObj.CD_CallerAddressId,
        //                    CD_CallerAddress1 = vResultEnquiryObj.CD_CallerAddress1,
        //                    CD_CallerRegionId = vResultEnquiryObj.CD_CallerRegionId,
        //                    CD_CallerStateId = vResultEnquiryObj.CD_CallerStateId,
        //                    CD_CallerDistrictId = vResultEnquiryObj.CD_CallerDistrictId,
        //                    CD_CallerCityId = vResultEnquiryObj.CD_CallerCityId,
        //                    CD_CallerPinCode = vResultEnquiryObj.CD_CallerPinCode,
        //                    CD_CallerRemarks = vResultEnquiryObj.CD_CallerRemarks,

        //                    CD_IsSiteAddressSameAsCaller = vResultEnquiryObj.CD_IsSiteAddressSameAsCaller,
        //                    CD_BatterySerialNumber = vResultEnquiryObj.CD_BatterySerialNumber,
        //                    CD_CustomerTypeId = vResultEnquiryObj.CD_CustomerTypeId,
        //                    CD_CustomerNameId = vResultEnquiryObj.CD_CustomerNameId,
        //                    CD_CustomerMobile = vResultEnquiryObj.CD_CustomerMobile,

        //                    CD_CustomerAddressId = vResultEnquiryObj.CD_CustomerAddressId,
        //                    CD_CustomerAddress1 = vResultEnquiryObj.CD_CustomerAddress1,
        //                    CD_CustomerRegionId = vResultEnquiryObj.CD_CustomerRegionId,
        //                    CD_CustomerStateId = vResultEnquiryObj.CD_CustomerStateId,
        //                    CD_CustomerDistrictId = vResultEnquiryObj.CD_CustomerDistrictId,
        //                    CD_CustomerCityId = vResultEnquiryObj.CD_CustomerCityId,
        //                    CD_CustomerPinCode = vResultEnquiryObj.CD_CustomerPinCode,

        //                    CD_SiteCustomerName = vResultEnquiryObj.CD_SiteCustomerName,
        //                    CD_SiteContactName = vResultEnquiryObj.CD_SiteContactName,
        //                    CD_SitContactMobile = vResultEnquiryObj.CD_SitContactMobile,

        //                    CD_SiteAddressId = vResultEnquiryObj.CD_SiteAddressId,
        //                    CD_SiteCustomerAddress1 = vResultEnquiryObj.CD_SiteCustomerAddress1,
        //                    CD_SiteCustomerRegionId = vResultEnquiryObj.CD_SiteCustomerRegionId,
        //                    CD_SiteCustomerStateId = vResultEnquiryObj.CD_SiteCustomerStateId,
        //                    CD_SiteCustomerDistrictId = vResultEnquiryObj.CD_SiteCustomerDistrictId,
        //                    CD_SiteCustomerCityId = vResultEnquiryObj.CD_SiteCustomerCityId,
        //                    CD_SiteCustomerPinCode = vResultEnquiryObj.CD_SiteCustomerPinCode,

        //                    BD_BatteryBOMNumber = vResultEnquiryObj.BD_BatteryBOMNumber,
        //                    BD_BatterySegmentId = vResultEnquiryObj.BD_BatterySegmentId,
        //                    BD_BatterySubSegmentId = vResultEnquiryObj.BD_BatterySubSegmentId,
        //                    BD_BatteryProductModelId = vResultEnquiryObj.BD_BatteryProductModelId,
        //                    BD_BatteryCellChemistryId = vResultEnquiryObj.BD_BatteryCellChemistryId,
        //                    BD_DateofManufacturing = vResultEnquiryObj.BD_DateofManufacturing,
        //                    BD_ProbReportedByCustId = vResultEnquiryObj.BD_ProbReportedByCustId,
        //                    BD_WarrantyStartDate = vResultEnquiryObj.BD_WarrantyStartDate,
        //                    BD_WarrantyEndDate = vResultEnquiryObj.BD_WarrantyEndDate,
        //                    BD_WarrantyStatusId = vResultEnquiryObj.BD_WarrantyStatusId,
        //                    BD_TechnicalSupportEnggId = vResultEnquiryObj.BD_TechnicalSupportEnggId,

        //                    IsConvertToTicket = true,
        //                    IsActive = vResultEnquiryObj.IsActive,
        //                };

        //                int resultManageEnquiry = await _manageEnquiryRepository.SaveManageEnquiry(vManageEnquiry_Request);

        //                _response.Message = "Record details saved sucessfully";
        //            }

        //            _response.Id = result;
        //        }
        //    }

        //    return _response;
        //}

        #endregion
    }
}
