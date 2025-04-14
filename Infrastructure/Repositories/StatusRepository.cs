using Infrastructure.Data.Contexts;
using Infrastructure.Data.Entities;

namespace Infrastructure.Repositories;

public interface IStatusRepository : IBaseRepository<StatusEntity>
{
}

public class StatusRepository(DataContext context) : BaseRepository<StatusEntity>(context), IStatusRepository
{
}