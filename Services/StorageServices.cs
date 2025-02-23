using ASP.Seminar3.Abstraction;
using ASP.Seminar3.Models;
using ASP.Seminar3.Models.DTO;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;

namespace ASP.Seminar3.Services
{
    public class StorageServices :IStorageServices
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly AppDbContext _context;

        public StorageServices(IMapper mapper, IMemoryCache memoryCache, AppDbContext context)
        {
            _mapper = mapper;
            _cache = memoryCache;
            _context = context;
        }

        public int AddStorage(StorageModel storage)
        {
            using (_context)
            {
                var entStorage = _context.Storages.FirstOrDefault(x => x.Name.ToLower() == storage.Name.ToLower());
                if (entStorage == null)
                {
                    entStorage = _mapper.Map<Storage>(storage);
                    _context.Storages.Add(entStorage);
                    _context.SaveChanges();

                    _cache.Remove("storages");
                }
                return entStorage.Id;
            }
        }

        public IEnumerable<StorageModel> GetStorages()
        {
            using (_context)
            {
                if (_cache.TryGetValue("storages", out List<StorageModel> storage))
                {
                    return storage;
                }
                var storages = _context.Storages.Select(x => _mapper.Map<StorageModel>(x)).ToList();
                _cache.Set("\"storages", storages, TimeSpan.FromMinutes(30));
                return storages;
            }
        }
    }
}
