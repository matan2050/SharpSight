using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpSight.Math
{
	public class RotationMatrix : Matrix
	{
		#region FIELDDS
		private     double      x;
		private     double      y;
		private     double      z;
		#endregion


		#region CONSTRUCTORS
		public RotationMatrix(double _x, double _y, double _z) : base(3,3)
		{
			x = _x;
			y = _y;
			z = _z;

			Matrix	Rx	= new Matrix(3,3);
			Matrix	Ry	= new Matrix(3,3);
			Matrix	Rz	= new Matrix(3,3);

			Rx.Eye();
			Rx.Element(1, 1, System.Math.Cos(x));
			Rx.Element(1, 2, -System.Math.Sin(x));
			Rx.Element(2, 1, System.Math.Sin(x));
			Rx.Element(2, 2, System.Math.Cos(x));

			Ry.Eye();
			Ry.Element(0, 0, System.Math.Cos(y));
			Ry.Element(0, 2, System.Math.Sin(y));
			Ry.Element(2, 0, -System.Math.Sin(x));
			Ry.Element(2, 2, System.Math.Cos(x));

			Rz.Eye();
			Rz.Element(0, 0, System.Math.Cos(z));
			Rz.Element(0, 1, -System.Math.Sin(z));
			Rz.Element(1, 0, System.Math.Sin(z));
			Rz.Element(1, 1, System.Math.Cos(z));

			Matrix R = Rx*Ry*Rz;
			MatrixData = R.MatrixData;
		}
		#endregion


		#region METHODS
		/*public Matrix ToHomogenous()
		{
			Matrix homogenousEye = new Matrix(4,4);

		}*/
		#endregion
	}
}
