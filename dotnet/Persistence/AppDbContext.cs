using NtierCLA.API.Domain; 
using Microsoft.EntityFrameworkCore;

namespace NtierCLA.API.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<Product>().HasData(
                new Product("Золотий кулон", "Красива золотий кулон для жінок.", 199.99m),
                new Product("Срібні серйги", "Срібні серйги з кристалами.", 89.99m),
                new Product("Браслет з перлами", "Елегантний браслет з натуральними перлами.", 149.99m)
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("shopdb"); 
        }
    }
}
