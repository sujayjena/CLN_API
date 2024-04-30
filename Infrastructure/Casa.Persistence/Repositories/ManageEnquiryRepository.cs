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
            queryParameters.Add("@CallerName", parameters.CallerName);
            queryParameters.Add("@CallerMobile", parameters.CallerMobile);
            queryParameters.Add("@CallerEmailId", parameters.CallerEmailId);
            queryParameters.Add("@BatterySerialNumber", parameters.BatterySerialNumber);
            queryParameters.Add("@AddressId", parameters.AddressId);
            queryParameters.Add("@StatusId", parameters.StatusId);
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
