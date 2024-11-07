using System.Collections.Generic;
using System.Threading.Tasks;
using NtierEF.BLL.DTO.Requests;
using NtierEF.BLL.DTO.Responses;

namespace NtierEF.BLL.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponse>> GetAllAsync();
        Task<CategoryResponse> GetByIdAsync(int categoryId);
        Task<int> AddAsync(CategoryRequest request); 
        Task UpdateAsync(int categoryId, CategoryRequest request);
        Task DeleteAsync(int categoryId);
    }
}
