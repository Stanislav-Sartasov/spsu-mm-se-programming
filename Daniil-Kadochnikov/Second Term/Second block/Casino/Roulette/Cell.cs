namespace Roulette
{
	public class Cell
	{
		public readonly int Colour; // 0 - black; 1 - red; 2 - zero;
		public readonly int Parity; // 0 - even; 1 - odd; 2 - zero;
		public readonly int Dozen; // 1 - dozen 1; 2 - dozen 2; 3 - dozen 3; 0 - zero;

		public Cell(int colour, int parity, int dozen)
		{
			Colour = colour;
			Parity = parity;
			Dozen = dozen;
		}
	}
}
