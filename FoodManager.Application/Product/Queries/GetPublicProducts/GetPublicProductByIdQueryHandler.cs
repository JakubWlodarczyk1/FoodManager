using FoodManager.Application.Product.Dtos;
using FoodManager.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodManager.Application.Product.Queries.GetPublicProducts
{
    public class GetPublicProductByIdQueryHandler(IProductRepository productRepository) : IRequestHandler<GetPublicProductByIdQuery, PublicProductDto?>
    {
        public async Task<PublicProductDto?> Handle(GetPublicProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetPublicById(request.Id);
            if (product is null)
                return null;

            return new PublicProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name,
                CategoryTranslationKey = product.Category?.TranslationKey,
                Quantity = product.Quantity,
                Unit = product.Unit,
                Price = product.Price,
                ExpirationDate = product.ExpirationDate
            };
        }
    }
}
