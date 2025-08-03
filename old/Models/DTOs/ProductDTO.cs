namespace FlowerShop_BackEnd.Models.DTOs
{
    public class ProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int TypeID { get; set; }
        public ProductStatus Status { get; set; }
        public float BasePrice { get; set; }
        public int StockQuantity { get; set; }

    }
}
