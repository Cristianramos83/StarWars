using System;
using System.Collections.Generic;

namespace TestTrilateracion
{
    class Program
    {
        static void Main(string[] args)
        {
            Point p1 = new Point(-100, -200, 100);
            Point p2 = new Point(100, -100, 115.5);
            Point p3 = new Point(100, 100, 147.7);
            double[] a = Trilateration.Compute(p1, p2, p3);
            if (a != null)
                Console.WriteLine("Lat: " + a[0] + ",Lon   " + a[1]);
            else
                Console.WriteLine("No se encontro un punto ");
           


            List<PointF> listIntserctions = new List<PointF>();
            List<PointF> listIntserctions2 = new List<PointF>();
            List<PointF> listIntserctions3 = new List<PointF>();
            listIntserctions  = FindCircleCircleIntersections.Calculate(-500, -200, 500, 100, -100, 542.7);
            listIntserctions2 = FindCircleCircleIntersections.Calculate(-500, -200, 500, 500, 100, 542.7);
            listIntserctions3 = FindCircleCircleIntersections.Calculate(500, 100, 515.5, 100, -100, 542.7);

            bool encontro = calculateThreeCircleIntersection.calculate(
                -500,-200, 500,
                500,-100, 515.5,
                500, 100, 595.86231824769629);

           
            Console.ReadKey();
        }
    }
}
