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
}
