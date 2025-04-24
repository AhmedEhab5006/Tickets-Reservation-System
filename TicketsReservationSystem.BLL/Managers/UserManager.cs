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
                role = user.role,
                phoneNumber = user.phoneNumber,
            };
            _userRepository.Add(Added);

            return Added.id;
        }

        public UserReadDto GetByEmail(string email)
        {
            var foundModel = _userRepository.GetByEmail(email);

            var found = new UserReadDto{
                Id = foundModel.id,
                email = foundModel.email,
                firstname = foundModel.firstName,
                lastname = foundModel.lastName,
                password = foundModel.password,
                phoneNumber= foundModel.phoneNumber,
            };
            
            return found;
        }

        public void Update(UserUpdateDto user)
        {
            var found = _userRepository.GetById(user.Id);
            
            found.email = user.email;
            found.firstName = user.firstname;
            found.lastName = user.lastname;
            found.password = user.password;
            found.phoneNumber = user.phoneNumber;
            
            _userRepository.Update(found);
        }
    }
}
