using FoodManager.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace FoodManager.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }

        private string _name = default!;
        public required string Name
        {
            get => _name;
            set => _name = value.ToLowerInvariant();
        }

        public CategoryType Type { get; set; }
        public string? TranslationKey { get; set; }
        public string? CreatedById { get; set; }
        public IdentityUser? CreatedBy { get; set; }

        public void EncodeName() => Name = Name.ToLower();
    }
}
