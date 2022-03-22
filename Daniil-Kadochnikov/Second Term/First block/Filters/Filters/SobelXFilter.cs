using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filters
{
	public class SobelXFilter: AKernelFilter
	{
		public new int[] Kernel = { -1, 0, 1, -2, 0, 2, -1, 0, 1 };
		public new int Divider = 1;
	}
}
