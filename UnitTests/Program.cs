using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpSight;
using SharpSight.Math;
using SharpSight.Math.Tests;

namespace UnitTests
{
	class Program
	{
		static void Main(string[] args)
		{
			MatrixTest.TestConstructors();
			MatrixTest.TestSpecialMatrices();
			MatrixTest.TestConcatenation();
			MatrixTest.TestInversion();
		}		
	}
}
