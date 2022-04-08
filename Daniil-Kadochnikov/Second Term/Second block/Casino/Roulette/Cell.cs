namespace Roulette
{
	public class Cell
	{
		public readonly byte Colour; // 0 - black; 1 - red; 2 - zero;
		public readonly byte Parity; // 0 - even; 1 - odd; 2 - zero;
		public readonly byte Dozen; // 1 - dozen 1; 2 - dozen 2; 3 - dozen 3; 0 - zero;

		internal Cell(byte colour, byte parity, byte dozen)
		{
			Colour = colour;
			Parity = parity;
			Dozen = dozen;
		}
	}
}
