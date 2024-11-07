using FoodManager.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace FoodManager.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CategoryType Type { get; set; }

        public string? CreatedById { get; set; }
        public IdentityUser? CreatedBy { get; set; }
    }
}
