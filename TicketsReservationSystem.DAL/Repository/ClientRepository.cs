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

        public async Task<Client> GetClientWithAddressAsync(string clientId)
        {
            return await _context.Clients
                .Where(c => c.Id == clientId)
                .Include(c => c.address)
                .FirstOrDefaultAsync();
        }




        public bool Book(int ticketId, string clientId)
        {
            var ticket = _context.Tickets
                .Include(t => t.Event)
                .FirstOrDefault(t => t.id == ticketId);

            if (ticket == null)
            {
                Console.WriteLine($"[ERROR] Ticket with ID {ticketId} not found.");
                return false;
            }

            // Debug output
            Console.WriteLine($"[INFO] Ticket ID: {ticketId}, Status: {ticket.status}, AvailableSeats: {ticket.Event.avillableSeats}, BookedSeats: {ticket.Event.bookedSeats}");

            // Combined check for availability
            if (ticket.status != "Available" || ticket.Event.avillableSeats <= 0)
            {
                Console.WriteLine($"[ERROR] Ticket ID {ticketId} cannot be booked. Either not available or no available seats.");
                return false;
            }

            var client = _context.Clients.FirstOrDefault(c => c.Id == clientId);
            if (client == null)
            {
                Console.WriteLine($"[ERROR] Client with ID {clientId} not found.");
                return false;
            }

            // Update booking
            ticket.status = "Booked";
            ticket.ClientId = clientId;
            ticket.Event.bookedSeats += 1;
            ticket.Event.avillableSeats -= 1;

            _context.SaveChanges();

            Console.WriteLine($"[SUCCESS] Ticket ID {ticketId} successfully booked for Client ID {clientId}.");
            return true;
        }


        public bool CancelBooking(int ticketId, string clientId)
        {
            var ticket = _context.Tickets
                .Include(t => t.Event)
                .FirstOrDefault(t => t.id == ticketId && t.ClientId == clientId);

            if (ticket == null)
            {
                Console.WriteLine($"No booking found for ticket ID {ticketId} and client ID {clientId}.");
                return false;
            }

            if (ticket.status != "Booked")
            {
                Console.WriteLine($"Ticket ID {ticketId} is not booked, so it can't be cancelled.");
                return false;
            }

            // Cancel the booking
            ticket.status = "Available";
            ticket.ClientId = null;
            ticket.Event.bookedSeats -= 1;
            ticket.Event.avillableSeats += 1;

            _context.SaveChanges();
            Console.WriteLine($"Booking for ticket ID {ticketId} successfully cancelled.");
            return true;
        }

        public async Task<bool> EditAddressAsync(string clientId, Address address)
        {
            var client = await _context.Clients
               .Include(c => c.address) // Include the related address entity
               .SingleOrDefaultAsync(c => c.Id == clientId);


            if (client == null || client.address == null)
                return false;

            client.address.street = address.street;
            client.address.city = address.city;
            client.address.state = address.state;
            client.address.postalCode = address.postalCode;

            await _context.SaveChangesAsync();
            return true;
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



        public async Task<List<Ticket>> GetClientBookingsAsync(string clientId)
        {
            return await _context.Tickets
                .Include(t => t.Event)
                .Where(t => t.ClientId == clientId)
                .ToListAsync();
        }

        public int AddAddress(Address address)
        {
            var added = new Address
            {
                id = address.id,
                city = address.city,
                postalCode = address.postalCode,
                state = address.state,
                street = address.street,
            };

            _context.Address.Add(added);
            _context.SaveChanges();

            return added.id;
        }

    }
}
