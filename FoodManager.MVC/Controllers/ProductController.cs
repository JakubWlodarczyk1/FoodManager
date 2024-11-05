using AutoMapper;
using FoodManager.Application.Product.Commands.CreateProduct;
using FoodManager.Application.Product.Commands.DeleteProduct;
using FoodManager.Application.Product.Commands.EditProduct;
using FoodManager.Application.Product.Queries.GetProductById;
using FoodManager.Application.Product.Queries.GetUserProducts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodManager.MVC.Controllers
{
    [Authorize]
    public class ProductController(IMediator mediator, IMapper mapper) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var products = await mediator.Send(new GetUserProductsQuery());
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

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
            return RedirectToAction(nameof(Index));
        } 
        
        public async Task<IActionResult> Delete(int id)
        {
            await mediator.Send(new DeleteProductCommand(id));
            return RedirectToAction(nameof(Index));
        }
    }
}
