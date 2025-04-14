using Infrastructure.Data.Contexts;
using Infrastructure.Data.Entities;

namespace Infrastructure.Repositories;

public interface IUserRepository : IBaseRepository<UserEntity>
{
}
public class UserRepository(DataContext context) : BaseRepository<UserEntity>(context), IUserRepository
{

}