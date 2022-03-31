using Roulette.Common.GamePlay;

namespace Roulette.Common.Bet
{
	public class ParityBet : Bet
	{
		private readonly Parity _parity;

		public ParityBet(int money, IPlayer player, Parity parity) : base(money, player, 2)
		{
			_parity = parity;
		}

		public override void Play(Field field)
		{
			if (field.Number % 2 == (int) _parity)
				GiveWin();
		}
	}
}