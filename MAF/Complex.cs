using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAF
{
    public struct Complex
    {
        private double r, i;

        public Complex(double real, double imaginary)
        {
            this.r = real;
            this.i = imaginary;
        }

        public double Real { get { return r; } }
        public double Imaginary { get { return i; } }

        public override string ToString() => string.Join("", ((r != 0 ? $"{r} " : "")
            + (i > 0 ? (i != 1 ? (r != 0 ? $"+ {i}i" : $"{i}i") : (r != 0 ? "+ i" : "i"))
                : (i != 0 ? (i != -1 ? (r != 0 ? $"- {-1 * i}i" : $"-{-1 * i}i") : (r != 0 ? "- i" : "-i")) : "")))
                    .DefaultIfEmpty('0'));

        public static Complex Sum(Complex a, Complex b) => new Complex(a.r + b.r, a.i + b.i);
        public static Complex operator +(Complex a, Complex b) => Sum(a, b);

        public static Complex Multiply(Complex a, Complex b)
        {
            double real = a.r * b.r + (a.i * b.i * -1);
            double imaginary = a.r * b.i + a.i * b.r;
            return new Complex(real, imaginary);
        }
        public static Complex operator *(Complex a, Complex b) => Multiply(a, b);

        public static Complex Subtract(Complex a, Complex b) => new Complex(a.r - b.r, a.i - b.i);
        public static Complex operator -(Complex a, Complex b) => Subtract(a, b);

        public static Complex Divide(Complex a, Complex b)
        {
            Complex tmp = new Complex(b.r, -1 * b.i);
            Complex numerator = a * tmp;
            double denominator = (b * tmp).r + (b * tmp).i;
            return new Complex(numerator.r / denominator, numerator.i / denominator);
        }
        public static Complex operator /(Complex a, Complex b) => Divide(a, b);

        public static double Abs(Complex a) => Math.Sqrt(a.r * a.r + a.i * a.i);

        public static Complex Pow(Complex a, int n)
        {
            Complex res = a;
            for (int i = 1; i < n; i++)
                res *= a;
            return res;
        }

        public static implicit operator Complex(byte value) => new Complex(value, 0);
        public static implicit operator Complex(sbyte value) => new Complex(value, 0);
        public static implicit operator Complex(short value) => new Complex(value, 0);
        public static implicit operator Complex(ushort value) => new Complex(value, 0);
        public static implicit operator Complex(int value) => new Complex(value, 0);
        public static implicit operator Complex(uint value) => new Complex(value, 0);
        public static implicit operator Complex(long value) => new Complex(value, 0);
        public static implicit operator Complex(ulong value) => new Complex(value, 0);
        public static implicit operator Complex(float value) => new Complex(value, 0);
        public static implicit operator Complex(double value) => new Complex(value, 0);
        public static implicit operator Complex(decimal value) => new Complex((double)value, 0);
    }
}
