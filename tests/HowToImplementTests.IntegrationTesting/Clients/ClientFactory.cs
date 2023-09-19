using Bogus;
using HowToImplementTests.Api.Models;

namespace HowToImplementTests.IntegrationTesting.Clients
{
    public class ClientFactory
    {
        public static Client GenerateFakeClient()
        {
            var faker = new Faker<Client>()
                .RuleFor(c => c.FirstName, f => f.Name.FirstName())
                .RuleFor(c => c.LastName, f => f.Name.LastName())
                .RuleFor(c => c.Email, f => f.Internet.Email())
                .RuleFor(c => c.Telephone, f => f.Phone.PhoneNumber("###-###-####"));

            return faker.Generate();
        }
    }
}
