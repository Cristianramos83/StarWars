using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Satellite.Domain
{
    public class Satellite
    {       
        public int SatelliteId { get; set; }
        public string Name { get; set; }
        public double Distance { get; set; }
        public double CoordinateX { get; set; }
        public double CoordinateY { get; set; }
        public string Message { get; set; }
       
    }
}
