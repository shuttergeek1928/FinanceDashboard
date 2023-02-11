using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace FinanceDashboard.Models.Models.SegmentLimits
{
    public class SegmentLimitsListModel
    {
        public Guid Id { get; set; }
        public int AccountId { get; set; }
        public decimal SubscriptionLimit { get; set; }
        public decimal EmiLimit { get; set; }
    }
}
