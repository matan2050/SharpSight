using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpSight.Math
{
	public class Matrix<T> 
	{
		#region FIELDS
		private     T[,]       matrixData;
		private     uint[]      dimensions;
		#endregion


		#region CONSTRUCTORS
		public Matrix(uint nRows, uint nCols)
		{
			dimensions = new uint[2];
			dimensions[0] = nRows;
			dimensions[1] = nCols;

			matrixData = new T[dimensions[0], dimensions[1]];
		}
		#endregion


		#region METHODS
		public void Element(uint row, uint col, T value)
		{
			if ((row < 0) || (row > dimensions[0]) || (col < 0) || (col > dimensions[1]))
			{
				throw new IndexOutOfRangeException();
			}

			matrixData[row, col] = value;
		}

		public T Element(uint row, uint col)
		{
			return matrixData[row, col];
		}
		#endregion


		#region OPERATOR_OVERLOADS
		public static Matrix<T> operator +(Matrix<T> first, Matrix<T> second)
		{
			// dimensions check
			if ((first.dimensions[0] != second.dimensions[0]) || (first.dimensions[1] != second.dimensions[1]))
			{
				throw new Exception();	// TODO: replace with specific exceptions
			}

			Matrix<T> returned = new Matrix<T>(first.dimensions[0], first.dimensions[1]);
			for (uint i=0; i<first.dimensions[0]; i++)
			{
				for (uint j=0; j<first.dimensions[1]; j++)
				{
					//returned.Element(i, j, first.Element(i, j) + second.Element(i, j)); 
				}
			}
			return returned;
		} 
		#endregion


		#region PROPERTIES

		#endregion

	}
}
