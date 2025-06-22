using System.ComponentModel.DataAnnotations;

namespace FlowerShop_BackEnd.Models
{
    public class ProductType
    {
        [Key]
        public int TypeId { get; set; }
        [MaxLength(100)]
        public string TypeName { get; set; }
        public List<Product> Products { get; set; }
    }
    public class OrderItemDTO
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public float UnitPriceAtPurchase { get; set; }
    }
}
