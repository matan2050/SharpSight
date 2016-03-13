using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpSight;
using SharpSight.Math;

namespace UnitTests
{
	class Program
	{
		static void Main(string[] args)
		{
			Matrix testMatrix = new Matrix(3,3);
			testMatrix.Element(1, 1, 100);

			Matrix a = new Matrix(2,3);
			Matrix b = new Matrix(3,5);

			a.Element(0, 0, 1);
			a.Element(0, 1, 2);
			a.Element(0, 2, 3);
			a.Element(1, 0, 4);
			a.Element(1, 1, 5);
			a.Element(1, 2, 6);

			b.Element(0, 0, 1);
			b.Element(0, 1, 2);
			b.Element(0, 2, 3);
			b.Element(0, 3, 4);
			b.Element(0, 4, 5);
			b.Element(1, 0, 6);
			b.Element(1, 1, 7);
			b.Element(1, 2, 8);
			b.Element(1, 3, 9);
			b.Element(1, 4, 9);
			b.Element(2, 0, 9);
			b.Element(2, 1, 9);
			b.Element(2, 2, 9);
			b.Element(2, 3, 9);
			b.Element(2, 4, 9);


			Matrix c = a * b;
		}		
	}
}
