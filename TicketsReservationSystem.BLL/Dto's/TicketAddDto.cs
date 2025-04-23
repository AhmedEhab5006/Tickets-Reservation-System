using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsReservationSystem.BLL.Dto_s
{
    public class TicketAddDto
    {
        public int EventId { get; set; }
        public double price { get; set; }
        public int avillableNumber { get; set; }
        public string category { get; set; }
        public string status = "Availlable";
    }
}
