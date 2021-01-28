using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Satellite.Service.EventHandlers.Commands
{
    public class SatelliteUpdateCommand: INotification
    {
        public string Name { get; set; }
        public double Distance { get; set; }
        public List<string> message { get; set; }
    }
}
