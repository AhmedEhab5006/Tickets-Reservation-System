using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsReservationSystem.BLL.Dto_s
{
    public class EntertainmentAddDto
    {

        public string? vendorId { get; set; }
        [Required(ErrorMessage = "Missing Category")]
        [DefaultValue("Entertainment")]
        public string category { get; set; }
        [Required(ErrorMessage = "Missing Date")]
        public DateTime date { get; set; }
        [Required(ErrorMessage = "Missing Location")]
        public string location { get; set; }
        [Required(ErrorMessage = "Missing number of seats")]
        [DefaultValue(5000)]
        [Range(5000, 200000, ErrorMessage = "Number of seats must be between 5000 and 200000")]
        public int numberOfSeats { get; set; }
        [DefaultValue("Pending")]
        public string status {  get; set; }
        [Required(ErrorMessage = "Missing event id")]
        public int EventId { get; set; }
        [Required(ErrorMessage = "Performer name")]
        public string performerName { get; set; }
        [Required(ErrorMessage = "Missing show category")]
        public string showCategory { get; set; }
        [Required(ErrorMessage = "Missing age restriction")]
        public int ageRestriction { get; set; }
        [Required(ErrorMessage = "Missing duration")]
        public double duration { get; set; }
        [Required(ErrorMessage = "Missing genre")]
        public string genre { get; set; }
        [Required(ErrorMessage = "Missing event image")]
        public string eventImage { get; set; }
    }
}
