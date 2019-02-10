using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAF
{
    public struct Matrix
    {
        private double[,] mx;

        public Matrix(double[,] array2)
        {
            mx = (double[,])array2.Clone();
            CountRows = mx.GetLength(0);
            CountColumns = mx.GetLength(1);
        }

        public double[,] Get { get { return (double[,])mx.Clone(); } }

        public int Length { get { return mx.Length; } }

        public int CountRows { get; }

        public int CountColumns { get; }

        public static Matrix Sum(Matrix a, Matrix b)
        {
            if (a.CountRows != b.CountRows || a.CountColumns != b.CountColumns)
                throw new ArgumentException("Размерности матриц должны быть одинаковы.");
            int rs = a.CountRows, cs = a.CountColumns;
            double[,] res = new double[rs, cs];
            for (int i = 0; i < rs; i++)
                for (int j = 0; j < cs; j++)
                    res[i, j] = a.mx[i, j] + b.mx[i, j];
            return new Matrix(res);
        }

        public double[,] Multiplication(double[,] a, double[,] b)
        {
            int n = a.GetLength(0);
            double[,] res = new double[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    for (int k = 0; k < n; k++)
                        res[i, j] += a[i, k] * b[k, j];
            return res;
        }
        public double[,] Transpose(double[,] a)
        {
            int n = a.GetLength(0);
            double[,] res = new double[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    res[i, j] = a[j, i];
            return res;
        }
        public double Determinant(double[,] a)
        {
            int n = a.GetLength(0), m = n - 1;
            double res = 0;
            if (n == 1)
                res = a[0, 0];
            else if (n >= 2)
            {
                for (int i = 0; i < n; i++)
                {
                    double tmp = a[0, i];
                    double[,] b = new double[m, m];
                    for (int j = 0; j < m; j++)
                    {
                        for (int k = 0; k < m; k++)
                        {
                            if (i == 0 || k >= i)
                                b[j, k] = a[j + 1, k + 1];
                            else if (i == m || k < i)
                                b[j, k] = a[j + 1, k];
                        }
                    }
                    if (i % 2 == 0)
                        res += tmp * Determinant(b);
                    else
                        res -= tmp * Determinant(b);
                }
            }
            return res;
        }
        public static void Print(Matrix a)
        {
            int rs = a.CountRows, cs = a.CountColumns;
            for (int i = 0; i < rs; i++)
            {
                for (int j = 0; j < cs; j++)
                    Console.Write($"{a.mx[i, j],4}");
                Console.WriteLine();
            }
        }
    }
}
