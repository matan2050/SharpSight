using System;
using System.Collections.Generic;


namespace SharpSight.Math
{
	public partial class Matrix
	{
		#region FIELDS
		private     double[,]       m_MatrixData;
		private     uint[]          m_Dimensions;
		#endregion


		#region CONSTRUCTORS

		// default ctor
		public Matrix(uint nRows, uint nCols)
		{
			m_Dimensions = new uint[2];

			m_Dimensions[0] = nRows;
			m_Dimensions[1] = nCols;

			m_MatrixData = new double[m_Dimensions[0], m_Dimensions[1]];
		}

		// copy ctor
		public Matrix(Matrix copiedMat)
		{
			m_Dimensions = new uint[2];
			m_Dimensions[0] = copiedMat.m_Dimensions[0];
			m_Dimensions[1] = copiedMat.m_Dimensions[1];
			m_MatrixData = new double[m_Dimensions[0], m_Dimensions[1]];

			for (uint i = 0; i < copiedMat.m_Dimensions[0]; i++)
			{
				for (uint j = 0; j < copiedMat.m_Dimensions[1]; j++)
				{
					Element(i, j, copiedMat.Element(i, j));
				}
			}
		}

		// matrix expansion ctor
		public Matrix(Matrix matToExpand, uint rowPos, uint colPos, uint nRows, uint nCols)
		{
			m_Dimensions = new uint[2];
			m_Dimensions[0] = nRows;
			m_Dimensions[1] = nCols;
			m_MatrixData = new double[m_Dimensions[0], m_Dimensions[1]];

			// dimensions check
			if ((nRows < rowPos + matToExpand.m_Dimensions[0]) ||
				(nCols < colPos + matToExpand.m_Dimensions[1]))
			{
				throw new IndexOutOfRangeException();
			}

			for (uint i = 0; i < m_Dimensions[0]; i++)
			{
				for (uint j = 0; j < m_Dimensions[1]; j++)
				{
					if ((i >= rowPos) && (i < rowPos + matToExpand.m_Dimensions[0]) &&
						(j >= colPos) && (j < colPos + matToExpand.m_Dimensions[1]))
					{
						Element(i, j, 
							matToExpand.Element(i - rowPos, j - colPos));
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
			m_Dimensions = new uint[2];
			m_Dimensions[0] = nRows;
			m_Dimensions[1] = nCols;
			m_MatrixData = new double[m_Dimensions[0], m_Dimensions[1]];

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
		/// <summary>
		/// Assigns value to a certain cell in matrix
		/// </summary>
		/// <param name="row">assigned cell row index</param>
		/// <param name="col">assigned cell col index</param>
		/// <param name="value">assigned cell value</param>
		public virtual void Element(uint row, uint col, double value)
		{
			if ((row < 0) || (row > m_Dimensions[0]) || (col < 0) || (col > m_Dimensions[1]))
			{
				throw new IndexOutOfRangeException();
			}

			m_MatrixData[row, col] = value;
		}

		/// <summary>
		/// Returns value currently in cell represented by [row,col]
		/// </summary>
		/// <param name="row">cell row index</param>
		/// <param name="col">cell col index</param>
		/// <returns>value in queryied cell</returns>
		public virtual double Element(uint row, uint col)
		{
			return m_MatrixData[row, col];
		}

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
				Element(row, col, 
					value);
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

				Vector thisRow = new Vector(m_Dimensions[1]);

				for (uint i = 0; i < m_Dimensions[1]; i++)
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

				for (uint i = 0; i < m_Dimensions[1]; i++)
				{
					Element(row, i, 
						value.Element(i));
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
			if ((rowA > m_Dimensions[0]) || (rowA < 0) ||
				(rowB > m_Dimensions[0]) || (rowB < 0))
			{
				throw new IndexOutOfRangeException();
			}

			// checking if rowA and rowB are the same row
			if (rowA == rowB)
				return;

			double[] tempRow = new double[this.m_Dimensions[1]];

			// save rowA in temp array, 
			// and simultaneusly replace element in rowA
			for (uint i = 0; i < m_Dimensions[1]; i++)
			{
				tempRow[i] = this.Element(rowA, i);
				this.Element(rowA, i,
					this.Element(rowB, i));
			}

			// replace elements in rowB
			for (uint i = 0; i < m_Dimensions[1]; i++)
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
			for (uint i = 0; i < m_Dimensions[1]; i++)
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
			if ((rowA > m_Dimensions[0]) || (rowA < 0) ||
				(rowB > m_Dimensions[0]) || (rowB < 0))
			{
				throw new IndexOutOfRangeException();
			}

			for (uint i = 0; i < this.m_Dimensions[1]; i++)
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

			for (uint i = 0; i < m_Dimensions[0]; i++)
			{
				bool preceedingNonZeroFlag = false;

				for (int j = (int)col - 1; j >= 0; j--)
				{
					// checking for non-zero elements in the preceeding columns
					if (Element(i, (uint)j) != 0)
					{
						preceedingNonZeroFlag = true;
						break;
					}
				}
				if ((Element(i, col) != 0) && (!preceedingNonZeroFlag))
				{
					returnedIndices.Add(i);
				}
			}
			return returnedIndices;
		}
		#endregion


		#region PROPERTIES
		public uint[] Dimensions
		{
			get
			{
				return this.m_Dimensions;
			}
			set
			{
				this.m_Dimensions = value;
			}
		}

		public double[,] MatrixData
		{
			get
			{
				return m_MatrixData;
			}
			set
			{
				m_MatrixData = value;
			}
		}
		#endregion


		#region VALIDATION_METHODS
		/// <summary>
		/// method to check if two matrices have identical dimensions
		/// </summary>
		/// <param name="a">left hand side matrix in comparison</param>
		/// <param name="b">right hand side matrix in comparison</param>
		/// <returns>indication if identical</returns>
		public static bool CheckPairDimensions(Matrix A, Matrix B)
		{
			if ((A.m_Dimensions[0] != B.m_Dimensions[0])
				|| (A.m_Dimensions[1] != B.m_Dimensions[1]))
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// Checks validity of [row,col] access indices compared to matrix size
		/// </summary>
		/// <param name="A">input matrix</param>
		/// <param name="row">row index</param>
		/// <param name="col">col index</param>
		/// <returns>flag indicating if access indices are valid or not</returns>
		public static bool CheckMatrixAccessIndices(Matrix A, uint row, uint col)
		{
			// Making sure given row and col are valid
			if ((row >= A.m_Dimensions[0]) || (row < 0) ||
				(col >= A.m_Dimensions[1]) || (col < 0))
				return false;

			return true;
		}

		/// <summary>
		/// Checks validity of [row] access index compared to matrix size
		/// </summary>
		/// <param name="A">input matrix</param>
		/// <param name="row">row index</param>
		/// <returns>flag indicating if 'row' is a valid index in 'A' matrix</returns>
		public static bool CheckMatrixAccessIndices(Matrix A, uint row)
		{
			// Making sure given row and col are valid
			if ((row >= A.m_Dimensions[0]) || (row < 0))
				return false;

			return true;
		}
		#endregion
	}
}