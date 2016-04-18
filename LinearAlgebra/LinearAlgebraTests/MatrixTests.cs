using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinearAlgebra;

namespace LinearAlgebraTests
{
    [TestClass]
    public class MatrixTests
    {
        /// <summary>
        /// Checks for correct dimensions in Rows, Columns, and Array2D properties and then checks for correct values at each value in Array2D
        /// </summary>
        [TestMethod]
        public void Constructor_ValueInitialize()
        {
            int rows = 2;
            int columns = 3;
            Matrix A = new Matrix(3, rows, columns);

            //check rows
            Assert.AreEqual(rows, A.Rows);
            Assert.AreEqual(rows, A.Array2D.GetLength(0));

            //check columns
            Assert.AreEqual(columns, A.Columns);
            Assert.AreEqual(columns, A.Array2D.GetLength(1));

            //check values
            foreach (double value in A.Array2D)
            {
                Assert.AreEqual(3.0, value);
            }
        }

        [TestMethod]
        public void Constructor_2DArray()
        {
            double[,] array2D = new double[,] { { 1, 2, 3 },
                                                { 4, 5, 6 } };
            Matrix A = new Matrix(array2D);

            //check that the array in the matrix and the array used to initialize are the same
            Assert.AreEqual(array2D, A.Array2D);

            //check the dimensions
            Assert.AreEqual(array2D.GetLength(0), A.Array2D.GetLength(0));
            Assert.AreEqual(array2D.GetLength(1), A.Array2D.GetLength(1));

            //check the Rows and Columns properties

            Assert.AreEqual(array2D.GetLength(0), A.Rows);
            Assert.AreEqual(array2D.GetLength(1), A.Columns);
        }


        /// <summary>
        /// test matrix multiplication using overloaded * operator and correct arguments
        /// </summary>
        [TestMethod]
        public void Operator_MatrixMultiply()
        {
            //matricies and answer
            Matrix A = new Matrix(new double[,] { { -1,  2,  5, -2 },
                                                  {  4,  0, -4,  1 },
                                                  {  3,  6,  7,  8 },
                                                  {  9, 10, 11, 12 } });

            Matrix B = new Matrix(new double[,] { { -3, 13, -1 },
                                                  {  4,  0, 14 },
                                                  {  5,  2,  3 },
                                                  {  6, -4,  7 } });

            Matrix Answer = new Matrix(new double[,] {{ 24,  5, 30 },
                                                      {-26, 40, -9 },
                                                      { 98, 21, 158},
                                                      {140, 91, 248} });

            //carry out multiplication as test
            Matrix test = A * B;

            //check the result against the known answer
            for (int row = 0; row < test.Rows; row++)
            {
                for (int column = 0; column < test.Columns; column++)
                {
                    Assert.AreEqual(Answer.Array2D[row, column], test.Array2D[row, column]);
                }
            }
            Assert.AreEqual(Answer.Rows, test.Rows);
            Assert.AreEqual(Answer.Columns, test.Columns);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Operator_MatrixMultiply_IncorrectDimensions()
        {
            //matricies and answer
            Matrix A = new Matrix(new double[,] { { -1,  2,  5 },
                                                  {  4,  0, -4 },
                                                  {  3,  6,  7 },
                                                  {  9, 10, 11 } });

            Matrix B = new Matrix(new double[,] { { -3, 13, -1 },
                                                  {  4,  0, 14 },
                                                  {  5,  2,  3 },
                                                  {  6, -4,  7 } });

            //carry out multiplication as test
            Matrix test = A * B;
        }

        [TestMethod]
        public void Operator_ScalarMultiply()
        {
            int originalValue = 1;
            int rows = 3;
            int columns = 3;
            double scalar = 5.0;

            //generate a 3x3 matrix of all ones
            Matrix A = new Matrix(originalValue, rows, columns);

            //should have the same results as far as I'm concerned
            Matrix test1 = scalar * A;
            Matrix test2 = A * scalar;

            //test 1 results
            foreach (double value in test1.Array2D)
            {
                Assert.AreEqual(originalValue * scalar, value);
            }

            //test 2 results
            foreach (double value in test2.Array2D)
            {
                Assert.AreEqual(originalValue * scalar, value);
            }

            //the dimensions should not change
            Assert.AreEqual(rows, test1.Rows);
            Assert.AreEqual(columns, test1.Columns);
            Assert.AreEqual(rows, test2.Rows);
            Assert.AreEqual(columns, test2.Columns);
        }

        [TestMethod]
        public void Operator_MatrixAddition()
        {
            //known quantities
            Matrix A = new Matrix(new double[,] {   { 1, 2, 4 },
                                                    { 3, 1, 2 },
                                                    { 4, 1, 3 } });

            Matrix B = new Matrix(new double[,] {   { 7, 3, 1 },
                                                    { 2, 3, 5 },
                                                    { 8, 1, 6 } });

            Matrix Answer = new Matrix(new double[,] {  { 8, 5, 5 },
                                                        { 5, 4, 7 },
                                                        {12, 2, 9 } });

            //carry out the test
            Matrix test = A + B;

            //check the result against the known answer
            for (int row = 0; row < test.Rows; row++)
            {
                for (int column = 0; column < test.Columns; column++)
                {
                    Assert.AreEqual(Answer.Array2D[row, column], test.Array2D[row, column]);
                }
            }
            Assert.AreEqual(Answer.Rows, test.Rows);
            Assert.AreEqual(Answer.Columns, test.Columns);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Operator_MatrixAddition_incorrectArguments()
        {
            //known quantities
            Matrix A = new Matrix(new double[,] {   { 1, 2, 4 },
                                                    { 3, 1, 2 }, });

            Matrix B = new Matrix(new double[,] {   { 7, 3, 1 },
                                                    { 2, 3, 5 },
                                                    { 8, 1, 6 } });
            Matrix test = A + B;
        }

        [TestMethod]
        public void Operator_MatrixSubtraction()
        {
            //known quantities
            Matrix A = new Matrix(new double[,] {   { 1, 2, 4 },
                                                    { 3, 1, 2 },
                                                    { 4, 1, 3 } });

            Matrix B = new Matrix(new double[,] {   { 7, 3, 1 },
                                                    { 2, 3, 5 },
                                                    { 8, 1, 6 } });

            Matrix Answer = new Matrix(new double[,] {  {-6,-1, 3 },
                                                        { 1,-2,-3 },
                                                        {-4, 0,-3 } });

            //carry out the test
            Matrix test = A - B;

            //check the result against the known answer
            for (int row = 0; row < test.Rows; row++)
            {
                for (int column = 0; column < test.Columns; column++)
                {
                    Assert.AreEqual(Answer.Array2D[row, column], test.Array2D[row, column]);
                }
            }
            Assert.AreEqual(Answer.Rows, test.Rows);
            Assert.AreEqual(Answer.Columns, test.Columns);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Operator_MatrixSubtraction_incorrectArguments()
        {
            //known quantities
            Matrix A = new Matrix(new double[,] {   { 1, 2, 4 },
                                                    { 3, 1, 2 }, });

            Matrix B = new Matrix(new double[,] {   { 7, 3, 1 },
                                                    { 2, 3, 5 },
                                                    { 8, 1, 6 } });
            Matrix test = A - B;
        }

        [TestMethod]
        public void PivotingLUDecomposition_CorrectAnswerTest()
        {
            Matrix A = new Matrix(new double[,] {   { 2, 1, -3 },
                                                    {-1, 3,  2 },
                                                    { 3, 1, -3 }});
            Matrix UAnswer = new Matrix(new double[,] {  { 3,           1, -3 },
                                                         { 0, 3+(1.0/3.0),  1 },
                                                         { 0,           0, -1.1 }});
            Matrix LAnswer = new Matrix(new double[,] {  { 1,           0,   0 },
                                                         {-(1.0/3.0),   1,   0 },
                                                         { (2.0/3.0), 0.1,   1 }});



            Tuple<double[,], int, int[]> test = A.LUDecompositionWithPivoting();

            Tuple<double[,], double[,]> LUsplit = A.splitLU(test.Item1);
            
            //verify that the answer makes sense
            for(int row = 0; row < UAnswer.Rows; row++)
            {
                for(int column = 0; column < UAnswer.Columns; column++)
                {
                    Assert.AreEqual(LAnswer.Array2D[row, column], LUsplit.Item1[row, column]); //check L
                    Assert.AreEqual(UAnswer.Array2D[row, column], LUsplit.Item2[row, column]); //check U
                }
            }
            //check parity
            Assert.AreEqual(-1, test.Item2);
        }

        [TestMethod]
        public void SystemOfEquations_CorrectAnswerTest()
        {
            Matrix A = new Matrix(new double[,] {   { 15, 2, -4},
                                                    {  5, 1, -1},
                                                    {  7, 5,  3}});
            Matrix b = new Matrix(new double[,] {   { 3 },
                                                    { 5 },
                                                    { 1 }});
            Matrix x = A.solveLinearSystem(b);

            Matrix answer = new Matrix(new double[,] {  { -26   },
                                                        {  73.5 },
                                                        { -61.5 }});

            double machineEpsilon = 1.0d;

            do{
                machineEpsilon /= 2.0d;
            }while (1.0 + machineEpsilon != 1.0);

            for (int row = 0; row < x.Rows; row++)
            {
                Assert.AreEqual(x.Array2D[row, 0], answer.Array2D[row, 0], 0.0000000001);
            }
        }
    }
}