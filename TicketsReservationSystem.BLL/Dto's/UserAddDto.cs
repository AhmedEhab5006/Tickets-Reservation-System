using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsReservationSystem.BLL.Dto_s
{
    public class UserAddDto
    {
        public string firstname {  get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public string phoneNumber { get; set; }
    }
}
