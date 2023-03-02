using Restaurant.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Domain
{
    public class FoodItem:BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
