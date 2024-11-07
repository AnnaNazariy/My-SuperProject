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
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ShopDbContext databaseContext) : base(databaseContext) { }

        public async Task<Product> GetCompleteEntityAsync(int productId)
        {
            return await table
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == productId)
                ?? throw new EntityNotFoundException($"Product with ID {productId} not found.");
        }

        public async Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId)
        {
            return await table
                .Where(p => p.CategoryID == categoryId)
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByProductNameAsync(string productName)
        {
            return await table
                .Where(p => p.ProductName.Contains(productName))
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAsync()
        {
            return await table.ToListAsync();
        }

        public async Task InsertAsync(Product product)
        {
            await table.AddAsync(product);
        }

        public async Task UpdateAsync(Product product)
        {
            table.Update(product);
            await Task.CompletedTask; 
        }

        public async Task DeleteAsync(int productId)
        {
            var product = await GetCompleteEntityAsync(productId);
            table.Remove(product);
            await Task.CompletedTask; 
        }
    }
}
