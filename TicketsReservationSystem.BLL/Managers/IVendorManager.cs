using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsReservationSystem.BLL.Dto_s;
using TicketsReservationSystem.DAL.Models;

namespace TicketsReservationSystem.BLL.Managers
{
    public interface IVendorManager
    {
        public void Add(VendorAddDto vendor , int userId);
        public void AddEvent(Event Event);
        public void EditEvent(Event Event);
        public void AddEntertainmentEvent(EntertainmentEvent Event);
        public void EditEntertainmentEvent(EntertainmentEvent EntertainmentEvent);
        public void AddSportsEvent(SportEvent SportsEvent);
        public void EditSportsEvent(SportEvent sportEvent);
        public void DeleteEvent(int id);
        public int ShowBookings(int eventId);

    }
}
