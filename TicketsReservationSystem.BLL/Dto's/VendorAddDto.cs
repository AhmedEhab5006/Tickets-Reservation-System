using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsReservationSystem.BLL.Dto_s
{
    public class VendorAddDto
    {
        public string id { get; set; }
        [DefaultValue("Pending")]
        public string acceptanceStatus { get; set; }
    }
}
