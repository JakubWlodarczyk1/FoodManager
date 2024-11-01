using AutoMapper;
using FoodManager.Application.Product;
using FoodManager.Application.Product.Commands.EditProduct;

namespace FoodManager.Application.Mappings
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<ProductDto, Domain.Entities.Product>();
            CreateMap<Domain.Entities.Product, ProductDto>();
            CreateMap<ProductDto, EditProductCommand>();
        }
    }
}
