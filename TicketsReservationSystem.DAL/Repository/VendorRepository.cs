using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsReservationSystem.DAL.Database;
using TicketsReservationSystem.DAL.Models;

namespace TicketsReservationSystem.DAL.Repository
{
    public class VendorRepository : IVendorRepository
    {
        private ProgramContext _context;

        public VendorRepository(ProgramContext context) {
            _context = context; 
        }

        public void Add(Vendor vendor)
        {
            _context.Add(vendor);
            _context.SaveChanges();
        }

        public void AddEntertainmentEvent(EntertainmentEvent Event)
        {
            throw new NotImplementedException();
        }

        public void AddEvent(Event Event)
        {
            throw new NotImplementedException();
        }

        public void AddSportsEvent(SportEvent SportsEvent)
        {
            throw new NotImplementedException();
        }

        public void DeleteEvent(int id)
        {
            throw new NotImplementedException();
        }

        public void EditEntertainmentEvent(EntertainmentEvent EntertainmentEvent)
        {
            throw new NotImplementedException();
        }

        public void EditEvent(Event Event)
        {
            throw new NotImplementedException();
        }

        public void EditSportsEvent(SportEvent sportEvent)
        {
            throw new NotImplementedException();
        }

        public int ShowBookings(int eventId)
        {
            throw new NotImplementedException();
        }
    }
}
