using System.ComponentModel.DataAnnotations;

namespace FlowerShop_BackEnd.Models
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;
    }
}