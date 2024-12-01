using NameApi.Models;

namespace NameApi.Data
{
    public interface INameRepository
    {
        Task<IEnumerable<Name>> GetAll();

        Task<Name> Get(int id);

        Task CreateProduct(Name name);

        public Task<bool> SaveChangesAsync();
    }
}
