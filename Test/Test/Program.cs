using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAF;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Complex cmplx = new Complex();
            Matrix mtrx = new Matrix();
            Polynom plnm = new Polynom();
            Vector vctr = new Vector();
            NumeralSystem numsys = new NumeralSystem();
            Console.WriteLine(0.0000000000000000000000000001m);
            Console.WriteLine((0.0000000000000000000000000001d).ToString());
            Console.ReadKey();
        }
    }
}
