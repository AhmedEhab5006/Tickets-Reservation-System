namespace WebApplication2.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterVM
    {
        [Required(ErrorMessage = "Username is required")]
        public string username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Password must be at least 5 characters")]
        public string password { get; set; }

        [Required(ErrorMessage = "Role is required")]
        [AllowedValues("Client", "Vendor", ErrorMessage = "Role must be either Client or Vendor")]
        public string role { get; set; }

        public string phoneNumber { get; set; }
    }
}
