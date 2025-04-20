using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsReservationSystem.DAL.Database;
using TicketsReservationSystem.DAL.Models;

namespace TicketsReservationSystem.DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ProgramContext _context;

        public UserRepository(ProgramContext context) {
            _context = context;
        }
        public void Add(User user)
        {
            var added = _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User GetByEmail(string email)
        {
            var found = _context.Users.Where(a=> a.email == email).FirstOrDefault();
            return found;
        }

        public User GetById(int id)
        {
            var found = _context.Users.Find(id);
            return found;
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
