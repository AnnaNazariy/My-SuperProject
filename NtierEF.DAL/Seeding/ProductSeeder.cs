using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NtierEF.DAL.Interfaces;
using NtierEF.DAL.Entities;

namespace NtierEF.DAL.Seeding
{
    public class ProductSeeder : ISeeder<Product> 
    {
        private static readonly List<Product> products = new()
        {
            new Product
            {
                ProductId = 1,
                ProductName = "Diamond Ring",
                Description = "A beautiful diamond ring.",
                Price = 1500.00m,
                CategoryID = 1 
            },
            new Product
            {
                ProductId = 2,
                ProductName = "Gold Necklace",
                Description = "A stunning gold necklace.",
                Price = 1200.00m,
                CategoryID = 2 
            },
            
        };

        public void Seed(EntityTypeBuilder<Product> builder) 
        {
            builder.HasData(products);
        }
    }
}
