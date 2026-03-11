using FoodManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodManager.Application.Product.Dtos
{
    public class PublicProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }

        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryTranslationKey { get; set; }

        public int Quantity { get; set; }
        public Unit Unit { get; set; }

        public decimal? Price { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
