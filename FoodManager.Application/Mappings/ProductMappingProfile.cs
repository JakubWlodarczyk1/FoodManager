using AutoMapper;
using FoodManager.Application.ApplicationUser;
using FoodManager.Application.Product;
using FoodManager.Application.Product.Commands.EditProduct;

namespace FoodManager.Application.Mappings
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile(IUserContext userContext)
        {
            var user = userContext.GetCurrentUser();

            CreateMap<ProductDto, Domain.Entities.Product>();
            CreateMap<Domain.Entities.Product, ProductDto>()
                .ForMember(dto => dto.IsEditable, opt => opt.MapFrom(src => src.CreatedById == user.Id));

            CreateMap<ProductDto, EditProductCommand>();
        }
    }
}
