using Cards;
using BotStructure;

namespace Casino
{
    public static class Casino
    {
        private static int currentPlayerBet = 22;

        public static void Game(IBot bot, int drawCount, Shoes shoes)
        {
            Croupier croupier = new Croupier();

            for (int i = 0; i < drawCount; i++)
            {
                if (bot.Balance < currentPlayerBet)
                    return;

                Draw(shoes, croupier, bot);
                DecideWinner(croupier, bot);
                DrawEnd(croupier, bot);

                if (shoes.AllCardsCount < 416 / 3)
                    shoes = new Shoes();
            }
        }

        private static void Draw(Shoes shoes, Croupier croupier, IBot bot)
        {
            croupier.OpenCard = shoes.GetCard();
            croupier.Hand.Add(croupier.OpenCard);
            croupier.Hand.Add(shoes.GetCard());

            bot.Play(croupier.OpenCard, shoes);
            croupier.Play(shoes);
        }

        private static void DecideWinner(Croupier croupier, IBot bot)
        {
            foreach (var hand in bot.Hands)
            {
                int handValue = hand.GetHandValue();
                
                if (handValue == 21 && hand.Cards.Count == 2 && (croupier.Hand.Count != 2 || croupier.HandValue != 21))
                {
                    bot.Balance += (int) (1.5 * hand.Bet);
                    continue;
                }

                if (croupier.HandValue == 21 && croupier.Hand.Count == 2 && (handValue != 21 || hand.Cards.Count != 2))
                {
                    bot.Balance -= hand.Bet;
                    continue;
                }

                if (croupier.HandValue > 21 && handValue <= 21)
                    bot.Balance += hand.Bet;

                if (handValue > croupier.HandValue && handValue <= 21)
                    bot.Balance += hand.Bet;
                else if (handValue < croupier.HandValue || handValue > 21)
                    bot.Balance -= hand.Bet;
            }
        }

        private static void DrawEnd(Croupier croupier, IBot bot)
        {
            foreach (var hand in bot.Hands)
                currentPlayerBet = currentPlayerBet < hand.Bet ? currentPlayerBet : hand.Bet;

            bot.Hands = new List<Hand>();
            bot.Hands.Add(new Hand(0));
            croupier.Hand = new List<Card>();
        }
    }
}