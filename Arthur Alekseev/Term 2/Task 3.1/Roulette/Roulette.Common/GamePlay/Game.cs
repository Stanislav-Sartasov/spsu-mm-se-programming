using System.Collections.Generic;
using System.Linq;

namespace Roulette.Common.GamePlay
{
	public class Game
	{
		private readonly Roulette _roulette;
		public readonly List<IPlayer> Players;

		public Game()
		{
			Players = new List<IPlayer>();
			_roulette = new Roulette();
		}

		public void AddPlayer(IPlayer player)
		{
			Players.Add(player);
		}

		public void PlayGame(int turnAmount)
		{
			for (var i = 0; i < turnAmount; i++)
				PlayTurn();
		}

		private void PlayTurn()
		{
			var chosenField = _roulette.GetRandomField();
			foreach (var bet in Players.SelectMany(player => player.MakeBets())) bet.Play(chosenField);
		}
	}
}