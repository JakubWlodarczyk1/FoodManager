using FoodManager.Application.ProductCategory.Commands.CreateProductCategory;
using FoodManager.Application.ProductCategory.Queries.GetUserProductCategories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodManager.MVC.Controllers
{
    [Authorize]
    public class ProductCategoriesController(IMediator mediator) : Controller
    {
        /// <summary>
        /// Creates a new product category.
        /// </summary>
        /// <param name="command">The command containing product category details.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProductCategory(CreateProductCategoryCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var data = await mediator.Send(command);

            return Ok(data);
        }

        /// <summary>
        /// Get all product categories associated with the current user.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetProductCategories()
        {
            var data = await mediator.Send(new GetUserProductCategoriesQuery());
            return Ok(data);
        }
    }
}
