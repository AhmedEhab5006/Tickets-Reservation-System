    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TicketsReservationSystem.DAL.Database;
    using TicketsReservationSystem.DAL.Models;

    namespace TicketsReservationSystem.DAL.Repository
    {
        public class AdminRepository : IAdminRepository
        {
            private ProgramContext _context;

            public AdminRepository(ProgramContext context) {
                _context = context;
            }
            public void AddAdmin(User admin)
            {
                var added = _context.Users.Add(admin);
                _context.SaveChanges();
            }

            

            public void Delete(User deleted)
            {
                _context.Users.Remove(deleted);
                _context.SaveChanges();
            }

            public IQueryable<User> GetAll()
            {
                var query = _context.Users;
                return query;
            }

            public User GetById(int id)
            {
                var user = _context.Users.Find(id);
                return user;
            }

        public void ConfirmVendor(int vendorId)
        {
            var vendor = _context.vendors.Find(vendorId);
            if (vendor != null)
            {
                vendor.acceptanceStatus = "Accepted";
                _context.SaveChanges();
            }
        }

        public void RejectVendor(int vendorId)
        {
            var vendor = _context.vendors.Find(vendorId);
            if (vendor != null)
            {
                vendor.acceptanceStatus = "Rejected";
                _context.SaveChanges();
            }
        }

    }
}
