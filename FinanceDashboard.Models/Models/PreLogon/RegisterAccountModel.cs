using System.ComponentModel.DataAnnotations;

namespace FinanceDashboard.Models.PreLogon
{
    public class RegisterAccountModel
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string MobileNumber { get; set; }
    }
}
