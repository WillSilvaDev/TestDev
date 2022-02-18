using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TestDev.Model;

namespace TestDev.Helpers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }


        //public DbSet<Course> Courses { get; set; }

        //public static class MemoryCacheHelper
        //{
        //    public static TEntity CreateMemoryCache<TEntity>
        //        (IMemoryCache memoryCache, string ulrClient, TEntity result) => memoryCache.GetOrCreate(ulrClient, e =>
        //        {
        //            e.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
        //            return result;
        //        });
        //}


    }
}
