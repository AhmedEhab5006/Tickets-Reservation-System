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

        public VednorManager(IVendorRepository vendorRepository) {
            _vendorRepository = vendorRepository;
        }
        public void Add(VendorAddDto vendor , int userId)
        {
            _vendorRepository.Add(new Vendor
            {
                userId = userId,
                acceptanceStatus = vendor.acceptanceStatus
            });
        }

        public void AddEntertainmentEvent(EntertainmentEvent Event)
        {
            throw new NotImplementedException();
        }

        public void AddEvent(Event Event)
        {
            throw new NotImplementedException();
        }

        public void AddSportsEvent(SportEvent SportsEvent)
        {
            throw new NotImplementedException();
        }

        public void DeleteEvent(int id)
        {
            throw new NotImplementedException();
        }

        public void EditEntertainmentEvent(EntertainmentEvent EntertainmentEvent)
        {
            throw new NotImplementedException();
        }

        public void EditEvent(Event Event)
        {
            throw new NotImplementedException();
        }

        public void EditSportsEvent(SportEvent sportEvent)
        {
            throw new NotImplementedException();
        }

        public int ShowBookings(int eventId)
        {
            throw new NotImplementedException();
        }
    }
}
