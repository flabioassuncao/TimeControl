using System.ComponentModel.DataAnnotations;

namespace TimeControl.Models
{
    public class Register
    {
        // [Required]
        // [EmailAddress]
        // [Display(Name = "Email")]
        public string Email { get; set; }
 
        // [Required]
        // [StringLength(100, ErrorMessage = "O {0} deve ter pelo menos {2} caracteres.", MinimumLength = 6)]
        // [DataType(DataType.Password)]
        // [Display(Name = "Password")]
        public string Password { get; set; }
 
        // [DataType(DataType.Password)]
        // [Display(Name = "Confirm password")]
        // [Compare("Password", ErrorMessage = "As senhas não coincidem.")]
        public string ConfirmPassword { get; set; }
    }
}