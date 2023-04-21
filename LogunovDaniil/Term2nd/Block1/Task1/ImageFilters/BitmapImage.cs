namespace ImageFilters
{
	public class BitmapImage
	{
		public int Height { get; private set; }
		public int Width { get; private set; }
		public int ByteCount { get; private set; }
		public byte[,] Data { get; private set; } = new byte[0, 0];

		public BitmapImage() { }

		public BitmapImage(int height, int width, int byteCount)
		{
			Height = height;
			Width = width;
			ByteCount = byteCount;
			Data = new byte[height, width];
		}
	}
}
