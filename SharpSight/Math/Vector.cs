using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpSight.Math
{
	public class Vector : Matrix
	{
		public Vector(uint numElements) : base(numElements, 1)
		{
		}
	}
}
