using MediatR;
using System;
using System.Collections.Generic;

namespace Satellite.Service.EventHandlers.Commands
{
    public class SatellitesUpdateCommand: INotification
    {
        public List<SatelliteUpdateItem> satellites { get; set; } = new List<SatelliteUpdateItem>();
    }
    public class SatelliteUpdateItem
    {
        public string Name { get; set; }
        public double Distance { get; set; }
        public List<string> message { get; set; }

    }

}
