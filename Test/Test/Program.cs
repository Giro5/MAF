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
            while (true)
            {
                Console.WriteLine("complex, matrix, numeral system, polynom, vector");
                switch (Console.ReadLine().Trim().ToLower())
                {
                    case "complex":
                        ComplexWork();
                        break;
                    case "matrix":
                        MatrixWork();
                        break;
                    case "numeralsystem":
                        NumSysWork();
                        break;
                    case "polynom":
                        PolynomWork();
                        break;
                    case "vector":
                        VectorWork();
                        break;
                }

                Console.Clear();
                //Console.ReadKey();
                
            }
        }
        static void ComplexWork()
        {
            Console.Clear();
            while (true)
            {
                Console.WriteLine("Complex numbers");
                Console.WriteLine("Введите первое комплексное число, целую и мнимую часть разделять пробелом:");
                string str1 = Console.ReadLine();
                Complex c1 = new Complex(Convert.ToDouble(str1.Split()[0]), Convert.ToDouble(str1.Split()[1]));
                Console.WriteLine("Введите второе комплексное число, целую и мнимую часть разделять пробелом:");
                string str2 = Console.ReadLine();
                Complex c2 = new Complex(Convert.ToDouble(str2.Split()[0]), Convert.ToDouble(str2.Split()[1]));
                Console.Clear();
                Console.WriteLine($"Введенные комплексные числа:\n{c1}\n{c2}");
                Console.WriteLine($"Сложение комплексных чисел: {c1 + c2}");
                Console.WriteLine($"Умножение комплексных чисел: {c1 * c2}");
                Console.WriteLine($"Вычитание комплексных чисел: {c1 - c2}");
                Console.WriteLine($"Деление комплексных чисел: {c1 / c2}");
                Console.WriteLine($"Модуль комплексных чисел: {Complex.Abs(c1)}, {Complex.Abs(c2)}");
                Console.Write("Введите необходимую степень: ");
                int n = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine($"Возведение комплексных чисел в степень {n}: {Complex.Pow(c1, n)}, {Complex.Pow(c2, n)}");
                if (Console.ReadLine().ToLower() == "exit")
                    break;
                Console.Clear();
            }
        }
        static void MatrixWork()
        {

        }
        static void NumSysWork()
        {

        }
        static void PolynomWork()
        {
            Console.Clear();
            while (true)
            {
                Console.Write("Введите степень первого многочлена: ");
                double[] monomials = new double[Convert.ToInt32(Console.ReadLine()) + 1];
                for (int i = 0; i < monomials.Length; i++)
                {
                    Console.Write($"Введите {i}-й коэффициент: ");
                    monomials[i] = Convert.ToDouble(Console.ReadLine());
                    if (i == monomials.Length - 1 && monomials[i] == 0)
                        Console.WriteLine("Коэффициент последнего одночлена не может ровняться нулю", i--);
                }
                Polynom c1 = new Polynom(monomials);
                Console.Clear();
                Console.Write("Введите степень второго многочлена: ");
                Array.Resize(ref monomials, Convert.ToInt32(Console.ReadLine()) + 1);
                for (int i = 0; i < monomials.Length; i++)
                {
                    Console.Write($"Введите {i}-й коэффициент: ");
                    monomials[i] = Convert.ToDouble(Console.ReadLine());
                    if (i == monomials.Length - 1 && monomials[i] == 0)
                        Console.WriteLine("Коэффициент последнего одночлена не может ровняться нулю", i--);
                }
                Polynom c2 = new Polynom(monomials);
                c2.HatDisplay = false;
                Console.Clear();
                Console.WriteLine($"Первый многочлен: {c1}\nВторой многочлен: {c2}\n");
                Console.WriteLine($"Сумма многочленов равна: {c1 + c2}\n");
                Console.WriteLine($"Вычитание многочленов равно: {c1 - c2}\n");
                Console.WriteLine($"Произведение многочленов равно: {c1 * c2}\n");
                if (c1.Length > 1 && c2.Length > 1 && c1.Length >= c2.Length)
                    Console.WriteLine($"Деление многочленов с остатком:\nЧастное: {c1 / c2}, остаток: {c1 % c2}\n");
                else
                    Console.WriteLine("Деление невозможно.\n");
                Console.WriteLine($"Отношение многочленов:\nA == B - {c1 == c2}\nA != B - {c1 != c2}\n");
                Console.Write("Введите натуральную степень k: ");
                int k = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine($"Возведение многочленов в степень k = {k}, равно:\nA) {Polynom.Pow(c1, k)}\nB) {Polynom.Pow(c2, k)}\n");
                Console.WriteLine($"Производные многочленов равны:\nA) {Polynom.Derivative(c1)}\nB) {Polynom.Derivative(c2)}\n");
                Console.Write("Введите значение точки x0: ");
                double x0 = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine($"Значение многочленов в точке x0 = {x0}, равно:\nA) {Polynom.Derivative(c1).ToNumber(x0)}\nB) {Polynom.ReplaceX(Polynom.Derivative(c2), x0)}");
                if (Console.ReadLine().ToLower() == "exit")
                    break;
                Console.Clear();
            }
        }
        static void VectorWork()
        {

        }
    }
}
