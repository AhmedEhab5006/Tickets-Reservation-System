using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsReservationSystem.BLL.Dto_s
{
    public class EntertainmentAddDto
    {

        public string? vendorId { get; set; }
        public string category { get; set; }
        public DateTime date { get; set; }
        public string location { get; set; }
        public int numberOfSeats { get; set; }
        public string status = "Pending";
        public int EventId { get; set; }
        public string performerName { get; set; }
        public string showCategory { get; set; }
        public int ageRestriction { get; set; }
        public double duration { get; set; }
        public string genre { get; set; }
        public string eventImage { get; set; }
    }
}
