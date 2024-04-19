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
    public class AddressRepository : GenericRepository, IAddressRepository
    {
        private IConfiguration _configuration;

        public AddressRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> SaveAddress(Address_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@RefId", parameters.RefId);
            queryParameters.Add("@RefType", parameters.RefType);
            queryParameters.Add("@Address1", parameters.Address1);
            queryParameters.Add("@Address2", parameters.Address2);
            queryParameters.Add("@RegionId", parameters.RegionId);
            queryParameters.Add("@StateId", parameters.StateId);
            queryParameters.Add("@DistrictId", parameters.DistrictId);
            queryParameters.Add("@CityId", parameters.CityId);
            queryParameters.Add("@PinCode", parameters.PinCode);
            queryParameters.Add("@IsDefault", parameters.IsDefault);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveAddress", queryParameters);
        }

        public async Task<IEnumerable<Address_Response>> GetAddressList(Address_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@RefId", parameters.CustomerId);
            queryParameters.Add("@RefType", parameters.RefType);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@RefId", parameters.CustomerId);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<Address_Response>("GetAddressList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<Address_Response?> GetAddressById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", Id);

            return (await ListByStoredProcedure<Address_Response>("GetAddressById", queryParameters)).FirstOrDefault();
        }
    }
}
