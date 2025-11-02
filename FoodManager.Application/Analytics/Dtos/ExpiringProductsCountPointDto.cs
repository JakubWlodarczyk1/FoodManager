using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodManager.Application.Analytics.Dtos
{
    public class ExpiringProductsCountPointDto
    {
        public DateOnly Date { get; init; }
        public int Count { get; init; }
    }
}
