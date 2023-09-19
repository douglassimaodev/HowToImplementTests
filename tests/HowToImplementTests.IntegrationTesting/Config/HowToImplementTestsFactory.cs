using HowToImplementTests.Api.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using Testcontainers.MariaDb;

namespace HowToImplementTests.IntegrationTesting.Config
{
    public class HowToImplementTestsFactory : WebApplicationFactory<Api.Controllers.ClientController>, IAsyncLifetime
    {
        //private readonly MariaDbContainer _container;
        //private readonly MsSqlContainer _container;
        private string _connectionString { get; set; }

        public HowToImplementTestsFactory()
        {
            //_container = new MariaDbBuilder()    
            //    .WithImage("mariadb:latest")
            //    .WithDatabase("testdb")
            //    .WithUsername("root")
            //    .WithPassword("root")
            //    .WithCommand("--lower-case-table-names=1")
            //    .Build();
        }

        public async Task InitializeAsync()
        {
            //await _container.StartAsync();
            //_connectionString = _container.GetConnectionString();

            _connectionString = "Server=(localdb)\\mssqllocaldb;Database=MyDbContextTest;Trusted_Connection=True;MultipleActiveResultSets=true";
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                //Remove all loggin so we wont be registering while testing
                builder.ConfigureLogging(logging => logging.ClearProviders());

                // Remove the existing database context registration.
                var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<MyDbContext>));
                if (dbContextDescriptor != null)
                {
                    services.Remove(dbContextDescriptor);
                }

                // Add a new database context registration with your custom configuration.
                services.AddDbContext<MyDbContext>(options =>
                options.UseSqlServer(_connectionString ?? throw new InvalidOperationException("Connection string 'MyDbContext' not found.")));

                //services.AddDbContext<MyDbContext>(options => options.UseInMemoryDatabase("MyInMemoryDb"));

            });

            builder.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
            });
        }

        public T Deserialize<T>(string content) => JsonConvert.DeserializeObject<T>(content);
        public string Serialize(object obj) => System.Text.Json.JsonSerializer.Serialize(obj);
        public StringContent CreateContent(string jsonContent, string contentType = "application/json")
        {
            return new StringContent(jsonContent, Encoding.UTF8, contentType);
        }

        public new async Task DisposeAsync()
        {
            //await _container.StopAsync();
            //await _container.DisposeAsync();

            using (var scope = Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                // Create a temporary DbContext to delete the test database
                var dbContext = new MyDbContext(new DbContextOptionsBuilder<MyDbContext>()
                    .UseSqlServer(_connectionString)
                    .Options);
                
                    // Set the database to single user mode to drop it
                    await dbContext.Database.ExecuteSqlRawAsync(@"USE master;
                        ALTER DATABASE [MyDbContextTest] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                        DROP DATABASE [MyDbContextTest] ;");

                    dbContext.Dispose();
                
            }
        }
    }
}
