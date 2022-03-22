using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filters
{
	public class GreyFilter
	{
		public void ApplyingGreyFilter(Image Image)
		{
			int Counter1, Counter2, Sum;
			for (Counter1 = 0; Counter1 < Image.Height; Counter1++)
			{
				for (Counter2 = 0; Counter2 < Image.Width; Counter2++)
				{
					Sum = (Image.Pixels[Counter1, Counter2].B + Image.Pixels[Counter1, Counter2].G + Image.Pixels[Counter1, Counter2].R) / 3;
					Image.Pixels[Counter1, Counter2].B = (byte)Sum;
					Image.Pixels[Counter1, Counter2].G = (byte)Sum;
					Image.Pixels[Counter1, Counter2].R = (byte)Sum;
				}
			}
		}
	}
}
