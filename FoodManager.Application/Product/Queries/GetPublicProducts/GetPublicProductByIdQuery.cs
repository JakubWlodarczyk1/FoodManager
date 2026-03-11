using FoodManager.Application.Product.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodManager.Application.Product.Queries.GetPublicProducts
{
    public class GetPublicProductByIdQuery(int id) : IRequest<PublicProductDto?>
    {
        public int Id { get; set; } = id;
    }
}
