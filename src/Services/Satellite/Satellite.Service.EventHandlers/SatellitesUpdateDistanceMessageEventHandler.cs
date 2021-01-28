using MediatR;
using Microsoft.Extensions.Logging;
using Satellite.Persistence.Database;
using Satellite.Service.EventHandlers.Commands;
using Satellite.Service.EventHandlers.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Satellite.Service.EventHandlers
{
   public class SatellitesUpdateDistanceMessageEventHandler :INotificationHandler<SatellitesUpdateCommand>
    {       
            private readonly ApplicationDbContext _context;
            private readonly ILogger<SatellitesUpdateDistanceMessageEventHandler> _logger;

            public SatellitesUpdateDistanceMessageEventHandler(
                ApplicationDbContext context,
                ILogger<SatellitesUpdateDistanceMessageEventHandler> logger)
            {
                _context = context;
                _logger = logger;
            }
        public async Task Handle(SatellitesUpdateCommand notification, CancellationToken cancellationToken)
        {

            _logger.LogInformation("--- SatellitesUpdateCommand started");

            foreach (var item in notification.satellites)
            {


                _logger.LogInformation("--- Get satellite from database with name");

                var entry = _context.Satellites.SingleOrDefault(x => x.Name == item.Name);

                if (entry == null)
                {
                    _logger.LogError($"---the satellite with {item.Name} -doens't exist");
                    throw new SatelliteUpdateCommandException($"Satellite {item.Name} -  -doens't exist");
                }
                else
                {

                    entry.Distance = item.Distance;
                    entry.Message = string.Join(",", item.message);
                    _logger.LogInformation($"--- Update info Satellite {entry.Name}");

                }
                await _context.SaveChangesAsync();
                _logger.LogInformation("--- SatellitesUpdateCommand ended");
            }
          }        
       
    }
    
}
