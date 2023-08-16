using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinanceDashboard.Data.SqlServer.Entities
{
    public class Goals
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Key]
        public int GoalId { get; set; }

        [Required]
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Fk_Accounts_Goals_AccountId")]
        [Required]
        public int AccountId { get; set; }

        [ForeignKey("Fk_Accounts_Goals_AccountEntityId")]
        [Required]
        public Guid AccountEntityId { get; set; }

        public Account User { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime TargetDate { get; set; }

        [Required]
        public DateTime AchievedOn { get; set; }

        public DateTime CanceledOn { get; set; }

        public int CanceledBy { get; set; }

        public string Remarks { get; set; }
    }
}
