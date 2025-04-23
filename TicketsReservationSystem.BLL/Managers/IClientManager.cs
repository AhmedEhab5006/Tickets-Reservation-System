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
        public void Add(ClientAddDto clientAddDto , int userId);
        int AddAddressAsync(AddressAddDto addressDto);
        Task EditAddressAsync(AddressUpdateDto addressDto);
        public void Book(int ticketId);
        public void CancelBooking(int ticketId);
        public IEnumerable<FullDetailSportEventReadDto> GetSportEvents();
        public IEnumerable<FullDetailEntertainmentEventReadDto> GetEntertainmentEvents();
        Task<IEnumerable<ClientReadDto>> GetAllClientsAsync();

    }
}
