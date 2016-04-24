using System.Collections.Generic;

namespace SharpSight.Math.Numerical
{
	public static class MatrixOperations
	{
		/// <summary>
		/// Cholesky decomposition
		/// </summary>
		/// <param name="A">matrix to decompose</param>
		/// <returns>upper triangular matrix returned from decomposition</returns>
		public static Matrix Cholesky(Matrix A)
		{
			Matrix decomposed = new Matrix(A.Dimensions[0], A.Dimensions[1]);

			return decomposed;
		}


		/// <summary>
		/// Generate elementary matrix from matrix A
		/// </summary>
		/// <param name="A">matrix to apply row reduction</param>
		/// <returns>elementary matrix based on A</returns>
		public static Matrix GenerateElementaryMatrix(Matrix A)
		{
			Matrix elementary = new Matrix(A.Dimensions[0], A.Dimensions[1]);

			// initially finding the row with first most
			// non-zero element
			List<uint>	leadingNonZeroRows			= new List<uint>();
			uint		availableRowForIntechange	= 0;

			for (uint i = 0; i < A.Dimensions[1]; i++)
			{
				leadingNonZeroRows = A.IndexByFirstNonzeroElement(i);

				if (leadingNonZeroRows.Count != 0)
					continue;

				// interchanging found row with first row
				A.InterchangeRow(availableRowForIntechange, leadingNonZeroRows[0]);

				// scale row so first element is 1	TODO - CHECK FOR SOLUTION WHEN SCALING FACTOR CLOSE TO 0
				A.MultiplyRowByScalar(availableRowForIntechange, 
					A.Element(availableRowForIntechange, i));

				for (uint j = 0; j < leadingNonZeroRows.Count; j++)
				{
					A.AddMultipliedRow(leadingNonZeroRows[(int)j], availableRowForIntechange,
						-(A.Element(leadingNonZeroRows[(int)j], i)));
				}

				availableRowForIntechange++;
			}

			return elementary;
		}

		public static Matrix GaussJordanInversion(Matrix toInvert)
		{
			Matrix inverted = new Matrix(toInvert.Dimensions[0], toInvert.Dimensions[1]);
			inverted.Eye();

			// initially finding the row with first most
			// non-zero element
			var			pivotList   			     = new List<uint>();
			uint        availableRowForInterchange   = 0;

			// propogating by columns
			for (uint i = 0; i < toInvert.Dimensions[1]; i++)
			{
				pivotList = toInvert.IndexByFirstNonzeroElement(i);

				if (pivotList.Count == 0)
					continue;

				// interchanging found row with first row
				toInvert.InterchangeRow(availableRowForInterchange, pivotList[0]);
				inverted.InterchangeRow(availableRowForInterchange, pivotList[0]);

				// scale row so first element is 1	TODO - CHECK FOR SOLUTION WHEN SCALING FACTOR CLOSE TO 0
				double scaleFactor = 1 / toInvert.Element(availableRowForInterchange, i);
				inverted.MultiplyRowByScalar(availableRowForInterchange, scaleFactor);
				toInvert.MultiplyRowByScalar(availableRowForInterchange, scaleFactor);

				for (uint j = 0; j < pivotList.Count; j++)
				{
					toInvert.AddMultipliedRow(pivotList[(int)j], availableRowForInterchange,
						-(toInvert.Element(pivotList[(int)j], i)));

					inverted.AddMultipliedRow(pivotList[(int)j], availableRowForInterchange,
						-(toInvert.Element(pivotList[(int)j], i)));
				}

				availableRowForInterchange++;
			}
			return inverted;
		}
	}
}