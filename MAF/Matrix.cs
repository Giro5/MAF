using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace MAF
{
    [System.Serializable]
    class NonSqareMatrixException : Exception
    {
        public NonSqareMatrixException() { }
        public NonSqareMatrixException(string message) : base(message) { }
        public NonSqareMatrixException(string message, Exception inner) : base(message, inner) { }
        protected NonSqareMatrixException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    public class Matrix
    {
        private double[,] mx = new[,] { { 0d } };

        public Matrix(double[,] array2)
        {
            if (array2 == null || array2.Length == 0)
                return;
            for (int i = 0; i < array2.GetLength(0); i++)
                for (int j = 0; j < array2.GetLength(1); j++)
                    if (double.IsInfinity(array2[i, j]) || double.IsNaN(array2[i, j]))
                        throw new NotFiniteNumberException("All elements of the matrix must be real numbers.", array2[i, j]);
            mx = new double[array2.GetLength(0), array2.GetLength(1)];
            for (int i = 0; i < array2.GetLength(0); i++)
                for (int j = 0; j < array2.GetLength(1); j++)
                    mx[i, j] = Math.Round(array2[i, j], decimals);
            CountRows = mx.GetLength(0);
            CountColumns = mx.GetLength(1);
        }

        public Matrix(params double[][] array_arrays)
        {
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
            mx = new double[array2.GetLength(0), array2.GetLength(1)];
            for (int i = 0; i < array2.GetLength(0); i++)
                for (int j = 0; j < array2.GetLength(1); j++)
                    mx[i, j] = Math.Round(array2[i, j], decimals);
            CountRows = mx.GetLength(0);
            CountColumns = mx.GetLength(1);
        }

        public double[,] Get { get { return (double[,])mx.Clone(); } }

        public int Length { get { return mx.Length; } }

        public int CountRows { get; } = 1;

        public int CountColumns { get; } = 1;

        private int decimals = 15;

        public bool Irrationality { get; set; } = false;

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

        public static Matrix Multiply(Matrix a, Matrix b)
        {
            if (a.CountColumns != b.CountRows)
                throw new ArgumentException("The number of columns of the first matrix and rows of the second matrix must be equal.");
            double[,] res = new double[a.CountRows, b.CountColumns];
            for (int i = 0; i < a.CountRows; i++)
                for (int j = 0; j < b.CountColumns; j++)
                {
                    decimal solutionOverflow = 0;
                    for (int k = 0; k < a.CountColumns; k++)
                        solutionOverflow += (decimal)a.mx[i, k] * (decimal)b.mx[k, j];
                    res[i, j] = (double)solutionOverflow;
                }
            return new Matrix(res);
        }
        public static Matrix operator *(Matrix a, Matrix b) => Multiply(a, b);
        public static Matrix Multiply(double k, Matrix a)
        {
            if (double.IsInfinity(k) || double.IsNaN(k))
                throw new ArgumentException("The number k must be real number.", "k");
            double[,] res = new double[a.CountRows, a.CountColumns];
            for (int i = 0; i < a.CountRows; i++)
                for (int j = 0; j < a.CountColumns; j++)
                    res[i, j] = k * a.mx[i, j];
            return new Matrix(res);
        }
        public static Matrix operator *(double k, Matrix a) => Multiply(k, a);
        public static Matrix operator *(Matrix a, double k) => Multiply(k, a);

        public static Matrix operator /(Matrix a, double k) => 1 / k * a;

        public static Matrix Transpose(Matrix a)
        {
            double[,] res = new double[a.CountColumns, a.CountRows];//swap
            for (int i = 0; i < a.CountColumns; i++)
                for (int j = 0; j < a.CountRows; j++)
                    res[i, j] = a.mx[j, i];
            return new Matrix(res);
        }
        public Matrix Transpose() => Transpose(this);

        /// <summary>
        /// the parameter "<paramref name="a"/>" must be a square matrix
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static double Determinant(Matrix a)
        {
            if (a.CountRows != a.CountColumns)
                throw new ArgumentException("The Matrix must be a square matrix.", "a");
            if (a.CountRows == 1)
                return a.mx[0, 0];
            else
            {
                double res = 0;
                for (int i = 0; i < a.CountRows; i++)
                    res += a.mx[0, i] * Determinant(Minor(0, i, a)) * (i % 2 == 0 ? 1 : -1);
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
                    Console.Write($"{a.mx[i, j],25}");
                Console.WriteLine();
            }
        }

        public static Matrix Invertion(Matrix a)
        {
            if (a.CountRows != a.CountColumns)
                throw new ArgumentException("The matrix must be a square matrix.", "a");
            if (a.Determinant() == 0)
                throw new ArgumentException("The determinant of the matrix must be nonzero.", "a");
            double[,] res = new double[a.CountRows, a.CountColumns];
            for (int i = 0; i < a.CountColumns; i++)
                for (int j = 0; j < a.CountRows; j++)
                    res[i, j] = Determinant(Minor(i, j, a)) * ((i + j) % 2 == 0 ? 1 : -1);
            return new Matrix(res).Transpose() / a.Determinant();
        }
        public Matrix Invertion() => Invertion(this);

        public static Matrix Minor(int i, int j, Matrix a)
        {
            if (a.CountRows != a.CountColumns)
                throw new ArgumentException("The matrix must be a square matrix.", "a");
            if (i >= a.CountRows)
                throw new ArgumentException("The row index must be less then the number of rows.", "i");
            if (j >= a.CountColumns)
                throw new ArgumentException("The column index must be less then the number of columns.", "j");
            int m = a.CountRows - 1;
            double[,] res = new double[m, m];
            for (int ii = 0; ii < m; ii++)
                for (int jj = 0; jj < m; jj++)
                {
                    if (ii < i && jj < j)
                        res[ii, jj] = a.mx[ii, jj];
                    else if (ii < i && jj >= j)
                        res[ii, jj] = a.mx[ii, jj + 1];
                    else if (ii >= i && jj < j)
                        res[ii, jj] = a.mx[ii + 1, jj];
                    else if (ii >= i && jj >= j)
                        res[ii, jj] = a.mx[ii + 1, jj + 1];
                }
            return new Matrix(res);
        }
        public Matrix Minor(int i, int j) => Minor(i, j, this);
    }
}
