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

        public override string ToString()
        {
            return string.Concat((r != 0 ? $"{r} " : "") +
                (i > 0 ? (i != 1 ? (r != 0 ? $"+ {i}i" : $"{i}i") : (r != 0 ? "+ i" : "i")) :
                (i != 0 ? (i != -1 ? (r != 0 ? $"- {-i}i" : $"-{-i}i") : (r != 0 ? "- i" : "-i")) : "")))
                .DefaultIfEmpty('0').ToString();
        }

        public string ToString(string format)
        {
            if (format.ToLower()[0].ToString() == "t")
                return string.Concat("");
            return ToString();
        }

        public Complex Conjugate() => new Complex(this.r, -this.i);
        public static Complex Conjugate(Complex a) => new Complex(a.r, -a.i);

        public static Complex Negative(Complex a) => new Complex(-a.r, -a.i);
        public static Complex operator -(Complex a) => Negative(a);

        public static Complex Sum(Complex a, Complex b) => new Complex(a.r + b.r, a.i + b.i);
        public static Complex operator +(Complex a, Complex b) => Sum(a, b);

        public static Complex Multiply(Complex a, Complex b)
        {
            double real = a.r * b.r /*+ (a.i * b.i * -1)*/ - a.i * b.i;
            double imaginary = a.r * b.i + a.i * b.r;
            return new Complex(real, imaginary);
        }
        public static Complex operator *(Complex a, Complex b) => Multiply(a, b);

        public static Complex Subtract(Complex a, Complex b) => new Complex(a.r - b.r, a.i - b.i);
        public static Complex operator -(Complex a, Complex b) => Subtract(a, b);

        public static Complex Divide(Complex a, Complex b)
        {
            //Complex tmp = new Complex(b.r, -1 * b.i);
            Complex numerator = a * b.Conjugate();
            //double denominator = (b * b.Conjugate()).r + (b * b.Conjugate()).i;
            double denominator = b.r * b.r + b.i * b.i;
            return new Complex(numerator.r / denominator, numerator.i / denominator);
        }
        public static Complex operator /(Complex a, Complex b) => Divide(a, b);

        public static double Abs(Complex a) => Math.Sqrt(a.r * a.r + a.i * a.i);
        public double Abs() => Abs(this);

        public static Complex Pow(Complex a, int n)
        {
            Complex res = a;
            for (int i = 1; i < n; i++)
                res *= a;
            return res;
        }

        public static double Argument(Complex a) => Math.Atan2(a.i, a.r);
        public double Argument() => Math.Atan2(i, r);

        public bool Equals(Complex obj) => this.r == obj.r && this.i == obj.i;
        public static bool operator ==(Complex left, Complex right) => left.Equals(right);
        public static bool operator !=(Complex left, Complex right) => !left.Equals(right);

        public override bool Equals(object obj) => obj is Complex && this == (Complex)obj;

        public bool Equals(Complex left, Complex right) => left == right;

        public override int GetHashCode() => (this.r.GetHashCode() + this.i.GetHashCode()) % 997;

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

        public static explicit operator Complex(Vector value) => new Complex(value.X, value.Y);
    }
}
