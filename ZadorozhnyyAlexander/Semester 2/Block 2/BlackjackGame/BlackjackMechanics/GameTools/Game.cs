using AbstractClasses;
using BlackjackMechanics.Cards;
using BlackjackMechanics.Players;

namespace BlackjackMechanics.GameTools
{
    public class Game
    {
        private int NumberOfMoves = 0;
        public DeckOfCards Deck;
        public ABot Bot;
        public Dealer Dealer;

        public Game(ABot bot)
        {
            Bot = bot;
            Dealer = new Dealer();
        }

        private void GetAnotherCard(AParticipant player)
        {
            while (player.GetNextCard())
            {
                player.CardsInHand.Add(Deck.GetOneCard(player));
            }
        }

        private void MakeTurn()
        {
            PlayerTurn playerTurn = GetNextPlayerTurn();

            switch (playerTurn)
            {
                case PlayerTurn.Hit:
                    Bot.IsWantNextCard = true;
                    GetAnotherCard(Bot);
                    break;
                case PlayerTurn.Stand:
                    GetAnotherCard(Dealer);
                    break;
                case PlayerTurn.Double:
                    Bot.IsWantNextCard = true;
                    Bot.Rate *= 2;
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
                    Bot.CountGames++;

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
                    Bot.CountGames++; // Push

                ResetGame();
            }

            else if (playerTurn == PlayerTurn.Take)
            {
                Bot.Win();
                ResetGame();
            }

            else
            {
                NumberOfMoves++;
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
            if (!(NumberOfMoves == 0 && Bot.GetSumOfCards() == 21))
                return Bot.GetNextTurn(Dealer.VisibleCard);

            if (Dealer.VisibleCard.CardName == CardNames.Ace)
            {
                PlayerTurn answer = GetAnswerAfterFirstBlackjack();
                if (answer == PlayerTurn.Take)
                    return PlayerTurn.Take;
                else
                {
                    Bot.Multiplier = 1.5;
                    return PlayerTurn.Stand;
                }
            }

            else
            {
                Bot.Multiplier = 1.5;
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
            Dealer.CardsInHand.Clear();
            Dealer.VisibleCard = null;
            NumberOfMoves = 0;
            if (Deck.Deck.Count() < 2 * 52)
            {
                Deck.ResetDeckOfCards();
                Deck.ShuffleDeck();
            }
        }
    }
}