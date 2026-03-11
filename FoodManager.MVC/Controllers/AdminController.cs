using FoodManager.Domain.Constants;
using FoodManager.Infrastructure.Persistence;
using FoodManager.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodManager.MVC.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class AdminController(UserManager<IdentityUser> userManager, FoodManagerDbContext dbContext) : Controller
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly FoodManagerDbContext _dbContext = dbContext;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Users()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleLockout(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return NotFound();

            var isLocked = user.LockoutEnd.HasValue && user.LockoutEnd > DateTimeOffset.UtcNow;

            user.LockoutEnd = isLocked
                ? null
                : DateTimeOffset.UtcNow.AddYears(100);

            await _userManager.UpdateAsync(user);

            return RedirectToAction(nameof(Users));
        }

        public IActionResult Products()
        {
            var products = _dbContext.Products
                .Include(p => p.Category)
                .Include(p => p.CreatedBy)
                .OrderByDescending(p => p.ExpirationDate)
                .Select(p => new AdminProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    CategoryName = p.Category != null ? p.Category.Name : null,
                    CategoryTranslationKey = p.Category != null ? p.Category.TranslationKey : null,
                    Quantity = p.Quantity,
                    Unit = p.Unit,
                    Price = p.Price,
                    ExpirationDate = p.ExpirationDate,
                    CreatedByEmail = p.CreatedBy != null ? p.CreatedBy.Email : null
                })
                .ToList();

            return View(products);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);

            if (product is null)
                return NotFound();

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Products));
        }
    }
}
