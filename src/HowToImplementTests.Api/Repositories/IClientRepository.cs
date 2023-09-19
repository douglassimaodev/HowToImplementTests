using HowToImplementTests.Api.Models;

namespace HowToImplementTests.Api.Repositories
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllClientsAsync();
        Task<Client> GetClientByIdAsync(int id);
        Task CreateClientAsync(Client client);
        Task UpdateClientAsync(Client client);
        Task DeleteClientAsync(Client client);
        bool ClientExists(int id);
    }
}
