using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAF
{
    /// <summary>
    /// Class For Any Numeral System 
    /// (up to thirty-six: 0123456789abcdefghijklmnopqrstuvwxyz)
    /// </summary>
    public struct NumeralSystem : IEquatable<NumeralSystem>
    {
        public int GetBase { get; }
        public string GetValue { get; }

        public double ToNum => Convert.ToDouble(this.ToDec().GetValue);

        public NumeralSystem(string Value, int ValueBase)
        {
            if (ValueBase > 36 || ValueBase < 0)
                throw new ArgumentOutOfRangeException("ValueBase", ValueBase, "Base can't be less than zero or more then thirty-six");
            this.GetBase = ValueBase;
            GetValue = Value;
            for (int i = 0; i < GetValue.Length; i++)
            {
                if (char.IsLower(GetValue[i]))//replaces lowercase characters with uppercase characters
                    GetValue = GetValue.Replace(GetValue[i], (char)(GetValue[i] - 32));
                else if (!char.IsDigit(GetValue[i]) && !char.IsLetter(GetValue[i]))
                    throw new ArgumentException($"The Value can contain 0-9 and A-Z(a-z) symbols, the symbol \"{GetValue[i]}\" can't be contained in the Value", "Value");
            }
        }

        public NumeralSystem(string Value) : this(Value, 10) { }

        public NumeralSystem(double Value) : this(Value.ToString()) { }

        public NumeralSystem ToDec()
        {
            if (this.GetBase == 10)
                return new NumeralSystem(this.GetValue);

            double res = 0;
            for (int i = 0; i < this.GetValue.Length; i++)
            {
                if (char.IsDigit(this.GetValue[i]))
                    res += (this.GetValue[i] - 48) * Math.Pow(this.GetBase, this.GetValue.Length - i - 1.0);
                else if (char.IsLetter(this.GetValue[i]))
                    res += (this.GetValue[i] - 55) * Math.Pow(this.GetBase, this.GetValue.Length - i - 1.0);
                else
                    throw new ArgumentException($"The Value can contain 0-9 and A-Z(a-z) symbols, the symbol \"{GetValue[i]}\" mustn't be contained in the Value");
            }
            return new NumeralSystem(res);
        }

        public NumeralSystem ToAnySys(int newbase)
        {
            if (newbase == 10)
                return this.ToDec();
            string str = null;
            FromDecToAny((int)this.ToNum, newbase, ref str);
            return new NumeralSystem(str, newbase);
        }

        private void FromDecToAny(int a, int newbase, ref string str)
        {
            if (a >= newbase)
                FromDecToAny(a / newbase, newbase, ref str);
            int tmp = a % newbase;
            if (tmp <= 9)
                str += tmp;
            else
                str += (char)(tmp + 55);
        }

        public static NumeralSystem Add(NumeralSystem a, NumeralSystem b) => new NumeralSystem(a.ToNum + b.ToNum);
        public static NumeralSystem operator +(NumeralSystem a, NumeralSystem b) => Add(a, b);

        public static NumeralSystem Subtract(NumeralSystem a, NumeralSystem b)
        {
            double res = a.ToNum - b.ToNum;
            return new NumeralSystem(res > 0.0 ? res : 0.0);
        }
        public static NumeralSystem operator -(NumeralSystem a, NumeralSystem b) => Subtract(a, b);

        public static NumeralSystem Multiply(NumeralSystem a, NumeralSystem b) => new NumeralSystem(a.ToNum * b.ToNum);
        public static NumeralSystem operator *(NumeralSystem a, NumeralSystem b) => Multiply(a, b);

        public static NumeralSystem Divide(NumeralSystem a, NumeralSystem b) => new NumeralSystem(Math.Round(a.ToNum / b.ToNum));
        public static NumeralSystem operator /(NumeralSystem a, NumeralSystem b) => Divide(a, b);
        public static NumeralSystem operator %(NumeralSystem a, NumeralSystem b) => new NumeralSystem(Math.Round(a.ToNum % b.ToNum));

        public static bool operator ==(NumeralSystem left, NumeralSystem right) => left.ToNum == right.ToNum;
        public static bool operator !=(NumeralSystem left, NumeralSystem right) => left.ToNum != right.ToNum;
        public static bool operator >=(NumeralSystem left, NumeralSystem right) => left.ToNum >= right.ToNum;
        public static bool operator <=(NumeralSystem left, NumeralSystem right) => left.ToNum <= right.ToNum;
        public static bool operator >(NumeralSystem left, NumeralSystem right) => left.ToNum > right.ToNum;
        public static bool operator <(NumeralSystem left, NumeralSystem right) => left.ToNum < right.ToNum;

        public bool Equals(NumeralSystem other) => this == other;

        public override int GetHashCode() => this.ToNum.GetHashCode() * this.GetBase % 997;

        public override bool Equals(object obj) => (obj is NumeralSystem) && this == (NumeralSystem)obj;

        public override string ToString() => this.GetValue + "№" + this.GetBase.ToString();

        public static implicit operator NumeralSystem(byte value) => new NumeralSystem(value);
        public static implicit operator NumeralSystem(sbyte value) => new NumeralSystem(value);
        public static implicit operator NumeralSystem(short value) => new NumeralSystem(value);
        public static implicit operator NumeralSystem(ushort value) => new NumeralSystem(value);
        public static implicit operator NumeralSystem(int value) => new NumeralSystem(value);
        public static implicit operator NumeralSystem(uint value) => new NumeralSystem(value);
        public static implicit operator NumeralSystem(long value) => new NumeralSystem(value);
        public static implicit operator NumeralSystem(ulong value) => new NumeralSystem(value);
        public static implicit operator NumeralSystem(float value) => new NumeralSystem(value);
        public static implicit operator NumeralSystem(double value) => new NumeralSystem(value);
        public static implicit operator NumeralSystem(decimal value) => new NumeralSystem((double)value);
    }
}
