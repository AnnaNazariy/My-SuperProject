using NtierEF.DAL.Entities;
using NtierEF.DAL.Interfaces.Repositories;


public interface IProductRepository : IGenericRepository<Product>
{
    Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId);
    Task<Product> GetCompleteEntityAsync(int productId);
    Task<IEnumerable<Product>> GetAsync(); 
    Task InsertAsync(Product product); 
    Task UpdateAsync(Product product); 
    Task DeleteAsync(int productId);
    Task<IEnumerable<Product>> GetByProductNameAsync(string productName);
}
