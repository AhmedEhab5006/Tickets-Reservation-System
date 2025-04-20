using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsReservationSystem.BLL.Dto_s.ControllerDto
{
    public class FullEventAddDto
    {
        public EventAddDto Event { get; set; }
        public EntertainmentEventAddDto? EntertainmentEvent { get; set; }
        public SportEventAddDto? SportsEvent { get; set; }
    }
}
