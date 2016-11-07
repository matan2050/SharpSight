using SharpSight.Math;
using SharpSight.Exceptions;

namespace SharpSight
{
	public class Polynomial
	{
		#region FIELDS
		private Vector	m_Coefficients;
		private Vector  m_Powers;
		private uint	m_Degree;
		#endregion


		#region CONSTRUCTORS
		public Polynomial(Vector coeffs, Vector powers)
		{
			if (powers.MatrixData.Length != coeffs.MatrixData.Length)
			{
				throw new MatrixDimensionMismatchException();
			}

			m_Coefficients = coeffs;
			m_Powers = powers;

			m_Degree = (uint)m_Coefficients.MatrixData.Length;
		}
		#endregion


		#region METHODS
		public double Forward(Vector ndPoint)
		{
			if (ndPoint.Dimensions != m_Coefficients.Dimensions)
			{
				throw new MatrixDimensionMismatchException();
			}

			double value = 0;

			for (uint i = 0; i < m_Coefficients.MatrixData.Length; i++)
			{
				value += m_Coefficients[i] * ndPoint[i];
			}

			return value;
		}



		#endregion


		#region PROPERTIES
		public Vector Coefficients
		{
			get
			{
				return m_Coefficients;
			}
			set
			{
				m_Coefficients = value;
				m_Degree = (uint)m_Coefficients.MatrixData.Length;
			}
		}

		public uint Degree
		{
			get
			{
				return m_Degree;
			}
		}
		#endregion
	}
}
