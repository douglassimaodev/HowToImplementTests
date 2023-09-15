using HowToImplementTests.Api.DAL;
using HowToImplementTests.Api.Data;
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

            app.Run();
        }
    }
}