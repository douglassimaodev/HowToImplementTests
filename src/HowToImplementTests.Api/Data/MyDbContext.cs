using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HowToImplementTests.Api.Models;

namespace HowToImplementTests.Api.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext (DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public DbSet<HowToImplementTests.Api.Models.Client> Client { get; set; } = default!;
    }
}
