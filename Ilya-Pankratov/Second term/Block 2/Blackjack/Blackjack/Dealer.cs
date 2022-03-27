using GameTools;
using Player;

namespace Blackjack
{
    public class Dealer
    {
        public List<Card> Cards { get; private set; }
        public int Points { get; private set; }

        public Dealer()
        {
            Cards = new List<Card>();
            Points = 0;
        }

        public void Play(GameDeck gameDeck)
        {
            Cards[1].Flag = Visibility.Visible;
            Points += Cards[1].GetPoints(Points);

            while (Points < 17)
            {
                Cards.Add(DrawCard(gameDeck));
                Points += Cards.Last()
                    .GetPoints(Points);
            }
        }

        public Card DrawCard(GameDeck gameDeck)
        {
            return gameDeck.TakeCard();
        }

        public List<Card> DrawCards(GameDeck gameDeck, int numberOfCards)
        {
            return gameDeck.TakeCards(numberOfCards);
        }

        public void DrawCardToHand(GameDeck gameDeck, Hand playerHand)
        {
            Card card = DrawCard(gameDeck);
            playerHand.Cards.Add(card);
        }

        public void DrawTwoCardsToAll(GameDeck gameDeck, IPlayer player)
        {
            Card card;
            Hand playerHand = player.Hands[0];

            for (int i = 0; i < 2; i++)
            {
                card = DrawCard(gameDeck);
                Cards.Add(DrawCard(gameDeck));
                playerHand.Cards.Add(card);
            }

            Cards[1].Flag = Visibility.Invisible;
            Points += Cards[0].GetPoints(Points);
        }

        public void TakeCardsOnTheTable(IPlayer player)
        {
            player.Hands.Clear();
            player.Hands.Add(new Hand());

            Cards = new List<Card>();
            Points = 0;
        }

        public bool CheckAmountOfCards(GameDeck gameDeck, int totalCards)
        {
            int thirdPartOfDeck = totalCards / 3;
            return (gameDeck.Cards.Count < thirdPartOfDeck);
        }

        public void ResetGameDeck(GameDeck gameDeck)
        {
            gameDeck.Reset();
        }
    }
}