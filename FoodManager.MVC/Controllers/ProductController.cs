using AutoMapper;
using FoodManager.Application.Product.Commands.CreateProduct;
using FoodManager.Application.Product.Commands.DeleteProduct;
using FoodManager.Application.Product.Commands.EditProduct;
using FoodManager.Application.Product.Queries.GetProductById;
using FoodManager.Application.Product.Queries.GetUserProducts;
using FoodManager.Application.Product.Queries.GetUserTotalProductsPrice;
using FoodManager.Application.Resources.Localizations;
using FoodManager.MVC.Extensions;
using FoodManager.MVC.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodManager.MVC.Controllers
{
    [Authorize]
    public class ProductController(IMediator mediator, IMapper mapper) : Controller
    {
        /// <summary>
        /// Displays the main product management view with a list of products associated with the current user.
        /// </summary>
        public async Task<IActionResult> Index([FromQuery] GetUserProductsQuery query)
        {
            var products = await mediator.Send(query);
            return View(products);
        }

        /// <summary>
        /// Displays the create product view.
        /// </summary>
        public IActionResult Create()
        {
            var defaultModel = new CreateProductCommand()
            {
                Quantity = 1,
                ExpirationDate = DateTime.Now
            };

            return View(defaultModel);
        }

        /// <summary>
        /// Create a new product.
        /// </summary>
        /// <param name="command">The command containing product details.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await mediator.Send(command);
            this.SetNotification(NotificationType.Success, string.Format(Lang.AddedProductNotification, command.Name));

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Displays the edit view for a specific product.
        /// </summary>
        /// <param name="id">The ID of the product to edit.</param>
        [Route("Product/{id:int}/Edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var productDto = await mediator.Send(new GetProductByIdQuery(id));

            if (!productDto.IsEditable)
            {
                return RedirectToAction("NoAccess", "Home");
            }

            var model = mapper.Map<EditProductCommand>(productDto);

            return View(model);
        }

        /// <summary>
        /// Edit an existing product.
        /// </summary>
        /// <param name="id">The ID of the product to be edited.</param>
        /// <param name="command">The command containing updated product details.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Product/{id:int}/Edit")]
        public async Task<IActionResult> Edit(int id, EditProductCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await mediator.Send(command);
            this.SetNotification(NotificationType.Success, string.Format(Lang.ProductEditedNotification, command.Name));

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Deletes a specified product.
        /// </summary>
        /// <param name="id">The ID of the product to be deleted.</param>
        public async Task<IActionResult> Delete(int id)
        {
            await mediator.Send(new DeleteProductCommand(id));
            this.SetNotification(NotificationType.Success, Lang.DeleteProductSuccessNotification);

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Get the total price of all products associated with the current user.
        /// </summary>
        /// <returns>The total price of the user's products.</returns>
        [HttpGet]
        [Route("Products/TotalPrice")]
        public async Task<IActionResult> GetUserTotalProductsPrice()
        {
            var data = await mediator.Send(new GetUserTotalProductsPriceQuery());
            return Ok(data);
        }
    }
}
