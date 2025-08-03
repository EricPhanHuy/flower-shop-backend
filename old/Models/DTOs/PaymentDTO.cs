namespace FlowerShop_BackEnd.Models.DTOs
{
    public class PaymentDTO
    {
        public int OrderId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DateTime PaymentDate { get; set; }
        public string TransactionReference { get; set; } = string.Empty;
    }
}
