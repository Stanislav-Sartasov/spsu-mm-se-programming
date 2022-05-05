//using BlackjackBots;

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

                croupier.InitSetScore(shufMachine.GetBlackjackCardWeight());
                croupier.IncreaseScore(shufMachine.GetBlackjackCardWeight());

                if (croupier.IsBlackjack())
                {
                    player.Lose(betValue);

                    continue;
                }

                player.IncreaseScore(shufMachine.GetBlackjackCardWeight());
                player.IncreaseScore(shufMachine.GetBlackjackCardWeight());

                if (player.IsBlackjack())
                {
                    player.WinBlackjack(betValue);

                    continue;
                }

                while (player.DoesHit(croupier.VisibleCardWeight) && !player.IsBust())
                {
                    player.IncreaseScore(shufMachine.GetBlackjackCardWeight());
                }

                if (player.IsBust())
                {
                    player.Lose(betValue);

                    continue;
                }

                while (croupier.Score < 17)
                {
                    croupier.IncreaseScore(shufMachine.GetBlackjackCardWeight());
                }

                if (croupier.IsBust())
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
    }
}
