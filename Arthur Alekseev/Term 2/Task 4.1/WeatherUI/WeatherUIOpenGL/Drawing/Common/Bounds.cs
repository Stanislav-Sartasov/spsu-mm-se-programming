using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherUIOpenGL.Drawing.Common
{
	public class Bounds
	{
		private float[] coordinates;
		public Bounds(float x1, float y1, float x2, float y2)
		{
			coordinates = new float[] {x1, y1, x2, y2 };
		}
		public float this[int index]
		{
			get
			{
				return coordinates[index];
			}
			set
			{
				coordinates[index] = value;
			}
		}
	}
}
