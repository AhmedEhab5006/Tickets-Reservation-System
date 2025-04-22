using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsReservationSystem.BLL.Dto_s
{
    public class ClientReadDto
    {
        public int Id { get; set; } // Client ID
        public int UserId { get; set; } // Associated User ID
        public string? UserName { get; set; } // Full name of the user (e.g., "John Doe")
        public int AddressId { get; set; } // Associated Address ID
        public AddressReadDto? Address { get; set; } // Address details
        public ICollection<TicketReadDto>? Tickets { get; set; } // List of tickets associated with the client
    }
}
