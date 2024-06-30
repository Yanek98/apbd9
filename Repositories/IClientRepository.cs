using APBD09.Models.DTOs;

namespace APBD09.Repositories;

public interface IClientRepository
{
    bool ClientHasTrips(int clientId);
    void DeleteClient(int clientId);
    public bool ClientExists(string pesel);
    public bool ClientAssignedToAnyTrip(int idClient);
    public void AddClientToTrip(int tripId, ClientTripDto clientTripDto, DateTime registeredAt);
}