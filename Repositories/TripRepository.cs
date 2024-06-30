using APBD09.Models.DTOs;

namespace APBD09.Repositories;

public class TripRepository: ITripRepository
{
    private IConfiguration _configuration;
    private readonly MasterContext _context;
    
    public TripRepository(MasterContext context)
    {
        _context = context;
    }
    
    public void setConfig(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public PagedResultDto<TripDto> getTrip(int page, int pageSize)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 10;

        var totalTrips = _context.Trips.Count();

        var trips = _context.Trips
            .OrderByDescending(t => t.DateFrom)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(t => new TripDto
            {
                Name = t.Name,
                Description = t.Description,
                DateFrom = t.DateFrom,
                DateTo = t.DateTo,
                MaxPeople = t.MaxPeople,
            })
            .ToList();

        var totalPages = (int)Math.Ceiling((double)totalTrips / pageSize);

        return new PagedResultDto<TripDto>
        {
            PageNum = page,
            PageSize = pageSize,
            AllPages = totalPages,
            Items = trips
        };
    }

    public async Task postClientToTrip()
    {
        
    }
}