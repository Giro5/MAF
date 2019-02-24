using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAF;
using MAF.Braille;

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
                    case "com":
                    case "complex":
                        ComplexWork();
                        break;
                    case "mat":
                    case "matrix":
                        MatrixWork();
                        break;
                    case "num":
                    case "system":
                    case "sys":
                    case "numeralsystem":
                    case "numeral":
                        NumeralSystemWork.Start();
                        break;
                    case "pol":
                    case "polynom":
                        PolynomWork();
                        break;
                    case "vec":
                    case "vector":
                        VectorWork();
                        break;
                }

                Console.Clear();
                //Console.ReadKey();

            }
        }

        /// <summary>
        /// it seems to work
        /// </summary>
        static void ComplexWork()
        {
            Console.Clear();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Complex numbers");
                Console.WriteLine("Введите первое комплексное число, целую и мнимую часть разделять пробелом:");
                string[] str1 = Console.ReadLine().Split();
                Complex c1 = new Complex(Convert.ToDouble(str1[0]), Convert.ToDouble(str1[1]));
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
                Console.WriteLine($"Возведение комплексных чисел в степень {n}: {Complex.Pow(c1, n)}, {Complex.Pow(c2, n)}\nExit?");
                if (Console.ReadLine().ToLower() == "exit")
                    break;
            }
        }

        /// <summary>
        /// smth there
        /// </summary>
        static void MatrixWork()
        {
            Matrix c1 = new Matrix(new double[,] { { 55623, 425, -275, }, { 1.004121, -275, 04 }, { 398567564, -544, 744 } });
            Matrix c2 = new Matrix(new[] { new[] { 9d, 8.1234567890123456789d, 7 }, new[] { 6d, 5, 4 } });
            Matrix c3 = new Matrix(new double[,]{
                { 2, 3, 4, 2, 1, 3, 5 },
                { 2, 6, 8, 4, 5, 6, 2 },
                { 1, 2, 3, 5, 1, 3, 6 },
                { 4, 2, 5, 1, 3, 5, 4 },
                { 5, 6, 2, 3, 1, 2, 3 },
                { 1, 2, 3, 2, 3, 1, 5 },
                { 2, 7, 4, 8, 9, 9, 9 },})
            { Spaces = 4 };
            Matrix c4 = new Matrix((double[][])null);
            Matrix c5 = new Matrix(new double[,] { { 5, 9, 6 }, { 0, 4, 5 }, { 2, 8, 4 }, { 3, 5, 3 }, { 6, 1, 2 } });
            Matrix c6 = new Matrix(new[] { new double[] { 1, 2, 3, 7, 4 }, new double[] { 4, 5, 6, 2, 3, }, new double[] { 7, 8, 9, 4, 2 } });

            //Matrix.Print(c5);
            Console.WriteLine(c1.S());
            //Matrix.Print(c3);
            //Console.WriteLine(c3.Determinant());
            //Matrix.Print(Matrix.Minor(4, 3, c3));
            //Console.WriteLine();
            //Matrix.Print(Matrix.Invertion(c3));
            //Console.WriteLine();
            ////Matrix.Print(Matrix.Invertion(c3) * c3);
            //Console.WriteLine();
            //Matrix.Print(c1);
            //Console.WriteLine(c1.Determinant());
            //Matrix.Print(Matrix.Minor(0, 0, c1));
            //Console.WriteLine();
            //Matrix.Print(Matrix.Invertion(c1));
            //Console.WriteLine();
            //Matrix.Print(Matrix.Invertion(c1) * c1);
            Console.ReadKey();
        }

        /// <summary>
        /// it seems to work
        /// </summary>
        static class NumeralSystemWork
        {
            static NumeralSystem c1, c2, c3;
            static void Definition()
            {
                string str;
                bool b = true;
                while (b)
                {
                    Console.Write("Введите первое число, а через пробел его систему счисления: ");
                    str = Convert.ToString(Console.ReadLine());
                    b = false;
                    //c1 = new NumeralSystem { GetValue = str.Split(' ')[0] };
                    try
                    {
                        c1 = new NumeralSystem(str.Split(' ')[0], Convert.ToInt32(str.Split(' ')[1]));
                    }
                    catch
                    {
                        c1 = new NumeralSystem(str.Split(' ')[0]);
                    }
                    for (int i = 0; i < c1.GetValue.Length; i++)
                    {
                        if ((!char.IsLetter(c1.GetValue[i]) && Convert.ToInt32(c1.GetValue[i].ToString()) >= c1.GetBase) || c1.GetValue[i] - 55 >= c1.GetBase)
                        {
                            b = true;
                            Console.WriteLine("Число введенно не корректно, повторите ввод.");
                            break;
                        }
                    }
                }
                b = true;
                while (b)
                {
                    Console.Write("Введите второе число, а через пробел его систему счисления: ");
                    str = Convert.ToString(Console.ReadLine());
                    b = false;
                    //c2 = new NumeralSystem { GetValue = str.Split(' ')[0] };
                    try
                    {
                        c2 = new NumeralSystem(str.Split(' ')[0], Convert.ToInt32(str.Split(' ')[1]));
                    }
                    catch
                    {
                        c2 = new NumeralSystem(str.Split(' ')[0]);
                    }
                    for (int i = 0; i < c2.GetValue.Length; i++)
                    {
                        if ((!char.IsLetter(c2.GetValue[i]) && Convert.ToInt32(c2.GetValue[i].ToString()) >= c2.GetBase) || c2.GetValue[i] - 55 >= c2.GetBase)
                        {
                            b = true;
                            Console.WriteLine("Число введенно не корректно, повторите ввод.");
                            break;
                        }
                    }
                }
            }
            public static void Start()
            {
                Console.Clear();
                for (Definition(); ;)
                {
                    Console.Clear();
                    Console.WriteLine($"А) {c1.GetValue} в СС {c1.GetBase}");
                    Console.WriteLine($"В) {c2.GetValue} в СС {c2.GetBase}");
                    Console.WriteLine("Введите код операции для ее исполнения, через пробел укажите желаймую систему счисления.");
                    Console.Write("1 - Сумма двух чисел\n2 - Вычитание двух чисел\n3 - Умножение двух чисел\n4 - Деление двух чисел" +
                        "\n5 - Перевод в любую систему счисления\n6 - Отношение двух чисел\n7 - Изменить числа\n8 - Выход\nВаш код - ");
                    string str = Convert.ToString(Console.ReadLine());
                    //c3 = new NumeralSystem { GetValue = str.Split(' ')[0] };
                    try
                    {
                        c3 = new NumeralSystem(str.Split(' ')[0], Convert.ToInt32(str.Split(' ')[1]));
                    }
                    catch
                    {
                        c3 = new NumeralSystem(str.Split(' ')[0]);
                    }
                    switch (c3.GetValue)
                    {
                        case "1":
                            Console.WriteLine($"Сумма двух чисел равна: {(c1 + c2).ToAnySys(c3.GetBase).GetValue} в СС {c3.GetBase}");
                            break;
                        case "2":
                            Console.WriteLine($"Вычитание двух чисел равно: {(c1 - c2).ToAnySys(c3.GetBase).GetValue} в СС {c3.GetBase}");
                            break;
                        case "3":
                            Console.WriteLine($"Умножение двух чисел равно: {(c1 * c2).ToAnySys(c3.GetBase).GetValue} в СС {c3.GetBase}");
                            break;
                        case "4":
                            Console.WriteLine($"Деление двух чисел равно: {(c1 / c2).ToAnySys(c3.GetBase).GetValue} в СС {c3.GetBase}");
                            break;
                        case "5":
                            Console.WriteLine($"Числа в {c3.GetBase} системе счисления равны: {c1.ToAnySys(c3.GetBase).GetValue}, {c2.ToAnySys(c3.GetBase).GetValue}");
                            break;
                        case "6":
                            Console.WriteLine($"Отношения двух чисел таковы:\nA == B - {c1 == c2}\nA != B - {c1 != c2}\nA >= B - {c1 >= c2}" +
                                $"\nA <= B - {c1 <= c2}\nA > B - {c1 > c2}\nA < B - {c1 < c2}");
                            break;
                        case "7":
                            Definition();
                            break;
                        case "8":
                            return;
                    }
                    Console.Write("Для продолжения нажмите любую кнопку.");
                    Console.ReadKey();
                }
            }
        }

        /// <summary>
        /// it seems to work
        /// </summary>
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
                Console.WriteLine($"Сумма многочленов равна: {new Polynom((c1 + c2).Get) { HatDisplay = false }}\n");
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
                Console.WriteLine();
                if (Console.ReadLine().ToLower() == "exit")
                    break;
                Console.Clear();
            }
        }

        /// <summary>
        /// empty
        /// </summary>
        static void VectorWork()
        {

        }
    }
}
