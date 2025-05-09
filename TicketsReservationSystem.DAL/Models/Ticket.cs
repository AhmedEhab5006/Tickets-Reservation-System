using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsReservationSystem.DAL.Models
{
    public class Ticket
    {
        public int id { get; set; }
        public int EventId { get; set; }
        public Event? Event { get; set; }
        public double price {  get; set; }
        public int avillableCount { get; set; }
        public string category { get; set; }
        public string status { get; set; }
        // NEW: Link ticket to a client (who booked it)
        public string? ClientId { get; set; }
        public Client? Client { get; set; }
    }
}
