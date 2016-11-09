using System;
using System.Collections.Generic;

using SharpSight.Exceptions;

namespace SharpSight.Math
{
	public partial class Matrix
	{
		#region METHODS
		/// <summary>
		/// Transposes the matrix
		/// </summary>
		/// <returns>transposed matrix</returns>
		public Matrix Transpose()
		{

			uint cols = Dimensions[0];
			uint rows = Dimensions[1];

			Matrix transposed = new Matrix(cols, rows);

			for (uint i = 0; i < rows; i++)
			{
				for (uint j = 0; j < cols; j++)
				{
					transposed.Element(j, i,
						this.Element(i, j));
				}
			}

			return transposed;
		}

		/// <summary>
		/// Determinant methods
		/// </summary>
		/// <returns>Matrix determinant</returns>
		public double Det()
		{
			throw new NotImplementedException();        // TODO - IMPLEMENT
		}

		/// <summary>
		/// Returns matrix magnitude
		/// </summary>
		/// <returns>matrix norm</returns>
		public double Norm()
		{
			uint rows = Dimensions[0];
			uint cols = Dimensions[1];

			double squareSum = 0;

			for (uint i = 0; i < rows; i++)
			{
				for (uint j = 0; j < cols; j++)
				{
					squareSum += System.Math.Pow(Element(i, j), 2);
				}
			}
			return System.Math.Sqrt(squareSum);
		}

		/// <summary>
		/// Returns a normalized matrix from current 
		/// (all elements divided by matrix magnitude)
		/// </summary>
		/// <returns>normalized matrix</returns>
		public Matrix Normalize()
		{

			uint rows = Dimensions[0];
			uint cols = Dimensions[1];

			Matrix  normalized  = new Matrix(rows, cols);
			double  matrixNorm  = Norm();

			for (uint i = 0; i < rows; i++)
			{
				for (uint j = 0; j < cols; j++)
				{
					normalized.Element(i, j,
						Element(i, j) / matrixNorm);
				}
			}

			return normalized;
		}

		/// <summary>
		/// Returns diagonal elements in the matrix
		/// </summary>
		/// <returns>vector that represents the matrix diagonal elements</returns>
		public Vector Diag()
		{

			uint rows = Dimensions[0];
			uint cols = Dimensions[1];

			if (rows != cols)
				throw new Exception();       // TODO - IMPLEMENT SPECIFIC EXCEPTION

			Vector diagonal = new Vector(rows);

			for (uint i = 0; i < rows; i++)
				diagonal.Element(i,
					Element(i, i));

			return diagonal;
		}

		/// <summary>
		/// Concatenates two matrices if dimensions agree
		/// </summary>
		/// <param name="b">the matrix to concatenate to current matrix</param>
		/// <param name="horizontal">flag to indicate horizontal concatenation</param>
		/// <returns>concatenated matrix</returns>
		public Matrix Concat(Matrix b, bool horizontal)
		{
			Matrix returnedMat= new Matrix(0,0);

			uint rows = Dimensions[0];
			uint cols = Dimensions[1];

			uint bRows = Dimensions[0];
			uint bCols = Dimensions[1];


			if (horizontal)
			{
				if (rows == bRows)
				{
					returnedMat = new Matrix(rows, cols + bCols);
					for (uint i = 0; i < rows; i++)
					{
						for (uint j = 0; j < cols + bCols; j++)
						{
							if (j >= cols)
							{
								returnedMat.Element(i, j,
									b.Element(i, j - m_Dimensions[1]));
							}
							else
							{
								returnedMat.Element(i, j,
									this.Element(i, j));
							}
						}
					}
				}
			}
			return returnedMat;
		}

		/// <summary>
		/// ToString override method, outputs matrix string as Matlab string
		/// </summary>
		/// <returns>string representing matrix</returns>
		public override string ToString()
		{
			uint rows = Dimensions[0];
			uint cols = Dimensions[1];

			string returnedString = "[";

			for (uint i = 0; i < rows; i++)
			{
				for (uint j = 0; j < cols; j++)
				{
					returnedString += Element(i, j).ToString();

					if (j != cols - 1)
					{
						returnedString += ", ";
					}
					if ((j == cols - 1) && (i != rows - 1))
					{
						returnedString += ";\n";
					}
				}
			}

			returnedString += ']';

			return returnedString;
		}
		#endregion
	}
}
