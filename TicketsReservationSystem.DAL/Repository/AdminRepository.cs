using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsReservationSystem.DAL.Database;
using TicketsReservationSystem.DAL.Models;

namespace TicketsReservationSystem.DAL.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private ProgramContext _context;

        public AdminRepository(ProgramContext context)
        {
            _context = context;
        }

        public void AcceptEvent(Event Event)
        {
            _context.Update(Event);
            _context.SaveChanges();
        }

        public void AddAdmin(ApplicationUser admin)
        {
            
        }

        public Vendor ConfirmVendor(string vendorId)
        {
            var found = _context.vendors
                                    .Where(a => a.Id == vendorId)
                                    .FirstOrDefault();

            found.acceptanceStatus = "Accepted";
            _context.Update(found);
            _context.SaveChanges();
            return found;
            
        }

        public IQueryable<ApplicationUser> GetAllPendingVendors()
        {
           var found =  _context.vendors.Where(a => a.acceptanceStatus == "Pending");

            return found;
                                                          
        }

        public IQueryable<Event> GetPendingEntertainmentEvents()
        {
            var found = _context.Events.Include(a=>a.entertainment)
                                        .Where(a => a.status == "Pending")
                                       .Where(a => a.category == "Entertainment");
                                        

            return found;
        }

        public IQueryable<Event> GetPendingSportEvents()
        {
            var found = _context.Events.Include(a => a.sportEvent)
                                        .Where(a => a.status == "Pending")
                                       .Where(a => a.category == "Sport");

            return found;
        }

        public void RejectEvent(Event Event)
        {
            _context.Update(Event);
            _context.SaveChanges();
        }

        public Vendor RejectVendor(string vendorId)
        {
            var found = _context.vendors
                        .Where(a => a.Id == vendorId)
                        .FirstOrDefault();


            found.acceptanceStatus = "Rejected";
            _context.Update(found);
            _context.SaveChanges();


            return found;
        }
    }
}
