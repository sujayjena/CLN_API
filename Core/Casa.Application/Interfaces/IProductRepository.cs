using CLN.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<int> SaveProduct(Product_Request parameters);

        Task<IEnumerable<Product_Response>> GetProductList(Product_Search parameters);

        Task<Product_Response?> GetProductById(int Id);

        Task<IEnumerable<Product_Segment_SubSegment_ProductModel_Response>> GetProduct_Segment_SubSegment_ProductModel_List_ById(Product_Segment_SubSegment_ProductModel_Search parameters);

        Task<IEnumerable<ProductDataValidationErrors>> ImportProduct(List<ImportedProduct> parameters);
    }
}
