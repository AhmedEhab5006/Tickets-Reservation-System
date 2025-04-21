using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsReservationSystem.DAL.Models;

namespace TicketsReservationSystem.DAL.Repository
{
    public interface IClientRepository
    {
        
        public void Add(Client client);
        public void AddAddress (Address address);
        public void EditAddress (Address address);
        public void Book(int ticketId);
        public void CancelBooking(int ticketId);
        public IQueryable<Event> GetSportEvent();
        public IQueryable<Event> GetEntertainmentEvents();
    }
}
