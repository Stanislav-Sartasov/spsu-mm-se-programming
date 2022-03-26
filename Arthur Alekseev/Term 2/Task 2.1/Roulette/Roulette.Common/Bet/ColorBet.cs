using Roulette.Common.GamePlay;

namespace Roulette.Common.Bet
{
	public class ColorBet : Bet
	{
		private readonly Color _color;

		public ColorBet(int money, IPlayer player, Color color) : base(money, player, 2)
		{
			_color = color;
		}

		public override void Play(Field field)
		{
			if (_color == field.Color)
				GiveWin();
		}
	}
}