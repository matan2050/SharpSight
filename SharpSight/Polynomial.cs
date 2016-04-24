using SharpSight.Math;

namespace SharpSight
{
	public class Polynomial
	{
		#region FIELDS
		private Vector	coefficients;
		private uint    degree;
		#endregion


		#region CONSTRUCTORS
		public Polynomial(Vector coeffs)
		{
			this.coefficients = coeffs;
			this.degree = (uint)coefficients.MatrixData.Length;
		}
		#endregion


		#region METHODS

		#endregion


		#region PROPERTIES
		public Vector Coefficients
		{
			get
			{
				return this.coefficients;
			}
			set
			{
				this.coefficients = value;
				this.degree = (uint)coefficients.MatrixData.Length;
			}
		}

		public uint Degree
		{
			get
			{
				return this.degree;
			}
		}
		#endregion
	}
}
