using CLEAN.API.Domain;
using Microsoft.EntityFrameworkCore;

namespace CLEAN.API.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ключі
            modelBuilder.Entity<Cart>().HasKey(c => c.CartID);
            modelBuilder.Entity<CartItem>().HasKey(ci => ci.CartItemID);

            // Зв'язок між Cart і CartItem
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartID);

            // Початкові дані для Cart
            var cart1Id = Guid.NewGuid();
            var cart2Id = Guid.NewGuid();

            modelBuilder.Entity<Cart>().HasData(
                new Cart(cart1Id, "Cart for John Doe"),
                new Cart(cart2Id, "Cart for Jane Doe")
            );

            // Початкові дані для CartItem
            modelBuilder.Entity<CartItem>().HasData(
                new CartItem(Guid.NewGuid(), "Product A", 2, 49.99m, cart1Id),
                new CartItem(Guid.NewGuid(), "Product B", 1, 99.99m, cart2Id)
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Використовуємо MySQL
            optionsBuilder.UseMySql(
                "Server=localhost;Database=shopdb;User=root;Password=root",
                ServerVersion.AutoDetect("Server=localhost;Database=shopdb;User=root;Password=root")
            );
        }
    }
}
