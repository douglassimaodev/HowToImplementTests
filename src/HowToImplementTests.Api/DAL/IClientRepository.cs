using HowToImplementTests.Api.Models;

namespace HowToImplementTests.Api.DAL
{
    public interface IClientRepository
    {
        Task<List<Client>> GetAllClientsAsync();
        Task<Client> GetClientByIdAsync(int id);
        Task CreateClientAsync(Client client);
        Task UpdateClientAsync(Client client);
        Task DeleteClientAsync(Client client);
        bool ClientExists(int id);
    }
}
