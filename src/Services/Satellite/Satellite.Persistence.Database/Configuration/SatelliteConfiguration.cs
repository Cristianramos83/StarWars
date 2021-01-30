using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Satellite.Domain;
namespace Satellite.Persistence.Database.Configuration
{
    public class SatelliteConfiguration
    {
        public SatelliteConfiguration(EntityTypeBuilder<Satellite.Domain.Satellite> entityBuilder)
        {
            entityBuilder.HasIndex(x => x.SatelliteId);
            entityBuilder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            entityBuilder.Property(x => x.CoordinateX);
            entityBuilder.Property(x => x.CoordinateY);
            entityBuilder.Property(x => x.Distance);
            entityBuilder.Property(x => x.Message);           

            var satellites = new List<Satellite.Domain.Satellite>();
            
            
                satellites.Add(
                new Satellite.Domain.Satellite
                {
                    SatelliteId = 1,
                    Name = "Kenobi",
                    CoordinateX = -500,
                    CoordinateY= -200
                });
               satellites.Add(new Satellite.Domain.Satellite
                {
                    SatelliteId = 2,
                    Name = "Skywalker",
                    CoordinateX = 500,
                    CoordinateY = -100
               });
               satellites.Add(new Satellite.Domain.Satellite
               {
                    SatelliteId = 3,
                    Name = "Sato",
                    CoordinateX = 500,
                    CoordinateY = 100
               });

            entityBuilder.HasData(satellites);



        }
    }
}
