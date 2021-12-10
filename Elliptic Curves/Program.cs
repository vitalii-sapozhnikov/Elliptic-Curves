using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elliptic_Curves
{
    class Program
    {
        static void Main(string[] args)
        {
            //EllipticCurvePoint p1 = new EllipticCurvePoint(0, 1);
            //for (int i = 0; i < 8; i++)
            //{
            //    Console.WriteLine(p1*i);
            //}
            //Console.WriteLine(p1.GetOrder());

            EllipticCurve curve = new EllipticCurve(0, 1, 599);
            EllipticCurvePoint[] arr = curve.GetPointsArray();
            string str = "";
            foreach (var item in arr)
                str += item + "\n";
            Console.WriteLine(str);
            Console.WriteLine($"{curve.GetOrder()}");
        }
    }
}
