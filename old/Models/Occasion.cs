using System.ComponentModel.DataAnnotations;

namespace FlowerShop_BackEnd.Models
{
    public class Occasion
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = "";
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
