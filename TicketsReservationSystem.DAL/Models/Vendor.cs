using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsReservationSystem.DAL.Models
{
    public class Vendor
    {
        
        public int Id { get; set; }
        public int userId { get; set; }
        public User? user {  get; set; }
        public string acceptanceStatus {  get; set; }
        public ICollection<Event>? Events { get; set; }
    }
}
