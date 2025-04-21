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

        public ClientRepository(ProgramContext context) {
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
            throw new NotImplementedException();
        }

        public void CancelBooking(int ticketId)
        {
            throw new NotImplementedException();
        }

        public void EditAddress(Address address)
        {
            var updated = _context.Address.Update(address);
            _context.SaveChanges();
        }

        public IQueryable<Event> GetSportEvent()
        {
            var returned = _context.Events.Where(a=>a.status == "Aceepted")
                            .Include(a => a.sportEvent);

            return returned;
        }

        public IQueryable<Event> GetEntertainmentEvents()
        {
            var returned = _context.Events.Where(a => a.status == "Aceepted")
                            .Include(a => a.entertainment);

            return returned;
        }
    }
}
