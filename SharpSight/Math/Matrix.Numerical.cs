using System;
using System.Collections.Generic;

using SharpSight.Exceptions;

namespace SharpSight.Math
{
	public partial class Matrix
	{
		/// <summary>
		/// Cholesky decomposition
		/// </summary>
		/// <param name="A">matrix to decompose</param>
		/// <returns>upper triangular matrix returned from decomposition</returns>
		public static Matrix Cholesky(Matrix A)
		{
			Matrix decomposed = new Matrix(A.Dimensions[0], A.Dimensions[1]);

			//	TODO: IMPLEMENT CHOLESKY DECOMPOSITION
			return decomposed;
		}

		/// <summary>
		/// Generate elementary matrix from matrix A using gaussian elimination
		/// </summary>
		/// <param name="A">matrix to apply row reduction</param>
		/// <returns>elementary matrix based on A</returns>
		public static Matrix Reduce(Matrix A)
		{
			Matrix elementary = new Matrix(A.Dimensions[0], A.Dimensions[1]);

			// initially finding the row with first most
			// non-zero element
			List<uint>  leadingNonZeroRows          = new List<uint>();
			uint        availableRowForIntechange   = 0;

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

		public static void LU(Matrix A, out Matrix L, out Matrix U)
		{
			L = new Matrix(1, 1);
			U = new Matrix(1, 1);
			//	TODO: IMPLEMENT LU DECOMPOSITION
		}

		public static void QR(Matrix A, out Matrix Q, out Matrix R)
		{
			Q = new Matrix(1, 1);
			R = new Matrix(1, 1);
			// TODO: IMPLEMENT QR DECOMPOSITION
		}

		public static void SVD(Matrix A, out Matrix U, out Matrix S, out Matrix V)
		{
			U = new Matrix(1, 1);
			S = new Matrix(1, 1);
			V = new Matrix(1, 1);
			// TODO: IMPLEMENT SVD DECOMPOSITION
		}



		/// <summary>
		/// Calculate the matrix B, where A*B=I
		/// </summary>
		/// <param name="toInvert">the matrix we wish to invert</param>
		/// <returns>inverted matrix</returns>
		public static Matrix GaussJordanInversion(Matrix toInvert)
		{
			Matrix inverted = new Matrix(toInvert.Dimensions[0], toInvert.Dimensions[1]);
			inverted.Eye();

			// initially finding the row with first most
			// non-zero element
			var         pivotList                    = new List<uint>();
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