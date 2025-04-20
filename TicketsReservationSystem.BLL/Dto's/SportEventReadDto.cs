using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsReservationSystem.DAL.Models;

namespace TicketsReservationSystem.BLL.Dto_s
{
    public class SportEventReadDto
    {
        public int EventId { get; set; }
        public string team1 { get; set; }
        public string team2 { get; set; }
        public string tournament { get; set; }
        public string sport { get; set; }
        public string tournamentStage { get; set; }
    }
}
