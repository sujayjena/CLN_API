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
    public class EnggPartRequestOrderRepository : GenericRepository, IEnggPartRequestOrderRepository
    {
        private IConfiguration _configuration;

        public EnggPartRequestOrderRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> SaveEnggPartRequestOrder(EnggPartRequestOrder_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@OrderDate", parameters.OrderDate);
            queryParameters.Add("@EngineerId", parameters.EngineerId);
            queryParameters.Add("@Remarks", parameters.Remarks);
            queryParameters.Add("@CompanyId", parameters.CompanyId);
            queryParameters.Add("@BranchId", parameters.BranchId);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveEnggPartRequestOrder", queryParameters);
        }

        public async Task<IEnumerable<EnggPartRequestOrder_Response>> GetEnggPartRequestOrderList(EnggPartRequestOrderSearch_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@CompanyId", parameters.CompanyId);
            queryParameters.Add("@BranchId", parameters.BranchId);
            queryParameters.Add("@EngineerId", parameters.EngineerId);
            queryParameters.Add("@StatusId", parameters.StatusId);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<EnggPartRequestOrder_Response>("GetEnggPartRequestOrderList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<EnggPartRequestOrder_Response?> GetEnggPartRequestOrderById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);

            return (await ListByStoredProcedure<EnggPartRequestOrder_Response>("GetEnggPartRequestOrderById", queryParameters)).FirstOrDefault();
        }

        public async Task<int> SaveEnggPartRequestOrderDetails(EnggPartRequestOrderDetails_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@OrderId", parameters.OrderId);
            queryParameters.Add("@SpareDetailsId", parameters.SpareDetailsId);
            queryParameters.Add("@TypeOfBMSId", parameters.TypeOfBMSId);
            queryParameters.Add("@AvailableQty", parameters.AvailableQty);
            queryParameters.Add("@OrderQty", parameters.OrderQty);
            queryParameters.Add("@StatusId", parameters.StatusId);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveEnggPartRequestOrderDetails", queryParameters);
        }

        public async Task<IEnumerable<EnggPartRequestOrderDetails_Response>> GetEnggPartRequestOrderDetailsList(EnggPartRequestOrderDetailsSearch_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@OrderId", parameters.OrderId);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<EnggPartRequestOrderDetails_Response>("GetEnggPartRequestOrderDetailsList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<EnggPartRequestOrderDetails_Response?> GetEnggPartRequestOrderDetailsById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);

            return (await ListByStoredProcedure<EnggPartRequestOrderDetails_Response>("GetEnggPartRequestOrderDetailsById", queryParameters)).FirstOrDefault();
        }
    }
}
