using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsReservationSystem.DAL.Models
{
    public class Address
    {
        public int id { get; set; }
        public int? clientId { get; set; }
        public Client client { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public int postalCode { get; set; }
        public Ticket? ticket { get; set; }
    }
}
