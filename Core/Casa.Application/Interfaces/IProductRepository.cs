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
    }
}
