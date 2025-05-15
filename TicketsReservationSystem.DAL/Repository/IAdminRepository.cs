using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsReservationSystem.DAL.Models;

namespace TicketsReservationSystem.DAL.Repository
{
    public interface IAdminRepository
    {
        public IQueryable<ApplicationUser> GetAllPendingVendors();
        public void AddAdmin(ApplicationUser admin);
        public Vendor ConfirmVendor(string vendorId);
        public Vendor RejectVendor(string vendorId);
        public IQueryable<Event> GetPendingSportEvents();
        public IQueryable<Event> GetPendingEntertainmentEvents();
        public void AcceptEvent(Event Event);
        public void RejectEvent(Event Event);

    }
}

