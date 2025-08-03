using FlowerShop_BackEnd.Models;

namespace FlowerShop_BackEnd.Models.DTOs
{
    public class OrderCreateDTO
    {
        public string UserId { get; set; } = null!;
        public string DeliveryAddress { get; set; } = string.Empty;
        public PaymentMethod PaymentMethod { get; set; }
    }
}
