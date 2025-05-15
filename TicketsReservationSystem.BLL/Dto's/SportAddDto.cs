using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsReservationSystem.BLL.Dto_s
{
    public class SportAddDto
    {
        public string? vendorId { get; set; }
        [DefaultValue("Sport")]
        public string category {  get; set; }
        [Required(ErrorMessage = "Missing Date")]
        public DateTime date { get; set; }
        [Required(ErrorMessage = "Missing Location")]
        public string location { get; set; }
        [Required(ErrorMessage = "Missing Number of seats")]
        [DefaultValue(5000)]
        [Range(5000 , 200000 , ErrorMessage = "Number of seats must be between 5000 and 200000")]
        public int numberOfSeats { get; set; }
        [DefaultValue("Pending")]
        public string status {  get; set; }
        [Required(ErrorMessage = "Missing EventId")]
        public int EventId { get; set; }
        [Required(ErrorMessage = "Missing team1 name")]
        public string team1 { get; set; }
        [Required(ErrorMessage = "Missing team2 name")]
        public string team2 { get; set; }
        [Required(ErrorMessage = "Missing team1 image")]
        public string team1Image { get; set; }
        [Required(ErrorMessage = "Missing team2 image")]
        public string team2Image { get; set; }
        [Required(ErrorMessage = "Missing tournament")]
        public string tournament { get; set; }
        [Required(ErrorMessage = "Missing sport")]
        public string sport { get; set; }
        [Required(ErrorMessage = "Missing tournamentStage")]
        public string tournamentStage { get; set; }
    }
}
