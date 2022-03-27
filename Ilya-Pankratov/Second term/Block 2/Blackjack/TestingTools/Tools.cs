using GameTools;

namespace Bots.UnitTests
{
    public static class Tools
    {
        public static List<Card> CreateDealerCardSet(CardRank cardRank)
        {
            var result = CreateCardSet(cardRank);
            result.Last().Flag = Visibility.Invisible;
            return result;
        }

        public static List<Card> CreateCardSet(CardRank firstRank, CardRank secondRank)
        {
            var result = new List<Card>
            {
                new Card(0, firstRank),
                new Card(0, secondRank)
            };

            return result;
        }

        public static List<Card> CreateCardSet(CardRank rank)
        {
            return CreateCardSet(rank, rank);
        }

        public static void RepeatPlay(ABaseBot player, List<Card> dealerCards, int count)
        {
            for (int i = 0; i < count; i++)
            {
                player.Play(player.Hands[0], dealerCards);
            }
        }
    }
}