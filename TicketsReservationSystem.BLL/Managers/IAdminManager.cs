using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsReservationSystem.BLL.Dto_s;
using TicketsReservationSystem.DAL.Models;

namespace TicketsReservationSystem.BLL.Managers
{
    public interface IAdminManager
    {
        public IEnumerable<ApplicationUser> GetAllPendingVendors();
        public void AddAdmin(ApplicationUser admin);
        public bool ConfirmVendor(string vendorId);
        public bool RejectVendor(string vendorId);
        public IEnumerable<FullDetailSportEventReadDto> GetPendingSportEvents();
        public IEnumerable<FullDetailEntertainmentEventReadDto> GetPendingEntertainmentEvents();
        public bool AcceptEvent(int eventId);
        public bool RejectEvent(int eventId);
    }
}
