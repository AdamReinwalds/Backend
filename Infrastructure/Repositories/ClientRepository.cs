using Infrastructure.Data.Contexts;
using Infrastructure.Data.Entities;

namespace Infrastructure.Repositories;

public interface IClientRepository : IBaseRepository<ClientEntity>
{
}

public class ClientRepository(DataContext context) : BaseRepository<ClientEntity>(context), IClientRepository
{
}