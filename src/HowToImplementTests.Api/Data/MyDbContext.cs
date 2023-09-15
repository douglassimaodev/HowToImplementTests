using HowToImplementTests.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HowToImplementTests.Api.Data
{
    public class MyDbContext : DbContext
    {
        public DbSet<Client> Client { get; set; } = default!;

        public MyDbContext (DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            foreach (var property in builder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                                    .Where(p => p.ClrType == typeof(string) && p.GetMaxLength() == null && p.GetColumnType() == null)))
            {
                property.SetMaxLength(250);
                property.SetIsUnicode(false);
            }

            //Convert all table names to use the class name
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                // Skip shadow types
                if (entityType.ClrType == null)
                    continue;

                entityType.SetTableName(entityType.ClrType.Name);
            }

            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(MyDbContext).Assembly);
        }


    }
}
