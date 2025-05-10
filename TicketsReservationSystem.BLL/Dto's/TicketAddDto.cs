using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsReservationSystem.BLL.Dto_s
{
    public class TicketAddDto
    {
        [Required(ErrorMessage = "Missing Event id")]
        public int EventId { get; set; }
        [Required(ErrorMessage = "Missing Price")]
        [DefaultValue(200)]
        [Range(50 , 5000 , ErrorMessage = "Ticket Price must be between 50 and 5000 EGP")]
        public double price { get; set; }
        [Required(ErrorMessage = "Missing Available number")]
        [DefaultValue(100)]
        public int avillableNumber { get; set; }
        [Required(ErrorMessage = "Missing Category")]
        public string category { get; set; }
        [Required(ErrorMessage = "Missing Status")]
        [DefaultValue("Availlable")]
        public string status {  get; set; }
    }
}
