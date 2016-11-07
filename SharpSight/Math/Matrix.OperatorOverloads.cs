using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpSight.Exceptions;

namespace SharpSight.Math
{
    public partial class Matrix
    {

		/// <summary>
		/// Addition operator
		/// </summary>
		/// <param name="A">first matrix</param>
		/// <param name="B">second matrix</param>
		/// <returns>sum matrix of A and B</returns>
		public static Matrix operator +(Matrix A, Matrix B)
		{
			// dimensions check
			if (!CheckPairDimensions(A, B))
				throw new MatrixDimensionMismatchException();

			Matrix returnedMatrix = new Matrix(A.Dimensions[0], A.Dimensions[1]);
			for (uint i = 0; i < A.Dimensions[0]; i++)
			{
				for (uint j = 0; j < A.Dimensions[1]; j++)
				{
					returnedMatrix.Element(i, j,
						A.Element(i, j) + B.Element(i, j));
				}
			}
			return returnedMatrix;
		}

		/// <summary>
		/// Adding scalar to all matrix elements
		/// </summary>
		/// <param name="scalar">scalar number</param>
		/// <param name="mat">input matrix</param>
		/// <returns>matrix whos elements are sum of 'mat' matrix and the scalar</returns>
		public static Matrix operator +(double scalar, Matrix mat)
		{
			Matrix returnedMat = new Matrix(mat.Dimensions[0], mat.Dimensions[1]);
			for (uint i = 0; i < mat.Dimensions[0]; i++)
			{
				for (uint j = 0; j < mat.Dimensions[1]; j++)
				{
					returnedMat.Element(i, j,
						mat.Element(i, j) + scalar);
				}
			}
			return returnedMat;
		}

		/// <summary>
		/// Adding scalar to all matrix elements
		/// </summary>
		/// <param name="mat">input matrix</param>
		/// <param name="scalar">scalar number</param>
		/// <returns>matrix whos elements are sum of 'mat' elements and scalar</returns>
		public static Matrix operator +(Matrix mat, double scalar)
		{
			return scalar + mat;
		}

		/// <summary>
		/// Subtraction operator for two matrices
		/// </summary>
		/// <param name="A">first matrix</param>
		/// <param name="B">second matrix</param>
		/// <returns>matrix A subtracted by matrix B</returns>
		public static Matrix operator -(Matrix A, Matrix B)
		{
			// dimensions check
			if (!CheckPairDimensions(A, B))
				throw new MatrixDimensionMismatchException();

			for (uint i = 0; i < B.Dimensions[0]; i++)
			{
				for (uint j = 0; j < B.Dimensions[1]; j++)
				{
					B.Element(i, j,
						-B.Element(i, j));
				}
			}
			return A + B;
		}

		/// <summary>
		/// Subtract scalar from all matrix elements
		/// </summary>
		/// <param name="scalar">scalar number</param>
		/// <param name="mat">input matrix</param>
		/// <returns>matrix whos elements are 'mat' elements subtracted from scalar</returns>
		public static Matrix operator -(double scalar, Matrix mat)
		{
			Matrix returnedMat = new Matrix(mat.Dimensions[0], mat.Dimensions[1]);

			for (uint i = 0; i < mat.Dimensions[0]; i++)
			{
				for (uint j = 0; j < mat.Dimensions[1]; j++)
				{
					returnedMat.Element(i, j,
						scalar - mat.Element(i, j));
				}
			}

			return returnedMat;
		}

		/// <summary>
		/// Subtract scalar from all matrix elements
		/// </summary>
		/// <param name="mat">input matrix</param>
		/// <param name="scalar">scalar number</param>
		/// <returns>matrix whos elements are scalar subtracted from 'mat' elements</returns>
		public static Matrix operator -(Matrix mat, double scalar)
		{
			Matrix returnedMat = scalar - mat;

			returnedMat = (-1.0) * returnedMat;

			return returnedMat;
		}

		/// <summary>
		/// Multiplication operator for two matrices
		/// </summary>
		/// <param name="A">first matrix</param>
		/// <param name="B">second matrix</param>
		/// <returns>product of A and B</returns>
		public static Matrix operator *(Matrix A, Matrix B)
		{
			if (A.Dimensions[1] != B.Dimensions[0])
				throw new MatrixDimensionMismatchException();

			Matrix product = new Matrix(A.Dimensions[0], B.Dimensions[1]);

			for (uint i = 0; i < product.Dimensions[0]; i++)
			{
				for (uint j = 0; j < product.Dimensions[1]; j++)
				{
					for (uint k = 0; k < A.Dimensions[1]; k++)
					{
						product.Element(i, j,
							product.Element(i, j) + A.Element(i, k) * B.Element(k, j));
					}
				}
			}

			return product;
		}

		/// <summary>
		/// Multiplication operator for matrix and scalar
		/// </summary>
		/// <param name="scalar">scalar number</param>
		/// <param name="mat">input matrix</param>
		/// <returns>matrix whos elements are products of 'mat' elements and scalar</returns>
		public static Matrix operator *(double scalar, Matrix mat)
		{
			Matrix returnedMat = new Matrix(mat.Dimensions[0], mat.Dimensions[1]);

			for (uint i = 0; i < mat.Dimensions[0]; i++)
			{
				for (uint j = 0; j < mat.Dimensions[1]; j++)
				{
					returnedMat.Element(i, j,
						scalar * mat.Element(i, j));
				}
			}
			return returnedMat;
		}

		/// <summary>
		/// Multiplication operator for matrix and scalar
		/// </summary>
		/// <param name="mat">input matrix</param>
		/// <param name="scalar">scalar number</param>
		/// <returns>matrix whos elements are products of 'mat' elements and scalar</returns>
		public static Matrix operator *(Matrix mat, double scalar)
		{
			return scalar * mat;
		}

		/// <summary>
		/// Equality method oveloading
		/// </summary>
		/// <param name="A">first matrix</param>
		/// <param name="B">second matrix</param>
		/// <returns>true for equal, false for inequal</returns>
		public static bool Equals(Matrix A, Matrix B)
		{
			if ((A.Dimensions[0] != B.Dimensions[0]) && (A.Dimensions[1] != B.Dimensions[1]))
			{
				return false;
			}

			for (uint i = 0; i < A.Dimensions[0]; i++)
			{
				for (uint j = 0; j < A.Dimensions[1]; j++)
				{
					if (A.Element(i, j) != B.Element(i, j))
					{
						return false;
					}
				}
			}

			return true;
		}

		/// <summary>
		/// Equality operator oveloading
		/// </summary>
		/// <param name="A">first matrix</param>
		/// <param name="B">second matrix</param>
		/// <returns>true for equal, false for inequal</returns>
		public static bool operator ==(Matrix A, Matrix B)
		{
			if (A.Equals(B))
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Inequality operator oveloading
		/// </summary>
		/// <param name="A">first matrix</param>
		/// <param name="B">second matrix</param>
		/// <returns>true for inequal, false for equal</returns>
		public static bool operator !=(Matrix A, Matrix B)
		{
			if (A.Equals(B))
			{
				return false;
			}

			return true;
		}

	}
}
