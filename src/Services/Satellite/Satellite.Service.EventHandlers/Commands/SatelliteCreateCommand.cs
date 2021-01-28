using System;
using System.Collections.Generic;

namespace Satellite.Service.EventHandlers.Commands
{
    public class SatelliteCreateCommand
    {
        public string Name { get; set; }
        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }
    }
}
