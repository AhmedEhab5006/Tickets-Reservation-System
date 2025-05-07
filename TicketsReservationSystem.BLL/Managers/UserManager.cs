using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsReservationSystem.BLL.Dto_s;
using TicketsReservationSystem.DAL.Models;
using TicketsReservationSystem.DAL.Repository;

namespace TicketsReservationSystem.BLL.Managers
{
    public class UserManager : IUserManager
    {
        private IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> CreateVendor(VendorAddDto dto)
        {
            var done = await _userRepository.CreateVendor(new Vendor
            {
                Id = dto.id,
                acceptanceStatus = dto.acceptanceStatus,
            });

            if (done != null)
            {
                return "done";
            }

            return null;
        }
    }
}
