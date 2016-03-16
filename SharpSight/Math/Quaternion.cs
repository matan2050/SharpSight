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
			Vector axisNorm = axis.Normalize();
			Element(0, 0, System.Math.Cos(angle / 2));
			Element(1,
				axis.Element(0) * System.Math.Sin(angle / 2));
			Element(2,
				axis.Element(1) * System.Math.Sin(angle / 2));
			Element(3,
				axis.Element(2) * System.Math.Sin(angle / 2));

		}
		#endregion
	}
}
