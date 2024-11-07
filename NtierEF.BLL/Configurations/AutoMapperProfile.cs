using NtierEF.BLL.DTO.Requests;
using NtierEF.BLL.DTO.Responses;
using NtierEF.DAL.Entities;
using AutoMapper;

namespace NtierEF.BLL.Configurations
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateCategoryMaps();
            CreateProductMaps();
        }  

        private void CreateCategoryMaps()
        {
            CreateMap<CategoryRequest, Category>()
                .ForMember(dest => dest.CategoryID, opt => opt.Ignore()) // Ігноруємо CategoryID при створенні
                .ForMember(dest => dest.Products, opt => opt.Ignore()); // Ігноруємо навігаційне поле для продуктів

            CreateMap<Category, CategoryResponse>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Products)); // Мапінг для колекції продуктів у категорії
        }

        private void CreateProductMaps()
        {
            CreateMap<ProductRequest, Product>()
                .ForMember(dest => dest.ProductId, opt => opt.Ignore()) // Ігноруємо ProductId при створенні
                .ForMember(dest => dest.Category, opt => opt.Ignore()); // Ігноруємо навігаційне поле для категорії

            CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName)); // Мапінг для назви категорії
        }
    }
}
