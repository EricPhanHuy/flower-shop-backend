namespace FlowerShop_BackEnd.Models
{
    public class CartItem
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        // Navigation properties
        public Cart Cart { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
