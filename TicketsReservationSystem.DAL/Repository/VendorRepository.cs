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

        public VendorRepository(ProgramContext context)
        {
            _context = context;
        }

        public void Add(Vendor vendor)
        {
            _context.vendors.Add(vendor);
            _context.SaveChanges();
        }

        //public IQueryable<Vendor> GetAllPendingVendors()
        //{
        //    return _context.vendors.Include(v => v.user).Where(v => v.acceptanceStatus == "Pending");
        //}

        public int AddEvent(Event Event)
        {
            var added = new Event
            {
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

        public async Task <Vendor?> GetById(string id)
        {
            return await _context.vendors
                    .AsNoTracking()
                    .FirstOrDefaultAsync(v => v.Id == id);
        }

        public IQueryable<Event> GetMySportEvents(string id)
        {
            var returned = _context.Events.Where(a => a.vendorId == id)
                                .Include(a => a.sportEvent);

            return returned;

        }

        public IQueryable<Event> GetMyEntertainmentEvents(string id)
        {
            var returned = _context.Events.Where(a => a.vendorId == id)
                                            .Include(a => a.entertainment);

            return returned;

        }

        public int ShowBookings(int eventId)
        {
            throw new NotImplementedException();
        }

        public void AddTicket(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            _context.SaveChanges();
        }

        public void EditTicket(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
            _context.SaveChanges();
        }

        public void DeleteTicket(Ticket ticket)
        {
            _context.Tickets.Remove(ticket);
            _context.SaveChanges();
        }

        public Ticket GetTicketById(int id)
        {
            var found = _context.Tickets.Find(id);
            return found;
        }

        public IQueryable<Ticket> GetMyEventTickets(int eventId)
        {
            var found = _context.Tickets.Where(a => a.EventId == eventId);
            return found;
        }
    }
}
