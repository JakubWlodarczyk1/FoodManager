using AutoMapper;
using FoodManager.Application.Product.Commands.CreateProduct;
using FoodManager.Application.Product.Commands.DeleteProduct;
using FoodManager.Application.Product.Commands.EditProduct;
using FoodManager.Application.Product.Queries.GetAllProducts;
using FoodManager.Application.Product.Queries.GetProductById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodManager.MVC.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _mediator.Send(new GetAllProductsQuery());
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

            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        [Route("Product/{id:int}/Edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var FoodDto = await _mediator.Send(new GetProductByIdQuery(id));
            var model = _mapper.Map<EditProductCommand>(FoodDto);

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

            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        } 
        
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteProductCommand(id));
            return RedirectToAction(nameof(Index));
        }
    }
}
