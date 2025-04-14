using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public interface IUserService
{
    Task<User> GetUserByIdAsync(string id);
    Task<IEnumerable<User>> GetUsersAsync();
}

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;



    //Add create users

    public async Task<IEnumerable<User>> GetUsersAsync()
    {

        var entites = await _userRepository.GetAllAsync(sortBy: x => x.FirstName);
        var users = entites.Select(entity => new User
        {
            Id = entity.Id, 
            FirstName = entity.FirstName,
            LastName = entity.LastName
        });

        return users;
    }

    public async Task<User> GetUserByIdAsync(string id)
    {
        var entity = await _userRepository.GetAsync(x => x.Id == id);
        return entity == null ? null! : new User
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName
        };
    }

}