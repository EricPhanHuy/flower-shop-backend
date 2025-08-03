using System.ComponentModel.DataAnnotations;



namespace FlowerShop_BackEnd.Models
{
    public enum ProductStatus
    {
        Available,
        OutOfStock
    }

    public class Product
    {
        public int ID { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = "";
        [MaxLength(500)]
        public string Description { get; set; } = "";
        public int TypeId { get; set; }
        public ProductType ProductType { get; set; } = null!;
        public ProductStatus Status { get; set; }
        public float Price { get; set; }
        public int StockQuantity { get; set; }
        public ICollection<Occasion> Occasions { get; set; } = new List<Occasion>();
    }
}
