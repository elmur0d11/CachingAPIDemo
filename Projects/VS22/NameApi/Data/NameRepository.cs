using Microsoft.EntityFrameworkCore;
using NameApi.Models;

namespace NameApi.Data
{
    public class NameRepository : INameRepository
    {
        private readonly AppDbContext _context;

        public NameRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateProduct(Name name)
        {
            if(name == null)
                throw new ArgumentNullException(nameof(name));

            await _context.Names.AddAsync(name);
        }

        public async Task<Name> Get(int id)
        {
            return await _context.Names.FirstOrDefaultAsync(name => name.Id == id);
        }

        public async Task<IEnumerable<Name>> GetAll()
        {
            return await _context.Names.ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}
