using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsReservationSystem.BLL.Dto_s
{
    public class FullDetailEntertainmentEventReadDto
    {
        public string performerName { get; set; }
        public string showCategory { get; set; }
        public int ageRestriction { get; set; }
        public double duration { get; set; }
        public string genre { get; set; }
        public string day { get; set; }
        public string mounth { get; set; }
        public string year { get; set; }
        public string location { get; set; }
        public int aviilableSeats { get; set; }
        public string eventImage { get; set; }

    }
}
