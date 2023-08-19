using System.ComponentModel.DataAnnotations;

namespace FinanceDashboard.Models.Models.Emi
{
    public class EmiCreateModel
    {
        [Required]
        public string EmiName { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public decimal InterestRate { get; set; }

        [Required]
        public decimal GstRate { get; set; }

        [Required]
        public DateTime BillingDate { get; set; }

        [Required]
        public int InstallmentDate { get; set; }

        [Required]
        public DateTime CompletionDate { get; set; }

        public bool? IsCompleted { get; set; } = false;

        public DateTime? CancelledOn { get; set; } = null;

        public int? CancelledBy { get; set; } = null;

        public DateTime? LastUpdated { get; set; } = null;

        public int? LastUpdateBy { get; set; } = null;
    }
}
