using APBD09.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace APBD09.Repositories;

public class ClientRepository: IClientRepository
{
    private readonly MasterContext _context;

    public ClientRepository(MasterContext context)
    {
        _context = context;
    }
    
    public bool ClientExists(string pesel)
    {
        return _context.Clients.Any(c => c.Pesel == pesel);
    }
    
    public bool ClientAssignedToAnyTrip(int idClient)
    {
        return _context.ClientTrips.Any(ct => ct.IdClient == idClient);
    }

    public bool ClientHasTrips(int clientId)
    {
        return _context.Trips
            .Any(t => t.ClientTrips.Any(c => c.IdClient == clientId));
    }
    
    [HttpPost("/api/trips/{idTrip }/clients")]
    public void AddClientToTrip(int tripId, ClientTripDto clientTripDto, DateTime registeredAt)
    {
        var trip = _context.Trips.Include(t => t.IdTrip).FirstOrDefault(t => t.IdTrip == tripId);

        if (trip == null || trip.DateFrom <= DateTime.Now)
        {
            throw new InvalidOperationException("Trip does not exist or has already started.");
        }
        

        _context.SaveChanges();
    }

    public void DeleteClient(int clientId)
    {
        var client = _context.Clients.Find(clientId);
        if (client != null)
        {
            _context.Clients
                .Remove(client);
            _context
                .SaveChanges();
        }
    }
}