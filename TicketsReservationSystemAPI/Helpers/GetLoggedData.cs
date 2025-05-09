using System.Security.Claims;
using TicketsReservationSystem.BLL.Managers;
using Microsoft.AspNetCore.Http;



namespace TicketsReservationSystem.API.Helpers
{
    public class GetLoggedData : IGetLoggedData
    {
        private IVendorManager _vendorManager;
        private IHttpContextAccessor _httpContextAccessor;

        public GetLoggedData(IVendorManager vendorManager, IHttpContextAccessor httpContextAccessor)
        {
            _vendorManager = vendorManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            var id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value.ToString();

            return id;

        }

        public string GetVendorStatus(string id)
        {
            var found =  _vendorManager.GetById(id);
            return found.acceptanceStatus.ToString();

        }
    }
}
