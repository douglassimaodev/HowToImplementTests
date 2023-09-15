using HowToImplementTests.Api.Data;
using HowToImplementTests.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HowToImplementTests.Api.DAL
{
    public class ClientRepository : IClientRepository
    {
        private readonly MyDbContext _context;

        public ClientRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            return await _context.Client.ToListAsync();
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            return await _context.Client.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task CreateClientAsync(Client client)
        {
            _context.Add(client);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateClientAsync(Client client)
        {
            _context.Update(client);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteClientAsync(Client client)
        {
            _context.Client.Remove(client);
            await _context.SaveChangesAsync();
        }

        public bool ClientExists(int id)
        {
            return _context.Client.Any(e => e.Id == id);
        }
    }
}
