using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NtierEF.DAL.Interfaces;
using NtierEF.DAL.Entities;

namespace NtierEF.DAL.Seeding
{
    public class CategorySeeder : ISeeder<Category> 
    {
        private static readonly List<Category> categories = new()
        {
            new Category
            {
                CategoryID = 1,
                CategoryName = "Jewelry" 
            },
            new Category
            {
                CategoryID = 2,
                CategoryName = "Rings"
            },
            new Category
            {
                CategoryID = 3,
                CategoryName = "Necklaces"
            },
            new Category
            {
                CategoryID = 4,
                CategoryName = "Bracelets"
            },
            new Category
            {
                CategoryID = 5,
                CategoryName = "Earrings"
            }
        };

        public void Seed(EntityTypeBuilder<Category> builder) 
        {
            builder.HasData(categories);
        }
    }
}
