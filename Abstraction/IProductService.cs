using ASP.Seminar3.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace ASP.Seminar3.Abstraction
{
    public interface IProductService
    { 
        public int AddProduct(ProductModel product);
        public IEnumerable<ProductModel> GetProducts();
        
    }
}
