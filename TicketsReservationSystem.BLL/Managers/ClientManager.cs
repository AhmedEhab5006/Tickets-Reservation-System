using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsReservationSystem.BLL.Dto_s;
using TicketsReservationSystem.DAL.Models;
using TicketsReservationSystem.DAL.Repository;

namespace TicketsReservationSystem.BLL.Managers
{
    public class ClientManager : IClientManager
    {
        private IClientRepository _clientRepository;

        public ClientManager(IClientRepository clientRepository) {
            _clientRepository = clientRepository;
        }
        
        public void Add(int userId)
        {
            _clientRepository.Add(new Client
            {
                userId = userId,

            });
        }

        public void AddAddress(Address address)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
