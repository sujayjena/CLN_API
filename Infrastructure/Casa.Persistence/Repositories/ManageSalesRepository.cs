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
    public class ManageSalesRepository : GenericRepository, IManageSalesRepository
    {
        private IConfiguration _configuration;

        public ManageSalesRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> SaveCustomerAccessory(ManageSales_Accessory_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@CustomerId", parameters.CustomerId);
            queryParameters.Add("@AccessoryId", parameters.AccessoryId);
            queryParameters.Add("@Quantity", parameters.Quantity);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveCustomerAccessory", queryParameters);
        }

        public async Task<IEnumerable<ManageSales_Response>> GetCustomerAccessoryList(BaseSearchEntity parameters)
        {
            throw new NotImplementedException();
        }

        public async Task<ManageSales_Response?> GetCustomerAccessoryById(int Id)
        {
            throw new NotImplementedException();
        }

    }
}
