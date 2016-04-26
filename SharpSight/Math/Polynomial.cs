using SharpSight.Math;
using SharpSight.Exceptions;

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
		public double Value(Vector parameters)
		{
			if (parameters.Dimensions != this.coefficients.Dimensions)
			{
				throw new MatrixDimensionMismatchException();
			}

			double valueAtPoint = 0;

			for (uint i = 0; i < this.coefficients.MatrixData.Length; i++)
			{
				valueAtPoint += this.coefficients[i] * parameters[i];
			}

			return valueAtPoint;
		}
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
