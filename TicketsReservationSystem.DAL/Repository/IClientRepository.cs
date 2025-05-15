using TicketsReservationSystem.DAL.Models;
public interface IClientRepository
{
    public int AddAddress(Address address);

    Task<bool> EditAddressAsync(string clientId, Address address);

    public bool Book(int ticketId, string clientId);
    public bool CancelBooking(int ticketId, string clientId);
    IQueryable<Event> GetSportEvent();
    IQueryable<Event> GetEntertainmentEvents();

   public Task<Client> GetClientWithAddressAsync(string clientId);

    public Task<List<Ticket>> GetClientBookingsAsync(string clientId);
}

