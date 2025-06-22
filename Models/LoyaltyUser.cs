using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowerShop_BackEnd.Models
{
    public class LoyaltyAccount
    {
        [Key]
        [ForeignKey("User")]
        public string UserId { get; set; } = null!;

        public int PointsBalance { get; set; } = 0;

        public IdentityUser User { get; set; } = null!;
    }
}
