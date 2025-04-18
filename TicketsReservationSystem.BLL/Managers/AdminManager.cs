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
    public class AdminManager : IAdminManager
    {
        private IAdminRepository _adminRepository;

        public AdminManager(IAdminRepository adminRepository) {
            _adminRepository = adminRepository;
        }
        public void AddAdmin(UserAddDto admin)
        {
            _adminRepository.AddAdmin(new User
            {
                firstName = admin.firstname,
                lastName = admin.lastname,
                email = admin.email,
                password = admin.password,
                role = "Admin"
            });
        }

        public void ConfirmVendor(int vendorId)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            var found = _adminRepository.GetById(id);
            if (found != null)
            {
                _adminRepository.Delete(found);
            }
        }

        public IEnumerable<User> GetAll()
        {
            var users = _adminRepository.GetAll();
            return users;
        }

        public User GetById(int id)
        {
            var found = _adminRepository.GetById(id);
            if (found != null)
            {
                return found;
            }
            else
            {
                return null;
            }
        }

        public void RejectVendor(int vendorId)
        {
            throw new NotImplementedException();
        }
    }
}
