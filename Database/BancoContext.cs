
using challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace challenge.Database
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ContaBancaria> ContasBancaria { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContaBancaria>().HasKey(c => c.Conta);
        }
    }
}
