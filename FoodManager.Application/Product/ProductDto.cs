using System.ComponentModel;
using FoodManager.Domain.Enums;

namespace FoodManager.Application.Product
{
    public class ProductDto
    {
        public int Id { get; set; }

        [DisplayName("Nazwa")]
        public required string Name { get; set; }

        [DisplayName("Opis")]
        public string? Description { get; set; }

        [DisplayName("Kategoria")]
        public string? Category { get; set; }

        [DisplayName("Ilość")]
        public int Quantity { get; set; }

        [DisplayName("Jednostka")]
        public Unit Unit { get; set; }

        [DisplayName("Cena")]
        public decimal Price { get; set; }

        [DisplayName("Data ważności")]
        public DateTime ExpirationDate { get; set; }
    }
}
