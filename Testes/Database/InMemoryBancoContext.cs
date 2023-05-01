using challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace challenge.Testes.Database
{
    public class InMemoryBancoContext
    {
        public class InMemoryDbContext : DbContext
        {
            public InMemoryDbContext(DbContextOptions<InMemoryDbContext> options) : base(options)
            {
            }

            public DbSet<ContaBancaria> Contas { get; set; }
        }
    }
}
