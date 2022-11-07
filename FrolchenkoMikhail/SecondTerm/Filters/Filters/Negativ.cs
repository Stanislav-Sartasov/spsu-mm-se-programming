
namespace Filters
{
	class Negativ:Filter
	{
		internal override void Filtering(byte[] mas, uint width, uint height)
		{
			for (int i = 0; i < width * height; i++)
			{
				mas[i * 3] = (byte)(255 - mas[i * 3]);
				mas[i * 3 + 1] = (byte)(255 - mas[i * 3 + 1]);
				mas[i * 3 + 2] = (byte)(255 - mas[i * 3 + 2]);
			}
		}
	}
}
