using System;
using System.Collections.Generic;
using System.Text;

namespace Satellite.Common.Functions
{
    public class Point
    {
        private double x, y, r;
        public Point()
        {

        }
        public Point(double x, double y, double r) 
        { 
            this.x = x; 
            this.y = y; 
            this.r = r; 
        }
        public double PositionX() { return x; }
        public double PositionY() { return y; }
        public double Radio() { return r; }
    }
}
