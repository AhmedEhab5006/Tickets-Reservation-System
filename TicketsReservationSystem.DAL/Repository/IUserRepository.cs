using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsReservationSystem.DAL.Models;

namespace TicketsReservationSystem.DAL.Repository
{
    public interface IUserRepository
    {
        public void Add(User user);
        public void Update(User user);
        public User GetById(int id);
        public User GetByEmail(string email);
    }

}
