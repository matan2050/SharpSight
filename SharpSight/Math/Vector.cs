using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpSight.Math
{
	public class Vector : Matrix
	{
		#region CONSTRUCTOR
		public Vector(uint numElements) : base(numElements, 1)
		{
		}

		public Vector(Matrix mat) : base(mat.Dimensions[0], 1)
		{
		}
		#endregion


		#region ACCESS_METHODS
		/// <summary>
		/// Access vector element by row index
		/// </summary>
		/// <param name="row">row index</param>
		/// <returns>value in vector's row</returns>
		public double Element(uint row)
		{
			return base.Element(row, 0);
		}

		/// <summary>
		/// Insert new value into vector element
		/// </summary>
		/// <param name="row">row index</param>
		/// <param name="value">assined value</param>
		public void Element(uint row, double value)
		{
			base.Element(row, 0, value);
		}

		/// <summary>
		/// Square bracket operator for accessing vector element by index
		/// </summary>
		/// <param name="row">row index</param>
		/// <returns>value in vector at row index</returns>
		public new double this[uint row]
		{
			get
			{
				return Element(row);
			}
			set
			{
				Element(row, value);
			}
		}
		#endregion


		#region OPERATOR_OVERLOADS
		/// <summary>
		/// Multiplication operator overload for matrix and vector product
		/// </summary>
		/// <param name="mat">input matrix</param>
		/// <param name="vec">input vector</param>
		/// <returns>product of 'mat' and 'vec'</returns>
		public static Vector operator*(Matrix mat, Vector vec)
		{
			Matrix		vecAsMatrix		= new Matrix(vec);
			Matrix		productAsMatrix = mat*vecAsMatrix;
			return (Vector)productAsMatrix;
		}		  // TODO - TWO VECTOR PRODUCT AS MATRIX
		#endregion
	}
}
