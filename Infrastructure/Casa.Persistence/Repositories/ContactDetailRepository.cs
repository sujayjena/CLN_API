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
    public class ContactDetailRepository : GenericRepository, IContactDetailRepository
    {
        private IConfiguration _configuration;

        public ContactDetailRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> SaveContactDetail(ContactDetail_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@RefId", parameters.RefId);
            queryParameters.Add("@RefType", parameters.RefType);
            queryParameters.Add("@ContactName", parameters.ContactName);
            queryParameters.Add("@MobileNumber", parameters.MobileNumber);
            queryParameters.Add("@EmailId", parameters.EmailId);
            queryParameters.Add("@AadharCardImage", parameters.AadharCardImageFileName);
            queryParameters.Add("@AadharCardOriginalFileName", parameters.AadharCardOriginalFileName);
            queryParameters.Add("@PanCardImage", parameters.PanCardImageFileName);
            queryParameters.Add("@PanCardOriginalFileName", parameters.PanCardOriginalFileName);
            queryParameters.Add("@IsDefault", parameters.IsDefault);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveContactDetail", queryParameters);
        }

        public async Task<IEnumerable<ContactDetail_Response>> GetContactDetailList(ContactDetail_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@RefId", parameters.CustomerId);
            queryParameters.Add("@RefType", parameters.RefType);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<ContactDetail_Response>("GetContactDetailList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<ContactDetail_Response?> GetContactDetailById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", Id);

            return (await ListByStoredProcedure<ContactDetail_Response>("GetContactDetailById", queryParameters)).FirstOrDefault();
        }
    }
}
