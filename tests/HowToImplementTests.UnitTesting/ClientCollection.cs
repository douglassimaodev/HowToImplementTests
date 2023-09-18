using HowToImplementTests.UnitTesting.Clients;

namespace HowToImplementTests.UnitTesting
{
    [CollectionDefinition("Client collection")]
    public class ClientCollection : ICollectionFixture<ClientFixture>
    {
        // This class doesn't need to have any code or methods.
        // Its purpose is merely to be an anchor for the ICollectionFixture<>
    }
}
