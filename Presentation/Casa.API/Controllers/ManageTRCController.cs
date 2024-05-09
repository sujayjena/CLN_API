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
                    vManageTRCDetail_Response.Id = vResultObj.Id;
                    vManageTRCDetail_Response.TRCNumber = vResultObj.TRCNumber;
                    vManageTRCDetail_Response.TRCDate = vResultObj.TRCDate;
                    vManageTRCDetail_Response.TRCTime = vResultObj.TRCTime;

                    vManageTRCDetail_Response.TicketId = vResultObj.TicketId;
                    vManageTRCDetail_Response.TicketNumber = vResultObj.TicketNumber;
                    vManageTRCDetail_Response.TicketDate = vResultObj.TicketDate;
                    vManageTRCDetail_Response.TicketTime = vResultObj.TicketTime;
                    vManageTRCDetail_Response.TicketPriorityId = vResultObj.TicketPriorityId;
                    vManageTRCDetail_Response.TicketPriority = vResultObj.TicketPriority;
                    vManageTRCDetail_Response.TicketSLADays = vResultObj.TicketSLADays;

                    vManageTRCDetail_Response.CD_LoggingSourceId = vResultObj.CD_LoggingSourceId;
                    vManageTRCDetail_Response.CD_LoggingSourceChannel = vResultObj.CD_LoggingSourceChannel;
                    vManageTRCDetail_Response.CD_CallerName = vResultObj.CD_CallerName;
                    vManageTRCDetail_Response.CD_CallerMobile = vResultObj.CD_CallerMobile;
                    vManageTRCDetail_Response.CD_CallerEmailId = vResultObj.CD_CallerEmailId;

                    vManageTRCDetail_Response.CD_CallerAddressId = vResultObj.CD_CallerAddressId;
                    vManageTRCDetail_Response.CD_CallerAddress1 = vResultObj.CD_CallerAddress1;
                    vManageTRCDetail_Response.CD_CallerRegionId = vResultObj.CD_CallerRegionId;
                    vManageTRCDetail_Response.CD_CallerRegionName = vResultObj.CD_CallerRegionName;
                    vManageTRCDetail_Response.CD_CallerStateId = vResultObj.CD_CallerStateId;
                    vManageTRCDetail_Response.CD_CallerStateName = vResultObj.CD_CallerStateName;
                    vManageTRCDetail_Response.CD_CallerDistrictId = vResultObj.CD_CallerDistrictId;
                    vManageTRCDetail_Response.CD_CallerDistrictName = vResultObj.CD_CallerDistrictName;
                    vManageTRCDetail_Response.CD_CallerCityId = vResultObj.CD_CallerCityId;
                    vManageTRCDetail_Response.CD_CallerCityName = vResultObj.CD_CallerCityName;
                    vManageTRCDetail_Response.CD_CallerPinCode = vResultObj.CD_CallerPinCode;
                    vManageTRCDetail_Response.CD_CallerRemarks = vResultObj.CD_CallerRemarks;

                    vManageTRCDetail_Response.CD_IsSiteAddressSameAsCaller = vResultObj.CD_IsSiteAddressSameAsCaller;
                    vManageTRCDetail_Response.CD_BatterySerialNumber = vResultObj.CD_BatterySerialNumber;
                    vManageTRCDetail_Response.CD_CustomerTypeId = vResultObj.CD_CustomerTypeId;
                    vManageTRCDetail_Response.CD_CustomerType = vResultObj.CD_CustomerType;
                    vManageTRCDetail_Response.CD_CustomerMobile = vResultObj.CD_CustomerMobile;

                    vManageTRCDetail_Response.CD_CustomerAddressId = vResultObj.CD_CustomerAddressId;
                    vManageTRCDetail_Response.CD_CustomerAddress1 = vResultObj.CD_CustomerAddress1;
                    vManageTRCDetail_Response.CD_CustomerRegionId = vResultObj.CD_CustomerRegionId;
                    vManageTRCDetail_Response.CD_CustomerRegionName = vResultObj.CD_CustomerRegionName;
                    vManageTRCDetail_Response.CD_CustomerStateId = vResultObj.CD_CustomerStateId;
                    vManageTRCDetail_Response.CD_CustomerStateName = vResultObj.CD_CustomerStateName;
                    vManageTRCDetail_Response.CD_CustomerDistrictId = vResultObj.CD_CustomerDistrictId;
                    vManageTRCDetail_Response.CD_CustomerDistrictName = vResultObj.CD_CustomerDistrictName;
                    vManageTRCDetail_Response.CD_CustomerCityId = vResultObj.CD_CustomerCityId;
                    vManageTRCDetail_Response.CD_CustomerCityName = vResultObj.CD_CustomerCityName;
                    vManageTRCDetail_Response.CD_CustomerPinCode = vResultObj.CD_CustomerPinCode;

                    vManageTRCDetail_Response.CD_SiteCustomerName = vResultObj.CD_SiteCustomerName;
                    vManageTRCDetail_Response.CD_SiteContactName = vResultObj.CD_SiteContactName;
                    vManageTRCDetail_Response.CD_SitContactMobile = vResultObj.CD_SitContactMobile;

                    vManageTRCDetail_Response.CD_SiteAddressId = vResultObj.CD_SiteAddressId;
                    vManageTRCDetail_Response.CD_SiteCustomerAddress1 = vResultObj.CD_SiteCustomerAddress1;
                    vManageTRCDetail_Response.CD_SiteCustomerRegionId = vResultObj.CD_SiteCustomerRegionId;
                    vManageTRCDetail_Response.CD_SiteCustomerRegionName = vResultObj.CD_SiteCustomerRegionName;
                    vManageTRCDetail_Response.CD_SiteCustomerStateId = vResultObj.CD_SiteCustomerStateId;
                    vManageTRCDetail_Response.CD_SiteCustomerStateName = vResultObj.CD_SiteCustomerStateName;
                    vManageTRCDetail_Response.CD_SiteCustomerDistrictId = vResultObj.CD_SiteCustomerDistrictId;
                    vManageTRCDetail_Response.CD_SiteCustomerDistrictName = vResultObj.CD_SiteCustomerDistrictName;
                    vManageTRCDetail_Response.CD_SiteCustomerCityId = vResultObj.CD_SiteCustomerCityId;
                    vManageTRCDetail_Response.CD_SiteCustomerCityName = vResultObj.CD_SiteCustomerCityName;
                    vManageTRCDetail_Response.CD_SiteCustomerPinCode = vResultObj.CD_SiteCustomerPinCode;

                    vManageTRCDetail_Response.BD_BatteryPartCode = vResultObj.BD_BatteryPartCode;
                    vManageTRCDetail_Response.BD_BatterySegmentId = vResultObj.BD_BatterySegmentId;
                    vManageTRCDetail_Response.BD_Segment = vResultObj.BD_Segment;
                    vManageTRCDetail_Response.BD_BatterySubSegmentId = vResultObj.BD_BatterySubSegmentId;
                    vManageTRCDetail_Response.BD_SubSegment = vResultObj.BD_SubSegment;
                    vManageTRCDetail_Response.BD_BatterySpecificationId = vResultObj.BD_BatterySpecificationId;
                    vManageTRCDetail_Response.BD_BatterySpecification = vResultObj.BD_BatterySpecification;
                    vManageTRCDetail_Response.BD_BatteryCellChemistryId = vResultObj.BD_BatteryCellChemistryId;
                    vManageTRCDetail_Response.BD_CellChemistry = vResultObj.BD_CellChemistry;
                    vManageTRCDetail_Response.BD_DateofManufacturing = vResultObj.BD_DateofManufacturing;
                    vManageTRCDetail_Response.BD_ProbReportedByCustId = vResultObj.BD_ProbReportedByCustId;
                    vManageTRCDetail_Response.BD_ProbReportedByCust = vResultObj.BD_ProbReportedByCust;
                    vManageTRCDetail_Response.BD_WarrantyStartDate = vResultObj.BD_WarrantyStartDate;
                    vManageTRCDetail_Response.BD_WarrantyEndDate = vResultObj.BD_WarrantyEndDate;
                    vManageTRCDetail_Response.BD_WarrantyStatusId = vResultObj.BD_WarrantyStatusId;
                    vManageTRCDetail_Response.BD_WarrantyStatus = vResultObj.BD_WarrantyStatus;
                    vManageTRCDetail_Response.BD_TechnicalSupportEnggId = vResultObj.BD_TechnicalSupportEnggId;
                    vManageTRCDetail_Response.BD_TechnicalSupportEngg = vResultObj.BD_TechnicalSupportEngg;

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

                _response.Data = vManageTRCDetail_Response;
            }
            return _response;
        }

        #endregion
    }
}
