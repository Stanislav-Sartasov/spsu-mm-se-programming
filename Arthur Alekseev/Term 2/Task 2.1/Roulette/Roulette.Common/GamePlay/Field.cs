using System;

namespace Roulette.Common.GamePlay
{
	public class Field
	{
		public readonly Color Color;
		public readonly int Number;

		public Field(int number, Color color)
		{
			Number = number;
			Color = color;
		}

		public override string ToString()
		{
			return Convert.ToString(Number) + " " + NameColor(Color);
		}

		private string NameColor(Color color)
		{
			return color switch
			{
				Color.Black => "Black",
				Color.Red => "Red",
				_ => "Green"
			};
		}
	}
}