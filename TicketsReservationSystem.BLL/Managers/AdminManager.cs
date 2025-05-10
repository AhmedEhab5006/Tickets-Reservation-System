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
    public class AdminManager : IAdminManager
    {
        private IAdminRepository _adminRepository;
        private IVendorRepository _vendorRepository;

        public AdminManager(IAdminRepository adminRepository , IVendorRepository vendorRepository)
        {
            _adminRepository = adminRepository;
            _vendorRepository = vendorRepository;
        }

        public bool AcceptEvent(int eventId)
        {
           var found = _vendorRepository.GetEventById(eventId);
            if (found != null)
            {
                found.status = "Accepted";
                _adminRepository.AcceptEvent(found);
                return true;
            }
            return false;
            
        }

        public void AddAdmin(ApplicationUser admin)
        {
            throw new NotImplementedException();
        }

        public bool ConfirmVendor(string vendorId)
        {
            var found = _adminRepository.ConfirmVendor(vendorId);
            if (found != null)
            {
                found.acceptanceStatus = "Accepted";
                return true;
            }
            return false;
        }

        public IEnumerable<ApplicationUser> GetAllPendingVendors()
        {
            var found = _adminRepository.GetAllPendingVendors().ToList();
            return found;
        }

        public IEnumerable<FullDetailEntertainmentEventReadDto> GetPendingEntertainmentEvents()
        {
            var foundModel = _adminRepository.GetPendingEntertainmentEvents().ToList();

            if (foundModel != null)
            {
                var found = foundModel.Select(a => new FullDetailEntertainmentEventReadDto
                {
                    showCategory = a.entertainment.showCategory,
                    aviilableSeats = a.avillableSeats,
                    ageRestriction = a.entertainment.ageRestriction,
                    day = a.date.Day.ToString(),
                    mounth = a.date.Month.ToString(),
                    year = a.date.Year.ToString(),
                    duration = a.entertainment.duration,
                    eventImage = a.entertainment.eventImage,
                    genre = a.entertainment.genre,
                    location = a.location,
                    performerName = a.entertainment.performerName
                }).ToList();

                return found;
            }

            return null;
        }

        public IEnumerable<FullDetailSportEventReadDto> GetPendingSportEvents()
        {
            var foundModel = _adminRepository.GetPendingSportEvents().ToList();

            if (foundModel != null)
            {
                var found = foundModel.Select(a => new FullDetailSportEventReadDto
                {
                    sport = a.sportEvent.sport,
                    aviilableSeats = a.avillableSeats,
                    day = a.date.Day.ToString(),
                    mounth = a.date.Month.ToString(),
                    year= a.date.Year.ToString(),
                    tournamentStage = a.sportEvent.tournamentStage,
                    location = a.location,
                    team1 = a.sportEvent.team1,
                    team2 = a.sportEvent.team2,
                    team1Image = a.sportEvent.team1Image,
                    team2Image = a.sportEvent.team2Image,
                    tournament = a.sportEvent.tournament
                }).ToList();

                return found;
            }

            return null;
        }

        public bool RejectEvent(int eventId)
        {
            var found = _vendorRepository.GetEventById(eventId);
            if (found != null)
            {
                found.status = "Rejected";
                _adminRepository.AcceptEvent(found);
                return true;
            }

            return false;
        }

        public bool RejectVendor(string vendorId)
        {
            var found = _adminRepository.ConfirmVendor(vendorId);
            if (found != null)
            {
                found.acceptanceStatus = "Rejected";
                return true;
            }
            return false;
        }
    }
}
