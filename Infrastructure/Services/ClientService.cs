using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public interface IClientService
{
    Task<IEnumerable<Client>> GetClientsAsync();
    Task<Client> GetUserByClientNameAsync(string clientName);
    Task<Client> GetUserByIdAsync(string id);
}

public class ClientService(IClientRepository clientRepository) : IClientService
{
    private readonly IClientRepository _clientRepository = clientRepository;

    public async Task<IEnumerable<Client>> GetClientsAsync()
    {
        var entites = await _clientRepository.GetAllAsync(sortBy: x => x.ClientName);
        var clients = entites.Select(entity => new Client
        {
            Id = entity.Id,
            ClientName = entity.ClientName,
        });

        return clients;
    }

    public async Task<Client> GetUserByIdAsync(string id)
    {
        var entity = await _clientRepository.GetAsync(x => x.Id == id);
        return entity == null ? null! : new Client
        {
            Id = entity.Id,
            ClientName = entity.ClientName,
        };
    }


    public async Task<Client> GetUserByClientNameAsync(string clientName)
    {
        var entity = await _clientRepository.GetAsync(x => x.ClientName.Equals(clientName, StringComparison.CurrentCultureIgnoreCase));
        return entity == null ? null! : new Client
        {
            Id = entity.Id,
            ClientName = entity.ClientName,
        };
    }
}