using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceDashboard.Data.SqlServer.Entities
{
    public class SegmentLimits
    {
        [Required]
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Fk_Account_LimitAccountId")]
        [Required]
        public int AccountId { get; set; }
        public Account User { get; set; }

        [Required]
        public decimal SubscriptionLimit { get; set; }

        [Required]
        public decimal EmiLimit { get; set; }

    }
}
