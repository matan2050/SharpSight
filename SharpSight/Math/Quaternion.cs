using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpSight.Math
{
	public class Quaternion : Vector
	{
		#region FIELDS
		#endregion


		#region CONSTRUCTOR
		public Quaternion(Vector axis, double angle) : base(4)
		{
			Vector axisNorm = (Vector)axis.Normalize();
			Element(0, 0, System.Math.Cos(angle / 2));
			Element(1,
				axis.Element(0) * System.Math.Sin(angle / 2));
			Element(2,
				axis.Element(1) * System.Math.Sin(angle / 2));
			Element(3,
				axis.Element(2) * System.Math.Sin(angle / 2));
		}

		public Quaternion() : base(4)
		{
		}
		#endregion


		public Quaternion(Vector vec) : base(4)
		{
			if (vec.Dimensions[0] == 3)
			{
				Element(0, 0);
				Element(1, vec.Element(0));
				Element(2, vec.Element(1));
				Element(3, vec.Element(2));
			}

			if (vec.Dimensions[0] == 4)
			{
				Element(0, vec.Element(0));
				Element(1, vec.Element(1));
				Element(2, vec.Element(2));
				Element(3, vec.Element(3));
			}

			if ((vec.Dimensions[0] < 3) || (vec.Dimensions[0] > 4))
			{
				throw new IndexOutOfRangeException();
			}
		}


		#region METHODS
		public Quaternion Conjugate()  // TODO - IMPLEMENT QUATERNION CONJUGATE
		{
			Quaternion conj = new Quaternion();

			return conj;
		}

		public Vector Rotate(Vector toRotate)
		{
			Quaternion  vecAsQuaternion = new Quaternion(toRotate);
			Quaternion	conjugate		= Conjugate();
			Quaternion	rotated			= (this*vecAsQuaternion)*conjugate;

			return rotated;
		}
		#endregion


		#region OPERATOR_OVERLOADS
		public static Quaternion operator *(Quaternion a, Quaternion b)
		{
			Quaternion product = new Quaternion();
			product.Element(0,
				a.Element(0) * b.Element(0) - a.Element(1) * b.Element(1) -
				a.Element(2) * b.Element(2) - a.Element(3) * b.Element(3));

			product.Element(1,
				a.Element(0) * b.Element(1) + a.Element(1) * b.Element(0) +
				a.Element(2) * b.Element(3) - a.Element(3) * b.Element(2));

			product.Element(2,
				a.Element(0) * b.Element(2) - a.Element(1) * b.Element(3) +
				a.Element(2) * b.Element(0) + a.Element(3) * b.Element(1));

			product.Element(3,
				a.Element(0) * b.Element(3) + a.Element(1) * b.Element(2) -
				a.Element(2) * b.Element(1) + a.Element(3) * b.Element(0));

			return product;
		}
		#endregion
	}
}
