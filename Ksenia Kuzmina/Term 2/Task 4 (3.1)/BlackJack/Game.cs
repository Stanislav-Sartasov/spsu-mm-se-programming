using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
	public class Game
	{
		public Deck Deck;
		public List<Player> Players;
		public Dealer Dealer;

		public Game()
		{
			Dealer = new Dealer();
			Deck = new Deck();
			Deck.Shuffle();

			Players = new List<Player>();
		}

		public void PlayGame(int money)
		{
			int count = 0;

			WriteStartSum(money);

			while (count != 40)
			{
				Deck = new Deck();
				Deck.Shuffle();

				for (int i = 0; i < Players.Count; i++)
				{
					if (Players[i].Money <= 0)
					{
						WriteOutOfGameMessage(Players[i]);
						Players.Remove(Players[i]);
						i--;
					}
				}

				if (Players.Count == 0)
				{
					WriteGameOverMessage();
					return;
				}

				foreach (Player player in Players)
				{
					player.MakeBet();
					WritePlayerBet(player);
				}

				Dealer.Begin(Deck);

				foreach (Player player in Players)
				{
					player.Hands[0].Hit(Deck.GetCard());
					player.Hands[0].Hit(Deck.GetCard());
					WritePlayerCards(player);
				}

				if (Dealer.Cards[0].Number == CardNumber.Ace)
				{
					WriteDealerAce();
					foreach (Player player in Players)
						player.GetInsurance(Dealer);

					if (Dealer.Score == 21)
					{
						foreach (Player player in Players)
							player.Finish(Dealer);
						count++;
						Dealer.Finish();
						continue;
					}
				}

				if (DealerGetBlackjack())
				{
					count++;
					continue;
				}

				foreach (Player player in Players)
				{
					player.PlayTurn(Deck);
					Console.WriteLine();
				}

				Dealer.Play(Deck);
				WriteDealerCards();

				foreach (Player player in Players)
				{
					player.Finish(Dealer);
				}

				Dealer.Cards.Clear();
				Dealer.Score = 0;

				count++;
			}
			WriteResults();

		}

		public bool DealerGetBlackjack()
		{
			if (Dealer.Score == 21 && Dealer.Cards[0].Number == CardNumber.Ten)
			{
				Console.WriteLine("Dealer has blackjack.");
				foreach (Player player in Players)
				{
					player.Finish(Dealer);
				}
				Dealer.Finish();
				return true;
			}
			return false;
		}

		public void WriteOutOfGameMessage(Player player)
		{
			Console.WriteLine(player.Name + " doesn't have enough money. They are out of game.");
		}

		public void WriteGameOverMessage()
		{
			Console.WriteLine("There are no solvent players. The game is over.");
		}

		public void WritePlayerBet(Player player)
		{
			Console.WriteLine(player.Name + " made a bet of " + player.Hands[0].Bet + "$");
		}

		public void WritePlayerCards(Player player)
		{
			Console.Write(player.Name + " received the following cards: ");
			for (int i = 0; i < 2; i++)
			{
				Console.Write(player.Hands[0].Cards[i].FindOutTheNameOfTheCard() + " ");
			}
			Console.WriteLine("\n" + player.Name + " has " + player.Hands[0].Score + " points\n");
		}

		public void WriteDealerAce()
		{
			Console.WriteLine("Dealer has an ace.");
		}

		public void WriteDealerCards()
		{
			Console.Write("Dealer has the following cards: ");
			foreach (Card card in Dealer.Cards)
			{
				Console.Write(card.FindOutTheNameOfTheCard() + " ");
			}
			Console.WriteLine("\nDealer has got " + Dealer.Score + " points.\n");
		}

		public void WriteResults()
		{
			foreach (Player player in Players)
			{
				Console.WriteLine(player.Name + " has " + player.Money + "$");
			}
			Console.WriteLine("All other players spent all their money and they are out of game.");
		}

		public void WriteStartSum(int sum)
		{
			Console.WriteLine("Each player has " + sum + "$\n");
		}
	}
}
