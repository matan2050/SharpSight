using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpSight.Math
{
	public class Vector : Matrix
	{
		#region CONSTRUCTOR
		public Vector(uint numElements) : base(numElements, 1)
		{
		}

		public Vector(Matrix mat) : base(mat.Dimensions[0], 1)
		{
		}
		#endregion


		#region ACCESS_METHODS
		public double Element(uint row)
		{
			return base.Element(row, 0);
		}

		public void Element(uint row, double value)
		{
			base.Element(row, 0, value);
		}
		#endregion


		#region OPERATOR_OVERLOADS
		public static Vector operator*(Matrix mat, Vector vec)
		{
			Matrix		vecAsMatrix		= new Matrix(vec);
			Matrix		productAsMatrix = mat*vecAsMatrix;
			return (Vector)productAsMatrix;
		}
		#endregion
	}
}
