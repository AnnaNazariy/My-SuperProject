using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using NtierEF.BLL.DTO.Requests;
using NtierEF.BLL.DTO.Responses;
using NtierEF.BLL.Interfaces;
using NtierEF.DAL.Entities;
using NtierEF.DAL.Interfaces;

namespace NtierEF.BLL.Services
{
    public class ProductsService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductResponse>> GetAllAsync()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductResponse>>(products);
        }

        public async Task<ProductResponse> GetByIdAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            return _mapper.Map<ProductResponse>(product);
        }

        public async Task<int> AddAsync(ProductRequest request)
        {
            var product = _mapper.Map<Product>(request);
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
            return product.ProductId;
        }

        public async Task UpdateAsync(int productId, ProductRequest request)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(productId);
            if (product == null) throw new KeyNotFoundException("Product not found");

            _mapper.Map(request, product);
            _unitOfWork.Products.Update(product);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product != null)
            {
                _unitOfWork.Products.Delete(product);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
