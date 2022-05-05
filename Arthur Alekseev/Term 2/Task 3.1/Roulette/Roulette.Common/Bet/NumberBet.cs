using Roulette.Common.GamePlay;

namespace Roulette.Common.Bet
{
	public class NumberBet : Bet
	{
		private readonly int _number;

		public NumberBet(int money, IPlayer player, int number) : base(money, player, 36)
		{
			_number = number;
		}

		public override void Play(Field field)
		{
			if (_number == field.Number)
				GiveWin();
		}
	}
}