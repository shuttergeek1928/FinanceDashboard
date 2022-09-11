using System.ComponentModel.DataAnnotations;

namespace FinanceDashboard.Core.Models.Subscription
{
    public class SubscriptionUpdateModel
    {
        public string? SubscribedOnEmail { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string? SubscribedOnMobileNumber { get; set; }

        public string? Password { get; set; }

        public DateTime? RenewalDate { get; set; }

        public int? RenewalCycle { get; set; }

        public decimal? Amount { get; set; }

        public decimal? RenewalAmount { get; set; }

        public DateTime? LastUpdate { get; set; }

        public int? LastUpdateBy { get; set; }
    }
}
