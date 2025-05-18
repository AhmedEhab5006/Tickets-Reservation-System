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
        public int AddEvent(Event Event);
        public void EditEvent(Event Event);
        public void AddEntertainmentEvent(EntertainmentEvent Event);
        public void EditEntertainmentEvent(EntertainmentEvent EntertainmentEvent);
        public void AddSportsEvent(SportEvent SportsEvent);
        public void EditSportsEvent(SportEvent sportEvent);
        public void DeleteEvent(Event Event);
        public Event GetEventById(int id);
        public EntertainmentEvent GetEntertainmentEventById(int id);
        public SportEvent GetSportEventById(int id);
        public int ShowBookings(int eventId);
        public Vendor GetById(string id);
        public IQueryable<Event> GetMySportEvents(string id);
        public IQueryable<Event> GetMyEntertainmentEvents(string id);
        public void AddTicket(Ticket ticket);
        public void EditTicket(Ticket ticket);
        public void DeleteTicket(Ticket ticket);
        public Ticket GetTicketById(int id);
        //public IQueryable<Vendor> GetAllPendingVendors();
        public IQueryable<Ticket> GetMyEventTickets(int eventId);
        public string GetAcceptanceStatus (string vendorId);
        public IQueryable<Ticket> GetMyTickets (string vendorId);
    }
}
