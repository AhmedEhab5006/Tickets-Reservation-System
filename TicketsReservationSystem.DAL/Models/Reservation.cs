using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsReservationSystem.DAL.Models
{
    public class Reservation
    {
        public int id { get; set; }
        public string clientId { get; set; }
        public Client? client { get; set; }
        public int ticketId { get; set; }
        public Ticket? ticket { get; set; }  
        public int shippingAddressId { get; set; }
        public Address? shippingAddress { get; set; }
        [Range(1, 5)]
        public int bookedCount { get; set; }
    }
}
