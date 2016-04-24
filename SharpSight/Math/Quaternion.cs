using System;

namespace SharpSight.Math
{
	public class Quaternion : Vector
	{
		#region FIELDS
		#endregion


		#region CONSTRUCTOR
		/// <summary>
		/// Constructor from a vector and a rotation relative to that vector
		/// </summary>
		/// <param name="axis">3 element vector</param>
		/// <param name="angle">rotation angle</param>
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


		/// <summary>
		/// Default empty quaternion ctor
		/// </summary>
		public Quaternion() : base(4)
		{
		}
		#endregion

		/// <summary>
		/// Quaternion ctor from 3 element [0, vec] or 4 element vector [vec]
		/// </summary>
		/// <param name="vec">3 or 4 element vector</param>
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
		/// <summary>
		/// Generate quaternion conjugate
		/// </summary>
		/// <returns>conjugate Quaternion</returns>
		public Quaternion Conjugate()
		{
			Quaternion conj = new Quaternion();

			conj.Element(0, Element(0));
			conj.Element(1, -Element(1));
			conj.Element(2, -Element(2));
			conj.Element(3, -Element(3));

			return conj;
		}

		/// <summary>
		/// Rotate using quaternion
		/// </summary>
		/// <param name="toRotate">vector to rotate</param>
		/// <returns>rotated vector</returns>
		public Vector Rotate(Vector toRotate)
		{
			Quaternion  vecAsQuaternion = new Quaternion(toRotate);
			Quaternion	conjugate		= Conjugate();
			Quaternion	rotated			= (this*vecAsQuaternion)*conjugate;

			return rotated;
		}
		#endregion


		#region OPERATOR_OVERLOADS
		/// <summary>
		/// Quaternion multiplication overload
		/// </summary>
		/// <param name="a">left hand side Quaternion</param>
		/// <param name="b">right hand side Quaternion</param>
		/// <returns>product Quaternion</returns>
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
									