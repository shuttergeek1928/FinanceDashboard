using FinanceDashboard.Data.SqlServer.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceDashboard.Data.SqlServer.Authorization
{
    public class ApiUser : IdentityUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Key]
        public int AccountId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        public string HashingSalt { get; set; }

        public string PasswordHashHistory { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public DateTime? VerifiedOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        public int? DeletedBy { get; set; }

        //References
        public List<Subscription> Subscriptions { get; set; }
    }
}
