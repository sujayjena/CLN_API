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
    public class BOMRepository : GenericRepository, IBOMRepository
    {
        private IConfiguration _configuration;

        public BOMRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> SaveBOM(BOM_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@PartCode", parameters.PartCode.SanitizeValue());
            queryParameters.Add("@ProductCategoryId", parameters.ProductCategoryId);
            queryParameters.Add("@SegmentId", parameters.SegmentId);
            queryParameters.Add("@SubSegmentId", parameters.SubSegmentId);
            queryParameters.Add("@ProductModelId", parameters.ProductModelId);
            queryParameters.Add("@DrawingNumber", parameters.DrawingNumber);
            queryParameters.Add("@Warranty", parameters.Warranty);
            queryParameters.Add("@PartImage", parameters.PartImage);
            queryParameters.Add("@PartImageOriginalFileName", parameters.PartImageOriginalFileName);
            queryParameters.Add("@Remarks", parameters.Remarks);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveBOM", queryParameters);
        }

        public async Task<IEnumerable<BOM_Response>> GetBOMList(BaseSearchEntity parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<BOM_Response>("GetBOMList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<BOM_Response?> GetBOMById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", Id);

            return (await ListByStoredProcedure<BOM_Response>("GetBOMById", queryParameters)).FirstOrDefault();
        }
    }
}
