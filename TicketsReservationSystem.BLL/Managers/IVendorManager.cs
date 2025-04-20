using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsReservationSystem.BLL.Dto_s;
using TicketsReservationSystem.DAL.Models;

namespace TicketsReservationSystem.BLL.Managers
{
    public interface IVendorManager
    {
        public void Add(VendorAddDto vendor , int userId);
        public void AddEvent(EventAddDto Event, EntertainmentEventAddDto? entertaimentEvent = null, SportEventAddDto? SportsEvent = null);
        public void EditEvent(EventUpdateDto Event);
        public void EditEntertainmentEvent(EntertainmentEventUpdateDto entertainmentEventUpdateDto);
        public void EditSportsEvent(SportEventUpdateDto SportsEvent);
        public EventReadDto GetEventById(int id);
        public EntertainmentEventReadDto GetEntertainmentEventById(int id);
        public SportEventReadDto GetSportEventById(int id);
        public void DeleteEvent(int id);
        public int ShowBookings(int eventId);
        public VendorReadDto GetByUserId(int id);

    }
}
