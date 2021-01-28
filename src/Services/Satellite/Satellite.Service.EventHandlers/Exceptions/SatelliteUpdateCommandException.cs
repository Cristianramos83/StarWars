using System;
using System.Collections.Generic;
using System.Text;

namespace Satellite.Service.EventHandlers.Exceptions
{
    public class SatelliteUpdateCommandException : Exception
    {
        public SatelliteUpdateCommandException(string message) : base(message)
        {
        }
    }
}
