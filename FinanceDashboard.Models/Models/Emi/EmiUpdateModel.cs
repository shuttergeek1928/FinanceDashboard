using System.ComponentModel.DataAnnotations;

namespace FinanceDashboard.Models.Models.Emi
{
    public class EmiUpdateModel
    {
        public DateTime CompletionDate { get; set; }

        public bool? IsCompleted { get; set; }

        public DateTime? CancelledOn { get; set; }

        public int? CancelledBy { get; set; }

        public DateTime? LastUpdated { get; set; }

        public int? LastUpdateBy { get; set; }
    }
}
