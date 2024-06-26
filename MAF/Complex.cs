﻿using System;
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

        public static Complex Zero = new Complex(0, 0);
        public static Complex One = new Complex(1, 0);


        public override string ToString()
        {
            string str = ((r != 0 ? $"{r} " : null) +
                (i > 0 ? (i != 1 ? (r != 0 ? $"+ {i}i" : $"{i}i") : (r != 0 ? "+ i" : "i")) :
                (i != 0 ? (i != -1 ? (r != 0 ? $"- {-i}i" : $"-{-i}i") : (r != 0 ? "- i" : "-i")) : null)));
            return str == "" ? "0" : str;
        }

        public string ToString(string format)
        {
            if (format.ToLower()[0] == 't' || format.ToLower()[0] == 'p')
            {
                double abs = Abs(), arg = Argument();
                string str = $"{abs}(cos({arg}) + isin({arg}))";
                return str;
            }
            else if (format.ToLower()[0] == 'e')
            {
                double abs = Abs(), arg = Argument();
                string str = $"{abs}e^{arg}i";
                return str;
            }
            return ToString();
        }

        public Complex Conjugate() => new Complex(this.r, -this.i);
        public static Complex Conjugate(Complex a) => new Complex(a.r, -a.i);

        public static Complex Negative(Complex a) => new Complex(-a.r, -a.i);
        public static Complex operator -(Complex a) => Negative(a);

        public static Complex Add(Complex a, Complex b) => new Complex(a.r + b.r, a.i + b.i);
        public static Complex operator +(Complex a, Complex b) => Add(a, b);

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

        public static Complex Pow(Complex value, double power) => Pow(value, new Complex(power, 0));

        public static Complex Pow(Complex value, Complex power)
        {
            if (power == Zero)
                return One;
            if (power == One)
                return value;
            if (value == Zero)
                return Zero;
            if (value == One)
                return One;

            double a = value.r;
            double b = value.i;
            double c = power.r;
            double d = power.i;

            double lenval = value.Abs();
            double argval = value.Argument();

            double tmp = c * argval + d * Math.Log(lenval);
            double tmp1 = Math.Pow(lenval, c) * Math.Pow(Math.E, -d * argval);

            return new Complex(tmp1 * Math.Cos(tmp), tmp1 * Math.Sin(tmp));
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
