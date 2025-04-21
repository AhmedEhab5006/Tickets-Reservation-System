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
    public class VednorManager : IVendorManager
    {
        private IVendorRepository _vendorRepository;

        public VednorManager(IVendorRepository vendorRepository)
        {
            _vendorRepository = vendorRepository;
        }
        public void Add(VendorAddDto vendor, int userId)
        {
            _vendorRepository.Add(new Vendor
            {
                userId = userId,
                acceptanceStatus = vendor.acceptanceStatus
            });
        }

        public void AddEvent(EventAddDto Event, EntertainmentEventAddDto? entertaimentEvent = null, SportEventAddDto? SportsEvent = null)
        {
            var added = _vendorRepository.AddEvent(new Event
            {
                numberOfSeats = Event.numberOfSeats,
                vendorId = Event.vendorId,
                date = Event.date,
                location = Event.location,
                category = Event.category,
                avillableSeats = Event.numberOfSeats,
                status = Event.status
            });

            if (Event.category == "Sport")
            {
                _vendorRepository.AddSportsEvent(new SportEvent
                {
                    team1 = SportsEvent.team1,
                    team2 = SportsEvent.team2,
                    tournament = SportsEvent.tournament,
                    tournamentStage = SportsEvent.tournamentStage,
                    sport = SportsEvent.sport,
                    EventId = added
                });
            }

            if (Event.category == "Entetainment")
            {
                _vendorRepository.AddEntertainmentEvent(new EntertainmentEvent
                {
                    performerName = entertaimentEvent.performerName,
                    genre = entertaimentEvent.genre,
                    ageRestriction = entertaimentEvent.ageRestriction,
                    duration = entertaimentEvent.duration,
                    showCategory = entertaimentEvent.showCategory,
                    EventId = added
                });

            }
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

                _vendorRepository.EditSportsEvent(found);
            }

        }

        public VendorReadDto GetByUserId(int id)
        {
            var foundModel = _vendorRepository.GetByUserId(id);
            var found = new VendorReadDto
            {
                id = foundModel.Id,
                userId = foundModel.userId,
                acceptanceStatus = foundModel.acceptanceStatus,
            };
            return found;
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
                };
                return found;
            }

            return null;
        }

        public VendorReadDto GetById(int id)
        {
            var foundModel = _vendorRepository.GetById(id);


            if (foundModel != null)
            {
                var found = new VendorReadDto
                {
                    id = foundModel.Id,
                    acceptanceStatus = foundModel.acceptanceStatus,
                    userId = foundModel.userId,
                };

                return found;

            }

            return null;

        }

        public int ShowBookings(int eventId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FullDetailEntertainmentEventReadDto> GetMyEntertainmentEvent(int id)
        {
            var foundModel = _vendorRepository.GetMyEntertainmentEvents(id);

            
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
                    location = a.location

                }).ToList();
                return found;
            }

            return null; 
            
        }

        public IEnumerable<FullDetailSportEventReadDto> GetMySportEvent(int id)
        {
            var foundModel = _vendorRepository.GetMySportEvents(id);

            
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
                    location = a.location

                }).ToList();
                return found;
            }

            return null;
            
        }
    }
    }

