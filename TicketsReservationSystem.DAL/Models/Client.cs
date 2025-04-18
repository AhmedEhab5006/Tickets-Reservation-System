using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsReservationSystem.DAL.Models
{
    public class Client
    {
        
        public int id {  get; set; }
        public int userId { get; set; }
        public User? user { get; set; }
        public int addressId { get; set; }
        public Address? address { get; set; }
        public ICollection<Ticket>? tickets { get; set; }

    }
}
