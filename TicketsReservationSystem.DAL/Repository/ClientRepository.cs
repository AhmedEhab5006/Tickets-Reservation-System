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

        public void EditAddress(Address address)
        {
            _context.Address.Update(address);
            _context.SaveChanges();
        }

        public void Book(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
        }

        public void CancelBooking(Reservation reservation)
        {
            _context.Reservations.Remove(reservation);
            _context.SaveChanges();
        }

        public IQueryable<Reservation> GetClientBookings(string clientId)
        {
            var found = _context.Reservations.Where(a=>a.clientId == clientId)
                                             .Include(a => a.client)
                                             .Include(a => a.ticket)
                                             .Include(a => a.client.address)
                                             .Include(a => a.ticket.Event);   
            return found;
        }

        public Client GetAddressId(string clientId)
        {
            var found = _context.Clients.Where(a => a.Id == clientId)
                                        .Include(a => a.address)
                                        .FirstOrDefault();

            return found;
                                        
        }

        public Client GetAddress(string clientId)
        {
            var found = _context.Clients.Where(a => a.Id == clientId)
                                        .Include(a => a.address)
                                        .FirstOrDefault();

            return found;
        }

        public IQueryable<Ticket> GetEventTickets(int eventId)
        {
            var found = _context.Tickets.Where(a=>a.EventId == eventId);
            return found;
        }

        public Reservation GetReservation(int reservationId)
        {
            var found = _context.Reservations.Where(a=>a.id == reservationId).FirstOrDefault();
            return found;
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
