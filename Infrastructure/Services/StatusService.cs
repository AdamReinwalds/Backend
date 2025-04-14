using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public interface IStatusService
{
    Task<Status> GetStatusByIdAsync(int id);
    Task<Status> GetStatusByStatusNameAsync(string statusName);
    Task<IEnumerable<Status>> GetStatusesAsync();
}

public class StatusService(IStatusRepository statusRepository) : IStatusService
{
    private readonly IStatusRepository _statusRepository = statusRepository;

    public async Task<IEnumerable<Status>> GetStatusesAsync()
    {
        var entites = await _statusRepository.GetAllAsync(sortBy: x => x.Id);
        var statuses = entites.Select(entity => new Status
        {
            Id = entity.Id,
            StatusName = entity.StatusName
        });

        return statuses;
    }

    public async Task<Status> GetStatusByIdAsync(int id)
    {
        var entity = await _statusRepository.GetAsync(x => x.Id == id);
        return entity == null ? null! : new Status
        {
            Id = entity.Id,
            StatusName = entity.StatusName
        };
    }

    public async Task<Status> GetStatusByStatusNameAsync(string statusName)
    {
        var entity = await _statusRepository.GetAsync(x => x.StatusName == statusName);
        return entity == null ? null! : new Status
        {
            Id = entity.Id,
            StatusName = entity.StatusName
        };
    }
}