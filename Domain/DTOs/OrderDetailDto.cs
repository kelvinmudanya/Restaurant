namespace Restaurant.Domain.DTOs
{
    public class OrderDetailDto
    {
        public int FoodItemId { get; set; }
        public decimal? FoodItemPrice { get; set; }
        public int Quantity { get; set; }
    }
}
