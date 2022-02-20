using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TestDev.Model;

namespace TestDev.Helpers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }      
    }

}
