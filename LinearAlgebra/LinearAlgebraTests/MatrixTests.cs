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
            foreach(double value in A.Array2D)
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
    }
}
