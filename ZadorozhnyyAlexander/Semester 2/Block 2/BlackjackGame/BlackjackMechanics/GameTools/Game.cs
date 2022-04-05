using AbstractClasses;
using BlackjackMechanics.Cards;
using BlackjackMechanics.Players;

namespace BlackjackMechanics.GameTools
{
    public class Game
    {
        private int numberOfMoves = 0;
        private DeckOfCards Deck;
        private ABot Bot;
        private Dealer Dealer;

        public Game(ABot bot)
        {
            Bot = bot;
            Dealer = new Dealer();
        }

        private void GetAnotherCard(AParticipant player)
        {
            while (player.GetNextCard())
            {
                Dealer.GiveCard(Deck.GetOneCard(player), player);
            }
        }

        private void MakeTurn()
        {
            PlayerTurn playerTurn = GetNextPlayerTurn();

            switch (playerTurn)
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
            CheckGameStatus(playerTurn);
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
                numberOfMoves++;
                MakeTurn();
            }
        }

        private void ResetGame()
        {
            if (!(Bot.IsWantNextGame))
                return;
            ResetGameParams();
            StartGame();
        }

        public PlayerTurn GetAnswerAfterFirstBlackjack()
        {
            return Bot.IsStandAfterFirstBlackjack ? PlayerTurn.Stand : PlayerTurn.Take;
        }

        public PlayerTurn GetNextPlayerTurn()
        {
            if (!(numberOfMoves == 0 && Bot.GetSumOfCards() == 21))
                return Bot.GetNextTurn(Dealer.VisibleCard);

            if (Dealer.VisibleCard.CardName == CardNames.Ace)
            {
                PlayerTurn answer = GetAnswerAfterFirstBlackjack();
                if (answer == PlayerTurn.Take)
                    return PlayerTurn.Take;
                else
                {
                    Bot.MakeBlackjackMultiplayer();
                    return PlayerTurn.Stand;
                }
            }

            else
            {
                Bot.MakeBlackjackMultiplayer();
                return Dealer.VisibleCard.CardNumber == 10 ? PlayerTurn.Stand : PlayerTurn.Blackjack;
            }
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

        public void ResetGameParams()
        {
            Bot.CardsInHand.Clear();
            Dealer.ClearHands();
            numberOfMoves = 0;
            if (Deck.Deck.Count() < 2 * 52)
            {
                Deck.ResetDeckOfCards();
                Deck.ShuffleDeck();
            }
        }
    }
}