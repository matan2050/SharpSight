using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpSight.Math
{
	/// <summary>
	/// Command pattern base class for inheritive use
	/// </summary>
	public class Command
	{
		/// <summary>
		/// Command forward execution method
		/// </summary>
		public virtual void Execute()
		{

		}

		/// <summary>
		/// Command backward execution method
		/// </summary>
		public virtual void ReverseExecution()
		{

		}
	}
}
