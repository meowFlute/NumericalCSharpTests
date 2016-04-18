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

            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
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
            for (int ARow = 0; ARow < A.Rows; ARow++)
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
            for (int row = 0; row < A.Rows; row++)
            {
                for (int column = 0; column < A.Columns; column++)
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
            double[,] temp = new double[A.Rows, A.Columns];

            //add for each value in the matrix
            for (int row = 0; row < A.Rows; row++)
            {
                for (int column = 0; column < A.Columns; column++)
                {
                    temp[row, column] = A.Array2D[row, column] + B.Array2D[row, column];
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

        #region methods
        /// <summary>
        /// Computes the determinant of a matrix by using LU decomposition and then applying gamma*determinant(U) where gamma=determinant(inverse(P))
        /// </summary>
        /// <returns></returns>
        public Matrix determinant()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Computes the inverse of a matrix using LU decomposition with pivoting
        /// </summary>
        /// <returns></returns>
        public Matrix inverse()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Decomposed the matrix represented in Array2D into L and U
        /// </summary>
        /// <returns>Returns a tuple with the order LU, the parity of the permutation matrix, and a vector of a permutation</returns>
        public Tuple<double[,], int, int[]> LUDecompositionWithPivoting()
        {
            //verify that the matrix is square
            if (Rows != Columns)
                throw new ArgumentException("The matrix must be square");

            //store the solution in these objects
            int parity = 1;                              // represents even or odd number of pivots that have taken place
            double[,] LU = new double[Rows, Columns];    // Combined Lower and Upper diagonal matrix from decomposition
            double biggest, sum, temp;
            int biggestRow = 0;
            double[] scalingVector = new double[Rows];
            int[] permutations = new int[Columns];

            //copy the matrix over to LU
            for (int row = 0; row < this.Rows; row++)
            {
                for (int column = 0; column < this.Columns; column++)
                {
                    LU[row, column] = this.Array2D[row, column];
                }
            }

            // get implicit scaling information and check for singular matricies
            for (int row = 0; row < this.Rows; row++)
            {
                biggest = 0;
                for (int column = 0; column < this.Columns; column++)
                {
                    if (Math.Abs(LU[row, column]) > biggest)
                        biggest = Math.Abs(LU[row, column]); // at the end of the inner loop this is the biggest value in the row
                }
                if (biggest == 0) // detect singular matricies
                    throw new ArgumentException("The matrix is singular and cannot be decomposed");
                scalingVector[row] = 1.0 / biggest; //save scaling data for later for Crout's method
            }

            // loop over columns of Crout's method
            for (int column = 0; column < this.Columns; column++)
            {
                for (int row = 0; row < column; row++)
                {
                    sum = LU[row, column];
                    for (int k = 0; k < row; k++)
                    {
                        sum -= LU[row, k] * LU[k, column];
                    }
                    LU[row, column] = sum;
                }
                biggest = 0;
                for (int row = column; row < this.Rows; row++)
                {
                    sum = LU[row, column];
                    for (int k = 0; k < column; k++)
                    {
                        sum -= LU[row, k] * LU[k, column];
                    }
                    LU[row, column] = sum;
                    //would this be the best row so far to pivot?
                    if (scalingVector[row] * Math.Abs(sum) > biggest)
                    {
                        biggest = scalingVector[row] * Math.Abs(sum);
                        biggestRow = row;
                    }

                }
                if (column != biggestRow) //do we need to pivot?
                {
                    for (int k = 0; k < this.Rows; k++)
                    {
                        temp = LU[biggestRow, k];
                        LU[biggestRow, k] = LU[column, k];
                        LU[column, k] = temp;
                    }
                    parity *= -1; //swap the parity of the permutation
                    temp = scalingVector[biggestRow];
                    scalingVector[biggestRow] = scalingVector[column];
                    scalingVector[column] = temp;
                }
                //check for singularity
                permutations[column] = biggestRow;
                if (LU[column, column] == 0.0)
                    throw new ArgumentException("The input matrix is singular");
                if (column != Columns) //divide by the pivot element
                {
                    temp = 1.0 / LU[column, column];
                    for (int row = column + 1; row < Rows; row++)
                    {
                        LU[row, column] *= temp;
                    }
                }
            } // go back for next column reduction
            
            return new Tuple<double[,], int, int[]>(LU, parity, permutations);
        }

        /// <summary>
        /// This equations uses forward and back substitution to take LU and b and return x for Ax=b
        /// </summary>
        /// <param name="LU">The combined upper and lower diagonal matricies (the lower part is L, the upper part is U)</param>
        /// <param name="permutations">The permutation history that went into forming LU</param>
        /// <param name="b">The right hand side of Ax = b</param>
        /// <returns>the vector x</returns>
        public double[] LUBacksubstitution(double[,] LU, int[] permutations, double[] b)
        {
            int pivotRow; 
            int bRow = -1;
            double sum;
            double[] x = new double[this.Rows];
            
            //copy the data from b into x so b is not destroyed.
            //x will eventually be returned as the solution
            Array.Copy(b, x, this.Rows);

            //forward substitution first
            for (int row = 0; row < this.Rows; row++)
            {
                //when bRow is set to something besides -1 it will be the index of the 
                //first nonvansishing element of b. We then do forward substitution while
                //unraveling the previous permutations that went into forming LU
                pivotRow = permutations[row];
                sum = x[pivotRow];
                x[pivotRow] = x[row];
                if (bRow > -1)
                {
                    for (int column = bRow; column < row; column++)
                    {
                        sum -= LU[row, column] * x[column];
                    }
                }
                else if (sum > 0) //non-zero element encountered so from now on bRow will tell us to loop
                    bRow = row;
                //assign the sum to x
                x[row] = sum;
            }
            //backsubstitution
            for(int row = Rows-1; row >= 0; row--)
            {
                sum = x[row];
                for(int column = row+1; column < Rows; column++)
                {
                    sum -= LU[row, column] * x[column];
                }
                x[row] = sum / LU[row, row]; //store final answer
            }
            return x;
        }

        public Tuple<double[,], double[,]> splitLU(double[,] LU)
        {
            if (LU.GetLength(0) != LU.GetLength(1))
                throw new ArgumentException("LU must be a square matrix");

            //split into L and U
            double[,] L = new double[LU.GetLength(0), LU.GetLength(1)];    // Lower diagonal with 1's along main diagonal
            double[,] U = new double[LU.GetLength(0), LU.GetLength(1)];    // Upper diagonal with values along main diagonal

            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    if (row == column)
                    {
                        L[row, column] = 1.0;
                        U[row, column] = LU[row, column];
                    }
                    else if (row < column)
                        U[row, column] = LU[row, column];
                    else
                        L[row, column] = LU[row, column];
                }
            }

            return new Tuple<double[,], double[,]>(L, U);
        }

        public Matrix solveLinearSystem(Matrix b)
        {
            //verify that b has the correct dimensions
            if (b.Columns != 1)
                throw new ArgumentException("b is not a column vector.");
            if (b.Rows != this.Rows)
                throw new ArgumentException("b and A do not have the same number of rows");
            
            //extract the 1D array from b's 2D array
            double[] bCopy = new double[b.Rows];
            for(int row = 0; row < b.Rows; row++)
            {
                bCopy[row] = b.Array2D[row, 0];
            }

            //tuple contains LU, permutation sign, permutation history
            Tuple<double[,], int, int[]> LUDecomposition = this.LUDecompositionWithPivoting();
            double[] x = this.LUBacksubstitution(LUDecomposition.Item1, LUDecomposition.Item3, bCopy);

            //convert the x solution to a 2D array so we can return a matrix
            double[,] xCopy = new double[x.GetLength(0), 1];
            for(int row = 0; row < x.GetLength(0); row++)
            {
                xCopy[row, 0] = x[row];
            }

            //return a matrix
            return new Matrix(xCopy);
        }
            #endregion
    }
}
