using Microsoft.EntityFrameworkCore;
using NameApi.Models;

namespace NameApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

        public DbSet<Name> Names { get; set; }
    }
}
