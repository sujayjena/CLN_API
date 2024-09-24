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

            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@EmployeeId", parameters.EmployeeId);
            queryParameters.Add("@TicketStatusId", parameters.TicketStatusId);
            queryParameters.Add("@Filter_TicketStatusId", parameters.Filter_TicketStatusId);
            queryParameters.Add("@IsChangeStatus_LogTicket", parameters.IsChangeStatus_LogTicket);
            queryParameters.Add("@IsEngineerType", parameters.IsEngineerType);
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
    }
}
