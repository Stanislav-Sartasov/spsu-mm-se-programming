using ToolKit;
using PlayerStructure;

namespace GameTable
{
    public class PlayingField
    {
        public int MinBet { get; private set; }
        public int NumberOfDecks { get; private set; }

        private Shoe shoe;
        private Dealer dealer;
        private IPlayer player;

        public PlayingField(IPlayer player, int minBet = 100, int numberOfDecks = 8)
        {
            MinBet = minBet;
            NumberOfDecks = numberOfDecks;
            this.player = player;
            dealer = new Dealer();
            shoe = new Shoe(numberOfDecks);
            shoe.Shuffle();
        }

        public void Play()
        {
            while (player.State == PlayerState.Playing)
            {
                PlayGameLoop();
            }
        }

        public void UpdateShoe()
        {
            shoe = new Shoe(NumberOfDecks);
        }

        private void PlayGameLoop()
        {
            MakeBets();

            if (player.State == PlayerState.Stop)
            {
                return;
            }

            StartGame();
            MakeMoves();
            ProcessResult();
        }

        private void MakeBets()
        {
            player.MakeBet(MinBet);
        }

        private void StartGame()
        {
            dealer.StartGame(shoe, player);
        }

        private void MakeMoves()
        {
            ProcessBlackJack();

            for (int i = 0; i < player.Hands.Count; i++)
            {
                Hand currentHand = player.Hands[i];
                while (IsHandPlaying(currentHand))
                {
                    GamingState chosenMove = player.TakeMove(currentHand, dealer.VisibleCard ?? throw new Exception("the dealer did not receive the card :("));
                    switch (chosenMove)
                    {
                        case GamingState.Stand:
                            PlayerActions.Stand(currentHand);
                            break;
                        case GamingState.Double:
                            PlayerActions.DoubleDown(player, currentHand, dealer, shoe);
                            break;
                        case GamingState.Hit:
                            PlayerActions.Hit(currentHand, dealer, shoe);
                            break;
                        case GamingState.Split:
                            PlayerActions.Split(player, currentHand, shoe, dealer);
                            break;
                        case GamingState.Surrender:
                            PlayerActions.Surrender(currentHand);
                            break;
                    }
                }
            }

            dealer.Play(shoe);
        }

        private void ProcessResult()
        {
            int returnSum = 0;

            player.ThinkOver(dealer.Hand); // give the player time to make a plan for the next game

            foreach (var hand in player.Hands)
            {
                hand.UpdateScore();
                if (hand.State == GamingState.Surrender)
                {
                    returnSum += hand.Bet / 2;
                }
                else if (hand.State == GamingState.Draw)
                {
                    returnSum += hand.Bet;
                }
                else if (hand.State == GamingState.WinWithRisk)
                {
                    returnSum += hand.Bet * 5 / 2;
                }
                else if (hand.State == GamingState.WinWithoutRisk)
                {
                    returnSum += hand.Bet * 2;
                }
                else if (IsPlayerWin(hand))
                {
                    returnSum += hand.Bet * 2;
                }
                else if (IsDealerWin(hand))
                {
                    returnSum += 0;
                }
                else
                {
                    returnSum += hand.Bet; // draw
                }
            }

            player.ChangeBalance(returnSum);
            dealer.ResetTable(player);

            if (dealer.UpdateShoe(ref shoe, NumberOfDecks))
            {
                player.StateOfShoe = ShoeState.Reset;
            }
        }

        private bool IsHandPlaying(Hand hand)
        {
            return hand.State == GamingState.Playing || hand.State == GamingState.Hit;
        }

        private bool IsPlayerBlackJack(Hand hand)
        {
            return hand.Cards.Count == 2 && hand.Points == 21;
        }

        private bool IsPlayerWin(Hand hand)
        {
            return hand.Points < 22 && (hand.Points > dealer.Points || dealer.Points > 21);
        }

        private bool IsDealerWin(Hand hand)
        {
            return hand.Points > 21 || hand.Points < dealer.Points && dealer.Points < 22;
        }

        private void ProcessBlackJack()
        {
            if (dealer.IsBlackJack() || IsPlayerBlackJack(player.Hands[0]))
            {
                if (IsPlayerBlackJack(player.Hands[0]))
                {
                    if (dealer.VisibleCard.GetPoints() >= 10)
                    {
                        if (!player.TakeRiskWithBlackJack() && dealer.VisibleCard.CardPoint == CardPoints.Ace)
                        {
                            player.Hands[0].State = GamingState.WinWithoutRisk;
                        }
                        else if (dealer.IsBlackJack())
                        {
                            player.Hands[0].State = GamingState.Draw;
                        }
                        else
                        {
                            player.Hands[0].State = GamingState.WinWithRisk;
                        }
                    }
                    else
                    {
                        player.Hands[0].State = GamingState.WinWithRisk;
                    }
                }
                else
                {
                    player.Hands[0].State = GamingState.Lose;
                }
            }
        }
    }
}