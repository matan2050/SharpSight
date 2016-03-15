using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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

			return true;
		}

		public static void TestSpecialMatrices()
		{
			Matrix a = new Matrix(3,3);
			a.Ones();
			Console.WriteLine(a.ToString());

			Matrix b = new Matrix(4,4);
			b.Zeros();
			Console.WriteLine(b.ToString());

			Matrix c = new Matrix(2,2);
			c.Eye();
			Console.WriteLine(c.ToString());
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
		}
	}
}
