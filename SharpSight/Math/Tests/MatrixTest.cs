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
