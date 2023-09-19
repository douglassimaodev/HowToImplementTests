using HowToImplementTests.Api.DAL;
using HowToImplementTests.Api.Data;
using HowToImplementTests.Api.Repositories;
using HowToImplementTests.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace HowToImplementTests.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<MyDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MyDbContext") ?? throw new InvalidOperationException("Connection string 'MyDbContext' not found.")));

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ICalculatorModel, CalculatorModel>();
            builder.Services.AddScoped<IClientRepository, ClientRepository>();
            builder.Services.AddScoped<IClientService, ClientService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();


            //Make sure it will run automatically any migration that didnt run
            using (var scope = app.Services.CreateScope())
            {
                var myDbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();
                myDbContext.Database.Migrate();
            }

            app.Run();
        }
    }
}