using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsReservationSystem.DAL.Models;

namespace TicketsReservationSystem.DAL.Repository
{
    public interface IVendorRepository
    {
        public void Add(Vendor vendor);
        public void AddEvent(Event Event);
        public void EditEvent(Event Event);
        public void AddEntertainmentEvent (EntertainmentEvent Event);
        public void EditEntertainmentEvent (EntertainmentEvent EntertainmentEvent);
        public void AddSportsEvent(SportEvent SportsEvent);
        public void EditSportsEvent(SportEvent sportEvent);
        public void DeleteEvent(int id);
        public int ShowBookings(int eventId);
        
    }
}
