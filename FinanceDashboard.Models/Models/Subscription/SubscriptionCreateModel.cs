using System.ComponentModel.DataAnnotations;

namespace FinanceDashboard.Models.Subscription
{
    public class SubscriptionCreateModel
    {
        [Required]
        public int AccountId { get; set; }

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
    }
}
