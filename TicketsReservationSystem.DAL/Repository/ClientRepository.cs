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
    public class ClientRepository : IClientRepository
    {
        private ProgramContext _context;

        public ClientRepository(ProgramContext context)
        {
            _context = context;
        }

        public void Add(Client client)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();
        }

        public void AddAddress(Address address)
        {
            var added = _context.Address.Add(address);
            _context.SaveChanges();
        }

        public void Book(int ticketId)
        {
            var ticket = _context.Tickets.Include(t => t.Event).FirstOrDefault(t => t.id == ticketId);

            if (ticket == null)
            {
                throw new InvalidOperationException("Ticket not found.");
            }

            if (ticket.status != "Available")
            {
                throw new InvalidOperationException("Ticket is not available for booking.");
            }

            // Update ticket status and event's booked/available seats
            ticket.status = "Booked";
            ticket.Event.bookedSeats += 1;
            ticket.Event.avillableSeats -= 1;

            _context.SaveChanges();
        }

        public void CancelBooking(int ticketId)
        {
            var ticket = _context.Tickets.Include(t => t.Event).FirstOrDefault(t => t.id == ticketId);

            if (ticket == null)
            {
                throw new InvalidOperationException("Ticket not found.");
            }

            if (ticket.status != "Booked")
            {
                throw new InvalidOperationException("Ticket is not currently booked.");
            }

            // Update ticket status and event's booked/available seats
            ticket.status = "Available";
            ticket.Event.bookedSeats -= 1;
            ticket.Event.avillableSeats += 1;

            _context.SaveChanges();
        }

        public void EditAddress(Address address)
        {
            var updated = _context.Address.Update(address);
            _context.SaveChanges();
        }

        public IQueryable<Event> GetSportEvent()
        {
            var returned = _context.Events.Where(a => a.status == "Accepted")
                            .Include(a => a.sportEvent);

            return returned;
        }

        public IQueryable<Event> GetEntertainmentEvents()
        {
            var returned = _context.Events.Where(a => a.status == "Accepted")
                            .Include(a => a.entertainment);

            return returned;
        }

        public Client? GetClientById(int clientId)
        {
            return _context.Clients
                .Include(c => c.user) 
                .Include(c => c.address)
                .Include(c => c.tickets) 
                .FirstOrDefault(c => c.id == clientId); 
        }

        public IQueryable<Client> GetAllClients()
        {
            return _context.Clients
                .Include(c => c.user) 
                .Include(c => c.address) 
                .Include(c => c.tickets); 
        }
    }
}
