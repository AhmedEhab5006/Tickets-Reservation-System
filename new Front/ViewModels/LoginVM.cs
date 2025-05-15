using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Email is required")]
        // [EmailAddress(ErrorMessage = "Invalid email format")]
        public string email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string password { get; set; }
    }
} 