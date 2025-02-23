using ASP.Seminar3.Models.DTO;
using Microsoft.Extensions.Caching.Memory;

namespace ASP.Seminar3.Abstraction
{
    public interface ICategoryService
    {
        public int AddCategory(CategoryModel category);
        public IEnumerable<CategoryModel> GetCategories();
    }
}
