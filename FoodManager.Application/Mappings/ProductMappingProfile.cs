using AutoMapper;
using FoodManager.Application.ApplicationUser;
using FoodManager.Application.Product.Commands.EditProduct;
using FoodManager.Application.Product.Dtos;
using FoodManager.Application.ProductCategory.Dtos;

namespace FoodManager.Application.Mappings
{
    public class ProductMappingProfile : Profile
    {
        /// <summary>
        /// Configures mappings between domain entities and DTOs.
        /// </summary>
        /// <param name="userContext">Current user context.</param>
        public ProductMappingProfile(IUserContext userContext)
        {
            var user = userContext.GetCurrentUser();

            CreateMap<ProductDto, Domain.Entities.Product>();
            CreateMap<Domain.Entities.Product, ProductDto>()
                .ForMember(dto => dto.IsEditable, opt => opt.MapFrom(src => src.CreatedById == user.Id))
                .ForMember(dto => dto.CategoryName, opt => opt.MapFrom(src => src.Category == null ? null : src.Category.Name))
                .ForMember(dto => dto.CategoryTranslationKey, opt => opt.MapFrom(src => src.Category == null ? null : src.Category.TranslationKey));

            CreateMap<ProductDto, EditProductCommand>();

            CreateMap<ProductCategoryDto, Domain.Entities.ProductCategory>()
                .ReverseMap();

            CreateMap<Domain.Entities.ProductCategory, ProductCategoryResultDto>();

            CreateMap<decimal, TotalProductsPriceDto>()
                .ForMember(dto => dto.TotalPrice, opt => opt.MapFrom(src => src));
        }
    }
}
