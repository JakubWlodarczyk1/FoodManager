using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodManager.Application.Analytics.Dtos
{
    public class ExpiringProductsCountDto
    {
        public string SeriesName { get; init; } = "Expiring products";
        public IReadOnlyList<ExpiringProductsCountPointDto> Points { get; init; } = Array.Empty<ExpiringProductsCountPointDto>();
        public int Total { get; init; }
    }
}
