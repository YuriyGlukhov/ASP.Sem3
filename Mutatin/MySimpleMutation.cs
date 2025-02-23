using ASP.Seminar3.Abstraction;
using ASP.Seminar3.Models.DTO;

namespace ASP.Seminar3.Mutatin
{
    public class MySimpleMutation
    {
        public int AddProduct([Service] IProductService service, ProductModel product)
        {
            var id = service.AddProduct(product);
            return id;
        }

        public int AddCategory([Service] ICategoryService service, CategoryModel category)
        {
            var id = service.AddCategory(category);
            return id;
        }
        public int AddStorage([Service] IStorageServices service, StorageModel storage)
        {
            var id = service.AddStorage(storage);
            return id;
        }
    }
}
