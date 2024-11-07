using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NtierEF.DAL.Entities;
using NtierEF.DAL.Interfaces.Repositories;
using NtierEF.DAL.Exceptions;
using NtierEF.DAL.Data.Repositories;

namespace NtierEF.DAL.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ShopDbContext databaseContext) : base(databaseContext) { }

        public async Task<Category> GetCompleteEntityAsync(int categoryId)
        {
            return await table
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.CategoryID == categoryId)
                ?? throw new EntityNotFoundException($"Category with ID {categoryId} not found.");
        }

        public async Task<IEnumerable<Category>> GetByCategoryNameAsync(string categoryName)
        {
            return await table
                .Where(c => c.CategoryName.Contains(categoryName))
                .Include(c => c.Products)
                .ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetAsync()
        {
            return await table.ToListAsync();
        }

        public async Task InsertAsync(Category category)
        {
            await table.AddAsync(category);
        }

        public async Task UpdateAsync(Category category)
        {
            table.Update(category);
            await Task.CompletedTask; 
        }

        public async Task DeleteAsync(int categoryId)
        {
            var category = await GetCompleteEntityAsync(categoryId);
            table.Remove(category);
            await Task.CompletedTask; 
        }
    }
}
