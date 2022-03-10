using Task_1.Utils;

namespace Task_1.Core.Image
{
	public class Bitmap : ICloneable
	{
		private readonly byte[] data;

		private readonly byte[] header;
		private readonly byte[] ending;

		private readonly int pixelsOffset;
		private readonly short bitsPerPixel;

		public int Width { get; }
		public int Height { get; }

		public Pixel[,] Pixels;

		public Bitmap(string filename) : this(File.ReadAllBytes(filename)) { }

		// Private constructor for clone method
		private Bitmap(byte[] data)
		{
			this.data = data;
			Parser parser = new Parser(data);

			// Parsing bitmap info
			pixelsOffset = parser.ParseInt(10);
			Width = parser.ParseInt(18);
			Height = parser.ParseInt(22);
			bitsPerPixel = parser.ParseShort(28);

			// Parsing data before pixels bytes
			header = data.Take(pixelsOffset).ToArray();

			// Parsing data after pixels bytes
			int endingOffset = pixelsOffset + Height * Width * (bitsPerPixel / 8);
			ending = data
				.Skip(endingOffset)
				.Take(data.Length - endingOffset)
				.ToArray();

			// Parsing image pixels with packs of 3 or 4 bytes
			Pixels = new Pixel[Height, Width];

			for (int i = 0; i < Height; i++)
			{
				for (int j = 0; j < Width; j++)
				{
					// Calculating offset
					int pos = pixelsOffset + (i * Width + j) * (bitsPerPixel / 8);

					Pixels[i, j] = new Pixel(data[pos + 2], data[pos + 1], data[pos]);
				}
			}
		}

		public void Save(string filename)
		{
			// Creating output data array
			byte[] data = header
				.Concat(new byte[Width * Height * (bitsPerPixel / 8)])
				.Concat(ending)
				.ToArray();

			for (int i = 0; i < Height; i++)
			{
				for (int j = 0; j < Width; j++)
				{
					// Calculating offset again
					int pos = pixelsOffset + (i * Width + j) * (bitsPerPixel / 8);

					// Filling output pixels data
					(data[pos + 2], data[pos + 1], data[pos]) = Pixels[i, j];
				}
			}

			File.WriteAllBytes(filename, data);
		}

		public object Clone() => new Bitmap((byte[])data.Clone());
	}
}
