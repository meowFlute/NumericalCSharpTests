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
    }
}
