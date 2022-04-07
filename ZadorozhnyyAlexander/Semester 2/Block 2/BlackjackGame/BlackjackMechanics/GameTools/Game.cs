using AbstractClasses;
using BlackjackMechanics.Cards;
using BlackjackMechanics.Players;

namespace BlackjackMechanics.GameTools
{
    public class Game
    {
        public ABot Bot { get; private set; }
        public DeckOfCards Deck { get; private set; }
        public Dealer Dealer { get; private set; }

        public Game(ABot bot)
        {
            Bot = bot;
            Dealer = new Dealer();
        }

        private void GetAnotherCard(AParticipant player)
        {
            while (player.IsNeedNextCard())
            {
                Dealer.GiveCard(Deck.GetOneCard(player), player);
            }
        }

        private void MakeTurn()
        {
            Bot.MakeNextPlayerTurn(Dealer.VisibleCard);

            switch (Bot.PlayerTurnNow)
            {
                case PlayerTurn.Hit:
                    GetAnotherCard(Bot);
                    break;
                case PlayerTurn.Stand:
                    GetAnotherCard(Dealer);
                    break;
                case PlayerTurn.Double:
                    Bot.DoubleRate();
                    GetAnotherCard(Bot);
                    break;
                default:
                    break;
            }
            CheckGameStatus(Bot.PlayerTurnNow);
        }

        private void CheckGameStatus(PlayerTurn playerTurn)
        {
            if (playerTurn == PlayerTurn.Blackjack)
            {
                Bot.Win();
                ResetGame();
            }

            else if ((playerTurn == PlayerTurn.Hit || playerTurn == PlayerTurn.Double) && Bot.GetSumOfCards() == 21)
            {
                GetAnotherCard(Dealer);
                if (Dealer.GetSumOfCards() != 21)
                    Bot.Win();
                else
                    Bot.Push();

                ResetGame();
            }

            else if ((playerTurn == PlayerTurn.Hit || playerTurn == PlayerTurn.Double) && Bot.GetSumOfCards() > 21)
            {
                Bot.Lose();
                ResetGame();
            }

            else if (playerTurn == PlayerTurn.Stand)
            {
                if (Dealer.GetSumOfCards() > 21 || Bot.GetSumOfCards() > Dealer.GetSumOfCards())
                    Bot.Win();
                else if (Bot.GetSumOfCards() < Dealer.GetSumOfCards())
                    Bot.Lose();
                else
                    Bot.Push();

                ResetGame();
            }

            else if (playerTurn == PlayerTurn.Take)
            {
                Bot.Win();
                ResetGame();
            }

            else
            {
                Bot.NextGameRound();
                MakeTurn();
            }
        }

        private void ResetGame()
        {
            if (!(Bot.IsWantNextGame))
                return;
            Bot.CardsInHand.Clear();
            Dealer.ClearHands();
            if (Deck.Deck.Count() < 2 * 52)
            {
                Deck.ResetDeckOfCards();
                Deck.ShuffleDeck();
            }
            StartGame();
        }     

        public void CreateGame(int countDecks)
        {
            Deck = new DeckOfCards(countDecks);
            Deck.ShuffleDeck();
        }

        public void StartGame()
        {
            Dealer.HandOutCards(Deck, Bot);
            if (Bot.IsWantNextGame)
                MakeTurn();
        }
    }
}