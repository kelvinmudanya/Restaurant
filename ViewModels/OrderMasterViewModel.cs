using Restaurant.Domain;
using Restaurant.Domain.DTOs;

namespace Restaurant.ViewModels
{
    public class OrderMasterViewModel
    {
        public List<OrderDetailDto> OrderDetails { get; set; } 
        public string PaymentMethod { get; set; }
    }
}