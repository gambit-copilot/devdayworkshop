using Microsoft.EntityFrameworkCore;
using TechItEasyBackend.Persistence.Models;

namespace TechItEasyBackend.Persistence.Context
{
    public class StoreDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        public StoreDbContext()
            : base()
        {
        }

        public StoreDbContext(DbContextOptions<StoreDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(GetConnectionString());
        }

        public static string GetConnectionString()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            if (baseDir.Contains("bin"))
            {
                int index = baseDir.IndexOf("bin");
                baseDir = baseDir[..index];
            }
            return $"Data Source={baseDir}\\SQLite.db";
        }
    }
}
