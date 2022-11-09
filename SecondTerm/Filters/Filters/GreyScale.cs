namespace Filters
{
	class GreyScale : Filter
	{
		internal override void Filtering(byte[] mas, uint width, uint height)
		{
			for (int i = 0; i < width * height; i++)
			{
				byte grey = (byte)((mas[i * 3] + mas[i * 3 + 1] + mas[i * 3 + 2]) / 3);
				mas[i * 3] = grey;
				mas[i * 3 + 1] = grey;
				mas[i * 3 + 2] = grey;
			}
		}
	}
}
