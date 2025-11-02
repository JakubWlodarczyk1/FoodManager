using FoodManager.Application.Analytics.Queries.GetExpiringProductsCount;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodManager.MVC.Controllers
{
    [Authorize]
    public class AnalyticsController(IMediator mediator) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Analytics/ExpiringCount")]
        public async Task<IActionResult> ExpiringCount([FromQuery] GetExpiringProductsCountQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }
    }
}
