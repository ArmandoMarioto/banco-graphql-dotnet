using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace challenge.Database
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Conta> Contas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conta>().HasKey(c => c.Numero);
        }
    }
}
