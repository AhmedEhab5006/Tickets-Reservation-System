using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsReservationSystem.DAL.Models
{
    public class Client : ApplicationUser
    {
        public int addressId { get; set; }
        public Address? address { get; set; }
        public ICollection<Ticket>? tickets { get; set; }
        public ICollection<Address>? addresses { get; set; }

    }
}
