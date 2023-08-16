using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceDashboard.Data.SqlServer.Entities
{
    public class EMI
    {
        [Required]
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("UserAccountId")]
        public int AccountId { get; set; }

        public Account User { get; set; }

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
        public DateTime? InstallmentDate { get; set; }

        [Required]
        public DateTime? CompletionDate { get; set; }

        public DateTime? CanceledOn { get; set; }

        public int? CanceledBy { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime? LastUpdate { get; set; }

        public int? LastUpdateBy { get; set; }
    }
}
