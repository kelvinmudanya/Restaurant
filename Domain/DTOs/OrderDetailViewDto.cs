using Restaurant.Domain.Context;

namespace Restaurant.Domain.DTOs
{
    public class OrderDetailViewDto
    {
        public FoodItemDto FoodItem { get; set; }
        public decimal FoodItemPrice { get; set; }
        public int Quantity { get; set; }
    }
}
