using System.ComponentModel.DataAnnotations;

namespace TimeControl.Models
{
public class LoginM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}