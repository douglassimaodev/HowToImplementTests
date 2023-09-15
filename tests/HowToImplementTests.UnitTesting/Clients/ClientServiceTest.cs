using FluentAssertions;
using HowToImplementTests.Api.DAL;
using HowToImplementTests.Api.Models;
using HowToImplementTests.Api.Services;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace HowToImplementTests.UnitTesting.Clients
{
    public class ClientServiceTest
    {
        private readonly ClientService _sut;

        private readonly IClientRepository _clientRepository = Substitute.For<IClientRepository>();
        private readonly ILogger<ClientService> _logger = Substitute.For<ILogger<ClientService>>();

        public ClientServiceTest()
        {           
            _sut = new ClientService(_clientRepository, _logger);
        }

        [Fact]
        public async Task GetAllClientsAsync_ShouldReturnEmptyList_WhenNoClientsExists()
        {
            // Arrange
            _clientRepository.GetAllClientsAsync().Returns(Enumerable.Empty<Client>());

            // Act
            var result = await _sut.GetAllClientsAsync();

            //Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllClientsAsync_ShouldReturnClients_WhenClientsExists()
        {
            // Arrange
            var johnPit = new Client { Id = 1, FirstName = "John", LastName = "Pit" };
            var expectedUsers = new[]
            {
                johnPit
            };

            _clientRepository.GetAllClientsAsync().Returns(expectedUsers);

            // Act
            var result = await _sut.GetAllClientsAsync();

            //Assert
            result.Single().Should().BeEquivalentTo(johnPit);
            result.Should().BeEquivalentTo(expectedUsers);
        }

        [Fact]
        public async Task GetAllClientsAsync_ShouldLogMessages_WhenInvocked()
        {
            // Arrange
            _clientRepository.GetAllClientsAsync().Returns(Enumerable.Empty<Client>());

            // Act
            _ = await _sut.GetAllClientsAsync();

            //Assert
            _logger.Received(1).LogInformation("the user xxx is retrieving all clients");
        }
    }
}
