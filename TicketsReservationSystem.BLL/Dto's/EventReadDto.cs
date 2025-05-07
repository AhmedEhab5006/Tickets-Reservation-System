using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsReservationSystem.BLL.Dto_s
{
    public class EventReadDto
    {
        public string vendorId { get; set; }
        public string category { get; set; }
        public DateTime date { get; set; }
        public string location { get; set; }
        public int avillableSeats { get; set; }
    }
}
