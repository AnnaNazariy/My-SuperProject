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
    public class CategoriesService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoriesService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryResponse>> GetAllAsync()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryResponse>>(categories);
        }

        public async Task<CategoryResponse> GetByIdAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            return _mapper.Map<CategoryResponse>(category);
        }

        public async Task<int> AddAsync(CategoryRequest request)
        {
            var category = _mapper.Map<Category>(request);
            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();
            return category.CategoryID;
        }

        public async Task UpdateAsync(int categoryId, CategoryRequest request)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(categoryId);
            if (category == null) throw new KeyNotFoundException("Category not found");

            _mapper.Map(request, category);
            _unitOfWork.Categories.Update(category);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category != null)
            {
                _unitOfWork.Categories.Delete(category);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
