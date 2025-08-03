using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public enum OrderStatus
{
    Pending,
    Processing,
    Shipped,
    Delivered,
    Cancelled
}

public enum PaymentMethod
{
    CashOnDelivery,
    CreditCard,
    BankTransfer,
    EWallet
}


namespace FlowerShop_BackEnd.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; } = null!;

        public DateTime OrderDate { get; set; }

        public OrderStatus Status { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        
        public float TotalAmount { get; set; }

        [MaxLength(255)]
        public string DeliveryAddress { get; set; } = string.Empty;
        public List<OrderItem> OrderItems { get; set; } = null!;
        public Payment Payment { get; set; } = null!;
    }
}
