using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SIM.Application.Interfaces;
using SIM.Domain.Models.Item;
using SIM.Domain.Models.Invoice;

namespace SIM.Persistence.Context
{
    class SIMContext : DbContext, ISIMContext
    {
        public SIMContext()
        {
        }

        public SIMContext(DbContextOptions<SIMContext> options) : base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    @"Server=.;Initial Catalog =SIMContext;MultipleActiveResultSets=true;User ID=sa;Password=q1!w2@e3#r4$");
            }
        }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Item> Items { get; set; }


        public Task SaveAsync(CancellationToken cancellationToken) =>
            base.SaveChangesAsync(cancellationToken);

        public Task CloseConnection() =>
            base.Database.CloseConnectionAsync();


        public Task CloseConnection(CancellationToken cancellationToken)
            => base.Database.CloseConnectionAsync();

        public void Save() =>
            base.SaveChanges();

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SIMContext).Assembly);
    }
}
