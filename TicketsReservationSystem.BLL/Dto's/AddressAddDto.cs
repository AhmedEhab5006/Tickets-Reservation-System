using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsReservationSystem.BLL.Dto_s
{
    public class AddressAddDto
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int PostalCode { get; set; }

    }
}
