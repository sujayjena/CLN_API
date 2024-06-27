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
    public class ProductRepository : GenericRepository, IProductRepository
    {
        private IConfiguration _configuration;

        public ProductRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> SaveProduct(Product_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@ProductCategoryId", parameters.ProductCategoryId);
            queryParameters.Add("@SegmentId", parameters.SegmentId);
            queryParameters.Add("@SubSegmentId", parameters.SubSegmentId);
            queryParameters.Add("@ProductModelId", parameters.ProductModelId);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveProduct", queryParameters);
        }

        public async Task<IEnumerable<Product_Response>> GetProductList(Product_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<Product_Response>("GetProductList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<Product_Response?> GetProductById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", Id);

            return (await ListByStoredProcedure<Product_Response>("GetProductById", queryParameters)).FirstOrDefault();
        }

        public async Task<IEnumerable<Product_Segment_SubSegment_ProductModel_Response>> GetProduct_Segment_SubSegment_ProductModel_List_ById(Product_Segment_SubSegment_ProductModel_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@ProductCategoryId", parameters.ProductCategoryId);
            queryParameters.Add("@SegmentId", parameters.SegmentId);
            queryParameters.Add("@SubSegmentId", parameters.SubSegmentId);
            queryParameters.Add("@ProductModelId", parameters.ProductModelId);

            var result = await ListByStoredProcedure<Product_Segment_SubSegment_ProductModel_Response>("GetProduct_Segment_SubSegment_ProductModel_List_ById", queryParameters);

            return result;
        }
    }
}
