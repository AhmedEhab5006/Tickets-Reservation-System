namespace TicketsReservationSystem.API.Helpers
{
    public interface IGetLoggedData
    {
        public int GetId();
        public string GetVendorStatus(int id);
    }
}
