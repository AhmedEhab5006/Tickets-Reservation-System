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
        
        public void Add(ClientAddDto clientAddDto, int userId)
        {
            _clientRepository.Add(new Client
            {
                userId = userId,
                addressId = clientAddDto.AddressId,

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

        public IEnumerable<FullDetailSportEventReadDto> GetSportEvents()
        {
            var foundModel = _clientRepository.GetSportEvent();

            if (foundModel != null)
            {
                var found = foundModel.Select(a => new FullDetailSportEventReadDto
                {
                    aviilableSeats = a.avillableSeats,
                    team1 = a.sportEvent.team1,
                    team2 = a.sportEvent.team2,
                    location = a.location,
                    day = a.date.Day.ToString(),
                    year = a.date.Year.ToString(),
                    mounth = a.date.Month.ToString(),
                    tournament = a.sportEvent.tournament,
                    tournamentStage = a.sportEvent.tournamentStage,
                    sport = a.sportEvent.sport,

                }).ToList();

                return found;

            }

            return null;

        }

        public IEnumerable<FullDetailEntertainmentEventReadDto> GetEntertainmentEvents()
        {
            var foundModel = _clientRepository.GetEntertainmentEvents();

            
            if (foundModel != null)
            {
                var found = foundModel.Select(a => new FullDetailEntertainmentEventReadDto
                {
                    aviilableSeats = a.avillableSeats,
                    showCategory = a.entertainment.showCategory,
                    performerName = a.entertainment.performerName,
                    location = a.location,
                    day = a.date.Day.ToString(),
                    year = a.date.Year.ToString(),
                    mounth = a.date.Month.ToString(),
                    ageRestriction = a.entertainment.ageRestriction,
                    duration = a.entertainment.duration,
                    genre = a.entertainment.genre,

                }).ToList();
                return found;
            }

            return null;
        }
    }
}
