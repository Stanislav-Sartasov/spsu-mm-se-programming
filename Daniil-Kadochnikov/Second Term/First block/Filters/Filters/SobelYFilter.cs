using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filters
{
	public class SobelYFilter: AKernelFilter
	{
		public new int[] Kernel = { -1, -2, -1, 0, 0, 0, 1, 2, 1 };
		public new int Divider = 1;
	}
}