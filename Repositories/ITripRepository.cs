using APBD09.Models;
using APBD09.Models.DTOs;

namespace APBD09.Repositories;

public interface ITripRepository
{
    public PagedResultDto<TripDto> getTrip(int page, int pageSize);

    public Task postClientToTrip();

    public void setConfig(IConfiguration configuration);
}