using System;
using System.Collections.Generic;
using System.Text;

namespace TestTrilateracion
{
    public class PointF
    {
        private double lt, ln;
        public PointF()
        {

        }
        public PointF(double lt, double ln) { this.lt = lt; this.ln = ln; }
        public double glt() { return lt; }
        public double gln() { return ln; }
    }
}

