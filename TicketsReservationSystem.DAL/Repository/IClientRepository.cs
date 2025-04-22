using TicketsReservationSystem.DAL.Models;

public interface IClientRepository
{
    void Add(Client client);
    void AddAddress(Address address);
    void EditAddress(Address address);
    void Book(int ticketId);
    void CancelBooking(int ticketId);
    IQueryable<Event> GetSportEvent();
    IQueryable<Event> GetEntertainmentEvents();
    Client? GetClientById(int clientId);
    IQueryable<Client> GetAllClients();

    // New method to get an address by ID
    Address? GetAddressById(int addressId);
    bool AddressExists(int addressId);

}

