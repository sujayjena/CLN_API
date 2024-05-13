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
    public class PartRequestOrderRepository : GenericRepository, IPartRequestOrderRepository
    {
        private IConfiguration _configuration;

        public PartRequestOrderRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        #region Engg Part Request

        public async Task<int> SaveEnggPartRequest(EnggPartRequest_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@RequestNumber", parameters.RequestNumber);
            queryParameters.Add("@RequestDate", parameters.RequestDate);
            queryParameters.Add("@CompanyId", parameters.CompanyId);
            queryParameters.Add("@BranchId", parameters.BranchId);
            queryParameters.Add("@EngineerId", parameters.EngineerId);
            queryParameters.Add("@Remarks", parameters.Remarks);
            queryParameters.Add("@StatusId", parameters.StatusId);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveEnggPartRequest", queryParameters);
        }

        public async Task<IEnumerable<EnggPartRequest_Response>> GetEnggPartRequestList(EnggPartRequest_Search parameters)
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

            var result = await ListByStoredProcedure<EnggPartRequest_Response>("GetEnggPartRequestList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<EnggPartRequest_Response?> GetEnggPartRequestById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);

            return (await ListByStoredProcedure<EnggPartRequest_Response>("GetEnggPartRequestById", queryParameters)).FirstOrDefault();
        }



        public async Task<int> SaveEnggPartRequestDetail(EnggPartRequestDetails_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@RequestId", parameters.RequestId);
            queryParameters.Add("@CategoryId", parameters.CategoryId);
            queryParameters.Add("@SpareId", parameters.SpareId);
            queryParameters.Add("@UOM", parameters.UOM);
            queryParameters.Add("@TypeOfBMSId", parameters.TypeOfBMSId);
            queryParameters.Add("@AvailableQty", parameters.AvailableQty);
            queryParameters.Add("@RequiredQty", parameters.RequiredQty);
            queryParameters.Add("@Remarks", parameters.Remarks);
            queryParameters.Add("@RGP", parameters.RGP);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveEnggPartRequestDetails", queryParameters);
        }

        public async Task<IEnumerable<EnggPartRequestDetails_Response>> GetEnggPartRequestDetailList(EnggPartRequestDetails_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@RequestId", parameters.RequestId);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<EnggPartRequestDetails_Response>("GetEnggPartRequestDetailsList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<EnggPartRequestDetails_Response?> GetEnggPartRequestDetailById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);

            return (await ListByStoredProcedure<EnggPartRequestDetails_Response>("GetEnggPartRequestDetailsById", queryParameters)).FirstOrDefault();
        }

        #endregion


        #region TRC Part Request

        public async Task<int> SaveTRCPartRequest(TRCPartRequest_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@RequestNumber", parameters.RequestNumber);
            queryParameters.Add("@RequestDate", parameters.RequestDate);
            queryParameters.Add("@CompanyId", parameters.CompanyId);
            queryParameters.Add("@BranchId", parameters.BranchId);
            queryParameters.Add("@EngineerId", parameters.EngineerId);
            queryParameters.Add("@Remarks", parameters.Remarks);
            queryParameters.Add("@StatusId", parameters.StatusId);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveTRCPartRequest", queryParameters);
        }

        public async Task<IEnumerable<TRCPartRequest_Response>> GetTRCPartRequestList(TRCPartRequest_Search parameters)
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

            var result = await ListByStoredProcedure<TRCPartRequest_Response>("GetTRCPartRequestList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<TRCPartRequest_Response?> GetTRCPartRequestById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);

            return (await ListByStoredProcedure<TRCPartRequest_Response>("GetTRCPartRequestById", queryParameters)).FirstOrDefault();
        }



        public async Task<int> SaveTRCPartRequestDetail(TRCPartRequestDetails_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@RequestId", parameters.RequestId);
            queryParameters.Add("@CategoryId", parameters.CategoryId);
            queryParameters.Add("@SpareId", parameters.SpareId);
            queryParameters.Add("@UOM", parameters.UOM);
            queryParameters.Add("@TypeOfBMSId", parameters.TypeOfBMSId);
            queryParameters.Add("@AvailableQty", parameters.AvailableQty);
            queryParameters.Add("@RequiredQty", parameters.RequiredQty);
            queryParameters.Add("@Remarks", parameters.Remarks);
            queryParameters.Add("@RGP", parameters.RGP);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveTRCPartRequestDetails", queryParameters);
        }

        public async Task<IEnumerable<TRCPartRequestDetails_Response>> GetTRCPartRequestDetailList(TRCPartRequestDetails_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@RequestId", parameters.RequestId);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<TRCPartRequestDetails_Response>("GetTRCPartRequestDetailsList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<TRCPartRequestDetails_Response?> GetTRCPartRequestDetailById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);

            return (await ListByStoredProcedure<TRCPartRequestDetails_Response>("GetTRCPartRequestDetailsById", queryParameters)).FirstOrDefault();
        }

        #endregion
    }
}
