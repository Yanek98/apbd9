using APBD09.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using APBD09.Repositories;

namespace APBD09.Controllers;

[ApiController]
[System.Web.Http.Route("api/[controller]")]
public class AppController: Controller
{
    private readonly ITripRepository _tripRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IConfiguration _configuration;

    public AppController(ITripRepository tripRepository, IConfiguration configuration)
    {
        _tripRepository = tripRepository;
        _configuration = configuration;
        _tripRepository.setConfig(_configuration);
    }

    [HttpGet("/api/trips")]
    public IActionResult GetTrips([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = _tripRepository.getTrip(page, pageSize);
        return Ok(result);
    }

    [HttpPost("/api/trips.{idTrip}/clients")]
    public async Task<ActionResult> PostClientToTrip(int idTrip, ClientTripDto clientTripDto)
    {
        try
        {
            _clientRepository.AddClientToTrip(idTrip, clientTripDto, DateTime.Now);
            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("/api/clients/{idClient}")]
    public IActionResult DeleteClient(int idClient)
    {
        if (_clientRepository.ClientHasTrips(idClient))
        {
            return BadRequest(new { message = "Client has assigned trips and cannot be deleted." });
        }

        _clientRepository.DeleteClient(idClient);
        return NoContent();
    }
}