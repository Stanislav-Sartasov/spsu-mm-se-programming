using Task_1.Utils;

namespace Task_1.Core.Image
{
	public class Bitmap : ICloneable
	{
		private readonly byte[] data;

		private readonly int pixelsOffset;
		private readonly short bitsPerPixel;

		public int Width { get; }
		public int Height { get; }

		// Pixels data indexer 
		public Pixel this[int i, int j]
		{
			get => new Pixel(data[Offset(i, j) + 2], data[Offset(i, j) + 1], data[Offset(i, j)]);

			set => (data[Offset(i, j) + 2], data[Offset(i, j) + 1], data[Offset(i, j)]) = value;
		}

		private int Offset(int i, int j) =>
			pixelsOffset + (i * Width + j) * (bitsPerPixel / 8) + Width % 4 * i;

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
		}

		public void Save(string filename) =>
			File.WriteAllBytes(filename, data);

		public object Clone() => new Bitmap((byte[])data.Clone());
	}
}
