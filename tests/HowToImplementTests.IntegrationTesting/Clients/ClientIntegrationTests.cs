using FluentAssertions;
using HowToImplementTests.Api.Models;
using HowToImplementTests.IntegrationTesting.Clients;
using HowToImplementTests.IntegrationTesting.Config;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HowToImplementTests.IntegrationTesting
{
    public class ClientIntegrationTests : IClassFixture<HowToImplementTestsFactory>
    {
        private readonly HowToImplementTestsFactory _factory;
        private readonly HttpClient _client;

        public ClientIntegrationTests(HowToImplementTestsFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Get_ShouldReturnOk_WhenRetrievingAllClients()
        {
            // Arrange            

            // Act
            var sut = await _client.GetAsync("/api/client");
            var data = await sut.Content.ReadAsAsync<IEnumerable<Client>>();

            //Assert
            sut.StatusCode.Should().Be(HttpStatusCode.OK);
            //data.Should().Equal(1, ((Client)result.Value).Id);
        }

        [Fact]
        public async Task Add_ShouldReturnOk_WhenRetrievingAllClients()
        {
            // Arrange
            var expectedClient = ClientFactory.GenerateFakeClient();
            var content = _factory.CreateContent(_factory.Serialize(expectedClient));

            // Act
            var sut = await _client.PostAsync("/api/client", content) ;
            var returnedClient = _factory.Deserialize<Client>(await sut.Content.ReadAsStringAsync());
            //Assert
            sut.StatusCode.Should().Be(HttpStatusCode.Created);

            // If you also want to check the returned Location header:
            var location = sut.Headers.Location.ToString();
            location.Should().Contain("/api/client/1");  // Assuming that the created client has ID of 1
        }
    }
}
