using System;
using System.Collections.Generic;
using System.Text;

namespace Satellite.Service.Queries.DTOs
{
    public class SatelliteDto
    {
        public SatelliteDto()
        {
            Position = new Position();
        }

        public Position Position{ get; set; } 
        public string Message { get; set; }

    }
}
