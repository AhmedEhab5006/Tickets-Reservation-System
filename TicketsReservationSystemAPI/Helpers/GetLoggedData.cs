using System.Security.Claims;
using TicketsReservationSystem.BLL.Managers;
using Microsoft.AspNetCore.Http;



namespace TicketsReservationSystem.API.Helpers
{
    public class GetLoggedData : IGetLoggedData
    {
        private IUserManager _userManager;
        private IVendorManager _vendorManager;
        private IHttpContextAccessor _httpContextAccessor;

        public GetLoggedData(IUserManager userManager , IVendorManager vendorManager , IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _vendorManager = vendorManager;
            _httpContextAccessor = httpContextAccessor;
        }
        
        public int GetId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            var searchEmail = user.FindFirst(ClaimTypes.Email)?.Value;
            
            var found = _userManager.GetByEmail(searchEmail);
            int userId = found.Id;
            var vendor = _vendorManager.GetByUserId(userId);
            int vendorId = vendor.id;
            
            return vendorId;

        }
    }
}
