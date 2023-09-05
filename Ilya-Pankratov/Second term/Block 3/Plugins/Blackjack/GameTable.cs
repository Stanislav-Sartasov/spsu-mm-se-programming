using GameTools;
using Player;

namespace Blackjack
{
    public class GameTable
    {
        // Settings of the table

        public int NumberOfDecks { get; private set; }
        public int SizeOfDeck { get; private set; }
        public int MinBet { get; private set; }

        // Statictic of the table

        private int casinoProfit = 0;
        private int gamePlayed = 0;

        // Important parts of tha table

        public GameDeck GameDeck { get; private set; }
        public Dealer Dealer { get; private set; }
        public IPlayer Player { get; private set; }

        public GameTable(IPlayer player, int minBet = 5)
        {
            NumberOfDecks = 8;
            SizeOfDeck = 52;
            MinBet = minBet;
            Dealer = new Dealer();
            GameDeck = new GameDeck(NumberOfDecks);
            GameDeck.Shuffle();
            Player = player;
        }

        public void Play()
        {
            while (Player.Flag == PlayerState.Play)
            {
                if (Dealer.CheckAmountOfCards(GameDeck, SizeOfDeck * NumberOfDecks))
                    ResetGameDeck();

                PlayRound();
            }
        }

        public void ResetGameDeck()
        {
            Dealer.ResetGameDeck(GameDeck);
        }

        private void PlayRound()
        {
            MakeBet();
            Start();
            PlayerAct();
            DealerAct();
            Finish();

            gamePlayed += 1;
        }

        private void MakeBet()
        {
            Player.MakeBet(MinBet);
        }

        private void Start() 
        {
            Dealer.DrawTwoCardsToAll(GameDeck, Player);
        }

        private void PlayerAct()
        {
            for(int i = 0; i < Player.Hands.Count; i++)
            {
                Hand playerHand = Player.Hands[i];

                while (IsPlayerHandPlaying(playerHand))
                {
                    HandState flag = Player.Play(playerHand, Dealer.Cards);

                    switch (flag)
                    {
                        case HandState.Stand:
                            GameActions.Stand(playerHand);
                            break;
                        case HandState.Hit:
                            GameActions.Hit(playerHand, Dealer, GameDeck);
                            break;
                        case HandState.Double:
                            GameActions.Double(playerHand, Dealer, GameDeck);
                            break;
                        case HandState.Split:
                            GameActions.Split(Player, playerHand);
                            break;
                        case HandState.Surrender:
                            GameActions.Surrender(playerHand);
                            break;
                    }

                    playerHand.RecountPoints();
                }
            }

            Player.Flag = PlayerState.Stop;
        }

        private void DealerAct()
        {
            Dealer.Play(GameDeck);
        }

        private void Finish()
        {
            foreach (Hand playerHand in Player.Hands)
            {
                Player.Play(playerHand, Dealer.Cards); // let players to make some actions in the and of the round
            }

            Player.Flag = PlayerState.Play;

            int playerPrize = 0;
            int casinoProfit = 0;

            for (int i = 0; i < Player.Hands.Count; i++)
            {
                Hand hand = Player.Hands[i];

                if (hand.Flag == HandState.Surrender)
                {
                    playerPrize += hand.Bet / 2;
                    casinoProfit += hand.Bet / 2;
                }
                else if (IsPlayerHandWin(hand))
                {
                    playerPrize += (int)(1.5 * hand.Bet);
                    casinoProfit -= (int)(0.5 * hand.Bet);
                    hand.Flag = HandState.Win;
                }
                else if (IsDealerWin(hand))
                {
                    casinoProfit += hand.Bet;
                    hand.Flag = HandState.Lose;
                }
                else // Draw
                {
                    hand.Flag = HandState.Draw;
                    playerPrize += hand.Bet;
                }

                Player.Cash += playerPrize;
                this.casinoProfit += casinoProfit;
            }

            Player.Flag = PlayerState.Play;
            Dealer.TakeCardsOnTheTable(Player);

            if (IsNotEnoughCashToPlay(Player.Cash) || Player.IsLeave())
            {
                Player.Flag = PlayerState.Stop;
            }
        }

        private bool IsPlayerHandWin(Hand playerHand)
        {
            return (playerHand.Points < 22 && playerHand.Points > Dealer.Points || playerHand.Points < 22 && Dealer.Points > 21);
        }

        private bool IsDealerWin(Hand playerHand)
        {
            return (playerHand.Points > 21 && Dealer.Points < 22 || playerHand.Points < Dealer.Points && Dealer.Points < 22);
        }

        private bool IsNotEnoughCashToPlay(int cash)
        {
            return cash < MinBet;
        }

        private bool IsPlayerHandPlaying(Hand playerHand)
        {
            return (playerHand.Flag != HandState.Stand && playerHand.Flag != HandState.Double && playerHand.Flag != HandState.Surrender);
        }
    }
}