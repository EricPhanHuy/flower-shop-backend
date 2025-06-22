using System.ComponentModel.DataAnnotations.Schema;
namespace FlowerShop_BackEnd.Models
{



    public class OrderItem
    {
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public int Quantity { get; set; }

        
       
        public float UnitPriceAtPurchase { get; set; }
    }

}
