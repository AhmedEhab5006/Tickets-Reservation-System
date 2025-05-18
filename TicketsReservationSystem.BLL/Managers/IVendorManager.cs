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
        public void Add(VendorAddDto vendor);
        public void EditEvent(EventUpdateDto Event);
        public void EditEntertainmentEvent(EntertainmentEventUpdateDto entertainmentEventUpdateDto);
        public void EditSportsEvent(SportEventUpdateDto SportsEvent);
        public EventReadDto GetEventById(int id);
        public EntertainmentEventReadDto GetEntertainmentEventById(int id);
        public SportEventReadDto GetSportEventById(int id);
        public void DeleteEvent(int id);
        public int ShowBookings(int eventId);
        public VendorReadDto GetById(string id);
        public IEnumerable<FullDetailEntertainmentEventReadDto> GetMyEntertainmentEvent(string id);
        public IEnumerable<FullDetailSportEventReadDto> GetMySportEvent(string id);
        public bool AddTicket(TicketAddDto ticketAddDto);
        public bool UpdateTicket(TicketUpdateDto ticketUpdateDto);
        public void DeleteTicket(int id);
        public TicketReadDto GetTicketById(int id);
        public string GetAcceptanceStatus(string vendorId);
        public void AddSportEvent(SportAddDto sportAdd);
        public void AddEntertainmentEvent(EntertainmentAddDto entertainmentAdd);
        public IEnumerable<TicketReadDto> GetMyTicket(string vendorId);
    }
}
