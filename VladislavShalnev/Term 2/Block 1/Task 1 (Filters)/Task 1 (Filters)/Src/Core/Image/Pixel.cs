
namespace Core.Image
{
	public class Pixel
	{
		public int Red { get; set; }
		public int Green { get; set; }
		public int Blue { get; set; }

		public Pixel(int red, int green, int blue)
		{
			Red = red;
			Green = green;
			Blue = blue;
		}

		public void Deconstruct(out byte red, out byte green, out byte blue)
		{
			red = (byte)Red;
			green = (byte)Green;
			blue = (byte)Blue;
		}

		public override bool Equals(object? obj)
		{
			if (obj is Pixel pixel)
				return (Red == pixel.Red && Green == pixel.Green && Blue == pixel.Blue);
			return false;
		}

		public static Pixel operator *(Pixel pixel, int multiplier) =>
			new Pixel(pixel.Red * multiplier, pixel.Green * multiplier, pixel.Blue * multiplier);

		public static Pixel operator /(Pixel pixel, int divider) =>
			new Pixel(pixel.Red / divider, pixel.Green / divider, pixel.Blue / divider);

		public static Pixel operator +(Pixel first, Pixel second) =>
			new Pixel(first.Red + second.Red, first.Green + second.Green, first.Blue + second.Blue);

	}
}
