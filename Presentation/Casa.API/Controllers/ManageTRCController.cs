using CLN.Application.Enums;
using CLN.Application.Helpers;
using CLN.Application.Interfaces;
using CLN.Application.Models;
using CLN.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.ComponentModel;
using System.Globalization;
using LicenseContext = OfficeOpenXml.LicenseContext;

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
            //Reserve Pickup >> image upload
            if (parameters! != null && !string.IsNullOrWhiteSpace(parameters.RP_ProductPackingPhoto1_Base64))
            {
                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(parameters.RP_ProductPackingPhoto1_Base64, "\\Uploads\\Ticket\\", parameters.RP_ProductPackingPhotoOriginalFileName1);

                if (!string.IsNullOrWhiteSpace(vUploadFile))
                {
                    parameters.RP_ProductPackingPhotoFileName1 = vUploadFile;
                }
            }

            if (parameters! != null && !string.IsNullOrWhiteSpace(parameters.RP_ProductPackingPhoto2_Base64))
            {
                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(parameters.RP_ProductPackingPhoto2_Base64, "\\Uploads\\Ticket\\", parameters.RP_ProductPackingPhotoOriginalFileName2);

                if (!string.IsNullOrWhiteSpace(vUploadFile))
                {
                    parameters.RP_ProductPackingPhotoFileName2 = vUploadFile;
                }
            }

            if (parameters! != null && !string.IsNullOrWhiteSpace(parameters.RP_ProductPackingPhoto3_Base64))
            {
                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(parameters.RP_ProductPackingPhoto3_Base64, "\\Uploads\\Ticket\\", parameters.RP_ProductPackingPhotoOriginalFileName3);

                if (!string.IsNullOrWhiteSpace(vUploadFile))
                {
                    parameters.RP_ProductPackingPhotoFileName3 = vUploadFile;
                }
            }

            if (parameters! != null && !string.IsNullOrWhiteSpace(parameters.RP_DeliveryChallanPhoto_Base64))
            {
                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(parameters.RP_DeliveryChallanPhoto_Base64, "\\Uploads\\Ticket\\", parameters.RP_DeliveryChallanPhotoOriginalFileName);

                if (!string.IsNullOrWhiteSpace(vUploadFile))
                {
                    parameters.RP_DeliveryChallanPhotoFileName = vUploadFile;
                }
            }

            if (parameters! != null && !string.IsNullOrWhiteSpace(parameters.RP_ReservePickupFormat_Base64))
            {
                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(parameters.RP_ReservePickupFormat_Base64, "\\Uploads\\Ticket\\", parameters.RP_ReservePickupFormatOriginalFileName);

                if (!string.IsNullOrWhiteSpace(vUploadFile))
                {
                    parameters.RP_ReservePickupFormatFileName = vUploadFile;
                }
            }

            //Document Number and Varification
            if (parameters! != null && !string.IsNullOrWhiteSpace(parameters.DNV_DeliveryChallanPhoto_Base64))
            {
                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(parameters.DNV_DeliveryChallanPhoto_Base64, "\\Uploads\\Ticket\\", parameters.DNV_DeliveryChallanPhotoOriginalFileName);

                if (!string.IsNullOrWhiteSpace(vUploadFile))
                {
                    parameters.DNV_DeliveryChallanPhotoFileName = vUploadFile;
                }
            }

            //Initial Inspection
            if (parameters! != null && !string.IsNullOrWhiteSpace(parameters.II_Tempered1_Base64))
            {
                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(parameters.II_Tempered1_Base64, "\\Uploads\\Ticket\\", parameters.II_TemperedOriginalFileName1);

                if (!string.IsNullOrWhiteSpace(vUploadFile))
                {
                    parameters.II_TemperedFileName1 = vUploadFile;
                }
            }

            if (parameters! != null && !string.IsNullOrWhiteSpace(parameters.II_Tempered2_Base64))
            {
                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(parameters.II_Tempered2_Base64, "\\Uploads\\Ticket\\", parameters.II_TemperedOriginalFileName2);

                if (!string.IsNullOrWhiteSpace(vUploadFile))
                {
                    parameters.II_TemperedFileName2 = vUploadFile;
                }
            }

            if (parameters! != null && !string.IsNullOrWhiteSpace(parameters.II_Tempered3_Base64))
            {
                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(parameters.II_Tempered3_Base64, "\\Uploads\\Ticket\\", parameters.II_TemperedOriginalFileName3);

                if (!string.IsNullOrWhiteSpace(vUploadFile))
                {
                    parameters.II_TemperedFileName3 = vUploadFile;
                }
            }

            if (parameters! != null && !string.IsNullOrWhiteSpace(parameters.II_PhysicallyDamage1_Base64))
            {
                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(parameters.II_PhysicallyDamage1_Base64, "\\Uploads\\Ticket\\", parameters.II_PhysicallyDamageOriginalFileName1);

                if (!string.IsNullOrWhiteSpace(vUploadFile))
                {
                    parameters.II_PhysicallyDamageFileName1 = vUploadFile;
                }
            }

            if (parameters! != null && !string.IsNullOrWhiteSpace(parameters.II_PhysicallyDamage2_Base64))
            {
                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(parameters.II_PhysicallyDamage2_Base64, "\\Uploads\\Ticket\\", parameters.II_PhysicallyDamageOriginalFileName2);

                if (!string.IsNullOrWhiteSpace(vUploadFile))
                {
                    parameters.II_PhysicallyDamageFileName2 = vUploadFile;
                }
            }

            if (parameters! != null && !string.IsNullOrWhiteSpace(parameters.II_PhysicallyDamage3_Base64))
            {
                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(parameters.II_PhysicallyDamage3_Base64, "\\Uploads\\Ticket\\", parameters.II_PhysicallyDamageOriginalFileName3);

                if (!string.IsNullOrWhiteSpace(vUploadFile))
                {
                    parameters.II_PhysicallyDamageFileName3 = vUploadFile;
                }
            }

            //Warranty Status
            if (parameters! != null && !string.IsNullOrWhiteSpace(parameters.WS_Invoice_Base64))
            {
                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(parameters.WS_Invoice_Base64, "\\Uploads\\Ticket\\", parameters.WS_InvoiceOriginalFileName);

                if (!string.IsNullOrWhiteSpace(vUploadFile))
                {
                    parameters.WS_InvoiceFileName = vUploadFile;
                }
            }

            //PDI Inspection
            if (parameters! != null && !string.IsNullOrWhiteSpace(parameters.PI_SOCPercentage_Base64))
            {
                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(parameters.PI_SOCPercentage_Base64, "\\Uploads\\Ticket\\", parameters.PI_SOCPercentageOriginalFileName);

                if (!string.IsNullOrWhiteSpace(vUploadFile))
                {
                    parameters.PI_SOCPercentageFileName = vUploadFile;
                }
            }

            if (parameters! != null && !string.IsNullOrWhiteSpace(parameters.PI_FinalVoltage_Base64))
            {
                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(parameters.PI_FinalVoltage_Base64, "\\Uploads\\Ticket\\", parameters.PI_FinalVoltageOriginalFileName);

                if (!string.IsNullOrWhiteSpace(vUploadFile))
                {
                    parameters.PI_FinalVoltageFileName = vUploadFile;
                }
            }

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
                        SpareCategoryId = item.SpareCategoryId,
                        SpareDetailsId = item.SpareDetailsId,
                        Quantity = item.Quantity,
                        AvailableQty = item.AvailableQty,
                        //SparePartNo = item.SparePartNo,
                        //PartDescription = item.PartDescription,
                        //Quantity = item.Quantity,
                        //Remarks = item.Remarks,
                        //PartStatusId = item.PartStatusId,
                    };
                    int resultTechnicalSupportAddUpdate = await _manageTRCRepository.SaveManageTRCPartDetail(vManageTRCPartDetails_Request);
                }
            }

            // Save Ticket Log History
            if (parameters.TicketId > 0)
            {
                int resultManageTicketLog = await _manageTicketRepository.SaveManageTicketLogHistory(Convert.ToInt32(parameters.TicketId));
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
        public async Task<ResponseModel> GetManageTRCById(int Id)
        {
            var vManageTRCDetail_Response = new ManageTRCDetail_Response();

            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _manageTRCRepository.GetManageTRCById(Id);
                if (vResultObj != null)
                {
                    // TRC Deatil Bind
                    vManageTRCDetail_Response.Id = vResultObj.Id;
                    vManageTRCDetail_Response.TicketId = vResultObj.TicketId;

                    vManageTRCDetail_Response.TRCNumber = vResultObj.TRCNumber;
                    vManageTRCDetail_Response.TRCDate = vResultObj.TRCDate;
                    vManageTRCDetail_Response.TRCTime = vResultObj.TRCTime;

                    vManageTRCDetail_Response.RP_IsCLNOrCustomer = vResultObj.RP_IsCLNOrCustomer;
                    vManageTRCDetail_Response.RP_ProblemReportedByCustId= vResultObj.RP_ProblemReportedByCustId;
                    vManageTRCDetail_Response.RP_ProblemReportedByCust = vResultObj.RP_ProblemReportedByCust;
                    vManageTRCDetail_Response.RP_ProblemDecription= vResultObj.RP_ProblemDecription;
                    vManageTRCDetail_Response.RP_ProductPackingPhotoOriginalFileName1= vResultObj.RP_ProductPackingPhotoOriginalFileName1;
                    vManageTRCDetail_Response.RP_ProductPackingPhotoFileName1= vResultObj.RP_ProductPackingPhotoFileName1;
                    vManageTRCDetail_Response.RP_ProductPackingPhotoURL1 = vResultObj.RP_ProductPackingPhotoURL1;

                    vManageTRCDetail_Response.RP_ProductPackingPhotoOriginalFileName2= vResultObj.RP_ProductPackingPhotoOriginalFileName2;
                    vManageTRCDetail_Response.RP_ProductPackingPhotoFileName2= vResultObj.RP_ProductPackingPhotoFileName2;
                    vManageTRCDetail_Response.RP_ProductPackingPhotoURL2 = vResultObj.RP_ProductPackingPhotoURL2;

                    vManageTRCDetail_Response.RP_ProductPackingPhotoOriginalFileName3= vResultObj.RP_ProductPackingPhotoOriginalFileName3;
                    vManageTRCDetail_Response.RP_ProductPackingPhotoFileName3= vResultObj.RP_ProductPackingPhotoFileName3;
                    vManageTRCDetail_Response.RP_ProductPackingPhotoURL3 = vResultObj.RP_ProductPackingPhotoURL3;

                    vManageTRCDetail_Response.RP_DeliveryChallanPhotoOriginalFileName= vResultObj.RP_DeliveryChallanPhotoOriginalFileName;
                    vManageTRCDetail_Response.RP_DeliveryChallanPhotoFileName= vResultObj.RP_DeliveryChallanPhotoFileName;
                    vManageTRCDetail_Response.RP_DeliveryChallanPhotoURL = vResultObj.RP_DeliveryChallanPhotoURL;

                    vManageTRCDetail_Response.RP_ReservePickupFormatOriginalFileName= vResultObj.RP_ReservePickupFormatOriginalFileName;
                    vManageTRCDetail_Response.RP_ReservePickupFormatFileName= vResultObj.RP_ReservePickupFormatFileName;
                    vManageTRCDetail_Response.RP_ReservePickupFormatURL = vResultObj.RP_ReservePickupFormatURL;

                    vManageTRCDetail_Response.RP_IsReservePickupMailToLogistic= vResultObj.RP_IsReservePickupMailToLogistic;
                    vManageTRCDetail_Response.RP_DocketDetails= vResultObj.RP_DocketDetails;
                    vManageTRCDetail_Response.RP_IsBatteryInTransit= vResultObj.RP_IsBatteryInTransit;

                    vManageTRCDetail_Response.DNV_IsDeliveryChallanOrDebitNote = vResultObj.DNV_IsDeliveryChallanOrDebitNote;
                    vManageTRCDetail_Response.DNV_DeliveryChallanPhotoOriginalFileName= vResultObj.DNV_DeliveryChallanPhotoOriginalFileName;
                    vManageTRCDetail_Response.DNV_DeliveryChallanPhotoFileName= vResultObj.DNV_DeliveryChallanPhotoFileName;
                    vManageTRCDetail_Response.DNV_DeliveryChallanPhotoURL = vResultObj.DNV_DeliveryChallanPhotoURL;

                    vManageTRCDetail_Response.DNV_DebitNote= vResultObj.DNV_DebitNote;
                    vManageTRCDetail_Response.DNV_IsHandoverToMainStore= vResultObj.DNV_IsHandoverToMainStore;
                    vManageTRCDetail_Response.DNV_DeliveryChallanNumber= vResultObj.DNV_DeliveryChallanNumber;
                    vManageTRCDetail_Response.DNV_IsBatteryReceivedInTRC= vResultObj.DNV_IsBatteryReceivedInTRC;

                    vManageTRCDetail_Response.ATE_AssignedToEngineerId = vResultObj.ATE_AssignedToEngineerId;
                    vManageTRCDetail_Response.ATE_AssignedToEngineer = vResultObj.ATE_AssignedToEngineer;

                    vManageTRCDetail_Response.II_Visual = vResultObj.II_Visual;
                    vManageTRCDetail_Response.II_IsIntact = vResultObj.II_IsIntact;
                    vManageTRCDetail_Response.II_IsTempered = vResultObj.II_IsTempered;
                    vManageTRCDetail_Response.II_TemperedOriginalFileName1 = vResultObj.II_TemperedOriginalFileName1;
                    vManageTRCDetail_Response.II_TemperedFileName1 = vResultObj.II_TemperedFileName1;
                    vManageTRCDetail_Response.II_TemperedURL1 = vResultObj.II_TemperedURL1;
                    vManageTRCDetail_Response.II_TemperedOriginalFileName2 = vResultObj.II_TemperedOriginalFileName2;
                    vManageTRCDetail_Response.II_TemperedFileName2 = vResultObj.II_TemperedFileName2;
                    vManageTRCDetail_Response.II_TemperedURL2 = vResultObj.II_TemperedURL2;
                    vManageTRCDetail_Response.II_TemperedOriginalFileName3 = vResultObj.II_TemperedOriginalFileName3;
                    vManageTRCDetail_Response.II_TemperedFileName3 = vResultObj.II_TemperedFileName3;
                    vManageTRCDetail_Response.II_TemperedURL3 = vResultObj.II_TemperedURL3;
                    vManageTRCDetail_Response.II_PhysicallyDamageOriginalFileName1 = vResultObj.II_PhysicallyDamageOriginalFileName1;
                    vManageTRCDetail_Response.II_PhysicallyDamageFileName1 = vResultObj.II_PhysicallyDamageFileName1;
                    vManageTRCDetail_Response.II_PhysicallyDamageURL1 = vResultObj.II_PhysicallyDamageURL1;
                    vManageTRCDetail_Response.II_PhysicallyDamageOriginalFileName2 = vResultObj.II_PhysicallyDamageOriginalFileName2;
                    vManageTRCDetail_Response.II_PhysicallyDamageFileName2 = vResultObj.II_PhysicallyDamageFileName2;
                    vManageTRCDetail_Response.II_PhysicallyDamageURL2 = vResultObj.II_PhysicallyDamageURL2;
                    vManageTRCDetail_Response.II_PhysicallyDamageOriginalFileName3 = vResultObj.II_PhysicallyDamageOriginalFileName3;
                    vManageTRCDetail_Response.II_PhysicallyDamageFileName3 = vResultObj.II_PhysicallyDamageFileName3;
                    vManageTRCDetail_Response.II_PhysicallyDamageURL3 = vResultObj.II_PhysicallyDamageURL3;
                    vManageTRCDetail_Response.II_StringVoltageVariation = vResultObj.II_StringVoltageVariation;
                    vManageTRCDetail_Response.II_BatteryTerminalVoltage = vResultObj.II_BatteryTerminalVoltage;
                    vManageTRCDetail_Response.II_LifeCycle = vResultObj.II_LifeCycle;
                    vManageTRCDetail_Response.II_BatteryTemperature = vResultObj.II_BatteryTemperature;
                    vManageTRCDetail_Response.II_BMSSpecification = vResultObj.II_BMSSpecification;
                    vManageTRCDetail_Response.II_BMSBrand = vResultObj.II_BMSBrand;
                    vManageTRCDetail_Response.II_CellSpecification = vResultObj.II_CellSpecification;
                    vManageTRCDetail_Response.II_CellBrand = vResultObj.II_CellBrand;
                    vManageTRCDetail_Response.II_BMSSerialNumber = vResultObj.II_BMSSerialNumber;
                    vManageTRCDetail_Response.II_CellChemistryId = vResultObj.II_CellChemistryId;
                    vManageTRCDetail_Response.II_CellChemistry = vResultObj.II_CellChemistry;
                    vManageTRCDetail_Response.II_BatteryParameterSetting = vResultObj.II_BatteryParameterSetting;

                    vManageTRCDetail_Response.WS_IsWarrantyStatus = vResultObj.WS_IsWarrantyStatus;
                    vManageTRCDetail_Response.WS_IsInformedToCustomerByEmail = vResultObj.WS_IsInformedToCustomerByEmail;
                    vManageTRCDetail_Response.WS_IsCustomerAcceptance = vResultObj.WS_IsCustomerAcceptance;
                    vManageTRCDetail_Response.WS_IsPaymentClearance = vResultObj.WS_IsPaymentClearance;
                    vManageTRCDetail_Response.WS_InvoiceOriginalFileName = vResultObj.WS_InvoiceOriginalFileName;
                    vManageTRCDetail_Response.WS_InvoiceFileName = vResultObj.WS_InvoiceFileName;
                    vManageTRCDetail_Response.WS_InvoiceURL = vResultObj.WS_InvoiceURL;
                    vManageTRCDetail_Response.WS_IsReplacement = vResultObj.WS_IsReplacement;
                    vManageTRCDetail_Response.WS_NewProductSerialNumberId = vResultObj.WS_NewProductSerialNumberId;
                    vManageTRCDetail_Response.WS_NewProductSerialNumber = vResultObj.WS_NewProductSerialNumber;

                    vManageTRCDetail_Response.DA_ProblemObservedByEngId = vResultObj.DA_ProblemObservedByEngId;
                    vManageTRCDetail_Response.DA_ProblemObservedByEng = vResultObj.DA_ProblemObservedByEng;
                    vManageTRCDetail_Response.DA_ProblemObservedDesc = vResultObj.DA_ProblemObservedDesc;
                    vManageTRCDetail_Response.DA_RectificationActionId = vResultObj.DA_RectificationActionId;
                    vManageTRCDetail_Response.DA_RectificationAction = vResultObj.DA_RectificationAction;
                    vManageTRCDetail_Response.DA_ResolutionSummary = vResultObj.DA_ResolutionSummary;
                    vManageTRCDetail_Response.DA_CapacityAchieved = vResultObj.DA_CapacityAchieved;

                    vManageTRCDetail_Response.ATEFP_AssignedToEngineerId = vResultObj.ATEFP_AssignedToEngineerId;
                    vManageTRCDetail_Response.ATEFP_AssignedToEngineer = vResultObj.ATEFP_AssignedToEngineer;

                    vManageTRCDetail_Response.PI_BatteryReceivedDate = vResultObj.PI_BatteryReceivedDate;
                    vManageTRCDetail_Response.PI_BatteryReceivedTime = vResultObj.PI_BatteryReceivedTime;
                    vManageTRCDetail_Response.PI_PDIDoneDate = vResultObj.PI_PDIDoneDate;
                    vManageTRCDetail_Response.PI_PDIDoneTime = vResultObj.PI_PDIDoneTime;
                    vManageTRCDetail_Response.PI_PDIDoneById = vResultObj.PI_PDIDoneById;
                    vManageTRCDetail_Response.PI_PDIDoneByEngg = vResultObj.PI_PDIDoneByEngg;
                    vManageTRCDetail_Response.PI_SOCPercentageOriginalFileName = vResultObj.PI_SOCPercentageOriginalFileName;
                    vManageTRCDetail_Response.PI_SOCPercentageFileName = vResultObj.PI_SOCPercentageFileName;
                    vManageTRCDetail_Response.PI_SOCPercentageURL = vResultObj.PI_SOCPercentageURL;
                    vManageTRCDetail_Response.PI_VoltageDifference = vResultObj.PI_VoltageDifference;
                    vManageTRCDetail_Response.PI_FinalVoltageOriginalFileName = vResultObj.PI_FinalVoltageOriginalFileName;
                    vManageTRCDetail_Response.PI_FinalVoltageFileName = vResultObj.PI_FinalVoltageFileName;
                    vManageTRCDetail_Response.PI_FinalVoltageURL = vResultObj.PI_FinalVoltageURL;
                    vManageTRCDetail_Response.PI_AmpereHour = vResultObj.PI_AmpereHour;

                    vManageTRCDetail_Response.PIDD_DispatchedDeliveryChallan = vResultObj.PIDD_DispatchedDeliveryChallan;
                    vManageTRCDetail_Response.PIDD_DispatchedDate = vResultObj.PIDD_DispatchedDate;
                    vManageTRCDetail_Response.PIDD_DispatchedCityId = vResultObj.PIDD_DispatchedCityId;
                    vManageTRCDetail_Response.PIDD_DispatchedCity = vResultObj.PIDD_DispatchedCity;
                   
                    vManageTRCDetail_Response.DDB_DispatchedDoneBy = vResultObj.DDB_DispatchedDoneBy;
                    vManageTRCDetail_Response.DDB_DocketDetails = vResultObj.DDB_DocketDetails;
                    vManageTRCDetail_Response.DDB_CourierName = vResultObj.DDB_CourierName;

                    vManageTRCDetail_Response.CRD_CustomerReceivingDate = vResultObj.CRD_CustomerReceivingDate;

                    vManageTRCDetail_Response.TRCBranchId = vResultObj.TRCBranchId;
                    vManageTRCDetail_Response.TRCBranchName = vResultObj.TRCBranchName;

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


                    var vResultPartListObj = await _manageTRCRepository.GetManageTRCPartDetailById(Id);
                    foreach (var item in vResultPartListObj)
                    {
                        var vManageTRCPartDetails_Response = new ManageTRCPartDetails_Response()
                        {
                            Id = item.Id,
                            TRCId = item.TRCId,
                            SpareCategoryId = item.SpareCategoryId,
                            SpareCategory = item.SpareCategory,
                            SpareDetailsId = item.SpareDetailsId,
                            UniqueCode = item.UniqueCode,
                            SpareDesc = item.SpareDesc,
                            Quantity = item.Quantity,
                            AvailableQty = item.AvailableQty,
                            //PartStatusId = item.PartStatusId,
                            //PartStatus = item.PartStatus,
                            RGP = item.RGP,
                            //SparePartNo = item.SparePartNo,
                            //PartDescription = item.PartDescription,
                            //Quantity = item.Quantity,
                            //Remarks = item.Remarks,
                            //PartStatusId = item.PartStatusId
                        };

                        vManageTRCDetail_Response.PartDetails.Add(vManageTRCPartDetails_Response);
                    }

                    // Ticket Detail Bind
                    vManageTRCDetail_Response.TicketDetail = new ManageTicketDetail_Response();
                    var vResultTicketDetailObj = await _manageTicketRepository.GetManageTicketById(Convert.ToInt32(vResultObj.TicketId));
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
                        vManageTRCDetail_Response.TicketDetail.TSAD_CurrentChargingValue = vResultTicketDetailObj.TSAD_CurrentChargingValue;
                        vManageTRCDetail_Response.TicketDetail.TSAD_CurrentDischargingValue = vResultTicketDetailObj.TSAD_CurrentDischargingValue;
                        vManageTRCDetail_Response.TicketDetail.TSAD_BatteryTemperature = vResultTicketDetailObj.TSAD_BatteryTemperature;
                        vManageTRCDetail_Response.TicketDetail.TSAD_BatterVoltage = vResultTicketDetailObj.TSAD_BatterVoltage;
                        vManageTRCDetail_Response.TicketDetail.TSAD_CellDiffrence = vResultTicketDetailObj.TSAD_CellDiffrence;
                        vManageTRCDetail_Response.TicketDetail.TSAD_ProtectionsId = vResultTicketDetailObj.TSAD_ProtectionsId;
                        vManageTRCDetail_Response.TicketDetail.TSAD_Protections = vResultTicketDetailObj.TSAD_Protections;

                        vManageTRCDetail_Response.TicketDetail.TSAD_CycleCount = vResultTicketDetailObj.TSAD_CycleCount;
                        vManageTRCDetail_Response.TicketDetail.TSAD_ProblemObservedByEngId = vResultTicketDetailObj.TSAD_ProblemObservedByEngId;
                        vManageTRCDetail_Response.TicketDetail.TSAD_ProblemObservedByEng = vResultTicketDetailObj.TSAD_ProblemObservedByEng;
                        vManageTRCDetail_Response.TicketDetail.TSAD_ProblemObservedDesc = vResultTicketDetailObj.TSAD_ProblemObservedDesc;
                        vManageTRCDetail_Response.TicketDetail.TSAD_Gravity = vResultTicketDetailObj.TSAD_Gravity;
                        vManageTRCDetail_Response.TicketDetail.TSAD_IP_VoltageAC = vResultTicketDetailObj.TSAD_IP_VoltageAC;
                        vManageTRCDetail_Response.TicketDetail.TSAD_IP_VoltageDC = vResultTicketDetailObj.TSAD_IP_VoltageDC;
                        vManageTRCDetail_Response.TicketDetail.TSAD_OutputAC = vResultTicketDetailObj.TSAD_OutputAC;
                        vManageTRCDetail_Response.TicketDetail.TSAD_Protection = vResultTicketDetailObj.TSAD_Protection;
                        vManageTRCDetail_Response.TicketDetail.TSAD_AttachPhotoFileName = vResultTicketDetailObj.TSAD_AttachPhotoFileName;
                        vManageTRCDetail_Response.TicketDetail.TSAD_AttachPhotoURL = vResultTicketDetailObj.TSAD_AttachPhotoURL;
                        vManageTRCDetail_Response.TicketDetail.TSAD_FanStatus = vResultTicketDetailObj.TSAD_FanStatus;
                        vManageTRCDetail_Response.TicketDetail.TSAD_PhysicalPhotoFileName = vResultTicketDetailObj.TSAD_PhysicalPhotoFileName;
                        vManageTRCDetail_Response.TicketDetail.TSAD_PhysicalPhotoOriginalFileName = vResultTicketDetailObj.TSAD_PhysicalPhotoOriginalFileName;
                        vManageTRCDetail_Response.TicketDetail.TSAD_PhysicalPhotoURL = vResultTicketDetailObj.TSAD_PhysicalPhotoURL;
                        vManageTRCDetail_Response.TicketDetail.TSAD_IssueImageFileName = vResultTicketDetailObj.TSAD_IssueImageFileName;
                        vManageTRCDetail_Response.TicketDetail.TSAD_IssueImageOriginalFileName = vResultTicketDetailObj.TSAD_IssueImageOriginalFileName;
                        vManageTRCDetail_Response.TicketDetail.TSAD_IssueImageURL = vResultTicketDetailObj.TSAD_IssueImageURL;

                        vManageTRCDetail_Response.TicketDetail.TSPD_PhysicaImageFileName = vResultTicketDetailObj.TSPD_PhysicaImageFileName;
                        vManageTRCDetail_Response.TicketDetail.TSPD_PhysicaImageOriginalFileName = vResultTicketDetailObj.TSPD_PhysicaImageOriginalFileName;
                        vManageTRCDetail_Response.TicketDetail.TSPD_PhysicaImageURL = vResultTicketDetailObj.TSPD_PhysicaImageURL;
                        vManageTRCDetail_Response.TicketDetail.TSPD_AnyPhysicalDamage = vResultTicketDetailObj.TSPD_AnyPhysicalDamage;
                        vManageTRCDetail_Response.TicketDetail.TSPD_Other = vResultTicketDetailObj.TSPD_Other;
                        vManageTRCDetail_Response.TicketDetail.TSPD_IsWarrantyVoid = vResultTicketDetailObj.TSPD_IsWarrantyVoid;
                        vManageTRCDetail_Response.TicketDetail.TSPD_ReasonforVoid = vResultTicketDetailObj.TSPD_ReasonforVoid;
                        vManageTRCDetail_Response.TicketDetail.TSPD_TypeOfBMSId = vResultTicketDetailObj.TSPD_TypeOfBMSId;
                        vManageTRCDetail_Response.TicketDetail.TSPD_TypeOfBMS = vResultTicketDetailObj.TSPD_TypeOfBMS;
                      
                        vManageTRCDetail_Response.TicketDetail.TSSP_SolutionProvider = vResultTicketDetailObj.TSSP_SolutionProvider;
                        vManageTRCDetail_Response.TicketDetail.TSSP_AllocateToServiceEnggId = vResultTicketDetailObj.TSSP_AllocateToServiceEnggId;
                        vManageTRCDetail_Response.TicketDetail.TSSP_AllocateToServiceEngg = vResultTicketDetailObj.TSSP_AllocateToServiceEngg;
                        vManageTRCDetail_Response.TicketDetail.TSSP_Remarks = vResultTicketDetailObj.TSSP_Remarks;
                        vManageTRCDetail_Response.TicketDetail.TSSP_BranchId = vResultTicketDetailObj.TSSP_BranchId;
                        vManageTRCDetail_Response.TicketDetail.TSSP_BranchName = vResultTicketDetailObj.TSSP_BranchName;
                        vManageTRCDetail_Response.TicketDetail.TSSP_RectificationActionId = vResultTicketDetailObj.TSSP_RectificationActionId;
                        vManageTRCDetail_Response.TicketDetail.TSSP_RectificationAction = vResultTicketDetailObj.TSSP_RectificationAction;
                        vManageTRCDetail_Response.TicketDetail.TSSP_ResolutionSummary = vResultTicketDetailObj.TSSP_ResolutionSummary;

                        vManageTRCDetail_Response.TicketDetail.TS_AbnormalNoise = vResultTicketDetailObj.TS_AbnormalNoise;
                        vManageTRCDetail_Response.TicketDetail.TS_ConnectorDamage = vResultTicketDetailObj.TS_ConnectorDamage;
                        vManageTRCDetail_Response.TicketDetail.TS_ConnectorDamageFileName = vResultTicketDetailObj.TS_ConnectorDamageFileName;
                        vManageTRCDetail_Response.TicketDetail.TS_ConnectorDamageOriginalFileName = vResultTicketDetailObj.TS_ConnectorDamageOriginalFileName;
                        vManageTRCDetail_Response.TicketDetail.TS_ConnectorDamageURL = vResultTicketDetailObj.TS_ConnectorDamageURL;
                        vManageTRCDetail_Response.TicketDetail.TS_AnyBrunt = vResultTicketDetailObj.TS_AnyBrunt;
                        vManageTRCDetail_Response.TicketDetail.TS_AnyBruntFileName = vResultTicketDetailObj.TS_AnyBruntFileName;
                        vManageTRCDetail_Response.TicketDetail.TS_AnyBruntOriginalFileName = vResultTicketDetailObj.TS_AnyBruntOriginalFileName;
                        vManageTRCDetail_Response.TicketDetail.TS_AnyBruntURL = vResultTicketDetailObj.TS_AnyBruntURL;
                        vManageTRCDetail_Response.TicketDetail.TS_PhysicalDamage = vResultTicketDetailObj.TS_PhysicalDamage;
                        vManageTRCDetail_Response.TicketDetail.TS_PhysicalDamageFileName = vResultTicketDetailObj.TS_PhysicalDamageFileName;
                        vManageTRCDetail_Response.TicketDetail.TS_PhysicalDamageOriginalFileName = vResultTicketDetailObj.TS_PhysicalDamageOriginalFileName;
                        vManageTRCDetail_Response.TicketDetail.TS_PhysicalDamageURL = vResultTicketDetailObj.TS_PhysicalDamageURL;
                        vManageTRCDetail_Response.TicketDetail.TS_ProblemRemark = vResultTicketDetailObj.TS_ProblemRemark;
                        vManageTRCDetail_Response.TicketDetail.TS_IPCurrentAC_A = vResultTicketDetailObj.TS_IPCurrentAC_A;
                        vManageTRCDetail_Response.TicketDetail.TS_OutputCurrentDC_A = vResultTicketDetailObj.TS_OutputCurrentDC_A;
                        vManageTRCDetail_Response.TicketDetail.TS_OutputVoltageDC_V = vResultTicketDetailObj.TS_OutputVoltageDC_V;
                        vManageTRCDetail_Response.TicketDetail.TS_Type = vResultTicketDetailObj.TS_Type;
                        vManageTRCDetail_Response.TicketDetail.TS_Heating = vResultTicketDetailObj.TS_Heating;
                        vManageTRCDetail_Response.TicketDetail.TS_DisplayPhotoFileName = vResultTicketDetailObj.TS_DisplayPhotoFileName;
                        vManageTRCDetail_Response.TicketDetail.TS_DisplayPhotoOriginalFileName = vResultTicketDetailObj.TS_DisplayPhotoOriginalFileName;
                        vManageTRCDetail_Response.TicketDetail.TS_DisplayPhotoURL = vResultTicketDetailObj.TS_DisplayPhotoURL;
                        vManageTRCDetail_Response.TicketDetail.TS_OutputVoltageAC_V = vResultTicketDetailObj.TS_OutputVoltageAC_V;
                        vManageTRCDetail_Response.TicketDetail.TS_OutputCurrentAC_A = vResultTicketDetailObj.TS_OutputCurrentAC_A;
                        vManageTRCDetail_Response.TicketDetail.TS_IPCurrentDC_A = vResultTicketDetailObj.TS_IPCurrentDC_A;
                        vManageTRCDetail_Response.TicketDetail.TS_SpecificGravityC2 = vResultTicketDetailObj.TS_SpecificGravityC2;
                        vManageTRCDetail_Response.TicketDetail.TS_SpecificGravityC3 = vResultTicketDetailObj.TS_SpecificGravityC3;
                        vManageTRCDetail_Response.TicketDetail.TS_SpecificGravityC4 = vResultTicketDetailObj.TS_SpecificGravityC4;
                        vManageTRCDetail_Response.TicketDetail.TS_SpecificGravityC5 = vResultTicketDetailObj.TS_SpecificGravityC5;
                        vManageTRCDetail_Response.TicketDetail.TS_SpecificGravityC6 = vResultTicketDetailObj.TS_SpecificGravityC6;

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
                        vManageTRCDetail_Response.TicketDetail.CP_ProblemObservedByEngId = vResultTicketDetailObj.CP_ProblemObservedByEngId;
                        vManageTRCDetail_Response.TicketDetail.CP_ProblemObservedByEng = vResultTicketDetailObj.CP_ProblemObservedByEng;
                        vManageTRCDetail_Response.TicketDetail.CP_IsWarrantyVoid = vResultTicketDetailObj.CP_IsWarrantyVoid;
                        vManageTRCDetail_Response.TicketDetail.CP_ReasonForVoid = vResultTicketDetailObj.CP_ReasonForVoid;
                        vManageTRCDetail_Response.TicketDetail.CP_CommunicationWithMotorAndController = vResultTicketDetailObj.CP_CommunicationWithMotorAndController;
                        vManageTRCDetail_Response.TicketDetail.CP_UnderWarranty = vResultTicketDetailObj.CP_UnderWarranty;
                        vManageTRCDetail_Response.TicketDetail.CP_RPhaseVoltage = vResultTicketDetailObj.CP_RPhaseVoltage;
                        vManageTRCDetail_Response.TicketDetail.CP_YPhaseVoltage = vResultTicketDetailObj.CP_YPhaseVoltage;
                        vManageTRCDetail_Response.TicketDetail.CP_BPhaseVoltage = vResultTicketDetailObj.CP_BPhaseVoltage;
                        vManageTRCDetail_Response.TicketDetail.CP_BatteryOCV = vResultTicketDetailObj.CP_BatteryOCV;
                        vManageTRCDetail_Response.TicketDetail.CP_SystemVoltage = vResultTicketDetailObj.CP_SystemVoltage;
                        vManageTRCDetail_Response.TicketDetail.CP_SpecificGravityC1 = vResultTicketDetailObj.CP_SpecificGravityC1;
                        vManageTRCDetail_Response.TicketDetail.CP_SpecificGravityC2 = vResultTicketDetailObj.CP_SpecificGravityC2;
                        vManageTRCDetail_Response.TicketDetail.CP_SpecificGravityC3 = vResultTicketDetailObj.CP_SpecificGravityC3;
                        vManageTRCDetail_Response.TicketDetail.CP_SpecificGravityC4 = vResultTicketDetailObj.CP_SpecificGravityC4;
                        vManageTRCDetail_Response.TicketDetail.CP_SpecificGravityC5 = vResultTicketDetailObj.CP_SpecificGravityC5;
                        vManageTRCDetail_Response.TicketDetail.CP_SpecificGravityC6 = vResultTicketDetailObj.CP_SpecificGravityC6;
                        vManageTRCDetail_Response.TicketDetail.CP_BatteryVoltageDropOnLoad = vResultTicketDetailObj.CP_BatteryVoltageDropOnLoad;
                        vManageTRCDetail_Response.TicketDetail.CP_KmReading = vResultTicketDetailObj.CP_KmReading;
                        vManageTRCDetail_Response.TicketDetail.CP_TotalRun = vResultTicketDetailObj.CP_TotalRun;
                        vManageTRCDetail_Response.TicketDetail.CP_BatterySerialNo = vResultTicketDetailObj.CP_BatterySerialNo;

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
                        vManageTRCDetail_Response.TicketDetail.TRC_EngineerId = vResultTicketDetailObj.TRC_EngineerId;
                        vManageTRCDetail_Response.TicketDetail.TRC_Engineer = vResultTicketDetailObj.TRC_Engineer;

                        vManageTRCDetail_Response.TicketDetail.IsResolvedWithoutOTP = vResultTicketDetailObj.IsResolvedWithoutOTP;
                        vManageTRCDetail_Response.TicketDetail.IsClosedWithoutOTP = vResultTicketDetailObj.IsClosedWithoutOTP;
                        vManageTRCDetail_Response.TicketDetail.IsReopen = vResultTicketDetailObj.IsReopen;

                        vManageTRCDetail_Response.TicketDetail.RO_TSAD_Visual = vResultTicketDetailObj.RO_TSAD_Visual;
                        vManageTRCDetail_Response.TicketDetail.RO_TSAD_VisualImageFileName = vResultTicketDetailObj.RO_TSAD_VisualImageFileName;
                        vManageTRCDetail_Response.TicketDetail.RO_TSAD_VisualImageOriginalFileName = vResultTicketDetailObj.RO_TSAD_VisualImageOriginalFileName;
                        vManageTRCDetail_Response.TicketDetail.RO_TSAD_VisualImageURL = vResultTicketDetailObj.RO_TSAD_VisualImageURL;
                        vManageTRCDetail_Response.TicketDetail.RO_TSAD_BatteryTemperature = vResultTicketDetailObj.RO_TSAD_BatteryTemperature;
                        vManageTRCDetail_Response.TicketDetail.RO_TSAD_CurrentChargingValue = vResultTicketDetailObj.RO_TSAD_CurrentChargingValue;
                        vManageTRCDetail_Response.TicketDetail.RO_TSAD_CurrentDischargingValue = vResultTicketDetailObj.RO_TSAD_CurrentDischargingValue;
                        vManageTRCDetail_Response.TicketDetail.RO_TSAD_BatteryTemperature = vResultTicketDetailObj.RO_TSAD_BatteryTemperature;
                        vManageTRCDetail_Response.TicketDetail.RO_TSAD_BatterVoltage = vResultTicketDetailObj.RO_TSAD_BatterVoltage;
                        vManageTRCDetail_Response.TicketDetail.RO_TSAD_CellDiffrence = vResultTicketDetailObj.RO_TSAD_CellDiffrence;
                        vManageTRCDetail_Response.TicketDetail.RO_TSAD_ProtectionsId = vResultTicketDetailObj.RO_TSAD_ProtectionsId;
                        vManageTRCDetail_Response.TicketDetail.RO_TSAD_Protections = vResultTicketDetailObj.RO_TSAD_Protections;

                        vManageTRCDetail_Response.TicketDetail.RO_TSAD_CycleCount = vResultTicketDetailObj.RO_TSAD_CycleCount;
                        vManageTRCDetail_Response.TicketDetail.RO_TSAD_ProblemObservedByEngId = vResultTicketDetailObj.RO_TSAD_ProblemObservedByEngId;
                        vManageTRCDetail_Response.TicketDetail.RO_TSAD_ProblemObservedByEng = vResultTicketDetailObj.RO_TSAD_ProblemObservedByEng;
                        vManageTRCDetail_Response.TicketDetail.RO_TSAD_ProblemObservedDesc = vResultTicketDetailObj.RO_TSAD_ProblemObservedDesc;

                        vManageTRCDetail_Response.TicketDetail.RO_TSAD_Gravity = vResultTicketDetailObj.RO_TSAD_Gravity;
                        vManageTRCDetail_Response.TicketDetail.RO_TSAD_IP_VoltageAC = vResultTicketDetailObj.RO_TSAD_IP_VoltageAC;
                        vManageTRCDetail_Response.TicketDetail.RO_TSAD_IP_VoltageDC = vResultTicketDetailObj.RO_TSAD_IP_VoltageDC;
                        vManageTRCDetail_Response.TicketDetail.RO_TSAD_OutputAC = vResultTicketDetailObj.RO_TSAD_OutputAC;
                        vManageTRCDetail_Response.TicketDetail.RO_TSAD_Protection = vResultTicketDetailObj.RO_TSAD_Protection;
                        vManageTRCDetail_Response.TicketDetail.RO_TSAD_AttachPhotoFileName = vResultTicketDetailObj.RO_TSAD_AttachPhotoFileName;
                        vManageTRCDetail_Response.TicketDetail.RO_TSAD_AttachPhotoOriginalFileName = vResultTicketDetailObj.RO_TSAD_AttachPhotoOriginalFileName;
                        vManageTRCDetail_Response.TicketDetail.RO_TSAD_AttachPhotoURL = vResultTicketDetailObj.RO_TSAD_AttachPhotoURL;
                        vManageTRCDetail_Response.TicketDetail.RO_TSAD_FanStatus = vResultTicketDetailObj.RO_TSAD_FanStatus;
                        vManageTRCDetail_Response.TicketDetail.RO_TSAD_PhysicalPhotoFileName = vResultTicketDetailObj.RO_TSAD_PhysicalPhotoFileName;
                        vManageTRCDetail_Response.TicketDetail.RO_TSAD_PhysicalPhotoOriginalFileName = vResultTicketDetailObj.RO_TSAD_PhysicalPhotoOriginalFileName;
                        vManageTRCDetail_Response.TicketDetail.RO_TSAD_PhysicalPhotoURL = vResultTicketDetailObj.RO_TSAD_PhysicalPhotoURL;
                        vManageTRCDetail_Response.TicketDetail.RO_TSAD_IssueImageFileName = vResultTicketDetailObj.RO_TSAD_IssueImageFileName;
                        vManageTRCDetail_Response.TicketDetail.RO_TSAD_IssueImageOriginalFileName = vResultTicketDetailObj.RO_TSAD_IssueImageOriginalFileName;
                        vManageTRCDetail_Response.TicketDetail.RO_TSAD_IssueImageURL = vResultTicketDetailObj.RO_TSAD_IssueImageURL;

                        vManageTRCDetail_Response.TicketDetail.RO_TSPD_PhysicaImageFileName = vResultTicketDetailObj.RO_TSPD_PhysicaImageFileName;
                        vManageTRCDetail_Response.TicketDetail.RO_TSPD_PhysicaImageOriginalFileName = vResultTicketDetailObj.RO_TSPD_PhysicaImageOriginalFileName;
                        vManageTRCDetail_Response.TicketDetail.RO_TSPD_PhysicaImageURL = vResultTicketDetailObj.RO_TSPD_PhysicaImageURL;
                        vManageTRCDetail_Response.TicketDetail.RO_TSPD_AnyPhysicalDamage = vResultTicketDetailObj.RO_TSPD_AnyPhysicalDamage;
                        vManageTRCDetail_Response.TicketDetail.RO_TSPD_Other = vResultTicketDetailObj.RO_TSPD_Other;
                        vManageTRCDetail_Response.TicketDetail.RO_TSPD_IsWarrantyVoid = vResultTicketDetailObj.RO_TSPD_IsWarrantyVoid;
                        vManageTRCDetail_Response.TicketDetail.RO_TSPD_ReasonforVoid = vResultTicketDetailObj.RO_TSPD_ReasonforVoid;
                        vManageTRCDetail_Response.TicketDetail.RO_TSPD_TypeOfBMSId = vResultTicketDetailObj.RO_TSPD_TypeOfBMSId;
                        vManageTRCDetail_Response.TicketDetail.RO_TSPD_TypeOfBMS = vResultTicketDetailObj.RO_TSPD_TypeOfBMS;

                        vManageTRCDetail_Response.TicketDetail.RO_BD_TechnicalSupportEnggId = vResultTicketDetailObj.RO_BD_TechnicalSupportEnggId;
                        vManageTRCDetail_Response.TicketDetail.RO_BD_TechnicalSupportEngg = vResultTicketDetailObj.RO_BD_TechnicalSupportEngg;

                        vManageTRCDetail_Response.TicketDetail.RO_TSSP_SolutionProvider = vResultTicketDetailObj.RO_TSSP_SolutionProvider;
                        vManageTRCDetail_Response.TicketDetail.RO_TSSP_AllocateToServiceEnggId = vResultTicketDetailObj.RO_TSSP_AllocateToServiceEnggId;
                        vManageTRCDetail_Response.TicketDetail.RO_TSSP_AllocateToServiceEngg = vResultTicketDetailObj.RO_TSSP_AllocateToServiceEngg;
                        vManageTRCDetail_Response.TicketDetail.RO_TSSP_Remarks = vResultTicketDetailObj.RO_TSSP_Remarks;
                        vManageTRCDetail_Response.TicketDetail.RO_TSSP_BranchId = vResultTicketDetailObj.RO_TSSP_BranchId;
                        vManageTRCDetail_Response.TicketDetail.RO_TSSP_BranchName = vResultTicketDetailObj.RO_TSSP_BranchName;
                        vManageTRCDetail_Response.TicketDetail.RO_TSSP_RectificationActionId = vResultTicketDetailObj.RO_TSSP_RectificationActionId;
                        vManageTRCDetail_Response.TicketDetail.RO_TSSP_RectificationAction = vResultTicketDetailObj.RO_TSSP_RectificationAction;
                        vManageTRCDetail_Response.TicketDetail.RO_TSSP_ResolutionSummary = vResultTicketDetailObj.RO_TSSP_ResolutionSummary;

                        vManageTRCDetail_Response.TicketDetail.RO_TS_AbnormalNoise = vResultTicketDetailObj.RO_TS_AbnormalNoise;
                        vManageTRCDetail_Response.TicketDetail.RO_TS_ConnectorDamage = vResultTicketDetailObj.RO_TS_ConnectorDamage;
                        vManageTRCDetail_Response.TicketDetail.RO_TS_ConnectorDamageFileName = vResultTicketDetailObj.RO_TS_ConnectorDamageFileName;
                        vManageTRCDetail_Response.TicketDetail.RO_TS_ConnectorDamageOriginalFileName = vResultTicketDetailObj.RO_TS_ConnectorDamageOriginalFileName;
                        vManageTRCDetail_Response.TicketDetail.RO_TS_ConnectorDamageURL = vResultTicketDetailObj.RO_TS_ConnectorDamageURL;
                        vManageTRCDetail_Response.TicketDetail.RO_TS_AnyBrunt = vResultTicketDetailObj.RO_TS_AnyBrunt;
                        vManageTRCDetail_Response.TicketDetail.RO_TS_AnyBruntFileName = vResultTicketDetailObj.RO_TS_AnyBruntFileName;
                        vManageTRCDetail_Response.TicketDetail.RO_TS_AnyBruntOriginalFileName = vResultTicketDetailObj.RO_TS_AnyBruntOriginalFileName;
                        vManageTRCDetail_Response.TicketDetail.RO_TS_AnyBruntURL = vResultTicketDetailObj.RO_TS_AnyBruntURL;
                        vManageTRCDetail_Response.TicketDetail.RO_TS_PhysicalDamage = vResultTicketDetailObj.RO_TS_PhysicalDamage;
                        vManageTRCDetail_Response.TicketDetail.RO_TS_PhysicalDamageFileName = vResultTicketDetailObj.RO_TS_PhysicalDamageFileName;
                        vManageTRCDetail_Response.TicketDetail.RO_TS_PhysicalDamageOriginalFileName = vResultTicketDetailObj.RO_TS_PhysicalDamageOriginalFileName;
                        vManageTRCDetail_Response.TicketDetail.RO_TS_PhysicalDamageURL = vResultTicketDetailObj.RO_TS_PhysicalDamageURL;
                        vManageTRCDetail_Response.TicketDetail.RO_TS_ProblemRemark = vResultTicketDetailObj.RO_TS_ProblemRemark;
                        vManageTRCDetail_Response.TicketDetail.RO_TS_IPCurrentAC_A = vResultTicketDetailObj.RO_TS_IPCurrentAC_A;
                        vManageTRCDetail_Response.TicketDetail.RO_TS_OutputCurrentDC_A = vResultTicketDetailObj.RO_TS_OutputCurrentDC_A;
                        vManageTRCDetail_Response.TicketDetail.RO_TS_OutputVoltageDC_V = vResultTicketDetailObj.RO_TS_OutputVoltageDC_V;
                        vManageTRCDetail_Response.TicketDetail.RO_TS_Type = vResultTicketDetailObj.RO_TS_Type;
                        vManageTRCDetail_Response.TicketDetail.RO_TS_Heating = vResultTicketDetailObj.RO_TS_Heating;
                        vManageTRCDetail_Response.TicketDetail.RO_TS_DisplayPhotoFileName = vResultTicketDetailObj.RO_TS_DisplayPhotoFileName;
                        vManageTRCDetail_Response.TicketDetail.RO_TS_DisplayPhotoOriginalFileName = vResultTicketDetailObj.RO_TS_DisplayPhotoOriginalFileName;
                        vManageTRCDetail_Response.TicketDetail.RO_TS_DisplayPhotoURL = vResultTicketDetailObj.RO_TS_DisplayPhotoURL;
                        vManageTRCDetail_Response.TicketDetail.RO_TS_OutputVoltageAC_V = vResultTicketDetailObj.RO_TS_OutputVoltageAC_V;
                        vManageTRCDetail_Response.TicketDetail.RO_TS_OutputCurrentAC_A = vResultTicketDetailObj.RO_TS_OutputCurrentAC_A;
                        vManageTRCDetail_Response.TicketDetail.RO_TS_IPCurrentDC_A = vResultTicketDetailObj.RO_TS_IPCurrentDC_A;
                        vManageTRCDetail_Response.TicketDetail.RO_TS_SpecificGravityC2 = vResultTicketDetailObj.RO_TS_SpecificGravityC2;
                        vManageTRCDetail_Response.TicketDetail.RO_TS_SpecificGravityC3 = vResultTicketDetailObj.RO_TS_SpecificGravityC3;
                        vManageTRCDetail_Response.TicketDetail.RO_TS_SpecificGravityC4 = vResultTicketDetailObj.RO_TS_SpecificGravityC4;
                        vManageTRCDetail_Response.TicketDetail.RO_TS_SpecificGravityC5 = vResultTicketDetailObj.RO_TS_SpecificGravityC5;
                        vManageTRCDetail_Response.TicketDetail.RO_TS_SpecificGravityC6 = vResultTicketDetailObj.RO_TS_SpecificGravityC6;

                        vManageTRCDetail_Response.TicketDetail.RO_CP_Visual = vResultTicketDetailObj.RO_CP_Visual;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_VisualImageFileName = vResultTicketDetailObj.RO_CP_VisualImageFileName;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_VisualImageOriginalFileName = vResultTicketDetailObj.RO_CP_VisualImageOriginalFileName;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_VisualImageURL = vResultTicketDetailObj.RO_CP_VisualImageURL;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_TerminalVoltage = vResultTicketDetailObj.RO_CP_TerminalVoltage;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_CommunicationWithBattery = vResultTicketDetailObj.RO_CP_CommunicationWithBattery;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_TerminalWire = vResultTicketDetailObj.RO_CP_TerminalWire;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_TerminalWireImageFileName = vResultTicketDetailObj.RO_CP_TerminalWireImageFileName;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_TerminalWireImageOriginalFileName = vResultTicketDetailObj.RO_CP_TerminalWireImageOriginalFileName;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_TerminalWireImageURL = vResultTicketDetailObj.RO_CP_TerminalWireImageURL;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_LifeCycle = vResultTicketDetailObj.RO_CP_LifeCycle;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_StringVoltageVariation = vResultTicketDetailObj.RO_CP_StringVoltageVariation;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_BatteryParametersSetting = vResultTicketDetailObj.RO_CP_BatteryParametersSetting;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_BatteryParametersSettingImageFileName = vResultTicketDetailObj.RO_CP_BatteryParametersSettingImageFileName;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_BatteryParametersSettingImageOriginalFileName = vResultTicketDetailObj.RO_CP_BatteryParametersSettingImageOriginalFileName;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_BatteryParametersSettingImageURL = vResultTicketDetailObj.RO_CP_BatteryParametersSettingImageURL;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_Spare = vResultTicketDetailObj.RO_CP_Spare;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_BMSStatus = vResultTicketDetailObj.RO_CP_BMSStatus;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_BMSSoftwareImageFileName = vResultTicketDetailObj.RO_CP_BMSSoftwareImageFileName;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_BMSSoftwareImageOriginalFileName = vResultTicketDetailObj.RO_CP_BMSSoftwareImageOriginalFileName;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_BMSSoftwareImageURL = vResultTicketDetailObj.RO_CP_BMSSoftwareImageURL;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_BMSType = vResultTicketDetailObj.RO_CP_BMSType;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_BatteryTemp = vResultTicketDetailObj.RO_CP_BatteryTemp;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_BMSSerialNumber = vResultTicketDetailObj.RO_CP_BMSSerialNumber;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_ProblemObserved = vResultTicketDetailObj.RO_CP_ProblemObserved;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_ProblemObservedByEngId = vResultTicketDetailObj.RO_CP_ProblemObservedByEngId;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_ProblemObservedByEng = vResultTicketDetailObj.RO_CP_ProblemObservedByEng;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_IsWarrantyVoid = vResultTicketDetailObj.RO_CP_IsWarrantyVoid;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_ReasonForVoid = vResultTicketDetailObj.RO_CP_ReasonForVoid;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_CommunicationWithMotorAndController = vResultTicketDetailObj.RO_CP_CommunicationWithMotorAndController;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_UnderWarranty = vResultTicketDetailObj.RO_CP_UnderWarranty;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_RPhaseVoltage = vResultTicketDetailObj.RO_CP_RPhaseVoltage;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_YPhaseVoltage = vResultTicketDetailObj.RO_CP_YPhaseVoltage;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_BPhaseVoltage = vResultTicketDetailObj.RO_CP_BPhaseVoltage;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_BatteryOCV = vResultTicketDetailObj.RO_CP_BatteryOCV;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_SystemVoltage = vResultTicketDetailObj.RO_CP_SystemVoltage;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_SpecificGravityC1 = vResultTicketDetailObj.RO_CP_SpecificGravityC1;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_SpecificGravityC2 = vResultTicketDetailObj.RO_CP_SpecificGravityC2;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_SpecificGravityC3 = vResultTicketDetailObj.RO_CP_SpecificGravityC3;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_SpecificGravityC4 = vResultTicketDetailObj.RO_CP_SpecificGravityC4;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_SpecificGravityC5 = vResultTicketDetailObj.RO_CP_SpecificGravityC5;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_SpecificGravityC6 = vResultTicketDetailObj.RO_CP_SpecificGravityC6;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_BatteryVoltageDropOnLoad = vResultTicketDetailObj.RO_CP_BatteryVoltageDropOnLoad;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_KmReading = vResultTicketDetailObj.RO_CP_KmReading;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_TotalRun = vResultTicketDetailObj.RO_CP_TotalRun;
                        vManageTRCDetail_Response.TicketDetail.RO_CP_BatterySerialNo = vResultTicketDetailObj.RO_CP_BatterySerialNo;

                        vManageTRCDetail_Response.TicketDetail.RO_CC_BatteryRepairedOnSite = vResultTicketDetailObj.RO_CC_BatteryRepairedOnSite;
                        vManageTRCDetail_Response.TicketDetail.RO_CC_BatteryRepairedToPlant = vResultTicketDetailObj.RO_CC_BatteryRepairedToPlant;

                        vManageTRCDetail_Response.TicketDetail.RO_OV_IsCustomerAvailable = vResultTicketDetailObj.RO_OV_IsCustomerAvailable;
                        vManageTRCDetail_Response.TicketDetail.RO_OV_EngineerName = vResultTicketDetailObj.RO_OV_EngineerName;
                        vManageTRCDetail_Response.TicketDetail.RO_OV_EngineerNumber = vResultTicketDetailObj.RO_OV_EngineerNumber;
                        vManageTRCDetail_Response.TicketDetail.RO_OV_CustomerName = vResultTicketDetailObj.RO_OV_CustomerName;
                        vManageTRCDetail_Response.TicketDetail.RO_OV_CustomerNameSecondary = vResultTicketDetailObj.RO_OV_CustomerNameSecondary;
                        vManageTRCDetail_Response.TicketDetail.RO_OV_CustomerMobileNumber = vResultTicketDetailObj.RO_OV_CustomerMobileNumber;
                        vManageTRCDetail_Response.TicketDetail.RO_OV_RequestOTP = vResultTicketDetailObj.RO_OV_RequestOTP;
                        vManageTRCDetail_Response.TicketDetail.RO_OV_Signature = vResultTicketDetailObj.RO_OV_Signature;

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


                        var vResultTicketPartListObj = await _manageTicketRepository.GetManageTicketPartDetailById(Convert.ToInt32(vResultObj.TicketId));
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
                        var vTicketStatusLogListObj = await _manageTicketRepository.GetManageTicketStatusLogById(Convert.ToInt32(vResultObj.TicketId));
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
                }

                _response.Data = vManageTRCDetail_Response;
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ExportTRCData(ManageTRC_Search request)
        {
            _response.IsSuccess = false;
            byte[] result;
            int recordIndex;
            ExcelWorksheet WorkSheet1;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var lstObj = await _manageTRCRepository.GetManageTRCList(request);

            using (MemoryStream msExportDataFile = new MemoryStream())
            {
                using (ExcelPackage excelExportData = new ExcelPackage())
                {
                    WorkSheet1 = excelExportData.Workbook.Worksheets.Add("TRC");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "Ticket#";
                    WorkSheet1.Cells[1, 2].Value = "Ticket Date";
                    WorkSheet1.Cells[1, 3].Value = "Contact No";
                    WorkSheet1.Cells[1, 4].Value = "TRC#";
                    WorkSheet1.Cells[1, 5].Value = "TRC Date";
                    WorkSheet1.Cells[1, 6].Value = "Company Name";
                    WorkSheet1.Cells[1, 7].Value = "Contact Name";
                    WorkSheet1.Cells[1, 8].Value = "Mobile #";
                    WorkSheet1.Cells[1, 9].Value = "State";
                    WorkSheet1.Cells[1, 10].Value = "City";;

                    WorkSheet1.Cells[1, 11].Value = "Created By";
                    WorkSheet1.Cells[1, 12].Value = "Created Date";
                    WorkSheet1.Cells[1, 13].Value = "Modified By";
                    WorkSheet1.Cells[1, 14].Value = "Modified Date";
                    WorkSheet1.Cells[1, 15].Value = "Status";

                    recordIndex = 2;

                    foreach (var items in lstObj)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Value = items.TicketNumber;
                        WorkSheet1.Cells[recordIndex, 2].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 2].Value = items.TicketDate;
                        WorkSheet1.Cells[recordIndex, 3].Value = items.CD_CallerMobile;
                        WorkSheet1.Cells[recordIndex, 4].Value = items.TRCNumber;
                        WorkSheet1.Cells[recordIndex, 5].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 5].Value = items.TRCDate;
                        WorkSheet1.Cells[recordIndex, 6].Value = items.CD_SiteCustomerName;
                        WorkSheet1.Cells[recordIndex, 7].Value = items.CD_SiteContactName;
                        WorkSheet1.Cells[recordIndex, 8].Value = items.CD_SitContactMobile;
                        WorkSheet1.Cells[recordIndex, 9].Value = items.StateName;
                        WorkSheet1.Cells[recordIndex, 10].Value = items.CityName;
                       
                        WorkSheet1.Cells[recordIndex, 11].Value = items.CreatorName;
                        WorkSheet1.Cells[recordIndex, 12].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 12].Value = items.CreatedDate;

                        WorkSheet1.Cells[recordIndex, 13].Value = items.ModifierName;
                        WorkSheet1.Cells[recordIndex, 14].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 14].Value = items.ModifiedDate;
                        WorkSheet1.Cells[recordIndex, 15].Value = items.IsActive == true ? "Active" : "Inactive";

                        recordIndex += 1;
                    }

                    WorkSheet1.Columns.AutoFit();

                    excelExportData.SaveAs(msExportDataFile);
                    msExportDataFile.Position = 0;
                    result = msExportDataFile.ToArray();
                }
            }

            if (result != null)
            {
                _response.Data = result;
                _response.IsSuccess = true;
                _response.Message = "Exported successfully";
            }

            return _response;
        }

        #endregion
    }
}
