using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsReservationSystem.DAL.Models;

namespace TicketsReservationSystem.BLL.Dto_s
{
    public class TicketReadDto
    {
        public int Id { get; set; }
        public double price { get; set; }
        public int avillableNumber { get; set; }
        public string category { get; set; }
        public string status { get; set; }
        public int EventId { get; set; }
    }
}
