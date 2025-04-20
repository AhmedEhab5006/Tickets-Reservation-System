using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsReservationSystem.DAL.Models;

namespace TicketsReservationSystem.BLL.Dto_s
{
    public class EntertainmentEventUpdateDto
    {
        public int id { get; set; }
        public string? performerName { get; set; }
        public string? showCategory { get; set; }
        public int ageRestriction { get; set; }
        public double duration { get; set; }
        public string? genre { get; set; }
    }
}
