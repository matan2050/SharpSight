using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpSight.Math;

namespace SharpSight.CameraModel
{
	public abstract class BaseModel
	{
		#region METHODS
		public abstract Vector PointToPixel(Vector point);

		public abstract Vector PixelToVector(Vector pixel);
		#endregion
	}
}
