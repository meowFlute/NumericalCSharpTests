using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearAlgebra
{
    public class Matrix
    {
        //properties
        public double[,] Array2D { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }

        #region Constructors
        /// <summary>
        /// This function is used to populate a "rows by columns" matrix with the value passed in
        /// </summary>
        /// <param name="value"></param>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        public Matrix(double value, int rows, int columns)
        {
            //keep track of the dimensions of the matrix
            Rows = rows;
            Columns = columns;

            //populate the matrix with the value passed in
            double[,] newArray2D = new double[Rows, Columns];

            for(int row = 0; row < Rows; row++)
            {
                for(int column = 0; column < Columns; column++)
                {
                    newArray2D[row, column] = value;
                }
            }

            //store the array in our property
            Array2D = newArray2D;
        }

        /// <summary>
        /// Takes a 2D array and converts it to a matrix
        /// </summary>
        /// <param name="newArray2D"></param>
        public Matrix(double[,] newArray2D)
        {
            //convert the 2D array into its respective properties
            Rows = newArray2D.GetLength(0);
            Columns = newArray2D.GetLength(1);
            Array2D = newArray2D;
        }
        #endregion

        #region operators
        /// <summary>
        /// Applies matrix multiplaction when the * operator is used on two Matrix objects
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static Matrix operator *(Matrix A, Matrix B)
        {
            //enforce correct dimensions for matrix multiplication
            if (A.Columns != B.Rows)
                throw new ArgumentException("A.Rows must be equal to B.Columns");

            //carry out matrix multiplication
            double[,] tempArray = new double[A.Rows, B.Columns]; //temporary storage of new array
            for(int ARow = 0; ARow < A.Rows; ARow++)
            {
                for (int BColumn = 0; BColumn < B.Columns; BColumn++)
                {
                    double value = 0;
                    for (int i = 0; i < A.Columns; i++)
                    {
                        value += A.Array2D[ARow, i] * B.Array2D[i, BColumn];
                    }
                    tempArray[ARow, BColumn] = value;
                }
            }

            //create and return new Matrix object
            return new Matrix(tempArray);
        }

        /// <summary>
        /// scalar multiplication applied to a matrix
        /// </summary>
        /// <param name="x"></param>
        /// <param name="A"></param>
        /// <returns></returns>
        public static Matrix operator *(double x, Matrix A)
        {
            double[,] temp = new double[A.Rows, A.Columns];
            for(int row = 0; row < A.Rows; row++)
            {
                for(int column = 0; column < A.Columns; column++)
                {
                    // double x double so order doesn't matter
                    temp[row, column] = A.Array2D[row, column] * x;
                }
            }

            return new Matrix(temp);
        }

        /// <summary>
        /// possible syntax of scalar multiplication applied to a matrix
        /// </summary>
        /// <param name="A"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Matrix operator *(Matrix A, double x)
        {
            return x * A;
        }

        /// <summary>
        /// matrix addition - Matricies must have same dimensions
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static Matrix operator +(Matrix A, Matrix B)
        {
            //enforce the correct dimensions 
            if (A.Rows != B.Rows || A.Columns != B.Columns)
                throw new ArgumentException("A and B must have the same dimensions.");

            //temporary storage
            double [,] temp = new double[A.Rows, A.Columns];

            //add for each value in the matrix
            for(int row = 0; row < A.Rows; row++)
            {
                for(int column = 0; column < A.Columns; column++)
                {
                    temp[row,column] = A.Array2D[row, column] + B.Array2D[row, column];
                }
            }

            //return new matrix 
            return new Matrix(temp);
        }

        /// <summary>
        /// matrix subtraction, matricies must have same dimensions
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static Matrix operator -(Matrix A, Matrix B)
        {
            //enforce the correct dimensions 
            if (A.Rows != B.Rows || A.Columns != B.Columns)
                throw new ArgumentException("A and B must have the same dimensions.");

            //temporary storage
            double[,] temp = new double[A.Rows, A.Columns];

            //add for each value in the matrix
            for (int row = 0; row < A.Rows; row++)
            {
                for (int column = 0; column < A.Columns; column++)
                {
                    temp[row, column] = A.Array2D[row, column] - B.Array2D[row, column];
                }
            }

            //return new matrix 
            return new Matrix(temp);
        }
        #endregion
    }
}
