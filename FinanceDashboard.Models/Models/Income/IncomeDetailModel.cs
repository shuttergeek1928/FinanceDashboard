namespace FinanceDashboard.Models.Models.Income
{
    public class IncomeDetailModel
    {
        public Guid IncomeId { get; set; }

        public int AccountId { get; set; }

        public string IncomeName { get; set; }

        public decimal Amount { get; set; }

        public string CreditCycle { get; set; }

        public string Creditor { get; set; }
            
        public DateTime CredtiDate { get; set; }

        public DateTime? Expiry { get; set; }

        public int? ExpiredBy { get; set; }

        public string? ExpiringReason { get; set; }

        public decimal IncomeBalance { get; set; }
    }
}
