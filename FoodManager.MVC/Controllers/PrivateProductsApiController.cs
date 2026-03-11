using FoodManager.Application.Product.Commands.CreateProduct;
using FoodManager.Application.Product.Commands.DeleteProduct;
using FoodManager.Application.Product.Commands.EditProduct;
using FoodManager.Application.Product.Queries.GetProductById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodManager.MVC.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/private/products")]
    public class PrivateProductsApiController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {
            await mediator.Send(command);
            return Ok(new { message = "Product created successfully." });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] EditProductCommand command)
        {
            var existingProduct = await mediator.Send(new GetProductByIdQuery(id));

            if (existingProduct is null)
                return NotFound();

            if (!existingProduct.IsEditable)
                return Forbid();

            command.Id = id;

            await mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingProduct = await mediator.Send(new GetProductByIdQuery(id));

            if (existingProduct is null)
                return NotFound();

            if (!existingProduct.IsEditable)
                return Forbid();

            await mediator.Send(new DeleteProductCommand(id));
            return NoContent();
        }
    }
}
