using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowerShop_BackEnd.Models
{
    public enum PaymentMethod
    {
        CreditCard,
        BankTransfer,
        PayPal,
        CashOnDelivery
        // Add more if needed
    }

    public enum PaymentStatus
    {
        Pending,
        Completed,
        Failed,
        Refunded
        // Add more if needed
    }

    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public DateTime PaymentDate { get; set; }

        [MaxLength(100)]
        public string TransactionReference { get; set; }

        // Navigation Property
        public Order Order { get; set; } = null!;
    }
}
