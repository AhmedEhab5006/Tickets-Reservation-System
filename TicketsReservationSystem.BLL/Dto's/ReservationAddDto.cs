using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsReservationSystem.DAL.Models;

namespace TicketsReservationSystem.BLL.Dto_s
{
    public class ReservationAddDto
    {
        public string? clientId { get; set; }
        public int ticketId { get; set; }
        public int shippingAddressId { get; set; }
        [Range(1 , 5 , ErrorMessage = "You Can't book more than five ticket at one time")]    
        public int bookedCount { get; set; }
    }
}
