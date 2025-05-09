namespace TicketsReservationSystem.API.Helpers
{
    public interface IGetLoggedData
    {
        public string GetId();
        public string GetVendorStatus(string id);
    }
}
