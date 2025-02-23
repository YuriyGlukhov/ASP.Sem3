using ASP.Seminar3.Abstraction;
using ASP.Seminar3.Models;
using ASP.Seminar3.Models.DTO;

namespace ASP.Seminar3.Query
{
    public class MySimpleQuery
    {
        public IEnumerable<ProductModel> GetProducts([Service] IProductService service) => service.GetProducts();

        public IEnumerable<StorageModel> GetStorage([Service] IStorageServices service) => service.GetStorages();

        public IEnumerable<CategoryModel> GetCategories([Service] ICategoryService service) => service.GetCategories();

        public IQueryable<ProductModel> ProductsByStorage([Service] AppDbContext context, int storageId)
        {
            return context.Products
                     .Where(p => p.ProductStorages.Count(ps => ps.StorageId == storageId) > 0)
                     .Select(p => new ProductModel
                     {
                         Id = p.Id,
                         Name = p.Name,
                         Description = p.Description
                     });
        }
    }
}
