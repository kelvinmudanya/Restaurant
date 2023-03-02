using Restaurant.Common;

namespace Restaurant.Domain
{
    public class OrderMaster:BaseEntity
    {

        public string OrderNumber { get; set; }
        public Customer Customer { get; set; }
        public decimal GTotal { get; set; }
        public List<OrderDetail> OrderDetails { get; set; } = new();
        public string PaymentMethod { get; set; }
    }
}
