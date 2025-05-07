namespace TicketsReservationSystem.API.Helpers
{
    public interface IGetLoggedData
    {
        public string GetId();
        public Task<string> GetVendorStatus(string id);
    }
}
