using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Abstractions;
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

        //book 
        public bool Book(int ticketId, string clientId)
        {
            return  _clientRepository.Book(ticketId, clientId);
        }
        //cancelbooking
        public bool CancelBooking(int ticketId, string clientId)
        {
            return _clientRepository.CancelBooking(ticketId, clientId);
        }
        //edit address
        public async Task<bool> EditAddressAsync(string clientId, AddressUpdateDto addressDto)
        {
            var client = await _clientRepository.GetClientWithAddressAsync(clientId);

            if (client == null || client.address == null)
                return false;

            // Update the address with the new details
            var address = new Address
            {
                street = addressDto.Street,
                city = addressDto.City,
                state = addressDto.State,
                postalCode = addressDto.PostalCode
            };

            return await _clientRepository.EditAddressAsync(clientId, address);
        }


        public async Task<List<ClientBookingDto>> ViewBookingsAsync(string clientId)
        {
            var tickets = await _clientRepository.GetClientBookingsAsync(clientId);

            var clientBookings = new List<ClientBookingDto>();

            foreach (var ticket in tickets)
            {
                var eventDetails = ticket.Event;

                var bookingDto = new ClientBookingDto
                {
                    TicketId = ticket.id,
                    EventId = eventDetails.id,
                    EventDate = eventDetails.date,
                    EventLocation = eventDetails.location,
                    BookedSeats = eventDetails.bookedSeats,
                    TicketStatus = ticket.status,
                    TicketPrice = ticket.price, // comes from ticket
                    EventStatus = eventDetails.status,
                    EventCategory = eventDetails.category,                   
                };

                clientBookings.Add(bookingDto);
            }

            return clientBookings;
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
        //public async Task<IEnumerable<ClientReadDto>> GetAllClientsAsync()
        //{
        //    var clients = _clientRepository.GetAllClients();

        //    var clientDtos = clients.Select(client => new ClientReadDto
        //    {
        //        UserId = client.UserId,
        //        UserName = client.user != null ? client.user.firstName + " " + client.user.lastName : null,
        //        AddressId = client.addressId,
        //        Address = client.address != null ? new AddressReadDto
        //        {
        //            Id = client.address.id,
        //            Street = client.address.street,
        //            City = client.address.city,
        //            State = client.address.state,
        //            PostalCode = client.address.postalCode
        //        } : null,
        //        Tickets = client.tickets != null ? client.tickets.Select(ticket => new TicketReadDto
        //        {
        //            Id = ticket.id,
        //            price = ticket.price,
        //            status = ticket.status
        //        }).ToList() : new List<TicketReadDto>()
        //    }).ToList();

        //    return await Task.FromResult(clientDtos);
        //}

       


        //        // Removed the duplicate method definition for AddClientAddressAsync.  
        //        // The duplicate method was already defined earlier in the file.  
        //        // Keeping only one definition of AddClientAddressAsync.  



 

      
    }
}
//        }
//    }
//}
