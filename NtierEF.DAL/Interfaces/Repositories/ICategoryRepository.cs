using NtierEF.DAL.Entities;
using NtierEF.DAL.Interfaces.Repositories;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<Category> GetCompleteEntityAsync(int categoryId);
    Task<IEnumerable<Category>> GetAsync(); 
    Task<IEnumerable<Category>> GetByCategoryNameAsync(string categoryName); 
    Task InsertAsync(Category category); 
    Task UpdateAsync(Category category); 
    Task DeleteAsync(int categoryId); 
}
