using System.ComponentModel.DataAnnotations;

namespace FinanceDashboard.Models.Models.PreLogon
{
    public class PreLogonModel
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
