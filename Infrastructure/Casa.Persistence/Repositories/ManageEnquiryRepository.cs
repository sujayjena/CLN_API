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
    public class ManageEnquiryRepository : GenericRepository, IManageEnquiryRepository
    {
        private IConfiguration _configuration;

        public ManageEnquiryRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> SaveManageEnquiry(ManageEnquiry_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@EnquiryNumber", parameters.EnquiryNumber);
            queryParameters.Add("@EnquiryDate", parameters.EnquiryDate);
            queryParameters.Add("@EnquiryTime", parameters.EnquiryTime);
            queryParameters.Add("@CD_LoggingSourceId", parameters.CD_LoggingSourceId);
            queryParameters.Add("@CD_CallerTypeId", parameters.CD_CallerTypeId);
            queryParameters.Add("@CD_CallerName", parameters.CD_CallerName);
            queryParameters.Add("@CD_CallerMobile", parameters.CD_CallerMobile);
            queryParameters.Add("@CD_CallerEmailId", parameters.CD_CallerEmailId);
            queryParameters.Add("@CD_CallerAddressId", parameters.CD_CallerAddressId);
            queryParameters.Add("@CD_CallerRemarks", parameters.CD_CallerRemarks);
            queryParameters.Add("@CD_IsSiteAddressSameAsCaller", parameters.CD_IsSiteAddressSameAsCaller);
            queryParameters.Add("@CD_BatterySerialNumber", parameters.CD_BatterySerialNumber);
            queryParameters.Add("@CD_CustomerTypeId", parameters.CD_CustomerTypeId);
            queryParameters.Add("@CD_CustomerNameId", parameters.CD_CustomerNameId);
            queryParameters.Add("@CD_CustomerMobile", parameters.CD_CustomerMobile);
            queryParameters.Add("@CD_CustomerAddressId", parameters.CD_CustomerAddressId);
            queryParameters.Add("@CD_SiteCustomerName", parameters.CD_SiteCustomerName);
            queryParameters.Add("@CD_SiteContactName", parameters.CD_SiteContactName);
            queryParameters.Add("@CD_SitContactMobile", parameters.CD_SitContactMobile);
            queryParameters.Add("@CD_SiteAddressId", parameters.CD_SiteAddressId);

            queryParameters.Add("@BD_BatteryPartCode", parameters.BD_BatteryBOMNumber);
            queryParameters.Add("@BD_BatteryProductCategoryId", parameters.BD_BatteryProductCategoryId);
            queryParameters.Add("@BD_BatterySegmentId", parameters.BD_BatterySegmentId);
            queryParameters.Add("@BD_BatterySubSegmentId", parameters.BD_BatterySubSegmentId);
            queryParameters.Add("@BD_BatteryProductModelId", parameters.BD_BatteryProductModelId);
            queryParameters.Add("@BD_BatteryCellChemistryId", parameters.BD_BatteryCellChemistryId);
            queryParameters.Add("@BD_DateofManufacturing", parameters.BD_DateofManufacturing);
            queryParameters.Add("@BD_ProbReportedByCustId", parameters.BD_ProbReportedByCustId);
            queryParameters.Add("@BD_WarrantyStartDate", parameters.BD_WarrantyStartDate);
            queryParameters.Add("@BD_WarrantyEndDate", parameters.BD_WarrantyEndDate);
            queryParameters.Add("@BD_WarrantyStatusId", parameters.BD_WarrantyStatusId);
            queryParameters.Add("@BD_TechnicalSupportEnggId", parameters.BD_TechnicalSupportEnggId);
            queryParameters.Add("@IsConvertToTicket", parameters.IsConvertToTicket);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveEnquiry", queryParameters);
        }

        public async Task<IEnumerable<ManageEnquiry_Response>> GetManageEnquiryList(ManageEnquiry_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<ManageEnquiry_Response>("GetEnquiryList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<ManageEnquiry_Response?> GetManageEnquiryById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", Id);

            return (await ListByStoredProcedure<ManageEnquiry_Response>("GetEnquiryById", queryParameters)).FirstOrDefault();
        }
    }
}
