using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsReservationSystem.DAL.Models
{
    public class Vendor : ApplicationUser

    {
        public string acceptanceStatus {  get; set; }
        public ICollection<Event>? Events { get; set; }
    }
}
