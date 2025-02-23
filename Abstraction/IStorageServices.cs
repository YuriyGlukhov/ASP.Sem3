using ASP.Seminar3.Models.DTO;

namespace ASP.Seminar3.Abstraction
{
    public interface IStorageServices
    {
        public IEnumerable<StorageModel> GetStorages();
        public int AddStorage(StorageModel storage);
    }
}
