using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FinanceDashboard.Data.SqlServer.Entities;

namespace FinanceDashboard.Data.SqlServer.Entities
{
    public class Subscription
    {
        [Required]
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("UserAccountId")]
        public int AccountId { get; set; }

        public Account User { get; set; }

        [Required]
        public string SubscriptionName { get; set; }

        public string? SubscribedOnEmail { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string? SubscribedOnMobileNumber { get; set; }

        public string? Password { get; set; }

        [Required]
        public DateTime BillingDate { get; set; }

        [Required]
        public DateTime? RenewalDate { get; set; }

        [Required]
        public int RenewalCycle { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public decimal? RenewalAmount { get; set; }

        public DateTime? CanceledOn { get; set; }

        public int? CanceledBy { get; set; }

        public bool IsExpired { get; set; }

        public DateTime? LastUpdate { get; set; }

        public int? LastUpdateBy { get; set; }
    }
}
