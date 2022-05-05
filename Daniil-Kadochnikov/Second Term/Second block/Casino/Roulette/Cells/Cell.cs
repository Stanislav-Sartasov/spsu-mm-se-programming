namespace Roulette.Cells
{
	public class Cell
	{
		public readonly int Number;
		public readonly ColourEnum Colour; // 0 - black; 1 - red; 2 - zero;
		public readonly ParityEnum Parity; // 0 - even; 1 - odd; 2 - zero;
		public readonly DozenEnum Dozen; // 1 - dozen 1; 2 - dozen 2; 3 - dozen 3; 0 - zero;

		public Cell(int number, ColourEnum colour, ParityEnum parity, DozenEnum dozen)
		{
			Number = number;
			Colour = colour;
			Parity = parity;
			Dozen = dozen;
		}
	}
}