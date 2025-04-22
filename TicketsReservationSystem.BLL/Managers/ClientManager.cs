using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TicketsReservationSystem.BLL.Dto_s;
using TicketsReservationSystem.DAL.Models;
using TicketsReservationSystem.DAL.Repository;

namespace TicketsReservationSystem.BLL.Managers
{
    public class ClientManager : IClientManager
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientManager(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        // Add client manually
        public void Add(ClientAddDto clientAddDto, int userId)
        {
            _clientRepository.Add(new Client
            {
                userId = userId,
                addressId = clientAddDto.AddressId,
            });
        }

        // Async version using AutoMapper
        public async Task AddClientAsync(ClientAddDto clientDto)
        {
            var client = _mapper.Map<Client>(clientDto);
            _clientRepository.Add(client);
            await Task.CompletedTask;
        }

        // Add address
        public void AddAddress(Address address)
        {
            _clientRepository.AddAddress(address);
        }

        public async Task AddClientAddressAsync(AddressReadDto addressDto)
        {
            var address = _mapper.Map<Address>(addressDto);
            _clientRepository.AddAddress(address);
            await Task.CompletedTask;
        }

        // Edit address
        public void EditAddress(Address address)
        {
            _clientRepository.EditAddress(address);
        }

        public async Task EditClientAddressAsync(AddressReadDto addressDto)
        {
            var address = _mapper.Map<Address>(addressDto);
            _clientRepository.EditAddress(address);
            await Task.CompletedTask;
        }

        // Book ticket
        public void Book(int ticketId)
        {
            _clientRepository.Book(ticketId);
        }

        public async Task BookTicketAsync(int ticketId)
        {
            _clientRepository.Book(ticketId);
            await Task.CompletedTask;
        }

        // Cancel booking
        public void CancelBooking(int ticketId)
        {
            _clientRepository.CancelBooking(ticketId);
        }

        public async Task CancelTicketBookingAsync(int ticketId)
        {
            _clientRepository.CancelBooking(ticketId);
            await Task.CompletedTask;
        }

        // Get sport events with full details
        public IEnumerable<FullDetailSportEventReadDto> GetSportEvents()
        {
            var foundModel = _clientRepository.GetSportEvent();

            if (foundModel != null)
            {
                return foundModel.Select(a => new FullDetailSportEventReadDto
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
            }

            return null;
        }

        // Get mapped sport events (short info)
        public async Task<IEnumerable<EventAddDto>> GetSportEventsAsync()
        {
            var events = _clientRepository.GetSportEvent();
            return await Task.FromResult(_mapper.Map<IEnumerable<EventAddDto>>(events));
        }

        // Get entertainment events with full details
        public IEnumerable<FullDetailEntertainmentEventReadDto> GetEntertainmentEvents()
        {
            var foundModel = _clientRepository.GetEntertainmentEvents();

            if (foundModel != null)
            {
                return foundModel.Select(a => new FullDetailEntertainmentEventReadDto
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
            }

            return null;
        }

        // Get mapped entertainment events (short info)
        public async Task<IEnumerable<EventAddDto>> GetEntertainmentEventsAsync()
        {
            var events = _clientRepository.GetEntertainmentEvents();
            return await Task.FromResult(_mapper.Map<IEnumerable<EventAddDto>>(events));
        }

        // Get all clients with detailed info
        public async Task<IEnumerable<ClientReadDto>> GetAllClientsAsync()
        {
            var clients = _clientRepository.GetAllClients();

            var clientDtos = clients.Select(client => new ClientReadDto
            {
                Id = client.id,
                UserId = client.userId,
                UserName = client.user != null ? client.user.firstName + " " + client.user.lastName : null,
                AddressId = client.addressId,
                Address = client.address != null ? new AddressReadDto
                {
                    Id = client.address.id,
                    Street = client.address.street,
                    City = client.address.city,
                    State = client.address.state,
                    PostalCode = client.address.postalCode
                } : null,
                Tickets = client.tickets != null ? client.tickets.Select(ticket => new TicketReadDto
                {
                    Id = ticket.id,
                    EventId = ticket.EventId,
                    EventName = ticket.Event != null ? ticket.Event.category : null,
                    Price = ticket.price,
                    SeatNumber = ticket.seatNumber,
                    Status = ticket.status
                }).ToList() : new List<TicketReadDto>()
            }).ToList();

            return await Task.FromResult(clientDtos);
        }

        // Get a single client by ID
        public async Task<ClientReadDto?> GetClientByIdAsync(int clientId)
        {
            var client = _clientRepository.GetClientById(clientId);
            return await Task.FromResult(_mapper.Map<ClientReadDto>(client));
        }
    }
}
