using Microsoft.EntityFrameworkCore;
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
            _context.vendors.Add(vendor);
            _context.SaveChanges();
        }


        public int AddEvent(Event Event)
        {
            var added = new Event{
                numberOfSeats = Event.numberOfSeats,
                vendorId = Event.vendorId,
                location = Event.location,
                date = Event.date,
                category = Event.category,
                avillableSeats = Event.avillableSeats,
                bookedSeats = Event.bookedSeats,
                status = Event.status,
            };
            
            _context.Events.Add(added);
            _context.SaveChanges();

            return added.id;
        }

        public void AddEntertainmentEvent(EntertainmentEvent Event)
        {
           _context.EntertainmentEvents.Add(Event);
            _context.SaveChanges();
        }

        public void AddSportsEvent(SportEvent SportsEvent)
        {
            _context.SportEvents.Add(SportsEvent);
            _context.SaveChanges();
        }

        public void DeleteEvent(Event Event)
        {
            _context.Events.Remove(Event);
            _context.SaveChanges();
        }

        public void EditEntertainmentEvent(EntertainmentEvent EntertainmentEvent)
        {
            _context.Update(EntertainmentEvent);
            _context.SaveChanges();
        }

        public void EditEvent(Event Event)
        {
            _context.Update(Event);
            _context.SaveChanges();
        }

        public void EditSportsEvent(SportEvent sportEvent)
        {
            _context.Update(sportEvent);
            _context.SaveChanges();
        }


        public Event GetEventById(int id)
        {
            var found = _context.Events.Find(id);
            return found;

        }

        public EntertainmentEvent GetEntertainmentEventById(int id)
        {
            var found = _context.EntertainmentEvents.Find(id);
            return found;
        }

        public SportEvent GetSportEventById(int id)
        {
            var found = _context.SportEvents.Find(id);
            return found;
        }

        public Vendor GetByUserId(int id)
        {
            var found = _context.vendors.Where(a=> a.userId == id).FirstOrDefault();
            return found;
        }

        public Vendor GetById (int id)
        {
            var found = _context.vendors.Where(a=> a.Id == id).FirstOrDefault();
            return found;
        }

        public IQueryable<Event> GetMySportEvents(int id)
        {
            var returned = _context.Events.Where(a=>a.vendorId == id)
                                .Include(a => a.sportEvent);

            return returned;

        }

        public IQueryable<Event> GetMyEntertainmentEvents(int id)
        {
            var returned = _context.Events.Where(a => a.vendorId == id)
                                            .Include(a => a.entertainment);

            return returned;

        }

        public int ShowBookings(int eventId)
        {
            throw new NotImplementedException();
        }
    }
}
