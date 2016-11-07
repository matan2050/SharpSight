using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpSight.Math
{
    public partial class Matrix
    {
		/// <summary>
		/// Initializes all matrix elements to 1
		/// </summary>
		public void Ones()
		{
			for (uint i = 0; i < m_Dimensions[0]; i++)
			{
				for (uint j = 0; j < m_Dimensions[1]; j++)
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
			for (uint i = 0; i < m_Dimensions[0]; i++)
			{
				for (uint j = 0; j < m_Dimensions[1]; j++)
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
			for (uint i = 0; i < m_Dimensions[0]; i++)
			{
				for (uint j = 0; j < m_Dimensions[1]; j++)
				{
					Element(i, j, 0);
				}
			}
		}
	}
}
