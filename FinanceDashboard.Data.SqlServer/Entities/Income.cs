using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceDashboard.Data.SqlServer.Entities
{
    public class Income
    {
        [Required]
        [Key]
        public Guid IncomeId { get; set; }

        [ForeignKey("Fk_Account_UserAccountId")]
        [Required]
        public int AccountId { get; set; }

        public Account User { get; set; }

        [Required]
        public string IncomeName { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string CreditCycle { get; set; }

        [Required]
        public string Creditor { get; set; }

        [Required]
        public DateTime CredtiDate { get; set; }

        public DateTime? Expiry { get; set; }

        public int? ExpiredBy { get; set; }

        public string? ExpiringReason { get; set; }

        [Required]
        public decimal IncomeBalance { get; set; }
    }
}