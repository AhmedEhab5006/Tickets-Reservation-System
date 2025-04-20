using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsReservationSystem.BLL.Dto_s;
using TicketsReservationSystem.DAL.Models;

namespace TicketsReservationSystem.BLL.Managers
{
    public interface IUserManager
    {
        public int Add(UserAddDto user);
        public void Update(UserUpdateDto user);
        public UserReadDto GetByEmail(string email);

    }
}
