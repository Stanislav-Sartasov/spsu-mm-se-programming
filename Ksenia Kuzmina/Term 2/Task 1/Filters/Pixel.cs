using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filters
{
	public class Pixel
	{
		public int Red;
		public int Green;
		public int Blue;

		public Pixel(byte red, byte green, byte blue)
		{
			Red = red;
			Green = green;
			Blue = blue;
		}
	}
}
