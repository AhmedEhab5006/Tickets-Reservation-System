using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsReservationSystem.BLL.Dto_s
{
    public class TicketReadDto
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string EventName { get; set; }
        public double Price { get; set; }
        public int SeatNumber { get; set; }
        public string Status { get; set; }
    }
}
