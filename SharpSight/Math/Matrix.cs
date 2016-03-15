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
		private     uint[]			dimensions;
		#endregion


		#region CONSTRUCTORS
		
		// default ctor
		public Matrix(uint nRows, uint nCols)
		{
			dimensions = new uint[2];

			dimensions[0] = nRows;
			dimensions[1] = nCols;

			matrixData = new double[dimensions[0], dimensions[1]];
		}

		// copy ctor
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

		public Matrix Transpose()
		{
			Matrix transposed = new Matrix(this.dimensions[1], this.dimensions[0]);

			for (uint i = 0; i < this.dimensions[0]; i++)
			{
				for (uint j = 0; j < this.dimensions[1]; j++)
				{
					transposed.Element(j, i, this.Element(i, j));
				}
			}

			return transposed;
		}

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
								returnedMat.Element(i, j, b.Element(i, j - this.dimensions[1]));
							}
							else
							{
								returnedMat.Element(i, j, this.Element(i, j));
							}
						}
					}
				}
			}
			return returnedMat;
		}

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
						returnedString += '\n';
					}
				}
			}

			returnedString += ']';

			return returnedString;
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
					returnedMatrix.Element(i, j, 
						first.Element(i, j) + second.Element(i, j)); 
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
					returnedMat.Element(i, j, 
						mat.Element(i, j) + scalar);
				}
			}
			return returnedMat;
		}

		public static Matrix operator +(Matrix mat, double scalar)
		{
			return scalar + mat;
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
					second.Element(i, j, 
						-second.Element(i, j));
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
					returnedMat.Element(i, j,
						scalar - mat.Element(i, j));
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

		public static Matrix operator *(Matrix a, Matrix b)
		{
			if (a.dimensions[1] != b.dimensions[0])
				throw new Exception();

			Matrix product = new Matrix(a.dimensions[0], b.dimensions[1]);

			for (uint i = 0; i < product.dimensions[0]; i++)
			{
				for (uint j = 0; j < product.dimensions[1]; j++)
				{
					for (uint k = 0; k < a.dimensions[1]; k++)
					{
						product.Element(i, j,
							product.Element(i, j) + a.Element(i, k) * b.Element(k, j));
					}
				}
			}

			return product;
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

		public static Matrix operator *(Matrix mat, double scalar)
		{
			return scalar * mat;
		}

		public static bool Equals(Matrix a, Matrix b)
		{
			if ((a.dimensions[0] != b.dimensions[0]) && (a.dimensions[1] != b.dimensions[1]))
			{
				return false;
			}

			for (uint i = 0; i < a.dimensions[0]; i++)
			{
				for (uint j = 0; j < a.dimensions[1]; j++)
				{
					if (a.Element(i, j) != b.Element(i, j))
					{
						return false;
					}
				}
			}

			return true;
		}
		#endregion


		#region PROPERTIES
		public uint[] Dimensions
		{
			get
			{
				return this.dimensions;
			}
			set
			{
				this.dimensions = value;
			}
		}
		#endregion


		#region PRIVATE_METHODS
		private static bool CheckPairDimensions(Matrix a, Matrix b)
		{ 
			if ((a.dimensions[0] != b.dimensions[0]) 
				|| (a.dimensions[1] != b.dimensions[1]))
			{
				return false;
			}
			return true;
		}
		#endregion
	}
}