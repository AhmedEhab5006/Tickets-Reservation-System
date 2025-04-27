using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsReservationSystem.DAL.Models;

namespace TicketsReservationSystem.DAL.Repository
{
    public interface IAdminRepository
    {
        IQueryable<User> GetAll();
        User GetById(int id);
        void Delete(User deleted);
        void AddAdmin(User admin);
        void ConfirmVendor(int vendorId);
        void RejectVendor(int vendorId);
    }

}
