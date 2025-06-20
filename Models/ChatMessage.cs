using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

using System.Text.Json.Serialization;

namespace FlowerShop_BackEnd.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SenderTypes
    {
        User,
        Bot,
        Admin
    }

    public class ChatMessage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MessageId { get; set; }

        [Required]
        public int SessionId { get; set; }

        [ForeignKey("SessionId")]
        public ChatSession? Session { get; set; }

        [Required]
        public SenderTypes Sender { get; set; }

        [Required]
        public string MessageText { get; set; } = string.Empty;

        [Required]
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;

    }
}