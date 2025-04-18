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
        public int clientId { get; set; }
        public Client? client {get; set;}
        public int shippingAddressId {  get; set; }
        public Address? shippingAddress { get; set; }
        public double price {  get; set; }
        public int seatNumber { get; set; }
        public string category { get; set; }
        public string status { get; set; }
    }
}
