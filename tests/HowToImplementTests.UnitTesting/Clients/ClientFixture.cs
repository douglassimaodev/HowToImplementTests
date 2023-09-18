namespace HowToImplementTests.UnitTesting.Clients
{
    public class ClientFixture
    {
        //Shared with the class texted
        public Guid Id { get; set; }

        public ClientFixture()
        {
            Id = Guid.NewGuid();
        }
    }
}
