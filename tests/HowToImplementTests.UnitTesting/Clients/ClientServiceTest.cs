using FluentAssertions;
using HowToImplementTests.Api.Models;
using HowToImplementTests.Api.Repositories;
using HowToImplementTests.Api.Services;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace HowToImplementTests.UnitTesting.Clients
{
    [Collection("Client collection")]
    public class ClientServiceTest //: IClassFixture<ClientFixture>
    {
        private readonly ClientService _sut;

        private readonly IClientRepository _clientRepository = Substitute.For<IClientRepository>();
        private readonly ILogger<ClientService> _logger = Substitute.For<ILogger<ClientService>>();
        private readonly ClientFixture _clientFixture;
        
        public ClientServiceTest(ClientFixture clientFixture)
        {           
            _sut = new ClientService(_clientRepository, _logger);
            _clientFixture = clientFixture;
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

        [Fact]
        public async Task GetAllClientsAsync_ShouldLogMessagesAndException_WhenExceptionIsThown()
        {
            // Arrange
            var sqlExeption = new Exception("Simulated SQL error");
            _clientRepository.GetAllClientsAsync().Throws(sqlExeption);  // Here, we're using a standard exception as a stand-in.

            // Act
            var request = _sut.GetAllClientsAsync;

            //Assert
            await request.Should().ThrowAsync<Exception>().WithMessage("Simulated SQL error");
            _logger.Received(1).LogError(sqlExeption, "something went wrong while user xx was retrieving all clients");
        }
    }
}
