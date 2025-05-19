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
        public AddressReadDto GetMyAddress(string clientId);
        public void EditAddress(AddressUpdateDto address , string clientId);
        public bool Book(ReservationAddDto reservation);
        public void CancelBooking(int id);
        public IEnumerable<ReservationReadDto> GetClientBookings(string clientId);
        public IEnumerable<FullDetailSportEventReadDto> GetSportEvents();
        public IEnumerable<FullDetailEntertainmentEventReadDto> GetEntertainmentEvents();
        public IEnumerable<TicketReadDto> GetEventTickets(int eventId);

    }
}
