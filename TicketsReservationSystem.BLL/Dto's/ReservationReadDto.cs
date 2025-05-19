using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsReservationSystem.BLL.Dto_s
{
    public class ReservationReadDto
    {
        public int id { get; set; }
        public string clientName {  get; set; }
        public string addressStreet { get; set; }
        public string addressCity { get; set; }
        public string addressState { get; set; }
        public string ticketCategory { get; set; }
        public double totalPrice { get; set; }
        public string eventLocation { get; set; }
        public DateTime eventDate { get; set; }
        public string eventCategory { get; set; }
        public int eventId { get; set; }
        public int bookedCount { get; set; }

    }
}
