using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsReservationSystem.BLL.Dto_s;
using TicketsReservationSystem.DAL.Models;

namespace TicketsReservationSystem.BLL.Managers
{
    public interface IClientManager
    {
        public Task<bool> EditAddressAsync(string clientId, AddressUpdateDto addressDto);
        public bool Book(int ticketId, string clientId);
        public bool CancelBooking(int ticketId, string clientId);
        public IEnumerable<FullDetailSportEventReadDto> GetSportEvents();
        public IEnumerable<FullDetailEntertainmentEventReadDto> GetEntertainmentEvents();

        public Task<List<ClientBookingDto>> ViewBookingsAsync(string clientId);
    }
}

