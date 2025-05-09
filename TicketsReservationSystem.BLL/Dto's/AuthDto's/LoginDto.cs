using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsReservationSystem.BLL.Dto_s.AuthDto_s
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Missing Email")]
        public string email { get; set; }
        [Required(ErrorMessage = "Missing Password")]
        public string password { get; set; }
    }
}
