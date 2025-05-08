using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsReservationSystem.BLL.Dto_s.AuthDto_s
{
    public class LoginDto
    {
        public string email { get; set; }
        public string password { get; set; }
        public string Roles { get; set; }
    }
}
