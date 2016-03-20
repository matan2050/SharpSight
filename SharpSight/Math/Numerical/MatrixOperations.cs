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



			return echelonForm;
		}
	}
}
