using SharpSight.Math;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MatrixConstructTests
{
	[TestClass]
	public class MatrixConstructTests
	{
		[TestMethod]
		public void BuildMatrix()
		{
			Matrix newMat = new Matrix(3,4);
			newMat[0, 0] = 1;
			newMat[0, 1] = 2;
			newMat[0, 2] = 3;
			newMat[0, 3] = 4;

			newMat[1, 0] = 4;
			newMat[1, 1] = 3;
			newMat[1, 2] = 2;
			newMat[1, 3] = 1;

			newMat[2, 0] = 5;
			newMat[2, 1] = 6;
			newMat[2, 2] = 7;
			newMat[2, 3] = 8;

			Assert.AreEqual(newMat.MatrixData[0, 0], 1);
			Assert.AreEqual(newMat.MatrixData[0, 1], 2);
			Assert.AreEqual(newMat.MatrixData[0, 2], 3);
			Assert.AreEqual(newMat.MatrixData[0, 3], 4);
			Assert.AreEqual(newMat.MatrixData[1, 0], 4);
			Assert.AreEqual(newMat.MatrixData[1, 1], 3);
			Assert.AreEqual(newMat.MatrixData[1, 2], 2);
			Assert.AreEqual(newMat.MatrixData[1, 3], 1);
			Assert.AreEqual(newMat.MatrixData[2, 0], 5);
			Assert.AreEqual(newMat.MatrixData[2, 1], 6);
			Assert.AreEqual(newMat.MatrixData[2, 2], 7);
			Assert.AreEqual(newMat.MatrixData[2, 3], 8);
		}
	}
}
