﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace MAF
{
    public class Matrix
    {
        private double[,] mx = new[,] { { 0d } };

        public Matrix(double[,] array2)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            if (array2 == null)
                return;
            for (int i = 0; i < array2.GetLength(0); i++)
                for (int j = 0; j < array2.GetLength(1); j++)
                    if (double.IsInfinity(array2[i, j]) || double.IsNaN(array2[i, j]))
                        throw new NotFiniteNumberException("All elements of the matrix must be real numbers.", array2[i, j]);
            if (array2.Length == 0)
                mx = new[,] { { 0d } };
            else
                mx = (double[,])array2.Clone();
            CountRows = mx.GetLength(0);
            CountColumns = mx.GetLength(1);

            watch.Stop();
            TimeInitilization = watch.Elapsed;
        }

        public Matrix(params double[][] array_arrays)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            if (array_arrays == null || array_arrays.Length == 0 || array_arrays[0].Length == 0)
                return;
            for (int i = 1; i < array_arrays.Length; i++)
                if (array_arrays[0].Length != array_arrays[i].Length)
                    throw new ArgumentException("Internal arrays must be of the same length.");
            double[,] array2 = new double[array_arrays.Length, array_arrays[0].Length];
            for (int i = 0; i < array2.GetLength(0); i++)
                for (int j = 0; j < array2.GetLength(1); j++)
                    if (double.IsInfinity(array_arrays[i][j]) || double.IsNaN(array_arrays[i][j]))
                        throw new NotFiniteNumberException("All elements of the matrix must be real numbers.", array_arrays[i][j]);
                    else
                        array2[i, j] = array_arrays[i][j];
            mx = (double[,])array2.Clone();
            CountRows = mx.GetLength(0);
            CountColumns = mx.GetLength(1);

            watch.Stop();
            TimeInitilization = watch.Elapsed;
        }

        public double[,] Get { get { return (double[,])mx.Clone(); } }

        public int Length { get { return mx.Length; } }

        public int CountRows { get; } = 1;

        public int CountColumns { get; } = 1;

        public TimeSpan TimeInitilization { get; }

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
        public static Matrix operator +(Matrix a, Matrix b) => Sum(a, b);

        public static Matrix Subtract(Matrix a, Matrix b)
        {
            if (a.CountRows != b.CountRows || a.CountColumns != b.CountColumns)
                throw new ArgumentException("Размерности матриц должны быть одинаковы.");
            int rs = a.CountRows, cs = a.CountColumns;
            double[,] res = new double[rs, cs];
            for (int i = 0; i < rs; i++)
                for (int j = 0; j < cs; j++)
                    res[i, j] = a.mx[i, j] - b.mx[i, j];
            return new Matrix(res);
        }
        public static Matrix operator -(Matrix a, Matrix b) => Subtract(a, b);

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

        /// <summary>
        /// the parameter "<paramref name="a"/>" must be a square matrix
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static double Determinant(Matrix a)
        {
            if (a.CountRows != a.CountColumns)
                throw new ArgumentException("Matrix must be a square matrix.", "a");
            int n = a.CountRows, m = n - 1;
            if (n == 1)
                return a.mx[0, 0];
            else
            {
                double res = 0;
                for (int i = 0; i < n; i++)
                {
                    double tmp = a.mx[0, i];
                    double[,] b = new double[m, m];
                    for (int j = 0; j < m; j++)
                    {
                        for (int k = 0; k < m; k++)
                        {
                            if (i == 0 || k >= i)
                                b[j, k] = a.mx[j + 1, k + 1];
                            else if (i == m || k < i)
                                b[j, k] = a.mx[j + 1, k];
                        }
                    }
                    res += tmp * Determinant(new Matrix(b)) * (i % 2 == 0 ? 1 : -1);
                }
                return res;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public double Determinant() => Determinant(this);

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
