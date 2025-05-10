using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using TicketsReservationSystem.BLL.Dto_s;
using TicketsReservationSystem.DAL.Models;
using TicketsReservationSystem.DAL.Repository;

namespace TicketsReservationSystem.BLL.Managers
{
    public class VednorManager : IVendorManager
    {
        private IVendorRepository _vendorRepository;

        public VednorManager(IVendorRepository vendorRepository)
        {
            _vendorRepository = vendorRepository;
        }
        public void Add(VendorAddDto vendor)
        {
            _vendorRepository.Add(new Vendor
            {
                Id = vendor.id,
                acceptanceStatus = vendor.acceptanceStatus
            });
        }

        //public void AddEvent(EventAddDto Event, EntertainmentEventAddDto? entertaimentEvent = null, SportEventAddDto? SportsEvent = null)
        //{
        //    var added = _vendorRepository.AddEvent(new Event
        //    {
        //        numberOfSeats = Event.numberOfSeats,
        //        vendorId = Event.vendorId,
        //        date = Event.date,
        //        location = Event.location,
        //        category = Event.category,
        //        avillableSeats = Event.numberOfSeats,
        //        status = Event.status
        //    });

        //    if (Event.category == "Sport")
        //    {
        //        _vendorRepository.AddSportsEvent(new SportEvent
        //        {
        //            team1 = SportsEvent.team1,
        //            team2 = SportsEvent.team2,
        //            tournament = SportsEvent.tournament,
        //            tournamentStage = SportsEvent.tournamentStage,
        //            sport = SportsEvent.sport,
        //            EventId = added,
        //            team1Image = SportsEvent.team1Image,
        //            team2Image = SportsEvent.team2Image,
        //        });
        //    }

        //   if (Event.category == "Entertainment")
        //    {
        //        _vendorRepository.AddEntertainmentEvent(new EntertainmentEvent
        //        {
        //            performerName = entertaimentEvent.performerName,
        //            genre = entertaimentEvent.genre,
        //            ageRestriction = entertaimentEvent.ageRestriction,
        //            duration = entertaimentEvent.duration,
        //            showCategory = entertaimentEvent.showCategory,
        //            EventId = added,
        //            eventImage = entertaimentEvent.eventImage
        //        });

        //    }
        //}


        public void AddSportEvent (SportAddDto sportAdd)
        {
            var added = _vendorRepository.AddEvent(new Event
            {
                status = sportAdd.status,
                avillableSeats = sportAdd.numberOfSeats,
                category = sportAdd.category,
                vendorId = sportAdd.vendorId,
                date = sportAdd.date,
                location = sportAdd.location,
                numberOfSeats = sportAdd.numberOfSeats,
            });

            _vendorRepository.AddSportsEvent(new SportEvent
            {
                tournamentStage = sportAdd.tournamentStage,
                sport = sportAdd.sport,
                EventId = added,
                team1 = sportAdd.team1,
                team2 = sportAdd.team2,
                team1Image = sportAdd.team1Image,
                team2Image = sportAdd.team2Image,
                tournament = sportAdd.tournament,
            });
        }

        public void AddEntertainmentEvent (EntertainmentAddDto entertainmentAdd)
        {
            var added = _vendorRepository.AddEvent(new Event
            {
                status = entertainmentAdd.status,
                avillableSeats = entertainmentAdd.numberOfSeats,
                category = entertainmentAdd.category,
                vendorId = entertainmentAdd.vendorId,
                date = entertainmentAdd.date,
                location = entertainmentAdd.location,
                numberOfSeats = entertainmentAdd.numberOfSeats,
            });

            _vendorRepository.AddEntertainmentEvent(new EntertainmentEvent
            {
                showCategory = entertainmentAdd.showCategory,
                ageRestriction = entertainmentAdd.ageRestriction,
                duration = entertainmentAdd.duration,
                EventId = added,
                eventImage = entertainmentAdd.eventImage,
                genre = entertainmentAdd.genre,
                performerName = entertainmentAdd.performerName,
            });

        }


        public void DeleteEvent(int id)
        {
            var found = _vendorRepository.GetEventById(id);
            if (found != null)
            {
                _vendorRepository.DeleteEvent(found);
            }
        }

        public void EditEntertainmentEvent(EntertainmentEventUpdateDto entertainmentEventUpdateDto)
        {
            var found = _vendorRepository.GetEntertainmentEventById(entertainmentEventUpdateDto.id);
            if (found != null)
            {

                found.genre = !string.IsNullOrWhiteSpace(entertainmentEventUpdateDto.genre) ? entertainmentEventUpdateDto.genre : found.genre;
                found.ageRestriction = entertainmentEventUpdateDto.ageRestriction > 0 ? entertainmentEventUpdateDto.ageRestriction : found.ageRestriction;
                found.duration = entertainmentEventUpdateDto.duration > 0 ? entertainmentEventUpdateDto.duration : found.duration;
                found.performerName = !string.IsNullOrWhiteSpace(entertainmentEventUpdateDto.performerName) ? entertainmentEventUpdateDto.performerName : found.performerName;
                found.showCategory = !string.IsNullOrWhiteSpace(entertainmentEventUpdateDto.showCategory) ? entertainmentEventUpdateDto.showCategory : found.showCategory;
                found.eventImage = !string.IsNullOrEmpty(entertainmentEventUpdateDto.eventImage) ? entertainmentEventUpdateDto.eventImage : found.eventImage;

                _vendorRepository.EditEntertainmentEvent(found);
            }
        }

        public void EditEvent(EventUpdateDto Event)
        {
            var found = _vendorRepository.GetEventById(Event.id);
            if (found != null)
            {

                found.date = Event.date != default ? Event.date : found.date;
                found.location = !string.IsNullOrWhiteSpace(Event.location) ? Event.location : found.location;
                found.numberOfSeats = Event.numberOfSeats > 0 ? Event.numberOfSeats : found.numberOfSeats;
                found.avillableSeats = found.numberOfSeats - found.bookedSeats;

                _vendorRepository.EditEvent(found);
            }

        }

        public void EditSportsEvent(SportEventUpdateDto SportsEvent)
        {
            var found = _vendorRepository.GetSportEventById(SportsEvent.id);
            if (found != null)
            {

                found.tournamentStage = !string.IsNullOrWhiteSpace(SportsEvent.tournamentStage) ? SportsEvent.tournamentStage : found.tournamentStage;
                found.team1 = !string.IsNullOrWhiteSpace(SportsEvent.team1) ? SportsEvent.team1 : found.team1;
                found.team2 = !string.IsNullOrWhiteSpace(SportsEvent.team2) ? SportsEvent.team2 : found.team2;
                found.tournament = !string.IsNullOrWhiteSpace(SportsEvent.tournament) ? SportsEvent.tournament : found.tournament;
                found.sport = !string.IsNullOrWhiteSpace(SportsEvent.sport) ? SportsEvent.sport : found.sport;
                found.team1Image = !string.IsNullOrWhiteSpace(SportsEvent.team1Image) ? SportsEvent.team1Image : found.team1Image;
                found.team2Image = !string.IsNullOrWhiteSpace(SportsEvent.team2Image) ? SportsEvent.team2Image : found.team2Image;

                _vendorRepository.EditSportsEvent(found);
            }

        }

        public EntertainmentEventReadDto GetEntertainmentEventById(int id)
        {
            var foundModel = _vendorRepository.GetEntertainmentEventById(id);

            if (foundModel != null)
            {
                var found = new EntertainmentEventReadDto
                {
                    performerName = foundModel.performerName,
                    duration = foundModel.duration,
                    genre = foundModel.genre,
                    ageRestriction = foundModel.ageRestriction,
                    showCategory = foundModel.showCategory,
                    EventId = foundModel.EventId,
                    eventImage = foundModel.eventImage,

                };

                return found;
            }

            return null;
        }

        public EventReadDto GetEventById(int id)
        {
            var foundModel = _vendorRepository.GetEventById(id);

            if (foundModel != null)
            {
                var found = new EventReadDto
                {
                    avillableSeats = foundModel.avillableSeats,
                    category = foundModel.category,
                    date = foundModel.date,
                    location = foundModel.location,
                    vendorId = foundModel.vendorId,
                };
                return found;
            }

            return null;

        }

        public SportEventReadDto GetSportEventById(int id)
        {
            var foundModel = _vendorRepository.GetSportEventById(id);

            if (foundModel != null)
            {
                var found = new SportEventReadDto
                {
                    sport = foundModel.sport,
                    team1 = foundModel.team1,
                    team2 = foundModel.team2,
                    tournament = foundModel.tournament,
                    tournamentStage = foundModel.tournamentStage,
                    EventId = foundModel.EventId,
                    team1Image = foundModel.team1Image,
                    team2Image = foundModel.team2Image,
                };
                return found;
            }

            return null;
        }

        public VendorReadDto GetById(string id)
        {
            var foundModel = _vendorRepository.GetById(id);


            if (foundModel != null)
            {
                var found = new VendorReadDto
                {
                    id = foundModel.Id,
                    acceptanceStatus = foundModel.acceptanceStatus,
                };

                return found;

            }

            return null;

        }

        public int ShowBookings(int eventId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FullDetailEntertainmentEventReadDto> GetMyEntertainmentEvent(string id)
        {
            var foundModel = _vendorRepository.GetMyEntertainmentEvents(id).ToList();


            if (foundModel != null)
            {
                var found = foundModel.Select(a => new FullDetailEntertainmentEventReadDto
                {
                    ageRestriction = a.entertainment.ageRestriction,
                    duration = a.entertainment.duration,
                    performerName = a.entertainment.performerName,
                    genre = a.entertainment.genre,
                    showCategory = a.entertainment.showCategory,
                    aviilableSeats = a.avillableSeats,
                    day = a.date.Day.ToString(),
                    mounth = a.date.Month.ToString(),
                    year = a.date.Year.ToString(),
                    location = a.location,
                    eventImage = a.entertainment.eventImage

                }).ToList();
                return found;
            }

            return null;

        }

        public IEnumerable<FullDetailSportEventReadDto> GetMySportEvent(string id)
        {
            var foundModel = _vendorRepository.GetMySportEvents(id).ToList();


            if (foundModel != null)
            {
                var found = foundModel.Select(a => new FullDetailSportEventReadDto
                {
                    sport = a.sportEvent.sport,
                    team1 = a.sportEvent.team1,
                    team2 = a.sportEvent.team2,
                    tournament = a.sportEvent.tournament,
                    tournamentStage = a.sportEvent.tournamentStage,
                    aviilableSeats = a.avillableSeats,
                    day = a.date.Day.ToString(),
                    mounth = a.date.Month.ToString(),
                    year = a.date.Year.ToString(),
                    location = a.location,
                    team1Image = a.sportEvent.team1Image,
                    team2Image = a.sportEvent.team2Image,

                }).ToList();
                return found;
            }

            return null;

        }

        public bool AddTicket(TicketAddDto ticketAddDto)
        {
            var eventTickets = _vendorRepository.GetMyEventTickets(ticketAddDto.EventId).ToList();
            var targetEvent = _vendorRepository.GetEventById(ticketAddDto.EventId);

            var avillableSeatsCount = eventTickets.Sum(a => a.avillableCount);

            if (ticketAddDto.avillableNumber + avillableSeatsCount <= targetEvent.avillableSeats)
            {
                _vendorRepository.AddTicket(new Ticket
                {
                    avillableCount = ticketAddDto.avillableNumber,
                    category = ticketAddDto.category,
                    status = ticketAddDto.status,
                    EventId = ticketAddDto.EventId,
                    price = ticketAddDto.price,
                });
                return true;
            }
            return false;
        }



        public bool UpdateTicket(TicketUpdateDto ticketUpdateDto)
        {
            var foundModel = _vendorRepository.GetTicketById(ticketUpdateDto.Id);
            if (foundModel != null)
            {
                var eventTickets = _vendorRepository.GetMyEventTickets(foundModel.EventId).ToList();
                var targetEvent = _vendorRepository.GetEventById(foundModel.EventId);

                var avillableSeatsCount = eventTickets.Sum(a => a.avillableCount);
                if (ticketUpdateDto.avillableNumber + avillableSeatsCount <= targetEvent.avillableSeats)
                {
                    foundModel.price = ticketUpdateDto.price > 0 ? ticketUpdateDto.price : foundModel.price;
                    foundModel.avillableCount = ticketUpdateDto.avillableNumber > 0 ? ticketUpdateDto.avillableNumber : foundModel.avillableCount;
                    foundModel.category = !string.IsNullOrWhiteSpace(ticketUpdateDto.category) ? ticketUpdateDto.category : foundModel.category;

                    return true;
                }

            }

            return false;
        }

        public void DeleteTicket(int id)
        {
            var found = _vendorRepository.GetTicketById(id);
            if (found != null)
            {
                _vendorRepository.DeleteTicket(found);
            }
        }

        public TicketReadDto GetTicketById(int id)
        {
            var foundModel = _vendorRepository.GetTicketById(id);

            if (foundModel != null)
            {
                var returned = new TicketReadDto
                {
                    avillableNumber = foundModel.avillableCount,
                    category = foundModel.category,
                    status = foundModel.status,
                    price = foundModel.price,
                };

                return returned;
            }

            return null;
        }

        public string GetAcceptanceStatus(string vendorId)
        {
            var found = _vendorRepository.GetAcceptanceStatus(vendorId);
            return found;
        }
    }
}


