using HowToImplementTests.Api.DAL;
using HowToImplementTests.Api.Models;
using System.Diagnostics;

namespace HowToImplementTests.Api.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly ILogger<ClientService> _logger;

        public ClientService(IClientRepository clientRepository, ILogger<ClientService> logger)
        {
            _clientRepository = clientRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            _logger.LogInformation("the user xxx is retrieving all clients");

            var stopWatch = Stopwatch.StartNew();

            try
            {
                return await _clientRepository.GetAllClientsAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "something went wrong while user xx was retrieving all clients");
                throw;
            }
            finally
            {
                stopWatch.Stop();
                _logger.LogInformation($"User xxx got all clients in {stopWatch.ElapsedMilliseconds}ms");
            }
        }
    }
}
