using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceDashboard.Data.SqlServer.Entities
{
    public class Asset
    {
        [Required]
        [Key]
        public Guid AssetId { get; set; }

        [ForeignKey("Fk_Account_UserAccountId")]
        [Required]
        public int AccountId { get; set; }

        public Account User { get; set; }

        [Required]
        public string AssetName { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string AssetType { get; set; }

        [Required]
        public decimal InvestedValue { get; set; }

        [Required]
        public decimal CurrentValue { get; set; }

        [Required]
        public decimal AbsoluteReturns { get; set; }

        [Required]
        public decimal ReturnPercentage { get; set; }

        public DateTime? DissolvedOn { get; set; }

        public int? DissolvedBy { get; set; }

        public string? DissolvedReason { get; set; }

        public DateTime? LastUpdate { get; set; }

        public int? LastUpdateBy { get; set; }
    }
}