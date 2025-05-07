using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsReservationSystem.BLL.Dto_s.AuthDto_s
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Missing Username")]
        public string username { get; set; }
        [Required(ErrorMessage = "Missing Email")]
        public string email { get; set; }
        [Required(ErrorMessage = "Missing Password")]
        [StringLength(100, MinimumLength = 5 , ErrorMessage = "Password must be atleast 5 chars")]
        public string password { get; set; }
        [Required]
        [AllowedValues("Client", "Vendor" , ErrorMessage = "Invalid role (role must be Client or Vendor)")]
        public string role { get; set; }
        public string phoneNumber { get; set; }
    }
}
