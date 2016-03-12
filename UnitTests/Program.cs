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
			Matrix<double> testMatrix = new Matrix<double>(3,3);
			testMatrix.Element(1, 1, 100);
		}
	}
}
