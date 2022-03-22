using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filters
{
	public class AKernelFilter
	{
		public byte MatrixMultiplication(byte[] Massive, int[] Kernel, int Divider)
		{
			int Counter1, Result = 0;
			for(Counter1 = 0; Counter1 < Massive.Length; Counter1++)
			{
				Result += Massive[Counter1] * Kernel[Counter1] / Divider;
			}
			return (byte)Math.Min(255, Math.Abs(Result));
		}
		public void MatrixFormalization(Image Image, int Scale, int[] Kernel, int Divider)
		{
			byte[] Red = new byte[Scale * Scale], Green = new byte[Scale * Scale], Blue = new byte[Scale * Scale];
			Pixel[,] Buffer = new Pixel[Image.Height, Image.Width];
			int Counter1, Counter2, Counter3, Counter4, Counter5;
			int OffSet = Scale / 2, Row, Column;

			for(Counter1 = 0; Counter1 < Image.Height; Counter1++)
			{
				for (Counter2 = 0; Counter2 < Image.Width; Counter2++)
				{
					Counter5 = 0;
					for (Counter3 = -OffSet; Counter3 < OffSet + 1; Counter3++)
					{
						Row = Counter1 + Counter3;
						for (Counter4 = -OffSet; Counter4 < OffSet + 1; Counter4++)
						{
							Column = Counter2 + Counter4;

							if (Row < 0 || Row > Image.Height - 1)
							{
								Row -= Counter3;
							}
							if (Column < 0 || Column > Image.Width - 1)
							{
								Column -= Counter4;
							}
							Blue[Counter5] = Image.Pixels[Row, Column].B;
							Green[Counter5] = Image.Pixels[Row, Column].G;
							Red[Counter5] = Image.Pixels[Row, Column].R;
							Counter5++;
						}
					}
					Buffer[Counter1, Counter2] = new Pixel();
					Buffer[Counter1, Counter2].B = MatrixMultiplication(Blue, Kernel, Divider);
					Buffer[Counter1, Counter2].G = MatrixMultiplication(Green, Kernel, Divider);
					Buffer[Counter1, Counter2].R = MatrixMultiplication(Red, Kernel, Divider);
				}
			}
			Array.Copy(Buffer, Image.Pixels, Image.Height * Image.Width);
		}
	}
}
