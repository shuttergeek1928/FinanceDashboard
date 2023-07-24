using System.ComponentModel.DataAnnotations;

namespace FinanceDashboard.Models.Models.Income
{
    public class IncomeCreateModel
    {
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
