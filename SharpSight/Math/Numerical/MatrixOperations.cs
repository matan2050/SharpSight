using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpSight.Math.Numerical
{
	public static class MatrixOperations
	{
		public static Matrix Cholesky(Matrix A)
		{
			Matrix decomposed = new Matrix(A.Dimensions[0], A.Dimensions[1]);

			return decomposed;
		}


		public static Matrix GaussJordan(Matrix A)
		{
			Matrix echelonForm = new Matrix(A.Dimensions[0], A.Dimensions[1]);

			// initially finding the row with first most
			// non-zero element
			List<uint> leadingNonZeroRows = new List<uint>();
			for (uint i = 0; i < A.Dimensions[1]; i++)
			{
				leadingNonZeroRows = A.IndexByFirstNonzeroElement(i);

				if (leadingNonZeroRows.Count != 0)
					break;
			}

			// interchanging found row with first row
			A.InterchangeRow(1, leadingNonZeroRows[0]);

			return echelonForm;
		}
	}
}
