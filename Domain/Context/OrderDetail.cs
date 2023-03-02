using Restaurant.Common;

namespace Restaurant.Domain.Context
{
    public class OrderDetail : BaseEntity
    {
        public OrderMaster OrderMaster { get; set; }
        public FoodItem FoodItem { get; set; }
        public decimal FoodItemPrice { get; set; }
        public int Quantity { get; set; }
    }
}
