using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsReservationSystem.DAL.Models
{
    public class SportEvent
    {
        [Key]
        public int EventId { get; set; }
        public Event? Event { get; set; }
        public string team1 { get; set; }
        public string team1Image { get; set; }
        public string team2 { get; set; }
        public string team2Image { get; set; }
        public string tournament {  get; set; }
        public string sport { get; set; }
        public string tournamentStage { get; set; }

    }
}
