using Roulette.Common.GamePlay;

namespace Roulette.Common.Bet
{
	public class DozenBet : Bet
	{
		private readonly int _twelve;

		public DozenBet(int money, IPlayer player, int twelve) : base(money, player, 3)
		{
			_twelve = twelve;
		}

		public override void Play(Field field)
		{
			if (field.Number != 0 && (field.Number - 1) / 12 == _twelve)
				GiveWin();
		}
	}
}