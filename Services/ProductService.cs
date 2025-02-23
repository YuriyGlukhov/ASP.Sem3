
using ASP.Seminar3.Abstraction;
using ASP.Seminar3.Models;
using ASP.Seminar3.Models.DTO;
using AutoMapper;
using HotChocolate.Utilities;
using Microsoft.Extensions.Caching.Memory;

namespace ASP.Seminar3.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public ProductService(AppDbContext context, IMapper mapper, IMemoryCache cache)
        {
            _context =  context;
            _mapper = mapper;
            _cache = cache;
        }

        public int AddProduct(ProductModel product)
        {
            using (_context)
            {
                var entProduct = _context.Products.FirstOrDefault(x => x.Name.ToLower() == product.Name.ToLower());
                if (entProduct == null)
                {
                    entProduct = _mapper.Map<Product>(product);
                    _context.Products.Add(entProduct);
                    _context.SaveChanges();

                    if (entProduct.StorageId != null)
                    {
                        var productStorage = new ProductStorage
                        {
                            ProductId = entProduct.Id,
                            StorageId = entProduct.StorageId
                        };
                        _context.ProductStorages.Add(productStorage);
                        _context.SaveChanges();
                    }
                    _cache.Remove("products");
                }
                return entProduct.Id;
            }    
        }

        public IEnumerable<ProductModel> GetProducts()
        {
            using (_context)
            {
                if (_cache.TryGetValue("products", out List<ProductModel> product))
                {
                    return product;
                }
                var products = _context.Products.Select(x => _mapper.Map<ProductModel>(x)).ToList();
                _cache.Set("\"products", products, TimeSpan.FromMinutes(30));
                return products;
            }
        }
    }
}
