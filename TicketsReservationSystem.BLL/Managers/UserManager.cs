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

        public UserManager(IUserRepository userRepository) {
            _userRepository = userRepository;
        }
        public int Add(UserAddDto user)
        {

            var Added = new User
            {
                firstName = user.firstname,
                lastName = user.lastname,
                password = user.password,
                email = user.email,
                role = user.role
            };
            _userRepository.Add(Added);

            return Added.id;
        }

        public void Update(UserUpdateDto user)
        {
            var found = _userRepository.GetById(user.Id);
            
            found.email = user.email;
            found.firstName = user.firstname;
            found.lastName = user.lastname;
            found.password = user.password;
            
            _userRepository.Update(found);
        }
    }
}
