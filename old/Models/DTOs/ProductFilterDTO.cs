namespace FlowerShop_BackEnd.Models.DTOs
{
    public class ProductFilterDTO
    {
        public string? Name { get; set; }
        public int? ProductTypeId { get; set; }
        public int? OccasionId { get; set; }
        public float? MinPrice { get; set; }
        public float? MaxPrice { get; set; }
        public int? Page { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
    }
} 