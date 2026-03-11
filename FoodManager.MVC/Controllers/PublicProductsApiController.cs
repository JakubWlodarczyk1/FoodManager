using FoodManager.Application.Common;
using FoodManager.Application.Product.Dtos;
using FoodManager.Application.Product.Queries.GetPublicProducts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodManager.MVC.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/public/products")]
    public class PublicProductsApiController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PagedResult<PublicProductDto>>> GetProducts([FromQuery] GetPublicProductsQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PublicProductDto>> GetProductById(int id)
        {
            var result = await mediator.Send(new GetPublicProductByIdQuery(id));

            if (result is null)
                return NotFound();

            return Ok(result);
        }
    }
}
