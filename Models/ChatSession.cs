using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

using System.Text.Json.Serialization;

namespace FlowerShop_BackEnd.Models
{
    public class ChatSession
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SessionId { get; set; }

        [Required]
        public string UserId { get; set; } = default!;

        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }

        [Required]
        public DateTime StartTime { get; set; }
        public DateTime? EndTime{ get; set; }
    }
}