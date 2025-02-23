using ASP.Seminar3.Abstraction;
using ASP.Seminar3.Models;
using ASP.Seminar3.Models.DTO;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;

namespace ASP.Seminar3.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public CategoryService(AppDbContext context, IMapper mapper, IMemoryCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }
        public int AddCategory(CategoryModel category)
        {
            using (_context)
            {
                var entCategory = _context.Categories.FirstOrDefault(x => x.Name.ToLower() == category.Name.ToLower());
                if (entCategory == null)
                {
                    entCategory = _mapper.Map<Category>(category);
                    _context.Categories.Add(entCategory);
                    _context.SaveChanges();

                    _cache.Remove("categories");
                }
                return entCategory.Id;
            }
        }

        public IEnumerable<CategoryModel> GetCategories()
        {
            if (_cache.TryGetValue("categories", out List<CategoryModel> category))
            {
                return category;
            }

            using (_context)
            {
                var categories = _context.Categories.Select(x => _mapper.Map<CategoryModel>(x)).ToList();
                _cache.Set("categories", categories, TimeSpan.FromMinutes(30));
                return categories;
            }
        }
    }
}
