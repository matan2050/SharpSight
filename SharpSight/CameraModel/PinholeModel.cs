using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpSight.Math;

namespace SharpSight.CameraModel
{
	public class PinholeModel : BaseModel
	{
		#region FIELDS
		private		Matrix		K;
		private		Matrix		R;
		private		Matrix		C;
		private		Matrix		P;
		#endregion


		#region CONSTRUCTORS
		public PinholeModel()
		{
			K = new Matrix(3, 3);
			R = new Matrix(3, 3);
			C = new Matrix(3, 1);
		}

		public PinholeModel(Matrix _K, Matrix _R, Matrix _C)
		{
			K = _K;
			R = _R;
			C = _C;

			Matrix eye = new Matrix(3,3, "Eye");
			P = K * R * eye.Concat((-1) * C, true);
		}
		#endregion


		#region METHODS
		/// <summary>
		/// Projects 3D point to 2D point in image plane
		/// </summary>
		/// <param name="point">3D point in cartesian coordinates</param>
		/// <returns></returns>
		public override Vector PointToPixel(Vector point)
		{
			Vector		pixel			= new Vector(2);
			Vector		pixelHomogenous = new Vector(3);

			// if input point is non-homogenous

			pixelHomogenous = P * point;

			pixel.Element(0,
				pixelHomogenous.Element(0) / pixelHomogenous.Element(2));

			pixel.Element(1,
				pixelHomogenous.Element(1) / pixelHomogenous.Element(2));

			return pixel;
		}


		public override Vector PixelToVector(Vector pixel)
		{
			throw new NotImplementedException();
			// TODO: REQUIRES IMPLEMENTATION OF MATRIX INVERSION (KR^-1)
		}
		#endregion
	}
}
