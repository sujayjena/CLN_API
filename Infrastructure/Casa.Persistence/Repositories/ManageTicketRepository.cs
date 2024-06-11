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
            queryParameters.Add("@TicketDate", parameters.TicketdDate);
            queryParameters.Add("@TicketTime", parameters.TicketdTime);
            queryParameters.Add("@TicketPriorityId", parameters.TicketPriorityId);
            queryParameters.Add("@TicketSLADays", parameters.TicketSLADays);

            queryParameters.Add("@CD_LoggingSourceId", parameters.CD_LoggingSourceId);
            queryParameters.Add("@CD_CallerTypeId", parameters.CD_CallerTypeId);
            queryParameters.Add("@CD_CallerName", parameters.CD_CallerName);
            queryParameters.Add("@CD_CallerMobile", parameters.CD_CallerMobile);
            queryParameters.Add("@CD_CallerEmailId", parameters.CD_CallerEmailId);
            queryParameters.Add("@CD_CallerAddressId", parameters.CD_CallerAddressId);
            queryParameters.Add("@CD_CallerRemarks", parameters.CD_CallerRemarks);

            queryParameters.Add("@CD_IsSiteAddressSameAsCaller", parameters.CD_IsSiteAddressSameAsCaller);
            queryParameters.Add("@CD_ComplaintType", parameters.CD_ComplaintTypeId);
            queryParameters.Add("@CD_BatterySerialNumber", parameters.CD_BatterySerialNumber);
            queryParameters.Add("@CD_CustomerTypeId", parameters.CD_CustomerTypeId);
            queryParameters.Add("@CD_CustomerName", parameters.CD_CustomerName);
            queryParameters.Add("@CD_CustomerMobile", parameters.CD_CustomerMobile);
            queryParameters.Add("@CD_CustomerEmail", parameters.CD_CustomerEmail);
            queryParameters.Add("@CD_CustomerAddressId", parameters.CD_CustomerAddressId);
            queryParameters.Add("@CD_SiteCustomerName", parameters.CD_SiteCustomerName);
            queryParameters.Add("@CD_SiteContactName", parameters.CD_SiteContactName);
            queryParameters.Add("@CD_SitContactMobile", parameters.CD_SitContactMobile);
            queryParameters.Add("@CD_SiteAddressId", parameters.CD_SiteAddressId);

            queryParameters.Add("@BD_BatteryPartCode", parameters.BD_BatteryPartCode);
            queryParameters.Add("@BD_BatterySegmentId", parameters.BD_BatterySegmentId);
            queryParameters.Add("@BD_BatterySubSegmentId", parameters.BD_BatterySubSegmentId);
            queryParameters.Add("@BD_BatterySpecificationId", parameters.BD_BatterySpecificationId);
            queryParameters.Add("@BD_BatteryCellChemistryId", parameters.BD_BatteryCellChemistryId);
            queryParameters.Add("@BD_DateofManufacturing", parameters.BD_DateofManufacturing);
            queryParameters.Add("@BD_ProbReportedByCustId", parameters.BD_ProbReportedByCustId);
            queryParameters.Add("@BD_WarrantyStartDate", parameters.BD_WarrantyStartDate);
            queryParameters.Add("@BD_WarrantyEndDate", parameters.BD_WarrantyEndDate);
            queryParameters.Add("@BD_WarrantyStatusId", parameters.BD_WarrantyStatusId);
            queryParameters.Add("@BD_TechnicalSupportEnggId", parameters.BD_TechnicalSupportEnggId);

            queryParameters.Add("@TS_Visual", parameters.TS_Visual);
            queryParameters.Add("@TS_BatterTerminalVoltage", parameters.TS_BatterTerminalVoltage);
            queryParameters.Add("@TS_LifeCycle", parameters.TS_LifeCycle);
            queryParameters.Add("@TS_StringVoltageVariation", parameters.TS_StringVoltageVariation);
            queryParameters.Add("@TS_BatteryTemperature", parameters.TS_BatteryTemperature);
            queryParameters.Add("@TS_CurrentDischargingValue", parameters.TS_CurrentDischargingValue);
            queryParameters.Add("@TS_ProtectionsId", parameters.TS_ProtectionsId);
            queryParameters.Add("@TS_CurrentChargingValue", parameters.TS_CurrentChargingValue);
            queryParameters.Add("@TS_AllocateToServiceEnggId", parameters.TS_AllocateToServiceEnggId);
            queryParameters.Add("@TS_TicketDate", parameters.TS_TicketDate);
            queryParameters.Add("@TS_TicketTime", parameters.TS_TicketTime);

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
            queryParameters.Add("@CP_BMSSoftwareImageFileName", parameters.CP_BMSSoftwareImageFileName);
            queryParameters.Add("@CP_BMSSoftwareImageOriginalFileName", parameters.CP_BMSSoftwareImageOriginalFileName);

            queryParameters.Add("@CP_Spare", parameters.CP_Spare);

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
            queryParameters.Add("@OV_IsMoveToTRC", parameters.OV_IsMoveToTRC);

            queryParameters.Add("@TicketStatusId", parameters.TicketdStatusId);
            queryParameters.Add("@EnquiryId", parameters.EnquiryId);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveTicket", queryParameters);
        }

        public async Task<int> SaveManageTicketPartDetail(ManageTicketPartDetails_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@TicketId", parameters.TicketId);
            queryParameters.Add("@SparePartNo", parameters.SparePartNo);
            queryParameters.Add("@PartDescription", parameters.PartDescription);
            queryParameters.Add("@Quantity", parameters.Quantity);
            queryParameters.Add("@PartStatusId", parameters.PartStatusId);

            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveTicketPartDetails", queryParameters);
        }

        public async Task<IEnumerable<ManageTicketList_Response>> GetManageTicketList(ManageTicket_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@EmployeeId", parameters.EmployeeId);
            queryParameters.Add("@TicketStatusId", parameters.TicketStatusId);
            queryParameters.Add("@FilterType", parameters.FilterType);
            queryParameters.Add("@IsActive", parameters.IsActive);
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
    }
}
