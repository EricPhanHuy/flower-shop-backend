using System.ComponentModel.DataAnnotations;



namespace FlowerShop_BackEnd.Models
{
    public enum ProductCondition
    {
        New,
        Used,
        Refurbished
    }

    public enum ProductStatus
    {
        Available,
        OutOfStock,
        Discontinued
    }

    public class Product
    {
        public int ProductID { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }

        public int TypeId { get; set; }
        public ProductType ProductType { get; set; }
        public ProductCondition Condition { get; set; }
        public ProductStatus Status { get; set; }
        public float BasePrice { get; set; }
        public int StockQuantity { get; set; }




    }
}
