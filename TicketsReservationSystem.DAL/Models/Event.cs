using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsReservationSystem.DAL.Models
{
    public class Event
    {
        public int id { get; set; }
        public int vendorId { get; set; }
        public Vendor? vendor { get; set; }
        public DateTime date { get; set; }
        public string location { get; set; }
        public int numberOfSeats { get; set; }
        public int avillableSeats { get; set; }
        public int bookedSeats { get; set; }
        public string status { get; set; }
        public string category { get; set; }
        public ICollection<Ticket>? Tickets {get; set;}
        public EntertainmentEvent? entertainment { get; set; }
        public SportEvent? sportEvent { get; set; }

    }
}
