using System.ComponentModel.DataAnnotations;

namespace FinanceDashboard.Service.Models
{
    public class UserCreateModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string HashingSalt { get; set; }

        [Required]
        public string PasswordHashHistory { get; set; }

        [Required]
        public string MobileNumber { get; set; }

        public short? InvalidPasswordCount { get; set; }

        public bool? IsLocked { get; set; }

        public string? SecondFactorKey { get; set; }

        public bool? SecondFactorValidated { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public DateTime? VerifiedOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        public int? DeletedBy { get; set; }
    }
}
