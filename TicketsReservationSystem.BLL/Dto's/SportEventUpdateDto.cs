using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsReservationSystem.DAL.Models;

namespace TicketsReservationSystem.BLL.Dto_s
{
    public class SportEventUpdateDto
    {
        public int Id { get; set; }
        public string? team1 { get; set; }
        public string? team2 { get; set; }
        public string? tournament { get; set; }
        public string? sport { get; set; }
        public string? tournamentStage { get; set; }
        public string? day { get; set; }
        public string? mounth { get; set; }
        public string? year { get; set; }
        public DateTime date { get; set; }
        public string? location { get; set; }
        public int aviilableSeats { get; set; }
        public string? team1Image { get; set; }
        public string? team2Image { get; set; }
    }
}
