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
    public class ManageTRCRepository : GenericRepository, IManageTRCRepository
    {
        private IConfiguration _configuration;

        public ManageTRCRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> SaveManageTRC(ManageTRC_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@TicketId", parameters.TicketId);
            queryParameters.Add("@TRCNumber", parameters.TRCNumber);
            queryParameters.Add("@TRCDate", parameters.TRCDate);
            queryParameters.Add("@TRCTime", parameters.TRCTime);

            queryParameters.Add("@RP_IsCLNOrCustomer", parameters.RP_IsCLNOrCustomer);
            queryParameters.Add("@RP_ProblemReportedByCustId", parameters.RP_ProblemReportedByCustId);
            queryParameters.Add("@RP_ProblemDecription", parameters.RP_ProblemDecription);
            queryParameters.Add("@RP_ProductPackingPhotoOriginalFileName1", parameters.RP_ProductPackingPhotoOriginalFileName1);
            queryParameters.Add("@RP_ProductPackingPhotoFileName1", parameters.RP_ProductPackingPhotoFileName1);
            queryParameters.Add("@RP_ProductPackingPhotoOriginalFileName2", parameters.RP_ProductPackingPhotoOriginalFileName2);
            queryParameters.Add("@RP_ProductPackingPhotoFileName2", parameters.RP_ProductPackingPhotoFileName2);
            queryParameters.Add("@RP_ProductPackingPhotoOriginalFileName3", parameters.RP_ProductPackingPhotoOriginalFileName3);
            queryParameters.Add("@RP_ProductPackingPhotoFileName3", parameters.RP_ProductPackingPhotoFileName3);
            queryParameters.Add("@RP_DeliveryChallanPhotoOriginalFileName", parameters.RP_DeliveryChallanPhotoOriginalFileName);
            queryParameters.Add("@RP_DeliveryChallanPhotoFileName", parameters.RP_DeliveryChallanPhotoFileName);
            queryParameters.Add("@RP_ReservePickupFormatOriginalFileName", parameters.RP_ReservePickupFormatOriginalFileName);
            queryParameters.Add("@RP_ReservePickupFormatFileName", parameters.RP_ReservePickupFormatFileName);
            queryParameters.Add("@RP_IsReservePickupMailToLogistic", parameters.RP_IsReservePickupMailToLogistic);
            queryParameters.Add("@RP_DocketDetails", parameters.RP_DocketDetails);
            queryParameters.Add("@RP_IsBatteryInTransit", parameters.RP_IsBatteryInTransit);
            queryParameters.Add("@DNV_DeliveryChallanPhotoOriginalFileName", parameters.DNV_DeliveryChallanPhotoOriginalFileName);
            queryParameters.Add("@DNV_DeliveryChallanPhotoFileName", parameters.DNV_DeliveryChallanPhotoFileName);
            queryParameters.Add("@DNV_DebitNote", parameters.DNV_DebitNote);
            queryParameters.Add("@DNV_IsHandoverToMainStore", parameters.DNV_IsHandoverToMainStore);
            queryParameters.Add("@DNV_DeliveryChallanNumber", parameters.DNV_DeliveryChallanNumber);
            queryParameters.Add("@DNV_IsBatteryReceivedInTRC", parameters.DNV_IsBatteryReceivedInTRC);

            queryParameters.Add("@ATE_AssignedToEngineerId", parameters.ATE_AssignedToEngineerId);

            queryParameters.Add("@II_Visual", parameters.II_Visual);
            queryParameters.Add("@II_IsIntact", parameters.II_IsIntact);
            queryParameters.Add("@II_IsTempered", parameters.II_IsTempered);
            queryParameters.Add("@II_TemperedOriginalFileName1", parameters.II_TemperedOriginalFileName1);
            queryParameters.Add("@II_TemperedFileName1", parameters.II_TemperedFileName1);
            queryParameters.Add("@II_TemperedOriginalFileName2", parameters.II_TemperedOriginalFileName2);
            queryParameters.Add("@II_TemperedFileName2", parameters.II_TemperedFileName2);
            queryParameters.Add("@II_TemperedOriginalFileName3", parameters.II_TemperedOriginalFileName3);
            queryParameters.Add("@II_TemperedFileName3", parameters.II_TemperedFileName3);
            queryParameters.Add("@II_PhysicallyDamageOriginalFileName1", parameters.II_PhysicallyDamageOriginalFileName1);
            queryParameters.Add("@II_PhysicallyDamageFileName1", parameters.II_PhysicallyDamageFileName1);
            queryParameters.Add("@II_PhysicallyDamageOriginalFileName2", parameters.II_PhysicallyDamageOriginalFileName2);
            queryParameters.Add("@II_PhysicallyDamageFileName2", parameters.II_PhysicallyDamageFileName2);
            queryParameters.Add("@II_PhysicallyDamageOriginalFileName3", parameters.II_PhysicallyDamageOriginalFileName3);
            queryParameters.Add("@II_PhysicallyDamageFileName3", parameters.II_PhysicallyDamageFileName3);
            queryParameters.Add("@II_StringVoltageVariation", parameters.II_StringVoltageVariation);
            queryParameters.Add("@II_BatteryTerminalVoltage", parameters.II_BatteryTerminalVoltage);
            queryParameters.Add("@II_LifeCycle", parameters.II_LifeCycle);
            queryParameters.Add("@II_BatteryTemperature", parameters.II_BatteryTemperature);
            queryParameters.Add("@II_BMSSpecification", parameters.II_BMSSpecification);
            queryParameters.Add("@II_BMSBrand", parameters.II_BMSBrand);
            queryParameters.Add("@II_CellSpecification", parameters.II_CellSpecification);
            queryParameters.Add("@II_CellBrand", parameters.II_CellBrand);
            queryParameters.Add("@II_BMSSerialNumber", parameters.II_BMSSerialNumber);
            queryParameters.Add("@II_CellChemistryId", parameters.II_CellChemistryId);

            queryParameters.Add("@WS_IsWarrantyStatus", parameters.WS_IsWarrantyStatus);
            queryParameters.Add("@WS_IsInformedToCustomerByEmail", parameters.WS_IsInformedToCustomerByEmail);
            queryParameters.Add("@WS_IsCustomerAcceptance", parameters.WS_IsCustomerAcceptance);
            queryParameters.Add("@WS_IsPaymentClearance", parameters.WS_IsPaymentClearance);
            queryParameters.Add("@WS_InvoiceOriginalFileName", parameters.WS_InvoiceOriginalFileName);
            queryParameters.Add("@WS_InvoiceFileName", parameters.WS_InvoiceFileName);

            queryParameters.Add("@DA_ProblemObservedByEngId", parameters.DA_ProblemObservedByEngId);
            queryParameters.Add("@DA_ProblemObservedDesc", parameters.DA_ProblemObservedDesc);
            queryParameters.Add("@DA_RectificationActionId", parameters.DA_RectificationActionId);
            queryParameters.Add("@DA_ResolutionSummary", parameters.DA_ResolutionSummary);

            queryParameters.Add("@ATEFP_AssignedToEngineerId", parameters.ATEFP_AssignedToEngineerId);

            queryParameters.Add("@PI_BatteryReceivedDate", parameters.PI_BatteryReceivedDate);
            queryParameters.Add("@PI_BatteryReceivedTime", parameters.PI_BatteryReceivedTime);
            queryParameters.Add("@PI_PDIDoneDate", parameters.PI_PDIDoneDate);
            queryParameters.Add("@PI_PDIDoneTime", parameters.PI_PDIDoneTime);
            queryParameters.Add("@PI_PDIDoneById", parameters.PI_PDIDoneById);
            queryParameters.Add("@PI_SOCPercentageOriginalFileName", parameters.PI_SOCPercentageOriginalFileName);
            queryParameters.Add("@PI_SOCPercentageFileName", parameters.PI_SOCPercentageFileName);
            queryParameters.Add("@PI_VoltageDifference", parameters.PI_VoltageDifference);
            queryParameters.Add("@PI_FinalVoltageOriginalFileName", parameters.PI_FinalVoltageOriginalFileName);
            queryParameters.Add("@PI_FinalVoltageFileName", parameters.PI_FinalVoltageFileName);
          
            queryParameters.Add("@PIDD_DispatchedDeliveryChallan", parameters.PIDD_DispatchedDeliveryChallan);
            queryParameters.Add("@PIDD_DispatchedDate", parameters.PIDD_DispatchedDate);
            queryParameters.Add("@PIDD_DispatchedCity", parameters.PIDD_DispatchedCity);
          
            queryParameters.Add("@DDB_DispatchedDoneBy", parameters.DDB_DispatchedDoneBy);
            queryParameters.Add("@DDB_DocketDetails", parameters.DDB_DocketDetails);
            queryParameters.Add("@DDB_CourierName", parameters.DDB_CourierName);
           
            queryParameters.Add("@CRD_CustomerReceivingDate", parameters.CRD_CustomerReceivingDate);

            queryParameters.Add("@TRCBranchId", parameters.TRCBranchId);

            queryParameters.Add("@TRCStatusId", parameters.TRCStatusId);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveTRC", queryParameters);
        }

        public async Task<int> SaveManageTRCPartDetail(ManageTRCPartDetails_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@TRCId", parameters.TRCId);
            queryParameters.Add("@SpareCategoryId", parameters.SpareCategoryId);
            queryParameters.Add("@SpareDetailsId", parameters.SpareDetailsId);
            queryParameters.Add("@Quantity", parameters.Quantity);
            queryParameters.Add("@AvailableQty", parameters.AvailableQty);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveTRCPartDetails", queryParameters);
        }

        public async Task<IEnumerable<ManageTRCList_Response>> GetManageTRCList(ManageTRC_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@TRCStatusId", parameters.TRCStatusId);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<ManageTRCList_Response>("GetTRCList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<ManageTRCDetail_Response?> GetManageTRCById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", Id);

            return (await ListByStoredProcedure<ManageTRCDetail_Response>("GetTRCById", queryParameters)).FirstOrDefault();
        }

        public async Task<IEnumerable<ManageTRCPartDetails_Response>> GetManageTRCPartDetailById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@TRCId", Id);

            var result = await ListByStoredProcedure<ManageTRCPartDetails_Response>("GetTRCPartDetailById", queryParameters);

            return result;
        }
    }
}
