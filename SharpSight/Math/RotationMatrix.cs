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
		/// <summary>
		/// Euler angles based constructor 
		/// </summary>
		/// <param name="_x">rotation relative to x axis</param>
		/// <param name="_y">rotation relative to y axis</param>
		/// <param name="_z">rotation relative to z axis</param>
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


		/// <summary>
		/// Quaternion based constructor
		/// </summary>
		/// <param name="q">rotation represented by quaternion</param>
		public RotationMatrix(Quaternion q) : base(3,3)
		{
			Element(0, 0,
				2 * (System.Math.Pow(q.Element(0), 2)) - 1 + 
				2 * (System.Math.Pow(q.Element(1), 2)));

			Element(0, 1,
				2 * (q.Element(1) * q.Element(2) +
				q.Element(0) * q.Element(3)));

			Element(0, 2,
				2 * (q.Element(1) * q.Element(3) -
				q.Element(0) * q.Element(2)));


			Element(1, 0,
				2 * (q.Element(1) * q.Element(2) -
				q.Element(0) * q.Element(3)));

			Element(1, 1,
				 2 * (System.Math.Pow(q.Element(0), 2)) +
				 2 * (System.Math.Pow(q.Element(2), 2)));

			Element(1, 2,
				2 * (q.Element(2) * q.Element(3) +
				q.Element(0) * q.Element(1)));


			Element(2, 0,
				2 * (q.Element(1) * q.Element(3) +
				q.Element(0) * q.Element(2)));

			Element(2, 1,
				2 * (q.Element(2) * q.Element(3) -
				q.Element(0) * q.Element(1)));

			Element(2, 2,
				2 * System.Math.Pow(q.Element(0), 2) - 1 +
				2 * System.Math.Pow(q.Element(3), 2));
		}
		#endregion


		#region METHODS
		/*public Matrix ToHomogenous()
		{
			Matrix homogenousEye = new Matrix(4,4);

		}*/

		/// <summary>
		/// Conversion from current rotation matrix to euler angles
		/// </summary>
		/// <returns>a vector of three euler angles</returns>
		public Vector ToEuler()		 // TODO CHECK CORRECTNESS OF ALGORITHM
		{
			Vector euler = new Vector(3);

			double x = System.Math.Atan2(Element(2,1), Element(2,2));
			double y = -System.Math.Atan(Element(2,0)/System.Math.Sqrt(1-System.Math.Pow(Element(2,0),2)));
			double z = System.Math.Atan2(Element(1,0), Element(0,0));

			euler.Element(0, x);
			euler.Element(1, y);
			euler.Element(2, z);

			return euler;
		}
		#endregion
	}
}
