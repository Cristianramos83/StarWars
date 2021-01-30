using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Satellite.Common.Functions;
using Satellite.Persistence.Database;
using Satellite.Service.EventHandlers;
using Satellite.Service.EventHandlers.Commands;
using Satellite.Service.Queries;
using Satellite.Service.Queries.DTOs;
using Satellite.Test.Config;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Satellite.Test
{
    [TestClass]
    public class SatellitesUpdateDistanceMessageEventHandlerTest
    {
        private ILogger<SatellitesUpdateDistanceMessageEventHandler> GetLogger
        {
            get
            {
                return new Mock<ILogger<SatellitesUpdateDistanceMessageEventHandler>>()
                    .Object;
            }
        }        
        [TestMethod]
        public void TryToGetSourceAndMessage()
        {
            var context = ApplicationDbContextInMemory.Get();

            var satellites = new List<Satellite.Domain.Satellite>();
            context.Satellites.RemoveRange(context.Satellites.Select(x => x).ToList());
            context.SaveChanges();

            satellites.Add(
            new Satellite.Domain.Satellite
            {
                SatelliteId = 1,
                Name = "Kenobi",
                CoordinateX = -500,
                CoordinateY = -200
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

            context.Satellites.AddRange(satellites);

            context.SaveChanges();

            var handler = new SatellitesUpdateDistanceMessageEventHandler(context, GetLogger);

            handler.Handle(new SatellitesUpdateCommand
            {
                satellites = new List<SatelliteUpdateItem>()
                {
                     new SatelliteUpdateItem
                     {
                        Name = "Kenobi",
                        Distance=500,
                        message = new List<string>(new string[]{"este", "", "", "mensaje", "" })
                     },
                     new SatelliteUpdateItem
                     {
                        Name = "Skywalker",
                        Distance=515.5,
                        message= new List<string>(new string[]{ "", "es", "", "", "secreto" })
                     },
                     new SatelliteUpdateItem
                     {
                        Name = "Sato",
                        Distance=595.86231824769629,
                        message= new List<string>(new string[]{ "este", "", "un", "", "" })
                     }

                }

            }, new CancellationToken()).Wait();

            var query =new SatelliteQueryService(context);

            var getSource = query.GetSource();            
            Assert.AreEqual($"Position X:-0,5422119231201918 ;Position Y:-223,2791307687972 ; message:este es un mensaje secreto ", $"Position X:{getSource.Position.X} ;Position Y:{getSource.Position.Y} ; message:{getSource.Message}");

        }
        [TestMethod]
        public void TryToNotFoundSource()
        {
            var context = ApplicationDbContextInMemory.Get();

            var satellites = new List<Satellite.Domain.Satellite>();

            context.Satellites.RemoveRange(context.Satellites.Select(x=>x).ToList());
            context.SaveChanges();


            satellites.Add(
            new Satellite.Domain.Satellite
            {
                SatelliteId = 1,
                Name = "Kenobi",
                CoordinateX = -500,
                CoordinateY = -200
            });
            satellites.Add(new Satellite.Domain.Satellite
            {
                SatelliteId = 2,
                Name = "Skywalker",
                CoordinateX = -100,
                CoordinateY = -100
            });
            satellites.Add(new Satellite.Domain.Satellite
            {
                SatelliteId = 3,
                Name = "Sato",
                CoordinateX = 500,
                CoordinateY = 100
            });

            context.Satellites.AddRange(satellites);

            context.SaveChanges();

            var handler = new SatellitesUpdateDistanceMessageEventHandler(context, GetLogger);

            handler.Handle(new SatellitesUpdateCommand
            {
                satellites = new List<SatelliteUpdateItem>()
                {
                     new SatelliteUpdateItem
                     {
                        Name = "Kenobi",
                        Distance=100,
                        message = new List<string>(new string[]{"este", "", "", "mensaje", "" })
                     },
                     new SatelliteUpdateItem
                     {
                        Name = "Skywalker",
                        Distance=115.5,
                        message= new List<string>(new string[]{ "", "es", "", "", "secreto" })
                     },
                     new SatelliteUpdateItem
                     {
                        Name = "Sato",
                        Distance=142.7,
                        message= new List<string>(new string[] { "este", "", "un", "", "" })
                     }

                }

            }, new CancellationToken()).Wait();

            var query = new SatelliteQueryService(context);

            var getSource = query.GetSource();
            
            Assert.AreEqual(null, getSource);

        }
    }
}
