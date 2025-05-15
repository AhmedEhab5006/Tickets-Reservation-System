using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TicketsReservationSystem.BLL.Dto_s.AuthDto_s;

namespace TicketsReservationSystem.BLL.Managers.AuthManagers
{
    public interface IAuthManager
    {
        public Task<string> Login(LoginDto loginDto);
        public Task<string> Register(RegisterDto registerDto);
        public string GenerateToken(IList<Claim> claims);
    }
}
