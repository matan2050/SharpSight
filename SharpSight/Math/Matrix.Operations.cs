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
			Matrix transposed = new Matrix(this.dimensions[1], this.dimensions[0]);

			for (uint i = 0; i < this.dimensions[0]; i++)
			{
				for (uint j = 0; j < this.dimensions[1]; j++)
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
			double squareSum = 0;

			for (uint i = 0; i < dimensions[0]; i++)
			{
				for (uint j = 0; j < dimensions[1]; j++)
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
			Matrix  normalized  = new Matrix(dimensions[0], dimensions[1]);
			double  matrixNorm  = Norm();

			for (uint i = 0; i < dimensions[0]; i++)
			{
				for (uint j = 0; j < dimensions[1]; j++)
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
			if (dimensions[0] != dimensions[1])
				throw new Exception();       // TODO - IMPLEMENT SPECIFIC EXCEPTION

			Vector diagonal = new Vector(dimensions[0]);

			for (uint i = 0; i < dimensions[0]; i++)
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
			Matrix returnedMat = new Matrix(0,0);

			if (horizontal)
			{
				if (this.dimensions[0] == b.dimensions[0])
				{
					returnedMat = new Matrix(this.dimensions[0], this.dimensions[1] + b.dimensions[1]);
					for (uint i = 0; i < this.dimensions[0]; i++)
					{
						for (uint j = 0; j < this.dimensions[1] + b.dimensions[1]; j++)
						{
							if (j >= this.dimensions[1])
							{
								returnedMat.Element(i, j,
									b.Element(i, j - this.dimensions[1]));
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
			string returnedString = "[";

			for (uint i = 0; i < dimensions[0]; i++)
			{
				for (uint j = 0; j < dimensions[1]; j++)
				{
					returnedString += Element(i, j).ToString();

					if (j != dimensions[1] - 1)
					{
						returnedString += ", ";
					}
					if ((j == dimensions[1] - 1) && (i != dimensions[0] - 1))
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
