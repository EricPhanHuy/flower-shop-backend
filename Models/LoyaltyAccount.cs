using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;


namespace FlowerShop_BackEnd.Models
{
    public class LoyaltyAccount
    {
        [Key]
        [ForeignKey("User")]
        public string UserId { get; set; } = default!;

        public ApplicationUser? User { get; set; }

        [Required]
        public int PointsBalance { get; set; } = 0;

    }
}