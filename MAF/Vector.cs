using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAF
{
    public class Vector
    {
        public double x, y;
        public Vector Sum(Vector a, Vector b) => new Vector { x = a.x + b.x, y = a.y + b.y };
        public Vector Subtract(Vector a, Vector b) => new Vector { x = a.x - b.x, y = a.y - b.y };
        public double ScalarMultipication(Vector a, Vector b) => a.x * b.x + a.y * b.y;
        public Vector MultipicationByAFactor(Vector a, double k) => new Vector { x = k * a.x, y = k * a.y };
        public double Abs(Vector a) => Math.Sqrt(Math.Pow(a.x, 2) + Math.Pow(a.y, 2));
        public string Print(Vector a) => $"({a.x}; {a.y})";
    }
}
