using Microsoft.EntityFrameworkCore;
using Satellite.Persistence.Database.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Satellite.Persistence.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Satellite.Domain.Satellite> Satellites { get; set; }
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("Satellite");
            ModelConfig(builder);
        }

        private void ModelConfig(ModelBuilder modelBuilder)
        {
            
            
            new SatelliteConfiguration(modelBuilder.Entity<Satellite.Domain.Satellite>());
        }
    }
}
