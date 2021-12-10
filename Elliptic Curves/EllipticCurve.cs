using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CR = Cryptography.MatrixOperations;


namespace Elliptic_Curves
{
    class EllipticCurve
    {
        public int A { get; set; }
        public int B { get; set; }
        public int P { get; set; }

        public EllipticCurve(int a, int b, int p)
        {
            A = a;
            B = b;
            P = p;

            EllipticCurvePoint.A = a;
            EllipticCurvePoint.P = p;
        }

        public int Equotation(int x)
        {
            return CR.TakeMod(x * x * x + A * x + B, P);
        }

        public EllipticCurvePoint[] GetPointsArray()
        {
            List<EllipticCurvePoint> lst = new List<EllipticCurvePoint>();

            for (int x = 0; x < P; x++)
            {
                int y = CR.squareRoot(Equotation(x), P);
                if (y != -1 && y != 0)
                {
                    lst.Add(new EllipticCurvePoint(x, y));
                    lst.Add(new EllipticCurvePoint(x, -y));
                }
                else if(y == 0)
                    lst.Add(new EllipticCurvePoint(x, y));
            }
            return lst.ToArray();
        }

        public int GetOrder()
        {
            EllipticCurvePoint[] curvePoints = GetPointsArray();
            int nok = 1;
            foreach (var item in curvePoints)
                nok = CR.NOK(nok, item.GetOrder());
            int tmp = nok;
            while (tmp <= P - 2 * Math.Ceiling(Math.Sqrt(P)) + 1)
                tmp += nok;
            return tmp;
        }
    }
}
