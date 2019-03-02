using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MAF
{
    public class Vector
    {
        private double x, y;

        public Vector(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public static Vector Sum(Vector a, Vector b)
        {
            return new Vector(a.x + b.x, a.y + b.y);
        }

        public static Vector Subtract(Vector a, Vector b)
        {
            return new Vector(a.x - b.x, a.y - b.y);
        }

        public static double ScalarMultiply(Vector a, Vector b)
        {
            return a.x * b.x + a.y * b.y;
        }

        public static Vector MultiplyByAFactor(Vector a, double k)
        {
            return new Vector(k * a.x, k * a.y);
        }

        public static double Abs(Vector a)
        {
            return Math.Sqrt(Math.Pow(a.x, 2) + Math.Pow(a.y, 2));
        }

        public static string Print(Vector a)
        {
            return $"({a.x}; {a.y})";
        }
    }
}
