using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FlowerShop_BackEnd.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RuleTypes
    {
        TimeBased,
        QuantityBased,
        CustomerSegment
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AdjustmentTypes
    {
        Percentage,
        FixedAmount
    }

    public class PricingRule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RuleId { get; set; }

        [Required]
        public RuleTypes RuleType { get; set; }

        [Required]
        public string ConditionValue { get; set; } = string.Empty;

        [Required]
        public AdjustmentTypes AdjustmentType { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal AdjustmentValue { get; set; }

        [Required]
        public int Priority { get; set; }
    }
}