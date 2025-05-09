using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsReservationSystem.DAL.Models
{
    public class EntertainmentEvent
    {
        public int id {  get; set; }
        public int EventId { get; set; }
        public Event? Event { get; set; }
        public string performerName { get; set; }
        public string eventImage { get; set; }
        public string showCategory { get; set; }
        public int ageRestriction { get; set; }
        public double duration { get; set; }
        public string genre { get; set; }
    }
}
