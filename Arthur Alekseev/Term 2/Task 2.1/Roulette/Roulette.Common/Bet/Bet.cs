using Roulette.Common.GamePlay;

namespace Roulette.Common.Bet
{
	public abstract class Bet
	{
		private readonly int _coefficient;
		private readonly int _money;
		private readonly IPlayer _player;

		protected Bet(int money, IPlayer player, int coefficient)
		{
			_money = player.TakeMoney(money);
			_player = player;
			_coefficient = coefficient;
		}

		public abstract void Play(Field field);

		protected void GiveWin()
		{
			_player.GiveMoney(_coefficient * _money);
		}
	}
}