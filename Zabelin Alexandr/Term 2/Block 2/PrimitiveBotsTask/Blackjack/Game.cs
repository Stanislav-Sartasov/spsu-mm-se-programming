using BlackjackBots;

namespace Blackjack
{
    public class Game
    {
        public void Play(APlayer player, float betValue, int numOfBets, uint numOfDecks = 8)
        {
            if (numOfBets <= 0)
            {
                Console.WriteLine("The number of bets must be positive integer. It will be set to 1");
            }

            ShufflingMachine shufMachine = new ShufflingMachine(numOfBets, numOfDecks * 13);
            Croupier croupier = new Croupier();

            for (int i = 0; i < numOfBets; i++)
            {
                shufMachine.Shuffle();
                player.ResetScore();

                croupier.GetInitCardWeight(GiveCardWeight(shufMachine));
                croupier.GetCardWeight(GiveCardWeight(shufMachine));

                if (IsBlackjack(croupier.Score))
                {
                    player.Lose(betValue);

                    continue;
                }

                player.GetCardWeight(GiveCardWeight(shufMachine));
                player.GetCardWeight(GiveCardWeight(shufMachine));

                if (IsBlackjack(player.Score))
                {
                    player.WinBlackjack(betValue);

                    continue;
                }

                while (player.DoesHit(croupier.VisibleCardWeight) && player.Score < 22)
                {
                    player.GetCardWeight(GiveCardWeight(shufMachine));
                }

                if (IsBust(player.Score))
                {
                    player.Lose(betValue);

                    continue;
                }

                while (croupier.Score < 17)
                {
                    croupier.GetCardWeight(GiveCardWeight(shufMachine));
                }

                if (IsBust(croupier.Score))
                {
                    player.WinCasual(betValue);

                    continue;
                }

                if (croupier.Score > player.Score)
                {
                    player.Lose(betValue);
                }
                else if (croupier.Score < player.Score)
                {
                    player.WinCasual(betValue);
                }
            }
        }

        private byte ConvertCardWeight(Card card)
        {
            byte weight = card.Weight;

            if (weight < 11)
            {
                return weight;
            }
            else if (weight < 14)
            {
                return 10;
            }
            else
            {
                return 11;
            }
        }

        private byte GiveCardWeight(ShufflingMachine sm)
        {
            return ConvertCardWeight(sm.TakeCard());
        }

        private bool IsBlackjack(byte balance) => balance == 21;

        private bool IsBust(byte balance) => balance > 21;
    }
}
