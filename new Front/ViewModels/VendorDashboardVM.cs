using WebApplication2.Models;
using System.Collections.Generic;

namespace WebApplication2.ViewModels
{
    public class VendorDashboardVM
    {
        public List<SportEventVM> SportsEvents { get; set; }
        public List<EntertainmentVM> EntertainmentEvents { get; set; }
        public List<SportEventUpdateVM> SportEventUpdates { get; set; }

        public VendorDashboardVM()
        {
            SportsEvents = new List<SportEventVM>();
            EntertainmentEvents = new List<EntertainmentVM>();
            SportEventUpdates = new List<SportEventUpdateVM>();
        }
    }
}

