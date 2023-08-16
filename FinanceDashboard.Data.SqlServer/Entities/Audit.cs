using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinanceDashboard.Data.SqlServer.Entities
{
    public class Audit
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Key]
        public long AuditSequence { get; set; }

        [Required]
        [Key]
        public Guid AuditId { get; set; }

        [ForeignKey("Fk_Accounts_Audit_AccountId")]
        [Required]
        public int AccountId { get; set; }

        [ForeignKey("Fk_Accounts_Audit_AccountEntityId")]
        [Required]
        public Guid AccountEntityId { get; set; }

        public Account User { get; set; }

        [Required]
        public string FieldName { get; set; }

        [Required]
        public string OldValue { get; set; }

        [Required]
        public string NewValue { get; set; }

        [Required]
        public DateTime ModifiedOn { get; set; }

        [Required]
        public int ModifiedBy { get; set; }
    }
}
