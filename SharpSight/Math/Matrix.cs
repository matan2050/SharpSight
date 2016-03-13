using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpSight.Math
{
	public class Matrix
	{
		#region FIELDS
		private     double[,]       matrixData;
		private     uint[]      dimensions;
		#endregion


		#region CONSTRUCTORS
		public Matrix(uint nRows, uint nCols)
		{
			dimensions = new uint[2];
			dimensions[0] = nRows;
			dimensions[1] = nCols;

			matrixData = new double[dimensions[0], dimensions[1]];
		}

		public Matrix(Matrix copiedMat)
		{
			dimensions[0] = copiedMat.dimensions[0];
			dimensions[1] = copiedMat.dimensions[1];

			for (uint i = 0; i < copiedMat.dimensions[0]; i++)
			{
				for (uint j = 0; j < copiedMat.dimensions[1]; j++)
				{
					Element(i, j, copiedMat.Element(i, j));
				}
			}
		}
		#endregion


		#region ACCESS_METHODS
		public void Element(uint row, uint col, double value)
		{
			if ((row < 0) || (row > dimensions[0]) || (col < 0) || (col > dimensions[1]))
			{
				throw new IndexOutOfRangeException();
			}

			matrixData[row, col] = value;
		}

		public double Element(uint row, uint col)
		{
			return matrixData[row, col];
		}
		#endregion


		#region METHODS
		public void Ones()
		{
			for (uint i = 0; i < dimensions[0]; i++)
			{
				for (uint j = 0; j< dimensions[1]; j++)
				{
					Element(i, j, 1);
				}
			}
		}

		public void Eye()
		{
			for (uint i = 0; i<dimensions[0]; i++)
			{
				for (uint j = 0; j<dimensions[1]; j++)
				{
					if (i==j)
					{
						Element(i, j, 1);
					}
				}
			}
		}

		public void Zeros()
		{
			for (uint i = 0; i < dimensions[0]; i++)
			{
				for (uint j = 0; j < dimensions[1]; j++)
				{
					Element(i, j, 0);
				}
			}
		}
		#endregion


		#region OPERATOR_OVERLOADS
		public static Matrix operator +(Matrix first, Matrix second)
		{
			// dimensions check
			if (!CheckPairDimensions(first, second))
				throw new Exception();	// TODO: replace with specific exceptions

			Matrix returnedMatrix = new Matrix(first.dimensions[0], first.dimensions[1]);
			for (uint i=0; i<first.dimensions[0]; i++)
			{
				for (uint j=0; j<first.dimensions[1]; j++)
				{
					returnedMatrix.Element(i, j, first.Element(i, j) + second.Element(i, j)); 
				}
			}
			return returnedMatrix;
		} 

		public static Matrix operator +(double scalar, Matrix mat)
		{
			Matrix returnedMat = new Matrix(mat.dimensions[0], mat.dimensions[1]);
			for (uint i = 0; i < mat.dimensions[0]; i++)
			{
				for (uint j = 0; j < mat.dimensions[1]; j++)
				{
					returnedMat.Element(i, j, mat.Element(i, j) + scalar);
				}
			}
			return returnedMat;
		}

		public static Matrix operator -(Matrix first, Matrix second)
		{
			// dimensions check
			if (!CheckPairDimensions(first, second))
				throw new Exception();  // TODO: replace with specific exceptions

			for (uint i = 0; i<second.dimensions[0]; i++)
			{
				for (uint j = 0; j<second.dimensions[1]; j++)
				{
					second.Element(i, j, -second.Element(i, j));
				}
			}
			return first + second;
		}

		public static Matrix operator -(double scalar, Matrix mat)
		{
			Matrix returnedMat = new Matrix(mat.dimensions[0], mat.dimensions[1]);

			for (uint i = 0; i < mat.dimensions[0]; i++)
			{
				for (uint j = 0; j < mat.dimensions[1]; j++)
				{
					returnedMat.Element(i, j, scalar - mat.Element(i, j));
				}
			}

			return returnedMat;
		}

		public static Matrix operator -(Matrix mat, double scalar)
		{
			Matrix returnedMat = scalar - mat;

			returnedMat = (-1.0)*returnedMat;

			return returnedMat;
		}

		public static Matrix operator *(Matrix first, Matrix second)
		{
			if (first.dimensions[1] != second.dimensions[0])
				throw new Exception();

			Matrix		returnedMatrix		= new Matrix(first.dimensions[0], second.dimensions[1]);
			double      currElements		= 0;

			for (uint i = 0; i < returnedMatrix.dimensions[0]; i++)
			{
				for (uint j = 0; j < returnedMatrix.dimensions[1]; j++)
				{
					for (uint k = 0; k < first.dimensions[1]; k++)
					{
						returnedMatrix.Element(i, j, returnedMatrix.Element(i, j) + first.Element(i, k) * second.Element(k, j));
					}
				}
			}

			return returnedMatrix;
		}

		public static Matrix operator *(double scalar, Matrix mat)
		{
			Matrix returnedMat = new Matrix(mat.dimensions[0], mat.dimensions[1]);

			for (uint i = 0; i < mat.dimensions[0]; i++)
			{
				for (uint j = 0; j < mat.dimensions[1]; j++)
				{
					returnedMat.Element(i, j, scalar * mat.Element(i, j));
				}
			}
			return returnedMat;
		}
		#endregion


		#region PROPERTIES

		#endregion


		#region PRIVATE_METHODS
		private static bool CheckPairDimensions(Matrix a, Matrix b)
		{ 
			if ((a.dimensions[0] != b.dimensions[0]) || (a.dimensions[1] != b.dimensions[1]))
			{
				return false;
			}
			return true;
		}
		#endregion

	}
}