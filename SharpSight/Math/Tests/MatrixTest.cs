using System;
using SharpSight.Math;

namespace SharpSight.Math.Tests
{
	public static class MatrixTest
	{
		public static bool TestConstructors()
		{
			uint nRows = 3;
			uint nCols = 2;
			Matrix a = new Matrix(nRows,nCols);
			if ((a.Dimensions[0] != nRows) || (a.Dimensions[1] != nCols))
			{
				return false;
			}

			Matrix b = new Matrix(a);
			if ((a.Dimensions[0] != b.Dimensions[0]) || (a.Dimensions[1] != b.Dimensions[1]))
			{
				return false;
			}

			if ((a.Element(0,0) != b.Element(0,0)) ||
				(a.Element(0,1) != b.Element(0,1)) ||
				(a.Element(1,0) != b.Element(1,0)) ||
				(a.Element(1,1) != b.Element(1,1)) ||
				(a.Element(2,0) != b.Element(2,0)) ||
				(a.Element(2,1) != b.Element(2,1)))
			{
				return false;
			}

			Matrix c = new Matrix(5,5, "Ones");
			Console.WriteLine(c.ToString());
			Matrix d = new Matrix(4,7, "Zeros");
			Console.WriteLine(d.ToString());
			Matrix e = new Matrix(5,5, "Eye");
			Console.WriteLine(e.ToString());

			return true;
		}

		public static bool TestSpecialMatrices()
		{
			// Check if Zeros() produces an all zero matrix
			Matrix refMat = new Matrix(3,3);
			refMat.Zeros();

			if ((refMat.Element(0, 0) != 0) ||
				(refMat.Element(0, 1) != 0) ||
				(refMat.Element(0, 2) != 0) ||
				(refMat.Element(1, 0) != 0) ||
				(refMat.Element(1, 1) != 0) ||
				(refMat.Element(1, 2) != 0) ||
				(refMat.Element(2, 0) != 0) ||
				(refMat.Element(2, 1) != 0) ||
				(refMat.Element(2, 2) != 0))
			{
				return false;
			}


			// if zeros work, check Eye()
			refMat.Element(0, 0, 1);
			refMat.Element(1, 1, 1);
			refMat.Element(2, 2, 1);

			Matrix a = new Matrix(3,3);
			a.Eye();

			//if (a != refMat)
			if (a.Equals(refMat))
			{
				return false;
			}
			// Console.WriteLine(a.ToString());


			// check Ones()
			Matrix b = new Matrix(3,3);
			refMat.Element(0, 1, 1);
			refMat.Element(0, 2, 1);
			refMat.Element(1, 0, 1);
			refMat.Element(1, 2, 1);
			refMat.Element(2, 0, 1);
			refMat.Element(2, 1, 1);

			//if (b != refMat)
			if (b.Equals(refMat))
			{
				return false;
			}
			return true;
		}

		public static void TestConcatenation()
		{
			Matrix a = new Matrix(3,3);
			a.Ones();

			Matrix b = new Matrix(3,1);
			b.Zeros();

			Matrix c = a.Concat(b, true);
			Console.WriteLine(c.ToString());

			Matrix d = b.Concat(a, true);
			Console.WriteLine(d.ToString());
			
			/*	FIX VERTICAL CONCATENATION
			Matrix e = a.Concat(b.Transpose(), false);
			Console.WriteLine(e.ToString());

			Matrix f = b.Transpose().Concat(a, false);
			Console.WriteLine(f.ToString());
			*/ 
		}

		/*public static void TestInversion()
		{
			Matrix toInv = new Matrix(3,3);
			toInv.Eye();
			toInv[0, 0] = 2;
			Console.WriteLine(toInv.ToString());
											
			Matrix inverted = MatrixOperations.GaussJordanInversion(toInv);
			Console.WriteLine(inverted.ToString());
		}*/
	}
}
