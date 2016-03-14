using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpSight.Math;

namespace SharpSight.CameraModel
{
	public class PinholeModel
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
		}
		#endregion
	}
}
