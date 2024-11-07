using System.Collections.Generic;
using System.Threading.Tasks;
using NtierEF.BLL.DTO.Requests;
using NtierEF.BLL.DTO.Responses;

namespace NtierEF.BLL.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>> GetAllAsync();
        Task<ProductResponse> GetByIdAsync(int productId);
        Task<int> AddAsync(ProductRequest request); 
        Task UpdateAsync(int productId, ProductRequest request);
        Task DeleteAsync(int productId);
    }
}
