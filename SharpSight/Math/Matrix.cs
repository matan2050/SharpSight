using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpSight.Exceptions;

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
			dimensions = new uint[2];
			dimensions[0] = copiedMat.dimensions[0];
			dimensions[1] = copiedMat.dimensions[1];
			matrixData = new double[dimensions[0], dimensions[1]];

			for (uint i = 0; i < copiedMat.dimensions[0]; i++)
			{
				for (uint j = 0; j < copiedMat.dimensions[1]; j++)
				{
					Element(i, j, copiedMat.Element(i, j));
				}
			}
		}

		// matrix expansion ctor
		public Matrix(Matrix matToExpand, uint rowPos, uint colPos, uint nRows, uint nCols)
		{
			dimensions = new uint[2];
			dimensions[0] = nRows;
			dimensions[1] = nCols;
			matrixData = new double[dimensions[0], dimensions[1]];

			// dimensions check
			if ((nRows < rowPos + matToExpand.dimensions[0]) ||
				(nCols < colPos + matToExpand.dimensions[1]))
			{
				throw new IndexOutOfRangeException();
			}

			for (uint i = 0; i < dimensions[0]; i++)
			{
				for (uint j = 0; j < dimensions[1]; j++)
				{
					if ((i >= rowPos) && (i < rowPos + matToExpand.dimensions[0]) &&
						(j >= colPos) && (j < colPos + matToExpand.dimensions[1]))
					{
						Element(i, j, matToExpand.Element(i - rowPos, j - colPos));
					}
					else
					{
						Element(i, j, 0);
					}
				}
			}
		}


		// special case matrix
		public Matrix(uint nRows, uint nCols, string type)
		{
			dimensions = new uint[2];
			dimensions[0] = nRows;
			dimensions[1] = nCols;
			matrixData = new double[dimensions[0], dimensions[1]];

			switch (type)
			{
				case "Ones":
					Ones();
					break;
				case "Zeros":
					Zeros();
					break;
				case "Eye":
					Eye();
					break;
			}


		}
		#endregion


		#region ACCESS_METHODS
		public virtual void Element(uint row, uint col, double value)
		{
			if ((row < 0) || (row > dimensions[0]) || (col < 0) || (col > dimensions[1]))
			{
				throw new IndexOutOfRangeException();
			}

			matrixData[row, col] = value;
		}

		public virtual double Element(uint row, uint col)
		{
			return matrixData[row, col];
		}
		#endregion


		#region INITIALIZATION_METHODS
		/// <summary>
		/// Initializes all matrix elements to 1
		/// </summary>
		public void Ones()
		{
			for (uint i = 0; i < dimensions[0]; i++)
			{
				for (uint j = 0; j < dimensions[1]; j++)
				{
					Element(i, j, 1);
				}
			}
		}

		/// <summary>
		/// Initializes the matrix as an identity matrix if square dimensions
		/// </summary>
		public void Eye()
		{
			for (uint i = 0; i < dimensions[0]; i++)
			{
				for (uint j = 0; j < dimensions[1]; j++)
				{
					if (i == j)
					{
						Element(i, j, 1);
					}
				}
			}
		}

		/// <summary>
		/// Initializes all matrix elements to zeros
		/// </summary>
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


		#region ROW_OPERATIONS
		/// <summary>
		/// Perform elementary row replacement operation
		/// </summary>
		/// <param name="rowA">first row for replacement</param>
		/// <param name="rowB">second row for replacement</param>
		public void InterchangeRow(uint rowA, uint rowB)
		{
			// checking that row indices are within matrix borders
			if ((rowA > dimensions[0]) || (rowA < 0) ||
				(rowB > dimensions[0]) || (rowB < 0))
			{
				throw new IndexOutOfRangeException();
			}

			// checking if rowA and rowB are the same row
			if (rowA == rowB)
				return;

			double[] tempRow = new double[this.dimensions[1]];

			// save rowA in temp array, 
			// and simultaneusly replace element in rowA
			for (uint i = 0; i < dimensions[1]; i++)
			{
				tempRow[i] = this.Element(rowA, i);
				this.Element(rowA, i,
					this.Element(rowB, i));
			}

			// replace elements in rowB
			for (uint i = 0; i < dimensions[1]; i++)
			{
				this.Element(rowB, i,
					tempRow[i]);
			}
		}

		/// <summary>
		/// Replace row with row multiplied by 'scalar'
		/// </summary>
		/// <param name="row">row index</param>
		/// <param name="scalar">scalar to multiply row with</param>
		public void MultiplyRowByScalar(uint row, double scalar)
		{
			for (uint i = 0; i < dimensions[1]; i++)
			{
				this.Element(row, i,
					scalar * this.Element(row, i));
			}
		}

		/// <summary>
		/// Replace row with sum of rowA and scalar times rowB
		/// </summary>
		/// <param name="rowA">row which values are going to change</param>
		/// <param name="rowB">row that will be multiplied by scalar for summation with rowA</param>
		/// <param name="scalar">scalar number</param>
		public void AddMultipliedRow(uint rowA, uint rowB, double scalar)
		{
			// checking that row indices are within matrix borders
			if ((rowA > dimensions[0]) || (rowA < 0) ||
				(rowB > dimensions[0]) || (rowB < 0))
			{
				throw new IndexOutOfRangeException();
			}

			for (uint i = 0; i < this.dimensions[1]; i++)
			{
				Element(rowA, i,
					Element(rowA, i) +
					scalar * Element(rowB, i));
			}
		}

		/// <summary>
		/// Returns all indices of rows that have a non-zero
		/// element in the column 'col'
		/// </summary>
		/// <param name="col">column position to check if element is non-zero</param>
		/// <returns>list of rows that have non-zero element at 'col' column</returns>
		public List<uint> IndexByFirstNonzeroElement(uint col)
		{
			List<uint> returnedIndices = new List<uint>();

			for (uint i = 0; i < dimensions[0]; i++)
			{
				bool preceedingNonZeroFlag = false;
				for (uint j = col; col >= 0; col--)
				{
					if (Element(i, j) != 0)
						preceedingNonZeroFlag = true;
						break;
				}
				if ((Element(i, col) != 0) && (!preceedingNonZeroFlag))
				{
					returnedIndices.Add(i);
				}
			}
			return returnedIndices;
		}
		#endregion


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
					transposed.Element(j, i, this.Element(i, j));
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
			throw new NotImplementedException();		// TODO - IMPLEMENT
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
			Matrix	normalized	= new Matrix(dimensions[0], dimensions[1]);
			double	matrixNorm	= Norm();

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
				throw new Exception();		 // TODO - IMPLEMENT SPECIFIC EXCEPTION

			Vector diagonal = new Vector(dimensions[0]);

			for (uint i = 0; i < dimensions[0]; i++)
				diagonal.Element(i, Element(i, i));

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

		/// <summary>
		/// ToString override method
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
						returnedString += '\n';
					}
				}
			}

			returnedString += ']';

			return returnedString;
		}
		#endregion


		#region OPERATOR_OVERLOADS
		/// <summary>
		/// Overload for square bracket index accessing - single element
		/// </summary>
		/// <param name="i">zero-based row index</param>
		/// <param name="j">zero-based column index</param>
		/// <returns></returns>
		public double this[uint row, uint col]
		{
			get
			{
				if (!CheckMatrixAccessIndices(this, row, col))
					throw new IndexOutOfRangeException();

				return Element(row, col);
			}
			set
			{
				if (!CheckMatrixAccessIndices(this, row, col))
					throw new IndexOutOfRangeException();
				Element(row, col, value);
			}
		}

		/// <summary>
		/// Overload for square bracket index accessing - entire row
		/// </summary>
		/// <param name="row">zero-based row index</param>
		/// <returns>row as vector</returns>
		public Vector this[uint row]
		{
			get
			{
				if (!CheckMatrixAccessIndices(this, row))
					throw new IndexOutOfRangeException();

				Vector thisRow = new Vector(dimensions[1]);
				
				for (uint i = 0; i < dimensions[1]; i++)
				{
					thisRow.Element(i, 
						this.Element(row, i));
				}
				return thisRow;
			}
			set
			{
				if (!CheckMatrixAccessIndices(this, row))
					throw new IndexOutOfRangeException();

				for (uint i = 0; i < dimensions[1]; i++)
				{
					Element(row, i, value.Element(i));
				}
			}
		}

		public static Matrix operator +(Matrix A, Matrix B)
		{
			// dimensions check
			if (!CheckPairDimensions(A, B))
				throw new MatrixDimensionMismatchException();

			Matrix returnedMatrix = new Matrix(A.dimensions[0], A.dimensions[1]);
			for (uint i=0; i<A.dimensions[0]; i++)
			{
				for (uint j=0; j<A.dimensions[1]; j++)
				{
					returnedMatrix.Element(i, j, 
						A.Element(i, j) + B.Element(i, j)); 
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

		public static Matrix operator -(Matrix A, Matrix B)
		{
			// dimensions check
			if (!CheckPairDimensions(A, B))
				throw new MatrixDimensionMismatchException();

			for (uint i = 0; i<B.dimensions[0]; i++)
			{
				for (uint j = 0; j<B.dimensions[1]; j++)
				{
					B.Element(i, j, 
						-B.Element(i, j));
				}
			}
			return A + B;
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

		public static Matrix operator *(Matrix A, Matrix B)
		{
			if (A.dimensions[1] != B.dimensions[0])
				throw new MatrixDimensionMismatchException();

			Matrix product = new Matrix(A.dimensions[0], B.dimensions[1]);

			for (uint i = 0; i < product.dimensions[0]; i++)
			{
				for (uint j = 0; j < product.dimensions[1]; j++)
				{
					for (uint k = 0; k < A.dimensions[1]; k++)
					{
						product.Element(i, j,
							product.Element(i, j) + A.Element(i, k) * B.Element(k, j));
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

		/// <summary>
		/// Equality method oveloading
		/// </summary>
		/// <param name="A">first matrix</param>
		/// <param name="B">second matrix</param>
		/// <returns>true for equal, false for inequal</returns>
		public static bool Equals(Matrix A, Matrix B)
		{
			if ((A.dimensions[0] != B.dimensions[0]) && (A.dimensions[1] != B.dimensions[1]))
			{
				return false;
			}

			for (uint i = 0; i < A.dimensions[0]; i++)
			{
				for (uint j = 0; j < A.dimensions[1]; j++)
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

		public double[,] MatrixData
		{
			get
			{
				return matrixData;
			}
			set
			{
				matrixData = value;
			}
		}
		#endregion


		#region PRIVATE_METHODS
		/// <summary>
		/// method to check if two matrices have identical dimensions
		/// </summary>
		/// <param name="a">left hand side matrix in comparison</param>
		/// <param name="b">right hand side matrix in comparison</param>
		/// <returns>indication if identical</returns>
		private static bool CheckPairDimensions(Matrix A, Matrix B)
		{ 
			if ((A.dimensions[0] != B.dimensions[0]) 
				|| (A.dimensions[1] != B.dimensions[1]))
			{
				return false;
			}
			return true;
		}

		private static bool CheckMatrixAccessIndices(Matrix A, uint row, uint col)
		{
			// Making sure given row and col are valid
			if ((row >= A.dimensions[0]) || (row < 0) ||
				(col >= A.dimensions[1]) || (col < 0))
				return false;

			return true;
		}

		private static bool CheckMatrixAccessIndices(Matrix A, uint row)
		{
			// Making sure given row and col are valid
			if ((row >= A.dimensions[0]) || (row < 0))
				return false;

			return true;
		}
		#endregion
	}
}