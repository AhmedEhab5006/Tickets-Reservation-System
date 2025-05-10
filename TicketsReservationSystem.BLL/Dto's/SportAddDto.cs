using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsReservationSystem.BLL.Dto_s
{
    public class SportAddDto
    {
        public string? vendorId { get; set; }
        public string category { get; set; }
        public DateTime date { get; set; }
        public string location { get; set; }
        public int numberOfSeats { get; set; }
        public string status = "Pending";
        public int EventId { get; set; }
        public string team1 { get; set; }
        public string team2 { get; set; }
        public string team1Image { get; set; }
        public string team2Image { get; set; }
        public string tournament { get; set; }
        public string sport { get; set; }
        public string tournamentStage { get; set; }
    }
}
