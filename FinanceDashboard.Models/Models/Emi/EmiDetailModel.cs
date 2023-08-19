using System.ComponentModel.DataAnnotations;

namespace FinanceDashboard.Models.Models.Emi
{
    public class EmiDetaiModel
    {
        public Guid Id { get; set; }

        public int AccountId { get; set; }

        public string EmiName { get; set; }

        public decimal Amount { get; set; }

        public decimal InterestRate { get; set; }

        public decimal GstRate { get; set; }

        public DateTime BillingDate { get; set; }

        public int InstallmentDate { get; set; }

        public DateTime CompletionDate { get; set; }

        public bool? IsCompleted { get; set; }

        public DateTime? CancelledOn { get; set; }

        public int? CancelledBy { get; set; }

        public DateTime? LastUpdated { get; set; }

        public int? LastUpdateBy { get; set; }
    }
}
