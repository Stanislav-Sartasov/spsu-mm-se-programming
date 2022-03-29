using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
	public abstract class Player
	{
		public int Money;
		public List<Hand> Hands;
		public string Name;

		public Player(int money)
		{
			Name = "";
			Money = money;
			Hands = new List<Hand>();
			Hands.Add(new Hand(1));
		}

		public void PlayTurn(Deck deck)
		{
			if (Hands[0].Score == 21)
			{
				WriteWaitingMessage();
			}
			else
			{
				string answer = "";

				if (Hands[0].Cards[0].Number == Hands[0].Cards[1].Number && (Hands[0].Cards[1].Number == CardNumber.Eight || Hands[0].Cards[1].Number == CardNumber.Seven))
				{
					answer = "Split";
				}
				else if (Hands[0].Score == 10 || Hands[0].Score == 11)
				{
					answer = "Double";
				}

				if (answer == "Double")
				{
					Money -= Hands[0].Bet;
					Hands[0].Double(deck);
					WriteDoubleMessage();
					return;
				}
				else if (answer == "Split")
				{
					WriteSplitMessage();
					Split(deck);
				}

				foreach (Hand hand in Hands)
				{
					while (hand.Score < 21)
					{
						if (hand.Score > 18)
						{
							break;
						}
						else
						{
							Card card = deck.GetCard();
							hand.Hit(card);
							WriteHitMessage(hand);
						}
					}
					WriteHandCards(hand);
				}
			}
		}

		public void WriteDoubleMessage()
		{
			Console.WriteLine(Name + " doubled");
			Console.WriteLine(Name + " got the following card: ");
			foreach (Card card in Hands[0].Cards)
				Console.Write(card.FindOutTheNameOfTheCard() + " ");
			Console.WriteLine("(" + Hands[0].Score + " points)");
		}

		public void WriteWaitingMessage()
		{
			Console.WriteLine(Name + " have blackjack. Waiting for others to play.");
		}

		public void WriteSplitMessage()
		{
			Console.WriteLine(Name + " splitted");
		}

		public void WriteHitMessage(Hand hand)
		{
			Console.WriteLine(Name + " did a hit on the " + hand.Number + " hand.");
		}

		public void WriteHandCards(Hand hand)
		{
			Console.Write(Name + "'s " + hand.Number + " hand has the following cards: ");
			foreach (Card card in hand.Cards)
			{
				Console.Write(card.FindOutTheNameOfTheCard() + " ");
			}
			Console.WriteLine("(" + hand.Score + " points)");
		}

		public abstract void GetInsurance(Dealer dealer);

		public abstract void MakeBet();

		public void Split(Deck deck)
		{
			Hands.Add(new Hand(2));
			Hands[1].Score = Hands[0].Score / 2;
			Hands[0].Score = Hands[0].Score / 2;
			Hands[1].Cards.Add(Hands[0].Cards[0]);
			Hands[0].Cards.RemoveAt(0);
			Money -= Hands[0].Bet;
			Hands[1].Bet = Hands[0].Bet;
			foreach (Hand hand in Hands)
				hand.Hit(deck.GetCard());
		}

		public void Finish(Dealer dealer)
		{
			foreach (Hand hand in Hands)
			{
				if (hand.Score == 21)
				{
					if (hand.Cards.Count == 2)
					{
						if (dealer.Score == 21)
						{
							if (dealer.Cards.Count != 2)
							{
								double prize = (double)hand.Bet * 1.5;
								Hands[0].Bet = 0;
								Money += (int)prize;
								WriteFinishMessage(hand, dealer);
							}
							else
							{
								Money += hand.Bet;
								hand.Bet = 0;
								WriteFinishMessage(hand, dealer);
							}
						}
						else
						{
							double prize = (double)hand.Bet * 1.5;
							Hands[0].Bet = 0;
							Money += (int)prize;
							WriteFinishMessage(hand, dealer);
						}
					}
					else
					{
						if (dealer.Score == 21)
						{
							Money += hand.Bet;
							hand.Bet = 0;
							WriteFinishMessage(hand, dealer);
						}
						else
						{
							double prize = (double)hand.Bet * 1.5;
							Hands[0].Bet = 0;
							Money += (int)prize;
							WriteFinishMessage(hand, dealer);
						}
					}
				}
				else if (hand.Score > 21)
				{
					hand.Bet = 0;
					WriteFinishMessage(hand, dealer);
				}
				else
				{
					if (dealer.Score > hand.Score && dealer.Score <= 21)
					{
						hand.Bet = 0;
						WriteFinishMessage(hand, dealer);
					}
					else if (dealer.Score > 21)
					{
						double prize = (double)hand.Bet * 1.5;
						Hands[0].Bet = 0;
						Money += (int)prize;
						WriteFinishMessage(hand, dealer);
					}
					else if (dealer.Score < hand.Score)
					{
						double prize = (double)hand.Bet * 1.5;
						Hands[0].Bet = 0;
						Money += (int)prize;
						WriteFinishMessage(hand, dealer);
					}
					else if (dealer.Score == hand.Score)
					{
						Money += hand.Bet;
						hand.Bet = 0;
						WriteFinishMessage(hand, dealer);
					}
				}

				hand.Score = 0;
				hand.Cards.Clear();
			}
			if (Hands.Count == 2)
			{
				Hands.RemoveAt(1);
			}
		}

		public void WriteFinishMessage(Hand hand, Dealer dealer)
		{
			if (hand.Score == 21)
			{
				if (hand.Cards.Count == 2)
				{
					if (dealer.Score == 21)
					{
						if (dealer.Cards.Count != 2)
						{
							Console.WriteLine(Name + "'s " + hand.Number + " hand got a blackjack. Dealer got 21 points without blackjack. " + Name + " won!");
						}
						else
						{
							Console.WriteLine(Name + "'s " + hand.Number + " hand got a blackjack, but got push.");
						}
					}
					else
					{
						Console.WriteLine(Name + "'s " + hand.Number + " hand got a blackjack and won!");
					}
				}
				else
				{
					if (dealer.Score == 21)
					{
						Console.WriteLine(Name + "'s " + hand.Number + " got push.");
					}
					else
					{
						Console.WriteLine(Name + "'s " + hand.Number + " hand won!");
					}
				}
			}
			else if (hand.Score > 21)
			{
				Console.WriteLine(Name + "'s " + hand.Number + " hand got too much.");
			}
			else
			{
				if (dealer.Score > hand.Score && dealer.Score <= 21)
				{
					Console.WriteLine(Name + "'s " + hand.Number + " hand got fewer points than the dealer");
				}
				else if (dealer.Score > 21)
				{
					Console.WriteLine(Name + "'s " + hand.Number + " hand won!");
				}
				else if (dealer.Score < hand.Score)
				{
					Console.WriteLine(Name + "'s " + hand.Number + " hand won!");
				}
				else if (dealer.Score == hand.Score)
				{
					Console.WriteLine(Name + "'s" + hand.Number + " hand got push.");
				}
			}

			Console.WriteLine("Now " + Name + " has " + Money + "$\n");
		}
	}
}
