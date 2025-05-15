using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsReservationSystem.BLL.Dto_s
{
    public class ClientBookingDto
    {
        public int TicketId { get; set; }
        public int EventId { get; set; }

        // From Event
        public DateTime EventDate { get; set; }
        public string EventLocation { get; set; }
        public int BookedSeats { get; set; }
        public string EventStatus { get; set; }
        public string EventCategory { get; set; }

        // From Ticket
        public double TicketPrice { get; set; }
        public string TicketStatus { get; set; }

    }
}
