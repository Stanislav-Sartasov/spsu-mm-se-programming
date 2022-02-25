using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1._1
{
	public class GaussFilter : KernelFilter
	{
		public GaussFilter()
		{
			kernel = new int[]
			{
				1, 2, 1,
				2, 4, 2,
				1, 2, 1
			};
			divisor = 16;
		}
	}
}
