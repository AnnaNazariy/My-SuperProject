/*using System.Threading.Tasks;
using NtierEF.DAL.Entities;
using Dapper_Example.DAL.Connection;

namespace NtierEF.DAL.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task<int> AddAsync(T entity);
        Task ReplaceAsync(T entity);
        Task DeleteAsync(int id);
    }
}
*/