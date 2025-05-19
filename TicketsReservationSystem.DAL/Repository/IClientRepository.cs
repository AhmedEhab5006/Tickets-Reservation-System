using TicketsReservationSystem.DAL.Models;
public interface IClientRepository
{
    public int AddAddress(Address address);
    public void EditAddress(Address address);
    public void Book(Reservation reservation);
    public void CancelBooking(Reservation reservation);
    public IQueryable<Event> GetSportEvent();
    public IQueryable<Event> GetEntertainmentEvents();
    public IQueryable<Reservation> GetClientBookings(string clientId);
    public Client GetAddress (string clientId);
    public IQueryable<Ticket> GetEventTickets(int eventId);
    public Reservation GetReservation(int reservationId);

}

