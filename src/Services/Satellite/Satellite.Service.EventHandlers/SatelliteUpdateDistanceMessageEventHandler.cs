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
   public class SatelliteUpdateDistanceMessageEventHandler :INotificationHandler<SatelliteUpdateCommand>
    {       
            private readonly ApplicationDbContext _context;
            private readonly ILogger<SatelliteUpdateDistanceMessageEventHandler> _logger;

            public SatelliteUpdateDistanceMessageEventHandler(
                ApplicationDbContext context,
                ILogger<SatelliteUpdateDistanceMessageEventHandler> logger)
            {
                _context = context;
                _logger = logger;
            }
        public async Task Handle(SatelliteUpdateCommand notification, CancellationToken cancellationToken)
        {

                 _logger.LogInformation("--- SatelliteUpdateCommand started");           


                _logger.LogInformation("--- Get satellite from database with name");

                var entry = _context.Satellites.SingleOrDefault(x => x.Name == notification.Name);

                if (entry == null)
                {
                    _logger.LogError($"---the satellite with {notification.Name} -doens't exist");
                    throw new SatelliteUpdateCommandException($"Satellite {notification.Name} -  -doens't exist");
                }
                else
                {

                    entry.Distance = notification.Distance;
                    entry.Message = string.Join(",", notification.message);
                    _logger.LogInformation($"--- Update info Satellite {entry.Name}");

                }
                await _context.SaveChangesAsync();
                _logger.LogInformation("--- SatelliteUpdateCommand ended");
            }
             
       
    }
    
}
