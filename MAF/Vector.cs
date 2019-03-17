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

        public double X { get { return x; } }

        public double Y { get { return y; } }

        public Vector(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString() => $"({this.x}; {this.y})";

        public static Vector Add(Vector a, Vector b)
        {
            return new Vector(a.x + b.x, a.y + b.y);
        }
        public static Vector operator +(Vector a, Vector b) => Add(a, b);

        public static Vector Subtract(Vector a, Vector b)
        {
            return new Vector(a.x - b.x, a.y - b.y);
        }
        public static Vector operator -(Vector a, Vector b) => Subtract(a, b);

        public static double ScalarMultiply(Vector a, Vector b)
        {
            return a.x * b.x + a.y * b.y;
        }
        //public static double operator *(Vector a, Vector b) => ScalarMultiply(a, b);

        public static Vector MultiplyByAFactor(Vector a, double k)
        {
            return new Vector(k * a.x, k * a.y);
        }
        public static Vector operator *(Vector a, double k) => MultiplyByAFactor(a, k);
        public static Vector operator *(double k, Vector a) => MultiplyByAFactor(a, k);

        public static double Abs(Vector a)
        {
            return Math.Sqrt(a.x * a.y + a.y * a.y);
        }
        public double Abs() => Abs(this);

        public static explicit operator Vector(Complex value) => new Vector(value.Real, value.Imaginary);
    }
}
