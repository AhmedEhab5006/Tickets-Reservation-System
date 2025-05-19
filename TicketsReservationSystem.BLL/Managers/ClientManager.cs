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
        private readonly IVendorRepository _vendorRepository;

        public ClientManager(IClientRepository clientRepository , IVendorRepository vendorRepository)
        {
            _clientRepository = clientRepository;
            _vendorRepository = vendorRepository;
        }

        
        public IEnumerable<FullDetailSportEventReadDto> GetSportEvents()
        {
            var foundModel = _clientRepository.GetSportEvent();

            if (foundModel != null && foundModel.Any(a => a.category != "Entertainment"))
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
                    Id = a.id,
                }).ToList();
            }

            return null;
        }

        public IEnumerable<FullDetailEntertainmentEventReadDto> GetEntertainmentEvents()
        {
            var foundModel = _clientRepository.GetEntertainmentEvents().ToList();

            if (foundModel != null && foundModel.Any(a=>a.category != "Sport"))
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
                    Id = a.id,
                }).ToList();
            }

            return null;
        }

        public void EditAddress(AddressUpdateDto address , string clientId)
        {
            var foundModel = _clientRepository.GetAddress(clientId);

            if (foundModel != null)
            {
                foundModel.address.state = !string.IsNullOrWhiteSpace(address.State) ? address.State : foundModel.address.state;
                foundModel.address.postalCode = address.PostalCode > 0 ? address.PostalCode : foundModel.address.postalCode;
                foundModel.address.city = !string.IsNullOrWhiteSpace(address.City) ? address.City : foundModel.address.city;
                foundModel.address.street = !string.IsNullOrWhiteSpace(address.Street) ? address.Street : foundModel.address.street;

                _clientRepository.EditAddress(foundModel.address);
            }
        }

        public bool Book(ReservationAddDto reservation)
        {
            var foundTicket = _vendorRepository.GetTicketById(reservation.ticketId);
            
            if (foundTicket != null && foundTicket.avillableCount > 0)
            {
                foundTicket.avillableCount -= reservation.bookedCount;
                _clientRepository.Book(new Reservation
                {
                    shippingAddressId = reservation.shippingAddressId,
                    bookedCount = reservation.bookedCount,
                    clientId = reservation.clientId,
                    ticketId = reservation.ticketId,
                    totalPrice = foundTicket.price * reservation.bookedCount
                });

                _vendorRepository.EditTicket(foundTicket);

                return true;

            }

            return false;

        }

        public void CancelBooking(int id)
        {
            var found = _clientRepository.GetReservation(id);
            
            if (found != null)
            {
                var foundTicket = _vendorRepository.GetTicketById(found.ticketId);

                foundTicket.avillableCount += found.bookedCount;
                _clientRepository.CancelBooking(found);
                
            }
        }

        public IEnumerable<ReservationReadDto> GetClientBookings(string clientId)
        {
            var foundModel = _clientRepository.GetClientBookings(clientId).ToList();
            if (foundModel != null)
            {
                var found = foundModel.Select(a => new ReservationReadDto
                {
                    addressState = a.shippingAddress.state,
                    addressCity = a.shippingAddress.city,
                    addressStreet = a.shippingAddress.street,
                    bookedCount = a.bookedCount,
                    clientName = a.client.UserName,
                    eventCategory = a.ticket.Event.category,
                    eventDate = a.ticket.Event.date,
                    eventId = a.ticket.Event.id,
                    eventLocation = a.ticket.Event.location,
                    ticketCategory = a.ticket.Event.category,
                    totalPrice = a.totalPrice,
                    id = a.id

                }).ToList();

                return found;
            }

            return null;
        }

        public AddressReadDto GetMyAddress(string clientId)
        {
            var foundModel = _clientRepository.GetAddress(clientId);

            if (foundModel != null)
            {
                var found = new AddressReadDto
                {
                    State = !string.IsNullOrWhiteSpace(foundModel.address.state) ? foundModel.address.state : "No state Added yet",
                    Street = !string.IsNullOrWhiteSpace(foundModel.address.street) ? foundModel.address.street : "No street Added yet",
                    City = !string.IsNullOrWhiteSpace(foundModel.address.city) ? foundModel.address.city : "No city Added yet",
                    PostalCode = foundModel.address.postalCode,
                    id = foundModel.address.id,
                };

                return found;
            }

            return null;
        }

        public IEnumerable<TicketReadDto> GetEventTickets(int eventId)
        {
            var foundModel = _clientRepository.GetEventTickets(eventId).ToList();

            if (foundModel != null)
            {
                var found = foundModel.Select(a => new TicketReadDto
                {
                    status = a.status,
                    category = a.category,
                    price = a.price,
                    Id = a.id
                }).ToList();

                return found;
            }

            return null;
        }
    }
}