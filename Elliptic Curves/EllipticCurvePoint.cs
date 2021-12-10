using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CR = Cryptography.MatrixOperations;

namespace Elliptic_Curves
{
    class EllipticCurvePoint
    {
        public static int A = 2;
        public static int P = 41;
        private bool isInfinity = false;
        public EllipticCurvePoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public EllipticCurvePoint()
        {
            X = 0;
            Y = 0;
        }

        public EllipticCurvePoint(EllipticCurvePoint p)
        {
            X = p.X;
            Y = p.Y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public static EllipticCurvePoint operator +(EllipticCurvePoint first, EllipticCurvePoint second)
        {
            if(first.isInfinity || second.isInfinity)
            {
                EllipticCurvePoint curvePoint = new EllipticCurvePoint();
                curvePoint.isInfinity = true;
                return curvePoint;
            }
            int m, p = P;
            if (first.X != second.X)
                m = CR.TakeMod((second.Y - first.Y) * CR.ElementReverse(second.X - first.X, p), p);
            else if (first.X == second.X && first.Y == second.Y)
                m = CR.TakeMod((3 * first.X * first.X + A) * CR.ElementReverse(2 * first.Y, p),p);
            else
            {
                EllipticCurvePoint curvePoint = new EllipticCurvePoint();
                curvePoint.isInfinity = true;
                return curvePoint;
            }

            int x = CR.TakeMod(m * m - first.X - second.X, p);
            int y = CR.TakeMod(m * (first.X - x) - first.Y, p);

            return new EllipticCurvePoint(x, y);
        }

        public static EllipticCurvePoint operator *(EllipticCurvePoint point, int n)
        {
            EllipticCurvePoint newPoint = new EllipticCurvePoint(point);
            for (int i = 0; i < n; i++)
                newPoint += point;
            return newPoint;
        }
        public static EllipticCurvePoint operator *(int n, EllipticCurvePoint point)
        {
            EllipticCurvePoint newPoint = new EllipticCurvePoint(point);
            for (int i = 0; i < n; i++)
                newPoint += point;
            return newPoint;
        }
        public override string ToString()
        {
            if (isInfinity)
                return "(infinity)";
            return $"({X, 2}, {Y,2})";
        }
        public int GetOrder()
        {
            int c = 0;
            while(true)
            {
                EllipticCurvePoint point = c * this;
                if (point.ToString() == "(infinity)")
                    return ++c;
                c++;
            }
        }
    }
}
